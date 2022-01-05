using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CollectionMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectAllCollectionMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<CollectionMasterTypes> CollectionMasterTypesList { get; set; }
    }
}
