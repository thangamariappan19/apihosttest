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
    public class SaveStoreGroupRequest:BaseRequestType
    {
        [DataMember]
        public StoreGroupMaster StoreGroupMasterData { get; set; }
         [DataMember]
        public List<StoreGroupDetails> StoreGroupDetailsList { get; set; }
    }
}
