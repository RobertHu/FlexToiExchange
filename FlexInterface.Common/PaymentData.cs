using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FlexInterface.Common
{
    [DataContract]
    public class QueryObject
    {
        [DataMember]
        public BusinessTypeEnum Type { get; set; }
        public string FilePath { get; set; }
        [DataMember]
        public DateTime BeginDatetime { get; set; }
        [DataMember]
        public DateTime EndDatetime { get; set; }
        [DataMember]
        public string AccountStr { get; set; }
        [DataMember]
        public string DepositQueryStr { get; set; }
    }

}
