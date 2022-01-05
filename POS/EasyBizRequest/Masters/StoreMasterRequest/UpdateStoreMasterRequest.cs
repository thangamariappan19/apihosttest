using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StoreMasterRequest
{


    [DataContract]
    [Serializable]
    public class UpdateStoreMasterRequest:BaseRequestType
    {
        [DataMember]
        public StoreMaster StoreMasterRecord { get; set; }
        [
        DataMember]
        public List<StoreMaster> StoreImageList { get; set; }

        [DataMember]
        public List<StoreBrandMapping> StoreBrandMappingList { get; set; }

    }
}
