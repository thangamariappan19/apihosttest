using EasyBizDBTypes.Transactions.Promotion;
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
    public class UpdatePromotionMappingRequest : BaseRequestType
    {
        [DataMember]

        public PromotionMappingTypes PromotionMappingData { get; set; }
    }
}
