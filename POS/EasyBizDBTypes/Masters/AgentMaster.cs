using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class AgentMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string AgentCode { get; set; }
        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        //public Boolean Active { get; set; }
    }
}
