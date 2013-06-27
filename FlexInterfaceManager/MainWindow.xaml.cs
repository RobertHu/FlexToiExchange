using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FlexInterfaceManager.Model;
using FlexInterface.Common;
using System.ServiceModel;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using System.Configuration;
using ValidationLibrary;
using GalaSoft.MvvmLight.Messaging;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls.Primitives;
using FlexInterfaceManager.Util;
using FlexInterfaceManager.ViewModel;
using FlexInterfaceManager.Manager;
using FlexInterfaceManager.Mediator;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace FlexInterfaceManager
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        #region fields
        private ILog _Log = LogManager.GetLogger(typeof(MainWindow));
        private MainWindowAndPopupMediator _Meditor;
        private ComboBoxMediator _ComboMediator;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this._Meditor = new MainWindowAndPopupMediator(this, this.PopupLogin, this.popupProgressMessage, this.tbLoginName.Name);
            ControlParameter parameter = new ControlParameter();
            parameter.DepositWithDrawTranferType=this.cbDepositType;
            parameter.DepositCodeSelectType = this.cbSelectFundType;
            parameter.DepositCodeSelectByItemPanel=this.gbFund;
            parameter.DepositCodeSelectByRangePanel = this.spFund;
            parameter.DepositCodeSelectByItem = this.gridFund;
            parameter.DepositCodeSelectByRange = this.spFundByRange;
            parameter.QueryFundBtn = this.QueryFundBtn;
            this._ComboMediator = new ComboBoxMediator(parameter);
            Common.SynchronizationContext = SynchronizationContext.Current;
            Common.MainWindow = this;
            this.DataContext = new MainViewModel();

            Messenger.Default.Register<LoginSuccessMsg>(this, m =>
            {
                if (Common.SynchronizationContext == null) return;
                Common.SynchronizationContext.Post(ar =>
                {
                    PopupLogin.IsOpen = (m.LoginProgress == LoginProgressEnum.ProgressCloseLoginOpen || m.LoginProgress == LoginProgressEnum.LoginOpen) ? true : false;
                    popupProgressMessage.IsOpen = (m.LoginProgress == LoginProgressEnum.LoginCloseProgressOpen || m.LoginProgress == LoginProgressEnum.ProgressOpen) ? true : false;
                }, null);

            });


            Messenger.Default.Register<LoadingMsg>(this, m =>
            {
                StartStopWait();
            });

            Messenger.Default.Register<ShowProgressMsg>(this, m =>
            {
                if (m.IsShow)
                {
                    popupProgressMessage.IsOpen = true;
                    gridMain.IsEnabled = false;
                }
                else
                {
                    popupProgressMessage.IsOpen = false;
                    gridMain.IsEnabled = true;
                    Messenger.Default.Send(new ProgressMessage(string.Empty, 0));
                }
            });

            Messenger.Default.Register<CloseProgressMsg>(this, m =>
            {
                Common.SynchronizationContext.Post(ar => popupProgressMessage.IsOpen = false, null);
            });

            Messenger.Default.Register<ApplicationCloseMsg>(this, m =>
            {
                this.Close();
            });

            Messenger.Default.Register<HideUserManageComponentMsg>(this, m =>
            {
                if (m.IsAdmin) gbUser.Visibility = Visibility.Visible;
                else gbUser.Visibility = Visibility.Collapsed;
            });


            Messenger.Default.Register<UpdateWarnInfoMsg>(this, m =>
            {
                statusTxt.Text = m.Info;
            });


            Messenger.Default.Register<BeginFetchUserInputFundNoRangeMsg>(this, m =>
            {
                string fromNo = this.cbFundFrom.Text;
                string toNo = this.cbFundTo.Text;
                UserInputFundNoRangeMsg msg = new UserInputFundNoRangeMsg(fromNo, toNo);
                Messenger.Default.Send(msg);
            });

            Messenger.Default.Register<UncheckedFundCheckAllButton>(this, m =>
            {
                this.cbFundAll.IsChecked = false;
            });


        }
        private void StartStopWait()
        {
            LoadingAdorner.IsAdornerVisible = !LoadingAdorner.IsAdornerVisible;
            gridMain.IsEnabled = !gridMain.IsEnabled;
        }

        #region UIElement Event

        private void cbFundAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in FundManager.Default.DataCol)
            {
                item.Update(true);
            }
            Messenger.Default.Send(new FundDataCheckedMsg());
        }

        private void cbFundAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in FundManager.Default.DataCol)
            {
                item.Update(false);
            }
            Messenger.Default.Send(new FundDataCheckedMsg());
        }


        #endregion
    }


}
