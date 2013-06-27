using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace FlexInterface.Common
{
    [DataContract]
    public class InterestStorageLevyCommisionPLData:PLData
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

    }
}
