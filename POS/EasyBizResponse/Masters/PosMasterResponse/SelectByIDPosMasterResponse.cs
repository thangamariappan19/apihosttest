﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PosMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDPosMasterResponse:BaseResponseType
    {
        [DataMember]
        public PosMaster PosMasterRecord { get; set; }
    }
}
