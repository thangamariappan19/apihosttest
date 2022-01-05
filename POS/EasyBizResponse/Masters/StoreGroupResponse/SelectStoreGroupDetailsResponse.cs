using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreGroupResponse
{
    [Serializable]
    [DataContract]
     public class SelectStoreGroupDetailsResponse:BaseResponseType
    {

        [DataMember]
        public List<StoreGroupDetails> StoreGroupDetailsList { get; set; }
    }
}
