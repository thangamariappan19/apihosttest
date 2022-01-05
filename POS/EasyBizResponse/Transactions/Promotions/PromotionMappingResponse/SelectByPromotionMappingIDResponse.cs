using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.PromotionMappingResponse
{
    [DataContract]
    [Serializable]
   public class SelectByPromotionMappingIDResponse : BaseResponseType
    {
        [DataMember]

        public PromotionMappingTypes PromotionMappingRecord { get; set; }
    }
}
