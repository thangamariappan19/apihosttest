using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.AFSegamationMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectSegamationDetailsLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<SegmentMaster> SegmentDetailList { get; set; }
    }
}
