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
    public class UpdateScaleRequest : BaseRequestType
    {
        [DataMember]
        public ScaleMaster ScaleRecord { get; set; }
         [DataMember]
        public ScaleDetailMaster ScaleDetailMaster { get; set; }
    }
}
