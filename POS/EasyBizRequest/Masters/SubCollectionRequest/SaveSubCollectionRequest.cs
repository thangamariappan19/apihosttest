using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubCollectionRequest
{

    [DataContract]
    [Serializable]
    public class SaveSubCollectionRequest : BaseRequestType
    {
        [DataMember]

        public SubCollectionMaster SubCollectionMasterData { get; set; }

        [DataMember]
        public List<SubCollectionMaster> SubCollectionMasterlist { get; set; }

    }
}
