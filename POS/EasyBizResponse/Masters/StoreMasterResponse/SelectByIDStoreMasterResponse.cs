using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectByIDStoreMasterResponse:BaseResponseType
    {

        [DataMember]
        public StoreMaster StoreMasterData { get; set; }
        [DataMember]
        public List<StoreMaster> StoreImageList { get; set; }

        [DataMember]
        public List<StoreMaster> StoreList { get; set; }
    }
}
