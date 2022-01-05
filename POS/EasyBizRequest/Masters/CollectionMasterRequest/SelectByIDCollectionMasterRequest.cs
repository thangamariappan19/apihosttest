﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CollectionMasterRequest
{

    [DataContract]
    [Serializable]
    public class SelectByIDCollectionMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
