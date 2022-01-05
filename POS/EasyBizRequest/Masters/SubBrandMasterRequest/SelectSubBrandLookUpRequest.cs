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

   public class SelectSubBrandLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public string BrandName { get; set; }
    }
}
