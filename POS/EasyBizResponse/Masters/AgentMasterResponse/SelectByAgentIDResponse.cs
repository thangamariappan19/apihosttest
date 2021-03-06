using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.AgentMasterResponse
{ 
    [DataContract]
    [Serializable]
    public class SelectByAgentIDResponse : BaseResponseType
    {
        [DataMember]
        public AgentMaster AgentRecord { get; set; }
    }
}
