using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
namespace FlexInterfaceService
{
    public class LevyPLDataFactory:InterestStorageLevyCommissionFactoryBase
    {
        public static InterestStorageLevyCommisionPLData Create(System.Data.DataRow dr)
        {
             var data = new InterestStorageLevyCommisionPLData();
             CreateHelper(dr, data);
            data.Type = BusinessTypeEnum.Levy;
            data.OriginAmount = (decimal)dr["Levy"];
            data.EquvAmount = data.OriginAmount * data.ExchangeRate;
            return data;
        }
    }
}