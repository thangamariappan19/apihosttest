﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PromotionsMasterRequest
{
    [DataContract]
    [Serializable]
    public class DeletePromotionsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
