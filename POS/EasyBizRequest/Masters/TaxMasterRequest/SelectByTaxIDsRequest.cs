﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TaxMasterRequest
{

    [DataContract]
    [Serializable]
    public class SelectByTaxIDsRequest:BaseRequestType
    {

        [DataMember]
        public int IDs { get; set; }
    }
}