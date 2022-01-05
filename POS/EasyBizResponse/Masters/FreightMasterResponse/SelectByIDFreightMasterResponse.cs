﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.FreightMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDFreightMasterResponse: BaseResponseType
    {
        [DataMember]
        public FreightMaster FreightMasterRecord { get; set; }
    }
}
