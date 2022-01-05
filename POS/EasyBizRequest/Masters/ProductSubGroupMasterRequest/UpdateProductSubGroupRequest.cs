using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ProductSubGroupMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateProductSubGroupRequest : BaseRequestType
    {
        [DataMember]
        public ProductSubGroupMaster ProductSubGroupData { get; set; }
    }
}
