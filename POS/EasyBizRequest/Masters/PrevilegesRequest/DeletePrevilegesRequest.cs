﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PrevilegesRequest
{

    [Serializable]
    [DataContract]  
    public class DeletePrevilegesRequest : BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }
    }
}
