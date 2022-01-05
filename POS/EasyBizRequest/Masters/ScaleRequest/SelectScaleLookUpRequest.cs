using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ScaleRequest
{
    public class SelectScaleLookUpRequest : BaseRequestType

    {
        [DataMember]
        public int BrandID { get; set; }
    }
}
