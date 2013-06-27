using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace FlexInterface.Common
{
    [DataContract]
    public class AccountData
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int MT4LoginID { get; set; }
    }
}
