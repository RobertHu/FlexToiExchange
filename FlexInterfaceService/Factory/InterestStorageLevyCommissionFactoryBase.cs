using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
using System.Data;
namespace FlexInterfaceService
{
    public  class InterestStorageLevyCommissionFactoryBase
    {
        protected static void CreateHelper(DataRow dr, PLData data)
        {
            data.FromMt4LoginID = dr.EscapeDBNULL<int>("MT4LoginId").ToString();
            if (dr["AccountingMappingCode"] == DBNull.Value)
            {
                throw new ArgumentNullException("AccountingMappingCode");
            }
            data.ToMt4LoginID = dr.EscapeDBNULL<int>("AccountingMappingCode").ToString();
            data.CurrencyCode = dr.EscapeDBNULL<string>("AccountingCurrencyName");
            if (dr["AccountingCurrencyRate"] == DBNull.Value)
            {
                throw new ArgumentNullException("AccountingCurrencyRate");
            }
            data.ExchangeRate = (decimal)dr.EscapeDBNULL<double>("AccountingCurrencyRate");
            var otherPlData = data as InterestStorageLevyCommisionPLData;
            if (otherPlData != null)
            {
                otherPlData.CustomerCode = dr.EscapeDBNULL<string>("CustomerCode");
                otherPlData.CustomerName = dr.EscapeDBNULL<string>("CustomerDesc");
                otherPlData.ProductName = dr.EscapeDBNULL<string>("ProductName");
            }
        }
       
    }
}