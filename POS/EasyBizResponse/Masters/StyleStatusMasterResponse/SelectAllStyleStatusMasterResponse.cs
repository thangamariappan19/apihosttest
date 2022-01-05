﻿using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleStatusMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectAllStyleStatusMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<StyleStatusMasterType> StyleStatusMasterTypeList { get; set; }
    }
}
