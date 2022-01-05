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
    public class DeleteCollectionMasterResponse : BaseResponseType
    {
        [DataMember]
       public  int ID { get; set; }
    }
}
