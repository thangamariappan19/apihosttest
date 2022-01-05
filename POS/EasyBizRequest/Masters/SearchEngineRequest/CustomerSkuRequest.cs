using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SearchEngineRequest
{
    [DataContract]
    [Serializable]
    public class CustomerSkuRequest: BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }    
    }
}
