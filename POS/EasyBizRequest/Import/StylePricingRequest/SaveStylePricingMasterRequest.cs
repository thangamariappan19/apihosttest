using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Import.StylePricingRequest
{
    [DataContract]
    [Serializable]
   public class SaveStylePricingMasterRequest:BaseRequestType
    {
        [DataMember]
        public List<EasyBizDBTypes.Transactions.Pricing.StylePricing> ImportStylePricingExcelList { get; set; }
    }
}
