using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.CustomerSpecialPrice
{
    [DataContract]
    [Serializable]
    public class SelectByIDsCustomerSpecialPriceDetailsResponse : BaseResponseType
    {
        [DataMember]

        public List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterList = new List<CustomerSpecialPriceMasterTypes>();
    }
}
