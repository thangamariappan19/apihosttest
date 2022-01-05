using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ScaleRequest
{
    [DataContract]
    public class SelectScaleDetailsLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StyleID { get; set; }
    }
}
