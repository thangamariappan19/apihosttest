using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.RequestTypeMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectRequestTypeMasterLookUpRequest:BaseRequestType
    {
        [DataMember]
        public List<RequestTypeMaster> RequestTypeMasterList = new List<RequestTypeMaster>();
    }
}
