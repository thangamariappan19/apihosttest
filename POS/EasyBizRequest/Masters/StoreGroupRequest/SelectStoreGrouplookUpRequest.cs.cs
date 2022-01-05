using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StoreGroupRequest
{
    [Serializable]
    [DataContract]
   public class SelectStoreGroupLookUpRequest:BaseRequestType
    {
        [DataMember]
        public List<StoreGroupMaster> StoreGroupMasterList = new List<StoreGroupMaster>();
          [DataMember]
        public int CountryID { get; set; }
          [DataMember]
        public string FormType  { get; set; }

      
    }
}
