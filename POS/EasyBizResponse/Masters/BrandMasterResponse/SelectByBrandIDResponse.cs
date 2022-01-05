﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.Brand_Response
{
    [DataContract]
    [Serializable]
   public class SelectByBrandIDResponse : BaseResponseType
    {
          [DataMember]
        public BrandMaster BrandRecord { get; set; }
    }
}
