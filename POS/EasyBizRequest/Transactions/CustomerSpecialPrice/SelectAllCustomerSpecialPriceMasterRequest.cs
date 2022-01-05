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
    public class SelectAllCustomerSpecialPriceMasterRequest : BaseRequestType
    {
        public String Source { get; set; }

        [DataMember]
        public String CustomerSpecialPriceMasterInfo { get; set; }
    }
}
