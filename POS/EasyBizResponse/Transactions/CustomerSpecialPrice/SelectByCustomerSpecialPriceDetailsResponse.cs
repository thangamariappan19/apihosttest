using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByCustomerSpecialPriceDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterRecord { get; set; }

        public List<StoreGroupMaster> CustomerSpecialStoreRecord { get; set; }
        public List<CustomerMaster> CustomerSpecialCustomerRecord { get; set; }
    }
}
