using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ReasonMasterRequest
{
    [Serializable]
    [DataContract]
    public class UpdateReasonMasterRequest : BaseRequestType
    {
        [DataMember]
        public ReasonMaster ReasonMasterData { get; set; }
    }
}
