using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.AllocationTypeResponse
{
    [Serializable]
    [DataContract]
    public class SelectAllocationTypeMasterLookUpResponse:BaseResponseType
    {
        [DataMember]
        public List<AllocationTypeMaster> AllocationTypeMasterList { get; set; }
    }
}
