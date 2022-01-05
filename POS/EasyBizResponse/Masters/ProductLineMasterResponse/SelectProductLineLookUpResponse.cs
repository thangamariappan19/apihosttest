using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ProductLineMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectProductLineLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<ProductLineMaster> ProductLineMasterList { get; set; }
    }
}
