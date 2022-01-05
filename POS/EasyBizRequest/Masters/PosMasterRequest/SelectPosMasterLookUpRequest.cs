using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PosMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectPosMasterLookUpRequest:BaseRequestType
    {
        [DataMember]
        public List<PosMaster> PosMasterList = new List<PosMaster>();
        [DataMember]
        public int CountryID { get; set; }

        public int StoreID { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
