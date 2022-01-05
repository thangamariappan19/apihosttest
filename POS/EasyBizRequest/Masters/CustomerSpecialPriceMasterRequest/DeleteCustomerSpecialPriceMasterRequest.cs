using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest
{
    [Serializable]
    [DataContract]
    public class DeleteCustomerSpecialPriceMasterRequest : BaseRequestType
    {
        public long ID { get; set; }
    }
}
