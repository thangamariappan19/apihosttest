﻿using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class CountTypeMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CountTypeCode { get; set; }
        [DataMember]
        public string CountTypeName { get; set; }
    }
}
