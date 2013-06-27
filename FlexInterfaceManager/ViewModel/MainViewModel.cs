using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using FlexInterfaceManager.Model;
using FlexInterfaceManager.Manager;
using System.Windows;
using System.Threading;
using FlexInterface.Common;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace FlexInterfaceManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _LoginId;
        private string _Pwd;
        private CommandManager _CommandManager;
        private string _FromFundNoUserInput;
        private string _ToFundNoUserInput;
        public MainViewModel()
        {
            this._CommandManager = new CommandManager(this);
            this._CommandManager.Initialize();
            this.UserView = CollectionViewSource.GetDefaultView(UserManager.Default.UserCol);
            RegisterLoginEvent();
            this.MessengerInstance.Register<UpdateFundStatusMsg>(this, m =>
             {
                 Common.SynchronizationContext.Post(ar => this.FundStatusText = m.Msg, null);
             });
            
        }
        public ObservableCollection<TopAccountGroupModel> AccountGroupCol { get { return AccountManager.Default.AccountGroupCol; } }
        public ICollectionView FundSelectView { get { return FundManager.Default.FundSelectView; } }

        public ObservableCollection<FundData> FundCol { get { return FundManager.Default.DataCol; } }
        public ICollectionView FromSelectView_Fund { get { return FundManager.Default.FromView; } }

        public ICollectionView ToSelectView_Fund { get { return FundManager.Default.ToView; } }

        public ICollectionView BusinessTypeView { get { return QueryStringManager.Default.BusinessTypeView; } }

        public ICollectionView UserView { get; set; }
        public RelayCommand AddUserCmd { get; set; }
        public RelayCommand DelUserCmd { get; set; }
        public RelayCommand SaveUserCmd { get; set; }

        public RelayCommand ExportCmd { get; set; }

        public RelayCommand QueryFundCmd { get; set; }

        public DateTime FromTradeDay
        {
            get { return QueryStringManager.Default.FromTradeDay; }
            set
            {
                if (QueryStringManager.Default.FromTradeDay != value)
                {
                    QueryStringManager.Default.FromTradeDay = value;
                    RaisePropertyChanged(() => this.FromTradeDay);
                }
            }
        }
        public DateTime ToTradeDay
        {
            get { return QueryStringManager.Default.ToTradeDay; }
            set
            {
                if (QueryStringManager.Default.ToTradeDay != value)
                {
                    QueryStringManager.Default.ToTradeDay = value;
                    RaisePropertyChanged(() => this.ToTradeDay);
                }
            }
        }
        public string FromFundNoUserInput
        {
            get
            {
                return this._FromFundNoUserInput;
            }
            set
            {
                if (this._FromFundNoUserInput != value)
                {
                    this._FromFundNoUserInput = value;
                    RaisePropertyChanged(() => this.FromFundNoUserInput);
                }
            }
        }

        public string ToFundNoUserInput
        {
            get
            {
                return this._ToFundNoUserInput;
            }
            set
            {
                if (this._ToFundNoUserInput != value)
                {
                    this._ToFundNoUserInput = value;
                    RaisePropertyChanged(() => this.ToFundNoUserInput);
                }
            }
        }

       

        private string _FundStatusText = string.Empty;

        public string FundStatusText
        {
            get { return _FundStatusText; }
            set
            {
                if (_FundStatusText != value)
                {
                    _FundStatusText = value;
                    RaisePropertyChanged(() => this.FundStatusText);
                }
            }
        }

        private void RegisterLoginEvent()
        {
            this.MessengerInstance.Register<LoginMsg>(this, m =>
            {

                Action startWarnAction = () =>
                {

                };

                Action failedWarnAction = () =>
                {
                    Common.SynchronizationContext.Send(ar =>
                    {
                        this.MessengerInstance.Send(new LoginSuccessMsg(LoginProgressEnum.ProgressClose));
                        MessageBox.Show("The loginName or password is not correct");
                        this.MessengerInstance.Send(new LoginSuccessMsg(LoginProgressEnum.LoginOpen));
                    }, null);
                };

                Action exceptionWarnAction = () =>
                {
                    Common.SynchronizationContext.Post(ar =>
                    {
                        this.MessengerInstance.Send(new LoginSuccessMsg(LoginProgressEnum.ProgressClose));
                        MessageBox.Show("Network not connected!");
                        this.MessengerInstance.Send(new LoginSuccessMsg(LoginProgressEnum.LoginOpen));
                    }, null);
                };

                Action<bool> loginSuccessAction = isReconnect =>
                {
                    AccountManager.Default.AccountGroupCol.Clear();
                    FundManager.Default.DataCol.Clear();
                    InitData(isReconnect);
                };
                ConnectParamters parameters = new ConnectParamters();
                parameters.IsReconnect = false;
                parameters.LoginMsg = m;
                parameters.OriginLoginId = this._LoginId;
                parameters.OriginPwd = this._Pwd;
                parameters.SynchronizationContext = Common.SynchronizationContext;
                parameters.StartWarnAction = startWarnAction;
                parameters.FailedWarnAction = failedWarnAction;
                parameters.ExceptionWarnAction = exceptionWarnAction;
                parameters.LoginSuccessAction = loginSuccessAction;
                ConnectManager.Connect(parameters);
            });

            this.MessengerInstance.Register<LoginedMsg>(this, m =>
            {
                if (m.Msg.Item1)
                {
                    AliveKeeper.Default.ConnectionBroken += Reconnect;
                }
                Common.Service = m.Msg.Item2;
                Common.SessionId = m.Msg.Item3;
                this._LoginId = m.Msg.Item4;
                this._Pwd = m.Msg.Item5;
            });

        }

        private void Reconnect(object sender, EventArgs e)
        {
            ConnectParamters parameters = new ConnectParamters();
            parameters.IsReconnect = true;

            parameters.OriginLoginId = this._LoginId;
            parameters.OriginPwd = this._Pwd;
            ConnectManager.ConnectHelper(parameters);
        }


        private void InitData(bool isReconnect)
        {
            ThreadPool.QueueUserWorkItem(m =>
            {
                bool parameter = (bool)m;
                //InitWhenLoadFundNo(parameter);
                InitWhenLoadAccountData(parameter);
                InitWhenLoadUserData(parameter);
                AccountLoadService accountLoadService = new AccountLoadService(Common.Service, Common.SessionId, Common.SynchronizationContext, AccountManager.Default.GetTotalAccountGroupModel(), AccountManager.Default.AccountGroupCol);
                accountLoadService.Load();
            }, isReconnect);
        }

        private void InitWhenLoadFundNo(bool isReconnect)
        {
            Common.SynchronizationContext.Send(m =>
            {
                if (!isReconnect) this.MessengerInstance.Send(new ProgressMessage("Loading FundNo", 70));
            }, null);
            QueryObject parameter = new QueryObject();
            parameter.BeginDatetime = QueryStringManager.Default.FromTradeDay;
            parameter.EndDatetime = QueryStringManager.Default.ToTradeDay;
            parameter.AccountStr = string.Empty;
            var data = Common.Service.GetAllFundNo(Common.SessionId, parameter);
            Common.SynchronizationContext.Post(m =>
            {
                FundManager.Default.DataCol.Clear();
                FundManager.Default.PostGetFundNo(data);
            }, null);
        }

        private void InitWhenLoadAccountData(bool isReconnect)
        {
            Common.SynchronizationContext.Send(m =>
            {
                if (!isReconnect) this.MessengerInstance.Send(new ProgressMessage("Loading AccountData", 85));
            }, null);

            var customerData = Common.Service.GetAllCustomerCode(Common.SessionId);


            Common.SynchronizationContext.Post(ar =>
            {
                //AccountManager.Default.SelectByRangeCol_From.Clear();
                //AccountManager.Default.SelectByRangeCol_To.Clear();
                //foreach (var item in customerData)
                //{
                //    AccountManager.Default.SelectByRangeCol_From.Add(item);
                //    AccountManager.Default.SelectByRangeCol_To.Add(item);
                //}
            }, null);
        }

        private void InitWhenLoadUserData(bool isReconnect)
        {
            Common.SynchronizationContext.Send(m =>
            {
                if (!isReconnect) this.MessengerInstance.Send(new ProgressMessage("Loading UserData", 95));
            }, null);
            var userData = Common.Service.GetUsers(Common.SessionId);
            Common.SynchronizationContext.Post(m =>
            {
                foreach (var item in userData)
                {
                    UserManager.Default.AddUser(item);
                }
                if (!isReconnect)
                {
                    this.MessengerInstance.Send(new ProgressMessage("Initialize Completed", 100));
                    this.MessengerInstance.Send(new CloseProgressMsg());
                }
            }, null);
        }


    }
}
