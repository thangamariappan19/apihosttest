using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.FCPasses
{
    [DataContract]
    [Serializable]
    public class SelectPassMasterLookUpRequest : BaseRequestType
    {
        [DataMember]
        public String Type { get; set; }
    }
}
