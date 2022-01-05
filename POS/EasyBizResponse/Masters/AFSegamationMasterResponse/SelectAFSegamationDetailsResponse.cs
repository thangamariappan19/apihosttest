using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.AFSegamationMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectAFSegamationDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<SegmentMaster> AFSegmentDetailMasterRecord { get; set; }
    }
}
