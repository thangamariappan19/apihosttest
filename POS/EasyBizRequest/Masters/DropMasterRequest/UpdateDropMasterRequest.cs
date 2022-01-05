using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DropMasterRequest
{

    [DataContract]
    [Serializable]
    public class UpdateDropMasterRequest : BaseRequestType
    {
        [DataMember]
        public DropMasterTypes DropMasterTypesRequestData { get; set; }
    }
}
