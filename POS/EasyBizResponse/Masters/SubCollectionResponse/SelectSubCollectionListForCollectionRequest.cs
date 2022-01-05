using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubCollectionResponse
{
    [DataContract]
    [Serializable]
    public class SelectSubCollectionListForCollectionResponse : BaseResponseType
    {
        
        [DataMember]
        public List<SubCollectionMaster> SubCollectionMasterList { get; set; }
        public SubCollectionMaster SubCollectionMasterRecord { get; set; }
    }
}
