﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PriceTypeRequest
{

    [DataContract]
    [Serializable]
    public class SavePriceTypeRequest : BaseRequestType
    {
        [DataMember]
        public PriceTypeMasterTypes PriceTypesRecord { get; set; }
    }
}
