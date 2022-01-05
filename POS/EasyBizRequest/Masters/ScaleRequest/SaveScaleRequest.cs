using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ScaleRequest
{
    [DataContract]
    [Serializable]
    public class SaveScaleRequest : BaseRequestType
    {
        [DataMember]
        public ScaleMaster ScaleRecord { get; set; }
        public List<ScaleDetailMaster> ScaleDetailMasterList { get; set; }
        public List<BrandMaster> BrandMasterList { get; set; }
    }
}
