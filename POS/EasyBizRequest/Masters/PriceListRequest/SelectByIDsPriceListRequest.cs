﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PriceListRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDsPriceListRequest : BaseRequestType
    {
        [DataMember]
        public string PriceListIDS { get; set; }
    }
}
