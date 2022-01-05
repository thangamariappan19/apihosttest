using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StoreMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectStoreMasterLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<StoreMaster> StoreMasterList = new List<StoreMaster>();
        [DataMember]
        public int StoreGroupID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        public string CountryIDs { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public String Brand { get; set; }
        [DataMember]
        public string StoreGroupIDs { get; set; }
       
    }
}
