using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using iExchange.Common;
using log4net;
using FlexInterface.Common;
using FlexInterface.Repository;
using System.Diagnostics;
using FlexInterfaceService.Util;
using FlexInterfaceService.StoredProcedureParameter;
namespace FlexInterfaceService
{
    public class AccountService
    {
        private static ILog _Log = LogManager.GetLogger(typeof(AccountService));
        public IEnumerable<string> GetAllAccountCode()
        {
            var data = DataRepository.GetAccountMt4LoginID();
            if (data == null) yield break;
            foreach (var item in data)
            {
                yield return item;
            }
        }

        public int? GetExchangePlAccount()
        {
            int loginId = DataRepository.GetAccountingMappingMt4LoginID(0);
            Debug.Assert(loginId > 0, "Not Setting iexchange pl mt4loginid");
            if (loginId == 0) return null;
            return loginId;
        }



        public int? GetAccountingMapping(Tuple<int, Guid, Guid> key)
        {
            try
            {
                int mt4loginID = DataRepository.GetAccountingMappingMt4LoginID(key.Item1, key.Item2, key.Item3);
                if (mt4loginID == 0) return null;
                return mt4loginID;
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetAccountingMapping {0}", e);
                return null;
            }
        }


        public static string[] GetAllFundNo(QueryObject queryObj)
        {
            QueryDepositCodeParameter parameter = new QueryDepositCodeParameter
            {
                 FromTradeDay=queryObj.BeginDatetime,
                 ToTradeDay=queryObj.EndDatetime,
                 AccountCodeStr= queryObj.AccountStr,
                 PaymentType= queryObj.Type
            };
            string sql = ProcedureStrFactory.CreateQueryDepositCodeSql(parameter);
           _Log.InfoFormat("GetAllFundNo sql:{0}", sql);
            DataSet ds = DataAccess.GetData(sql, ConfigHelper.ConnectionString);
           _Log.InfoFormat("GetAllFundNo count:{0}", ds.Tables[0].Rows.Count);
            List<string> list = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add((string)dr["Code"]);
            }
            return list.ToArray();
        }
    }
}