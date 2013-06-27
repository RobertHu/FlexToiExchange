using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using FlexInterface.Common;
using iExchange.Common;
using System.Data;
using FlexInterfaceService.Util;
using FlexInterface.Repository;
using System.Diagnostics;
using FlexInterfaceService.StoredProcedureParameter;
namespace FlexInterfaceService
{
    public class DepositService
    {
        private ILog _Log = LogManager.GetLogger(typeof(DepositService));
        private Dictionary<string, List<DepositData>> _Dict = new Dictionary<string, List<DepositData>>();
        private object _SycBlock = new object();
        private CommonService _CommonService;
        private AccountService _AccountService;
        public DepositService()
        {
            this._CommonService = new CommonService();
            this._AccountService = new AccountService();
        }

        public QueryPageCountResult GetDepositPageCount(string sessionID, QueryObject parameter)
        {
            QueryPageCountResult result = new QueryPageCountResult();
            QueryDepositRecordParameter depositParameter = new QueryDepositRecordParameter
            {
                 FromTradeDay= parameter.BeginDatetime,
                 ToTradeDay=parameter.EndDatetime,
                 AccountCodeStr=parameter.AccountStr,
                 DepositCodeStr=parameter.DepositQueryStr,
                 PaymentType=parameter.Type,
                 IsGetRecordCount=false
            };
            string sql = ProcedureStrFactory.CreateQueryDepositRecordSql(depositParameter);
            _Log.Warn(string.Format("GetDepositData {0}", sql));
            DataSet ds = DataAccess.GetData(sql, ConfigHelper.ConnectionString, TimeSpan.FromMinutes(10));
            _Log.Warn(string.Format("GetDepositData End Count: {0}", ds.Tables[0].Rows.Count));
            Guid? accountingBaseCurrencyId = CurrencyRepository.GetAccountingBaseCurrency();
            if (accountingBaseCurrencyId == null)
            {
                result.Type = ReturnType.DataNotComplete;
                return result;
            }
            string baseCurrencyName = CurrencyRepository.GetCurrencyName(accountingBaseCurrencyId.Value);
            List<DepositData> list = new List<DepositData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    var data = Fill(dr, baseCurrencyName, parameter.Type);
                    if (data == null)
                    {
                        result.Type = ReturnType.DataNotComplete;
                        return result;
                    }
                    if (parameter.Type != BusinessTypeEnum.Transfer) FlexInterface.Helper.DepositInnerService.AddDesc(parameter.Type, data);
                    list.Add(data);
                }
                catch (Exception ex)
                {
                    _Log.ErrorFormat("GetDepositData  {0}", ex);
                    result.Type = ReturnType.Error;
                    return result;
                }
            }

