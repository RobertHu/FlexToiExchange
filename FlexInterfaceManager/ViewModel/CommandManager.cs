using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;
using FlexInterfaceManager.Manager;
using GalaSoft.MvvmLight.Messaging;
using FlexInterfaceManager.Model;
using System.Windows;
using log4net;
using GalaSoft.MvvmLight.Command;

namespace FlexInterfaceManager.ViewModel
{
    public class CommandManager
    {
        private ILog _Log = LogManager.GetLogger(typeof(CommandManager));
        private MainViewModel _MainViewModel;
        private ExportFileManager _ExportFileManager;
        public CommandManager(MainViewModel mainViewModel)
        {
            this._MainViewModel = mainViewModel;
            this._ExportFileManager = new ExportFileManager();
          
        }
        public void Initialize()
        {
            InitExportCmd();
            InitDelUserCmd();
            this._MainViewModel.AddUserCmd = new RelayCommand(() =>
            {
                AddUserView view = new AddUserView(Common.Service, Common.SessionId);
                view.Owner = Common.MainWindow;
                view.ShowDialog();
            });

            this._MainViewModel.SaveUserCmd = new RelayCommand(() =>
            {
                if (_MainViewModel.UserView.CurrentItem == null) return;
                UserModel model = _MainViewModel.UserView.CurrentItem as UserModel;
                UpdateUserView view = new UpdateUserView(model, Common.Service, Common.SessionId);
                view.Owner = Common.MainWindow;
                view.ShowDialog();
            });

            this._MainViewModel.QueryFundCmd = new RelayCommand(() =>
            {
                FundManager.Default.LoadFundCode();
            });


        }

        private void InitExportCmd()
        {
            this._MainViewModel.ExportCmd = new RelayCommand(() =>
            {
                if (FundManager.Default.IsByRange())
                {
                    Messenger.Default.Send(new BeginFetchUserInputFundNoRangeMsg());
                }
                QueryObject result;
                if (QueryStringManager.Default.GetAllParameters(out result) == false) return;
                try
                {
                    var parameter = this._ExportFileManager.GetExportFileParameters(result);
                   this._ExportFileManager.HandGenerateInterfaceFile(parameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data is not Complete!");
                    _Log.Error(ex.ToString());
                }
            });
        }

        private void InitDelUserCmd()
        {
            this._MainViewModel.DelUserCmd = new RelayCommand(() =>
            {
                if (_MainViewModel.UserView.CurrentItem == null) return;
                UserModel model = _MainViewModel.UserView.CurrentItem as UserModel;
                if (model.LoginID.ToLower() == "admin") return;
                TaskUtil.TaskHelper.Create<bool>(() =>
                {
                    Common.SynchronizationContext.Send(m =>
                    {
                        Messenger.Default.Send(new LoadingMsg());
                    }, null);
                    return Common.Service.DelUser(Common.SessionId, new User { Id = model.ID });
                }, ant =>
                {
                    Messenger.Default.Send(new LoadingMsg());
                    if (ant.Exception != null)
                    {
                        return;
                    }
                    if (ant.Result)
                    {
                        MessageBox.Show("Success!");
                    }
                    else
                    {
                        MessageBox.Show("Failed!");
                    }
                });
            });

        }


    }
}
