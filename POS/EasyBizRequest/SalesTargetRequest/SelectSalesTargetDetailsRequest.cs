﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.SalesTargetRequest
{
    [Serializable]
    [DataContract]
    public class SelectSalesTargetDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
