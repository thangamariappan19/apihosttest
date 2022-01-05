using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ProductGroupRequest
{
    [Serializable]
    [DataContract]
    public class SaveProductGroupRequest : BaseRequestType
    {
        [DataMember]
        public ProductGroupMaster ProductGroupRecord { get; set; }
    }
}
