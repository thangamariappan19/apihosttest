using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BrandDivisionMapRequest
{
    [DataContract]
    [Serializable]
    public class SelectBrandDivisionLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public int BrandCode { get; set; }

        [DataMember]
        public string DivisionName { get; set; }
    }
}
