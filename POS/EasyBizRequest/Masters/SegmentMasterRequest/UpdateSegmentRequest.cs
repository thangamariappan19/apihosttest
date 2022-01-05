using System;
using EasyBizDBTypes.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SegmentMasterRequest
{
    [Serializable]
    [DataContract]
  public  class UpdateSegmentRequest: BaseRequestType
    {
        [DataMember]
        public SegmentMaster SegmentMasterData { get; set; }
    }
}
