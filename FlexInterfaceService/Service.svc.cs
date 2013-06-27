using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FlexInterface.Common;
using iExchange.Common;
using System.Diagnostics;
using log4net;
using FlexInterface.Repository;

namespace FlexInterfaceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ValidateMustUnderstand = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IFlexService
    {
        private ILog _Log = LogManager.GetLogger(typeof(Service));
        public void DoWork()
        {
        }

        string[] IDepositService.GetAllFundNo(string sessionID, QueryObject parameter)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return AccountService.GetAllFundNo(parameter);
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetAllFundNo  {0}", e);
                return null;
            }
        }


        IEnumerable<string> IDepositService.GetAllCustomerCode(string sessionID)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return DataManager.Default.GetAllAccountCode();
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetAllCustomerCode  {0}", e);
                return null;
            }

        }

        string IFlexService.HelloWorld()
        {
            return "Hello World";
        }

        void IPLService.GetResult()
        {
            throw new NotImplementedException();
        }


        string IFlexService.GetConfigStr()
        {
            return ConfigHelper.ConnectionString;
        }


        QueryPageCountResult IDepositService.GetDepositPageCount(string sessionID, QueryObject data)
        {
            if (!ClientManager.Default.Contains(sessionID)) return new QueryPageCountResult { Type = ReturnType.Error };
            try
            {
                return DataManager.Default.GetDepositPageCount(sessionID,data);
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetDepositData  {0}", e);
                return new QueryPageCountResult { Type = ReturnType.Error };
            }
        }


        int? IFlexService.GetExchangePlAccount(string sessionID)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return DataManager.Default.GetExchangePlAccount();
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetExchangePlAccount  {0}", e);
                return null;
            }
        }



        QueryPageCountResult IPLService.GetPLDataPageCount(string sessionID, QueryObject parameter)
        {
            if (!ClientManager.Default.Contains(sessionID)) return new QueryPageCountResult { Type = ReturnType.Error };
            try
            {
                return DataManager.Default.GetPLDataPageCount(sessionID,parameter);
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetPLData  {0}", e);
                return new QueryPageCountResult { Type = ReturnType.Error };
            }
        }


      



        LoginResult IFlexService.ConnectServer(string loginId, string pwd)
        {
            LoginResult result = new LoginResult();
            result.IsSuccess = UserManager.Default.CanLogin(loginId, pwd);
            if (result.IsSuccess == false) return result;
            result.SessionId = Guid.NewGuid().ToString();
            ClientManager.Default.Add(result.SessionId, OperationContext.Current.GetCallbackChannel<IFlexServiceCallback>());
            _Log.Warn("ConnectServer");
            return result;
        }


        int IFlexService.GetClientSessionCount()
        {
            return 0;
        }

        bool IFlexService.ContainsSession(string sessionID)
        {
            return ClientManager.Default.Contains(sessionID);
        }


        bool IFlexService.KeepAlive(string sessionID)
        {
            return ClientManager.Default.Contains(sessionID);
        }





        DepositData[] IDepositService.GetDepositDataByPage(string sessionID, int pageIndex)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return DataManager.Default.GetDepositDataByPage(sessionID,pageIndex);
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("GetDepositData  {0}", ex);
                return null;
            }
        }


        PLData[] IPLService.GetPLDataByPage(string sessionID, int pageIndex)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return DataManager.Default.GetPLDataByPage(sessionID,pageIndex);
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("GetPLData  {0}", ex);
                return null;
            }
        }


        int IDepositService.GetDepositRecordCount(string sessionID, QueryObject parameter)
        {
            if (!ClientManager.Default.Contains(sessionID)) return 0;
            try
            {
                return DataManager.Default.GetDepositRecordCount(parameter);
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("GetDepositRecordCount  {0}", ex);
                return 0;
            }
        }


        int IPLService.GetPLDataRecordCount(string sessionID,QueryObject parameter)
        {
            if (!ClientManager.Default.Contains(sessionID)) return 0;
            try
            {
                return DataManager.Default.GetPLDataRecordCount(parameter);
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("GetPLDataRecordCount  {0}", ex);
                return 0;
            }
        }

        bool IUserService.AddUser(string sessionID, User user)
        {
            return UserManager.Default.AddUser(sessionID, user);
        }

        bool IUserService.DelUser(string sessionID, User user)
        {
            return UserManager.Default.DelUser(sessionID, user);
        }

        bool IUserService.UpdateUser(string sessionID, User user)
        {
            return UserManager.Default.UpdateUser(sessionID, user);
        }

        User[] IUserService.GetUsers(string sessionID)
        {
            return UserManager.Default.GetUserList(sessionID);
        }


        IEnumerable<string> IFlexService.GetAccountGroups(string sessionID)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
               return DataRepository.GetAccountGroups();
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return null;
            }
        }

        IEnumerable<AccountData> IFlexService.GetAccountDataByGroup(string sessionID, string groupCode)
        {
            if (!ClientManager.Default.Contains(sessionID)) return null;
            try
            {
                return DataRepository.GetAccountDataByGroup(groupCode);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return null;
            }
        }
    }
}
