using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ProductLineMasterRequest
{
    [Serializable]
    [DataContract]  
    public class UpdateProductLineMasterRequest: BaseRequestType
    {
        [DataMember]
        public ProductLineMaster ProductLineMasterData { get; set; }
    }
}
