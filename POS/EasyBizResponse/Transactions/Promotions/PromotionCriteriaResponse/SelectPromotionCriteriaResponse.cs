using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse
{
    [DataContract]
    [Serializable]
    public class SelectPromotionCriteriaResponse:BaseResponseType
    {
        [DataMember]
        public List<PromotionCriteria> PromotionCriteriaList { get; set; }
    }
}
