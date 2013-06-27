using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
using System.Data;
namespace FlexInterfaceService
{
    public class FloatingPlDataFactory:InterestStorageLevyCommissionFactoryBase
    {
        public static FloatingPLData Create(DataRow dr)
        {
            FloatingPLData data = new FloatingPLData();
            CreateHelper(dr, data);

            data.EquvAmount = dr.EscapeDBNULL<decimal>("TradePLFloat");
            data.MonthlyChangeRate = data.ExchangeRate;
            data.OriginAmount = data.EquvAmount / data.ExchangeRate;
            data.Type = BusinessTypeEnum.FloatingPL;
            data.ProductName = dr.EscapeDBNULL<string>("ProductName");
            data.AskPrice = dr.EscapeDBNULL<string>("Ask");
            data.BidPrice = dr.EscapeDBNULL<string>("Bid");
            data.TradingAmount = dr.EscapeDBNULL<decimal>("TradingAmount");
            InitializeBeforeData(data, dr);
            return data;
        }

        private static void InitializeBeforeData(FloatingPLData data,DataRow dr)
        {
            data.LastFloatingPLData = new FloatingPLData();
            var lastPL = data.LastFloatingPLData;
            lastPL.Type = data.Type;
            lastPL.FromMt4LoginID = data.FromMt4LoginID;
            lastPL.ToMt4LoginID = data.ToMt4LoginID;
            lastPL.CurrencyCode = data.CurrencyCode;

            if (dr["BfAccountingCurrencyRate"] == DBNull.Value)
            {
                throw new ArgumentNullException("BfAccountingCurrencyRate");
            }
            lastPL.ExchangeRate = (decimal)dr.EscapeDBNULL<double>("BfAccountingCurrencyRate");
            lastPL.EquvAmount = dr.EscapeDBNULL<decimal>("BfTradePLFloat");
            lastPL.MonthlyChangeRate = lastPL.ExchangeRate;
            lastPL.OriginAmount = lastPL.EquvAmount / lastPL.ExchangeRate;
            lastPL.TradingAmount = dr.EscapeDBNULL<decimal>("BfTradingAmount");
            lastPL.AskPrice = dr.EscapeDBNULL<string>("BfAsk");
            lastPL.BidPrice = dr.EscapeDBNULL<string>("BfBid");

            lastPL.ProductName = data.ProductName;

        }

    }
}