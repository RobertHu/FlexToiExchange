using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
namespace FlexInterfaceService
{
    public class ComissionPLDataFactory:InterestStorageLevyCommissionFactoryBase
    {
        public static InterestStorageLevyCommisionPLData Create(System.Data.DataRow dr)
        {
            var data = new InterestStorageLevyCommisionPLData();
            CreateHelper(dr, data);
            data.Type = BusinessTypeEnum.Commission;
            data.OriginAmount = (decimal)dr["Commission"];
            data.EquvAmount = data.OriginAmount * data.ExchangeRate;
            return data;
        }
    }
}