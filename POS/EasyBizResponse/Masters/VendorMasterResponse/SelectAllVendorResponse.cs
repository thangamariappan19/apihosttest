﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.VendorMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllVendorResponse : BaseResponseType
    {
        public List<VendorMaster> VendorList { get; set; }
    }
}