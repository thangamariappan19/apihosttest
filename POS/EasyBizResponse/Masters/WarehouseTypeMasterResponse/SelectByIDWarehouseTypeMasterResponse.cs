﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.WarehouseTypeMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDWarehouseTypeMasterResponse : BaseResponseType
    {
        [DataMember]
        public WarehouseTypeMaster WarehouseTypeMasterRecord { get; set; }
    }
}
