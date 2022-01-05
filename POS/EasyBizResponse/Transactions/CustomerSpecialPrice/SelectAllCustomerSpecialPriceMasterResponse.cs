using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllCustomerSpecialPriceMasterResponse : BaseResponseType
    {
        public List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterTypesList { get; set; }
        [DataMember]

        public List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterdata { get; set; }
    }
}
