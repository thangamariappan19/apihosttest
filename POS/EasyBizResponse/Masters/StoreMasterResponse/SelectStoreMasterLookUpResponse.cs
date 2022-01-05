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
    public class SelectStoreMasterLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<StoreMaster> StoreMasterList { get; set; }
        [DataMember]
        public List<StoreMaster> ToStoreMasterList { get; set; }       
        [DataMember]
         public StoreMaster StoreMasterData { get; set; }
    }
}
