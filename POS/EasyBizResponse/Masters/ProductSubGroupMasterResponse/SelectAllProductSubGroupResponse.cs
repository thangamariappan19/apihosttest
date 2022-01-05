using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ProductSubGroupMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllProductSubGroupResponse : BaseResponseType
    {
        [DataMember]
        public List<ProductSubGroupMaster> ProductSubGroupList { get; set; }
    }
}
