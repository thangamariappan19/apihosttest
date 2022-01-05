using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PriceListResponse
{
    [DataContract]
    [Serializable]
    public class SelectSalePriceListLookupResponse : BaseResponseType
    {
        [DataMember]

        public List<StylePricing> SalePriceListTypeData { get; set; }
    }
}
