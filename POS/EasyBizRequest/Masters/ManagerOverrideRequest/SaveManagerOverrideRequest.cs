using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ManagerOverrideRequest
{
    [DataContract]
    [Serializable]
    public class SaveManagerOverrideRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public ManagerOverride ManagerOverrideData { get; set; }
    }
}
