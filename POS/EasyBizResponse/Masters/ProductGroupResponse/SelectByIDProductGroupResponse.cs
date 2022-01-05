using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ProductGroupResponse
{

    [Serializable]
    [DataContract]
   public class SelectByIDProductGroupResponse:BaseResponseType
    {
        [DataMember]
        public ProductGroupMaster ProductGroupData { get; set; }
    }
}
