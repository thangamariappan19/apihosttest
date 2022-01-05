using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CollectionMasterResponse
{
   public  class SelectCollectionLookUpResponse:BaseResponseType
    {
        [DataMember]
        public List<CollectionMasterTypes> CollectionMasterTypesList { get; set; }
    }
}
