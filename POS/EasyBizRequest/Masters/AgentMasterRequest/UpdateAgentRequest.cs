using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.AgentMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateAgentRequest : BaseRequestType
    {
        [DataMember]
        public AgentMaster AgentRecord { get; set; }
    }
}
