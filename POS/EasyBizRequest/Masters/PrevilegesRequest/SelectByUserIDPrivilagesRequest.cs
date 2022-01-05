﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PrevilegesRequest
{
    [DataContract]
    [Serializable]
    public class SelectByUserIDPrivilagesRequest : BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public long UserID { get; set; }
    }
}
