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
    public class SelectByIDStorebrandmappingrespons : BaseResponseType
    {
        [DataMember]
        public List<StoreBrandMapping> StoreBrandMappingList { get; set; }
    }
}
