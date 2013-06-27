using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FlexInterface.Common
{
    public enum BusinessTypeEnum
    {
        Withdraw = 0,
        Deposit = 1,
        Transfer = 2,
        RealizedPL = 3,
        FloatingPL = 4,
        InterestPL = 5,
        StoragePL = 6,
        Commission = 7,
        Levy = 8
    }

    [DataContract]
    public class QueryPageCountResult
    {
        [DataMember]
        public int PageCount { get; set; }
        [DataMember]
        public ReturnType Type { get; set; }
    }

    public enum ReturnType
    {
        NoData = 0,
        Normal = 1,
        DataNotComplete = 2,
        Error = 3
    }

}
