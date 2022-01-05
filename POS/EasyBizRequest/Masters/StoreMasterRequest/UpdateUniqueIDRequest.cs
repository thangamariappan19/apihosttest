using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StoreMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateUniqueIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String DiskID { get; set; }
         [DataMember]
        public String CPUID { get; set; }
        [DataMember]
        public String Type { get; set; }
    }
}
