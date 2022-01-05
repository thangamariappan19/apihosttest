using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;

namespace EasyBizRequest.Masters.RequestTypeMasterRequest
{
    [Serializable]
    [DataContract]
    public class UpdateRequestTypeMasterRequest:BaseRequestType
    {
        [DataMember]
        public RequestTypeMaster RequestTypeMasterData { get; set; }
    }
}
