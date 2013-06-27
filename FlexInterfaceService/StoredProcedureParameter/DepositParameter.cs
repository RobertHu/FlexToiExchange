using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexInterfaceService.StoredProcedureParameter
{

    public class QueryDepositRecordParameter
    {
        public DateTime FromTradeDay { get; set; }
        public DateTime ToTradeDay { get; set; }
        public string AccountCodeStr { get; set; }
        public string DepositCodeStr { get; set; }
        public FlexInterface.Common.BusinessTypeEnum PaymentType { get; set; }
        public bool IsGetRecordCount { get; set; }
    }


    public class QueryDepositCodeParameter
    {
        public DateTime FromTradeDay { get; set; }
        public DateTime ToTradeDay { get; set; }
        public string AccountCodeStr { get; set; }
        public FlexInterface.Common.BusinessTypeEnum PaymentType { get; set; }
    }
   

}