            int count = list.Count;
            if (parameter.Type == BusinessTypeEnum.Transfer)
            {
                count = count / 2;
                List<DepositData> transferList = new List<DepositData>();
                foreach (var item in list.GroupBy(m => m.MarginReference).ToDictionary(m => m.Key, m => m.OrderBy(x => x.SourceAmount).ToList()))
                {
                    if (item.Value.Count != 2)
                    {
                        result.Type = ReturnType.Error;
                        return result;
                    }
                    var target = item.Value[1];
                    target.Transfer = item.Value[0];
                    FlexInterface.Helper.DepositInnerService.AddDesc(BusinessTypeEnum.Transfer, target);
                    transferList.Add(target);
                }
                lock (this._SycBlock)
                {
                    this._Dict[sessionID] = transferList;
                }

            }
            else
            {
                lock (this._SycBlock)
                {
                    this._Dict[sessionID] = list;
                }
            }
            _Log.Warn(string.Format("GetDepositData Complete Count: {0}", count));
            result.Type = ReturnType.Normal;
            result.PageCount = this._CommonService.GetPageCount(count);
            return result;
        }



        private DepositData Fill(DataRow dr, string baseCurrencyName, BusinessTypeEnum businessType)
        {
            DepositData data = new DepositData();
            data.Type = dr.EscapeDBNULL<int>("Type");
            data.AccountCode = dr.EscapeDBNULL<string>("AccountCode");
            data.MT4Code = dr.EscapeDBNULL<int>("MT4LoginId").ToString();
            data.TransactionNo = dr.EscapeDBNULL<string>("Code");
            data.SourceCustomerCode = dr.EscapeDBNULL<string>("CustomerCode");
            data.SourceCustomerName = dr.EscapeDBNULL<string>("CustomerName");
            data.SourceCustomerAliasCode = dr.EscapeDBNULL<string>("CustomerAlias");

            data.AccountingDesription = dr.EscapeDBNULL<string>("AccountingDesription");
            data.SourceAmount = dr.EscapeDBNULL<decimal>("TargetAmount");
            data.SourceCurrency = dr.EscapeDBNULL<Guid>("TargetCurrencyID");
            data.UpdateTime = dr.EscapeDBNULL<DateTime>("UpdateTime");
            data.MarginReference = dr.EscapeDBNULL<long>("MarginReference");

            if (dr["iExchangeCurrencyName"] == DBNull.Value) return null;
            data.SourceCurrencyName = dr.EscapeDBNULL<string>("iExchangeCurrencyName");
            if (dr["iExchangeCurrencyRate"] == DBNull.Value) return null;
            data.SourceCurrencyRate = (decimal)(dr.EscapeDBNULL<double>("iExchangeCurrencyRate"));
            if (businessType == BusinessTypeEnum.Withdraw || businessType == BusinessTypeEnum.Deposit)
            {
                data.BankName = dr.EscapeDBNULL<string>("BankName");
                data.AccountName = dr.EscapeDBNULL<string>("AccountName");
                data.BankCode = dr.EscapeDBNULL<string>("BankCode");
                if (dr["BankCurrencyRate"] == DBNull.Value) return null;
                if (dr["BankCurrencyName"] == DBNull.Value) return null;
                if (dr["BankMt4LoginId"] == DBNull.Value) return null;
                if (dr["BankCurrencyId"] == DBNull.Value) return null;
                data.TargetMt4 = dr.EscapeDBNULL<int>("BankMt4LoginId").ToString();
                data.TargetAmount = dr.EscapeDBNULL<decimal>("AmountRecevided");
                data.TargetCurrency = dr.EscapeDBNULL<Guid>("BankCurrencyId");
                data.TargetCurrencyRate = (decimal)(dr.EscapeDBNULL<double>("BankCurrencyRate"));
                data.TargetCurrencyName = dr.EscapeDBNULL<string>("BankCurrencyName");
                if (data.SourceCurrency != data.TargetCurrency)
                {
                    if (dr["DayTradeRate1"] == DBNull.Value || dr["DayTradeRate2"] == DBNull.Value)
                    {
                        return null;
                    }
                    var dtr1 = dr.EscapeDBNULL<double>("DayTradeRate1");
                    var dtr2 = dr.EscapeDBNULL<double>("DayTradeRate2");
                    data.DayTradeRate = Math.Max(dtr1, dtr2);
                }
            }
            int? iExchangePlAccount = this._AccountService.GetExchangePlAccount();
            if (iExchangePlAccount == null) return null;
            data.iExchangePlMT4LoginID = iExchangePlAccount.Value.ToString();
            data.BaseCurrencyName = baseCurrencyName;
            return data;
        }



        public int GetDepositRecordCount(QueryObject parameter)
        {
            QueryDepositRecordParameter depositParameter = new QueryDepositRecordParameter()
            {
                FromTradeDay=parameter.BeginDatetime,
                ToTradeDay=parameter.EndDatetime,
                AccountCodeStr=parameter.AccountStr,
                DepositCodeStr=parameter.DepositQueryStr,
                PaymentType=parameter.Type,
                IsGetRecordCount=true
            };
            string sql = ProcedureStrFactory.CreateQueryDepositRecordSql(depositParameter);
            _Log.Warn(string.Format("GetDepositData {0}", sql));
            return (int)DataAccess.ExecuteScalar(sql, ConfigHelper.ConnectionString);
        }


        public DepositData[] GetDepositDataByPage(string sessionId, int pageIndex)
        {
            lock (this._SycBlock)
            {
                if (!this._Dict.ContainsKey(sessionId)) return null;
                var data = this._Dict[sessionId].Skip(pageIndex - 1).Take(ConfigHelper.PageSize).ToArray();
                if (data == null || data.Length < ConfigHelper.PageSize) this._Dict[sessionId] = new List<DepositData>();
                return data;
            }
        }

    }
}