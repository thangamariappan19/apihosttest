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
    public class SelectByIDSubCollectionResponse : BaseResponseType
    {
        [DataMember]
        public SubCollectionMaster SubCollectionMasterData { get; set; }
    }
}
