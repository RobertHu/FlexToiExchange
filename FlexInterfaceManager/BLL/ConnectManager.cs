using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using FlexInterfaceManager.Model;
using System.Configuration;
using System.ServiceModel;
using FlexInterface.Common;
using log4net;
using System.ComponentModel;
using System.Threading;
using System.ServiceModel.Description;

namespace FlexInterfaceManager
{
    public static class ConnectManager
    {
        private static ILog _Log = LogManager.GetLogger(typeof(ConnectManager));
        public static  void InitializeConnection(ConnectParamters parameters,DoWorkEventArgs s)
        {
            try
            {
                if (!parameters.IsReconnect)
                {
                    parameters.StartWarnAction();
                }
                if (!parameters.IsReconnect) Messenger.Default.Send(new LoginSuccessMsg(LoginProgressEnum.LoginCloseProgressOpen));
                if (!parameters.IsReconnect) Messenger.Default.Send(new ProgressMessage("Initialize server", 0));
                FlexInterfaceService.StartupService service = new FlexInterfaceService.StartupService();
                service.Url = ConfigurationManager.AppSettings["WebServiceUrl"];
                if (!parameters.IsReconnect) Messenger.Default.Send(new ProgressMessage("Startup server", 5));
                service.HelloWorld();
                if (!parameters.IsReconnect) Messenger.Default.Send(new ProgressMessage("Create Connection", 10));
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                EndpointAddress endpoint = new EndpointAddress(ConfigurationManager.AppSettings["SVCURL"]);
                DuplexChannelFactory<IFlexService> factory = new DuplexChannelFactory<IFlexService>(new InstanceContext(new WindowClient()), binding, endpoint);
                foreach (var operationDescription in factory.Endpoint.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dcsob =
                        operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dcsob != null)
                    {
                        dcsob.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }
                var flexService = factory.CreateChannel();
                (flexService as IContextChannel).OperationTimeout = TimeSpan.FromMinutes(30);
                string loginId = string.Empty;
                string pwd = string.Empty;
                if (parameters.IsReconnect)
                {
                    loginId = parameters.OriginLoginId;
                    pwd = parameters.OriginPwd;
                }
                else
                {
                    loginId = parameters.LoginMsg.LoginId;
                    pwd = parameters.LoginMsg.Pwd;
                }
                if (!parameters.IsReconnect) Messenger.Default.Send(new ProgressMessage("Connection established", 15));
                LoginResult loginResult = flexService.ConnectServer(loginId, pwd);
                if (!parameters.IsReconnect) Messenger.Default.Send(new ProgressMessage("Connected to the server", 60));
                if (loginResult.IsSuccess)
                {
                    s.Result = Tuple.Create(flexService, loginResult.SessionId, loginId, pwd);
                    _Log.Warn("InitializeConnection Success!");
                }
                else
                {
                    _Log.Warn("Login Failed");
                    if (parameters.IsReconnect) return;
                    parameters.FailedWarnAction();
                }

            }
            catch (Exception ex)
            {
                if (!parameters.IsReconnect)
                {
                    parameters.FailedWarnAction();
                }
                _Log.ErrorFormat("InitializeConnection Failed   {0}", ex.ToString());
            }
        }


        public static void Connect(ConnectParamters parameters)
        {
            ConnectHelper(parameters);
        }


        public static void ConnectHelper(ConnectParamters parameters)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, s) =>
            {
                InitializeConnection(parameters,s);
            };

            worker.RunWorkerCompleted += (o, s) =>
            {
                if (s.Result == null) return;
                Tuple<IFlexService, string, string, string> loginInfo = (Tuple<IFlexService, string, string, string>)s.Result;
                if (parameters.IsReconnect)
                {
                    parameters.SynchronizationContext.Post(ar =>
                    {
                        parameters.LoginSuccessAction(parameters.IsReconnect);
                    }, null);
                }
                else
                {
                    parameters.LoginSuccessAction(parameters.IsReconnect);
                }
                AliveKeeper.Default.Attach(loginInfo.Item1, loginInfo.Item2);
                Messenger.Default.Send(new LoginedMsg(Tuple.Create(parameters.IsReconnect, loginInfo.Item1, loginInfo.Item2, loginInfo.Item3, loginInfo.Item4)));
            };
            worker.RunWorkerAsync();
        }
    }

    public class ConnectParamters
    {
        public bool IsReconnect { get; set; }
        public LoginMsg LoginMsg { get; set; }
        public string OriginLoginId { get; set; }
        public string OriginPwd { get; set; }
        public SynchronizationContext SynchronizationContext { get; set; }
        public Action<bool> LoginSuccessAction { get; set; }
        public Action StartWarnAction { get; set; }
        public Action FailedWarnAction { get; set; }
        public Action ExceptionWarnAction { get; set; }
    }
}
