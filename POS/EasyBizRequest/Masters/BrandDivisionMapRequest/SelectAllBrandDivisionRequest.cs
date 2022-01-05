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
    public class SelectAllBrandDivisionRequest : BaseRequestType
    {
        [DataMember]
        public long BrandID { get; set; }
    }
}
