using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
using System.Data;
namespace FlexInterfaceService
{
    public class RealizedPLFactory :InterestStorageLevyCommissionFactoryBase
    {
        public static RealizedPLData Create(DataRow dr)
        {
            RealizedPLData data = new RealizedPLData();
            CreateHelper(dr, data);
            data.EquvAmount = dr.EscapeDBNULL<decimal>("TradePLValued");
            data.MonthlyChangeRate = data.ExchangeRate;
            data.OriginAmount = data.EquvAmount / data.ExchangeRate;
            data.Type = BusinessTypeEnum.RealizedPL;
            data.ProductName = dr.EscapeDBNULL<string>("ProductName");
            return data;
        }
    }
}