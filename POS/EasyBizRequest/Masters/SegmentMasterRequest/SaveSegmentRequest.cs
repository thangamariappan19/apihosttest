﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SegmentMasterRequest
{
    [DataContract]
    [Serializable]
   public  class SaveSegmentRequest : BaseRequestType
    {
         [DataMember]
        public SegmentMaster SegmentationRecord { get; set; }
    }
}