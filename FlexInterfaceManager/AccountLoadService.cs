using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;
using log4net;
using System.Threading;
using FlexInterfaceManager.Model;
using System.Collections.ObjectModel;
using FlexInterfaceManager.Util;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Threading.Tasks;
using FlexInterfaceManager.Manager;

namespace FlexInterfaceManager
{
    public class AccountLoadService
    {
        private IFlexService _Service;
        private string _SessionId;
        private ILog _Log = LogManager.GetLogger(typeof(AccountLoadService));
        private SynchronizationContext _SynchronizationContext;
        private TopAccountGroupModel _TotalAccountGroupModel;
        private ObservableCollection<TopAccountGroupModel> _AccountGroupCol;
        public AccountLoadService(IFlexService service, string sessionID, SynchronizationContext sc, TopAccountGroupModel tagm, ObservableCollection<TopAccountGroupModel> accountGroupCol)
        {
            this._Service = service;
            this._SessionId = sessionID;
            this._SynchronizationContext = sc;
            this._TotalAccountGroupModel = tagm;
            this._AccountGroupCol = accountGroupCol;
        }
        public void Load()
        {
            Func<IEnumerable<string>> workerAction = () =>
            {
                return _Service.GetAccountGroups(_SessionId);
            };

            Action<IEnumerable<string>> workerCompeleteAction = e =>
            {
                var groupData = e;
                _SynchronizationContext.Post(ar =>
                {
                    _TotalAccountGroupModel.IsExpand = true;
                    foreach (var code in groupData)
                    {
                        AccountGroupModel model = new AccountGroupModel(code, false);
                        _TotalAccountGroupModel.AccountGroups.Add(model);
                        RegisterExpandEventForAccoutGroup(model);
                    }
                    this._AccountGroupCol.Add(_TotalAccountGroupModel);

                }, null);
            };

            BackgroundWorkerUtil.Start<IEnumerable<string>>(workerAction, workerCompeleteAction);

        }

        private void RegisterExpandEventForAccoutGroup(AccountGroupModel model)
        {
            model.PropertyChanged += (e, s) =>
            {
                if (s.PropertyName == "IsExpand")
                {
                    PropertyChangedEventHandlerDetail(model);
                }
            };
        }

        private void PropertyChangedEventHandlerDetail(AccountGroupModel model)
        {
            if (model.Accounts.Count != 1 || model.Accounts[0].AccountCode != string.Empty) return;
           
            Func<IEnumerable<AccountData>> taskBody = () =>
           {
               Common.SynchronizationContext.Send(m =>
               {
                   Messenger.Default.Send(new LoadingMsg());
               }, null);
              var result = this._Service.GetAccountDataByGroup(this._SessionId, model.Code);
              return result;
           };

            Action<Task<IEnumerable<AccountData>>> taskContinue = ant =>
            {
                Messenger.Default.Send(new LoadingMsg());
                if (ant.Exception != null)
                {
                    return;
                }
                model.Accounts.Clear();
                if (ant.Result == null) return;
                foreach (var item in ant.Result)
                {
                    var account=new AccountModel(item.MT4LoginID.ToString(), item.Code, false);
                    model.Accounts.Add(account);
                    account.Update(model.IsChecked);
                }
            };

            TaskUtil.TaskHelper.Create<IEnumerable<AccountData>>(taskBody, taskContinue);
            
        }
    }
}
