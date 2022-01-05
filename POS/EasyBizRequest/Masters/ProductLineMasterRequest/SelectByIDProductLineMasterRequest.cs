using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ProductLineMasterRequest
{
    [Serializable]
    [DataContract]  
    public class SelectByIDProductLineMasterRequest: BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
