using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.VendorGroupMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateVendorGroupRequest : BaseRequestType
    {
        [DataMember]
        public VendorGroupMaster VendorGroupRecord { get; set; }
    }
}
