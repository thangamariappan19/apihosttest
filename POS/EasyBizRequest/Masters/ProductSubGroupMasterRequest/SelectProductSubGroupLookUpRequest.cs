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
    public class SelectProductSubGroupLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int ProductGroupID { get; set; }
        [DataMember]
        public string ProductGroupName { get; set; }
    }
}
