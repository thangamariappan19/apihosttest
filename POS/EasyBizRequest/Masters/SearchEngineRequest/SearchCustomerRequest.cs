using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SearchEngineRequest
{
    public class SearchCustomerRequest: BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }
    }
}
