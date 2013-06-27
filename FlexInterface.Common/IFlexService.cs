using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace FlexInterface.Common
{
    [ServiceContract(CallbackContract = typeof(IFlexServiceCallback), SessionMode = SessionMode.Required)]
    public interface IFlexService : IDepositService, IPLService, IUserService
    {
        [OperationContract]
        string HelloWorld();
        [OperationContract]
        string GetConfigStr();
        [OperationContract]
        int? GetExchangePlAccount(string sessionID);

        [OperationContract]
        IEnumerable<string> GetAccountGroups(string sessionID);

        [OperationContract]
        IEnumerable<AccountData> GetAccountDataByGroup(string sessionID, string groupCode);
        
        [OperationContract]
        LoginResult ConnectServer(string loginId, string pwd);
        [OperationContract]
        int GetClientSessionCount();
        [OperationContract]
        bool ContainsSession(string sessionID);
        [OperationContract]
        bool KeepAlive(string sessionID);
    }
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool AddUser(string sessionID, User user);
        [OperationContract]
        bool DelUser(string sessionID, User user);
        [OperationContract]
        bool UpdateUser(string sessionID, User user);
        [OperationContract]
        User[] GetUsers(string sessionID);
    }


    [ServiceContract]
    public interface IDepositService
    {
        [OperationContract]
        string[] GetAllFundNo(string sessionID, QueryObject parameter);

        [OperationContract]
        IEnumerable<string> GetAllCustomerCode(string sessionID);
        [OperationContract]
        QueryPageCountResult GetDepositPageCount(string sessionID, QueryObject data);
        [OperationContract]
        int GetDepositRecordCount(string sessionID, QueryObject data);
        [OperationContract(Name = "GetDepositDataByIndex")]
        DepositData[] GetDepositDataByPage(string sessionID, int pageIndex);
    }

    [ServiceContract]
    public interface IPLService
    {
        [OperationContract]
        void GetResult();

        [OperationContract]
        QueryPageCountResult GetPLDataPageCount(string sessionID, QueryObject data);
        [OperationContract]
        int GetPLDataRecordCount(string sessionID, QueryObject data);
        [OperationContract(Name = "GetPLDataByIndex")]
        PLData[] GetPLDataByPage(string sessionID, int pageIndex);
    }

    [ServiceContract]
    public interface IFlexServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void PushUpdateUser(User user);
    }



    public enum SelectionEnum
    {
        None,
        BySelection,
        ByRange
    }

}
