using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByStyleIDsRequest : BaseRequestType
    {
        [DataMember]
        public string IDs { get; set; }
    }
}
