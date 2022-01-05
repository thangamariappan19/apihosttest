using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubBrandMasterRequest
{
    [DataContract]
    [Serializable]
   public class UpdateSubBrandRequest : BaseRequestType
    {
        [DataMember]
        public SubBrandMaster SubBrandData { get; set; }
    }
}
