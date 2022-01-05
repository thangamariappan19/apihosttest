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
    public class SelectAllAgentResponse : BaseResponseType
    {
        [DataMember]
        public List<AgentMaster> AgentList { get; set; }
    }
}
