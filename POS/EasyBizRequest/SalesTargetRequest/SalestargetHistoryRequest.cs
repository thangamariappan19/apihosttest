using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.SalesTargetRequest
{
    [DataContract]
    [Serializable]
    public class SalestargetHistoryRequest : BaseRequestType
    {
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreIDs { get; set; }
    }
}
