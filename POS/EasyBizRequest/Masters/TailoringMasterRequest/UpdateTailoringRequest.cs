﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TailoringMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateTailoringRequest : BaseRequestType
    {
        [DataMember]
        public TailoringMasterTypes TailoringMasterData { get; set; }
    }
}