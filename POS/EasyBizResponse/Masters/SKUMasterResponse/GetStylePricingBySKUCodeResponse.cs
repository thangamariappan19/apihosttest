using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterResponse
{
    [DataContract]
    [Serializable]
  public class GetStylePricingBySKUCodeResponse:BaseResponseType
    {
        [DataMember]
        public List<StylePricing> StylePricingList { get; set; }
        [DataMember]
        public StylePricing PriceRecord { get; set; }
    }
}
