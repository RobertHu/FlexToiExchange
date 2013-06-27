using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;

namespace FlexInterfaceService
{
    public class InterestPLDataFactory:InterestStorageLevyCommissionFactoryBase
    {
        public static FlexInterface.Common.InterestStorageLevyCommisionPLData Create(System.Data.DataRow dr)
        {
            var data = new InterestStorageLevyCommisionPLData();
            CreateHelper(dr, data);
            data.Type = FlexInterface.Common.BusinessTypeEnum.InterestPL;
            data.OriginAmount = (decimal)dr["InterestPLValued"];
            data.EquvAmount = data.ExchangeRate * data.OriginAmount;
            return data;
        }
    }

    
}