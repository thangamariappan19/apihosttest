using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.OrderTypeMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectOrderTypeMasterLookUpResponse:BaseResponseType
    {
        [DataMember]
        public List<OrderTypeMaster> OrderTypeMasterList { get; set; }
    }
}
