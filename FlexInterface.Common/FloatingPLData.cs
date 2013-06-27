using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace FlexInterface.Common
{
    [DataContract]
    public class FloatingPLData : PLData
    {
        [DataMember]
        public decimal TradingAmount { get; set; }
        [DataMember]
        public string AskPrice { get; set; }
        [DataMember]
        public string BidPrice { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public decimal MonthlyChangeRate { get; set; }

        [DataMember]
        public FloatingPLData LastFloatingPLData { get; set; }

    }

}
