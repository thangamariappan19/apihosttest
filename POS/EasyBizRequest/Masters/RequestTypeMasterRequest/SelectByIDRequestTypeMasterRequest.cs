using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizRequest.Masters.RequestTypeMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByIDRequestTypeMasterRequest:BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
