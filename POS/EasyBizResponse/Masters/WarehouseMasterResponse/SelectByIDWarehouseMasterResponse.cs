﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.WarehouseMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDWarehouseMasterResponse: BaseResponseType
    {
        public WarehouseMaster WarehouseMasterRecord { get; set; }
    }
}
