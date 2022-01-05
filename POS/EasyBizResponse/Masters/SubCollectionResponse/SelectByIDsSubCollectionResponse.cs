using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SubCollectionResponse
{

    [DataContract]
    [Serializable]
    public class SelectByIDsSubCollectionResponse:BaseResponseType
    {
        [DataMember]
        public List<SubCollectionMaster> SubCollectionMasterList  { get; set; }
    }
}
