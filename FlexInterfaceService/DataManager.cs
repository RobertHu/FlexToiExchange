using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using iExchange.Common;
using FlexInterface.Common;
using System.Web.Caching;
using log4net;
using System.Threading.Tasks;

namespace FlexInterfaceService
{
    public class DataManager
    {
        private static DataManager _Default;
        private ILog _Log = LogManager.GetLogger(typeof(DataManager));
        private AccountService _AccountService;
        private DepositService _DepositService;
        private PLService _PLService;
        private DataManager()
        {
            _AccountService = new AccountService();
            _DepositService = new DepositService();
            _PLService = new PLService(this._AccountService);
        }
        public static DataManager Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new DataManager();
                }
                return _Default;
            }
        }


        public IEnumerable<string>  GetAllAccountCode()
        {
            return _AccountService.GetAllAccountCode();
        }

       


        public int? GetExchangePlAccount()
        {
            return _AccountService.GetExchangePlAccount();
        }








        public QueryPageCountResult GetDepositPageCount(string sessionID, QueryObject data)
        {
            return _DepositService.GetDepositPageCount(sessionID,data);
        }


        public DepositData[] GetDepositDataByPage(string sessionID, int pageIndex)
        {
            return _DepositService.GetDepositDataByPage(sessionID, pageIndex);
        }


        public int GetDepositRecordCount(QueryObject parameter)
        {
            return _DepositService.GetDepositRecordCount(parameter);
        }


        public QueryPageCountResult GetPLDataPageCount(string sessionID, QueryObject parameter)
        {
            return this._PLService.GetPLDataPageCount(sessionID, parameter);
        }

        public PLData[] GetPLDataByPage(string sessionID, int pageIndex)
        {
            return this._PLService.GetPLDataByPage(sessionID, pageIndex);
        }

        public int GetPLDataRecordCount(QueryObject parameter)
        {
            return this._PLService.GetPLDataRecordCount(parameter);
        }

    }
}