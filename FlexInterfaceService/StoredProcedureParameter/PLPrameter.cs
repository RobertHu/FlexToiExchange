using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
namespace FlexInterfaceService.StoredProcedureParameter
{
    public class PLPrameter
    {
        public DateTime FromTradeDay { get; set; }
        public DateTime ToTradeDay { get; set; }
        public string AccountCodeString { get; set; }
        public BusinessTypeEnum Type { get; set; }
        public bool IsGetRecordCount { get; set; }
    }
}