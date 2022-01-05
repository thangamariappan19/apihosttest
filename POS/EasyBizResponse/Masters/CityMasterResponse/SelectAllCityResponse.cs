﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CityMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllCityResponse : BaseResponseType
    {
        [DataMember]
        public List<CityMaster> CityList { get; set; }
    }
}
