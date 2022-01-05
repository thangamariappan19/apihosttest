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
    [Serializable]
    [DataContract]
    public class UpdateManagerOverrideRequest : BaseRequestType
    {
        [DataMember]
        public ManagerOverride ManagerOverrideData { get; set; }
    }
}
