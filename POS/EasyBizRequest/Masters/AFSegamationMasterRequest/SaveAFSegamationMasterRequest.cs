using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.AFSegamationMasterRequest
{

    [DataContract]
    [Serializable]
    public class SaveAFSegamationMasterRequest : BaseRequestType
    {
        [DataMember]
        public AFSegamationMasterTypes AFSegamationMasterTypesRecord { get; set; }
        public List<SegmentMaster> AFSegmentationDetailMasterList { get; set; }
    }
}
