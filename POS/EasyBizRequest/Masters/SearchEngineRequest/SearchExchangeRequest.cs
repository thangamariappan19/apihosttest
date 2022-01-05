using EasyBizDBTypes.Masters;
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
    public class SearchExchangeRequest : BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
