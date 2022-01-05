﻿using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.AFSegamationMasterResponse
{

    [DataContract]
    [Serializable]
    public class DeleteAFSegamationMasterResponse : BaseResponseType
    {
        [DataMember]
       public  int ID { get; set; }
    }
}