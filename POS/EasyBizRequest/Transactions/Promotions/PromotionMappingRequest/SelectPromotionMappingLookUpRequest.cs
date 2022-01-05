using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionMappingRequest
{
    [DataContract]
    [Serializable]
   public class SelectPromotionMappingLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int WNPromotionID { get; set; }
        [DataMember]
        public int WNPromotionCode { get; set; }

    }
}
