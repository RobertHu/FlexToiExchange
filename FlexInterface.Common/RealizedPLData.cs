﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace FlexInterface.Common
{
    
    [DataContract]
    public class RealizedPLData:PLData
    {
      
        [DataMember]
        public decimal MonthlyChangeRate { get; set; }
        [DataMember]
        public string ProductName { get; set; }
    }
}
