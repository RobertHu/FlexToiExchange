using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace FlexInterface.Common
{
    [DataContract]
    [KnownType(typeof(RealizedPLData))]
    [KnownType(typeof(FloatingPLData))]
    [KnownType(typeof(InterestStorageLevyCommisionPLData))]
    public class PLData
    {
        [DataMember]
        public BusinessTypeEnum Type { get; set; }
        [DataMember]
        public string FromMt4LoginID { get; set; }
        [DataMember]
        public string ToMt4LoginID { get; set; }
        [DataMember]
        public DateTime FromTradeDay { get; set; }
        [DataMember]
        public DateTime ToTradeDay { get; set; }
        [DataMember]
        public decimal OriginAmount { get; set; }
        [DataMember]
        public decimal ExchangeRate { get; set; }
        [DataMember]
        public decimal EquvAmount { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
       
    }
}
