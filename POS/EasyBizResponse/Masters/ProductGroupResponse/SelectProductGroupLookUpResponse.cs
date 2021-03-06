using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ProductGroupResponse
{
    [DataContract]
    [Serializable]
    public class SelectProductGroupLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<ProductGroupMaster> ProductGroupList { get; set; }
    }
}
