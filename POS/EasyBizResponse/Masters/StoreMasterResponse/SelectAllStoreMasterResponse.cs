using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreMasterResponse
{

    [Serializable]
    [DataContract]
    public class SelectAllStoreMasterResponse:BaseResponseType
    {

        [DataMember]
        public List<StoreMaster> StoreMasterList { get; set; }
        public List<StoreBrandMapping> SelectALLStoreBrandMappingList { get; set; }
    }
}
