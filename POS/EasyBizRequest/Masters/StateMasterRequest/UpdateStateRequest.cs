using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StateMasterRequest
{
    [DataContract]
    [Serializable]
   public class UpdateStateRequest : BaseRequestType
    {
        [DataMember]
        public StateMaster StateRecord { get; set; }
    }
}
