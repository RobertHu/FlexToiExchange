using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using iExchange.Common;
using System.Data;
using FlexInterface.Common;
using System.Threading.Tasks;
using FlexInterfaceService.Util;
using FlexInterfaceService.StoredProcedureParameter;
namespace FlexInterfaceService
{
    public class PLService
    {
        private ILog _Log = LogManager.GetLogger(typeof(PLService));
        private Dictionary<string, List<PLData>> _Dict = new Dictionary<string, List<PLData>>();
        private object _SycBlock = new object();
        private AccountService _AccountService;
        private CommonService _CommonService;
        public PLService(AccountService accountService)
        {
            this._AccountService = accountService;
            this._CommonService = new CommonService();
        }
        public int GetPLDataRecordCount(QueryObject parameter)
        {
            PLPrameter plParameter = new PLPrameter()
            {
                 FromTradeDay = parameter.BeginDatetime,
                 ToTradeDay = parameter.EndDatetime,
                 AccountCodeString = parameter.AccountStr,
                 Type=parameter.Type,
                 IsGetRecordCount=true
            };
            string sql = ProcedureStrFactory.CreateQueryPlSql(plParameter);
            return (int)DataAccess.ExecuteScalar(sql, ConfigHelper.ConnectionString);
        }

        public PLData[] GetPLDataByPage(string sessionID, int pageIndex)
        {
            lock (this._SycBlock)
            {
                if (!this._Dict.ContainsKey(sessionID)) return null;
                var data = this._Dict[sessionID].Skip(pageIndex - 1).Take(ConfigHelper.PageSize).ToArray();
                if (data == null || data.Length < ConfigHelper.PageSize) this._Dict[sessionID] = new List<PLData>();
                return data;
            }
        }

        private PLData Fill(DataRow dr, BusinessTypeEnum plType)
        {
            PLData data = null;
            switch (plType)
            {
                case BusinessTypeEnum.RealizedPL: 
                    data = RealizedPLFactory.Create(dr);
                    break;
                case BusinessTypeEnum.FloatingPL:
                    data = FloatingPlDataFactory.Create(dr);
                    break;
                case BusinessTypeEnum.InterestPL:
                    data = InterestPLDataFactory.Create(dr);
                    break;
                case BusinessTypeEnum.StoragePL:
                    data = StoragePlDataFactory.Create(dr);
                    break;
                case BusinessTypeEnum.Commission:
                    data = ComissionPLDataFactory.Create(dr);
                    break;
                case BusinessTypeEnum.Levy:
                    data = LevyPLDataFactory.Create(dr);
                    break;
            }
            return data;
        }


        public QueryPageCountResult GetPLDataPageCount(string sessionID, QueryObject parameter)
        {
            PLPrameter plParameter = new PLPrameter()
            {
                FromTradeDay = parameter.BeginDatetime,
                ToTradeDay = parameter.EndDatetime,
                AccountCodeString = parameter.AccountStr,
                Type = parameter.Type,
                IsGetRecordCount = false
            };
            string sql = ProcedureStrFactory.CreateQueryPlSql(plParameter);
            QueryPageCountResult result = new QueryPageCountResult();
            List<PLData> plList = new List<PLData>();
            _Log.WarnFormat(" GET PL DATA, sql:   {0} ", sql);
            DataSet ds = DataAccess.GetData(sql, ConfigHelper.ConnectionString, TimeSpan.FromMinutes(10));
            _Log.Warn(string.Format("GET PL DATA End.  Result Count:{0}", ds.Tables[0].Rows.Count));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    PLData plData = Fill(dr, parameter.Type);
                    if (plData == null)
                    {
                        result.Type = ReturnType.DataNotComplete;
                        return result;
                    }
                    plData.FromTradeDay = parameter.BeginDatetime;
                    plData.ToTradeDay = parameter.EndDatetime;
                    if (plData is FloatingPLData)
                    {
                        FloatingPLData data = (FloatingPLData)plData;
                        DateTime targetDate = data.FromTradeDay.AddDays(-1);
                        data.LastFloatingPLData.FromTradeDay = targetDate;
                        data.LastFloatingPLData.ToTradeDay = targetDate;
                    }

                    if (plData.OriginAmount == 0) continue;
                    plList.Add(plData);
                }
                catch (ArgumentNullException ane)
                {
                    _Log.Error(ane);
                    result.Type = ReturnType.DataNotComplete;
                    return result;
                }
                catch (Exception ex)
                {
                    _Log.Error(ex);
                    result.Type = ReturnType.Error;
                    return result;
                }
                
            }
            int count = plList.Count;
            _Log.InfoFormat("Final Data quantity: {0}", count);
            if (count == 0)
            {
                result.Type = ReturnType.NoData;
            }
            else
            {
                result.Type = ReturnType.Normal;
            }
            result.PageCount = this._CommonService.GetPageCount(count);
            lock (this._SycBlock)
            {
                this._Dict[sessionID] = plList;
            }
            return result;
        }

    }
}