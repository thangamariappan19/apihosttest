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
    public class SelectProductGroupListForProductSubGroupRequest : BaseRequestType
    {
        [DataMember]
        public long ProductGroupID { get; set; }
    }
}
