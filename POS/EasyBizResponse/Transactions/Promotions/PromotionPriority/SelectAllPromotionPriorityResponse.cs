using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.PromotionPriority
{

    [DataContract]
    [Serializable]
    public class SelectAllPromotionPriorityResponse:BaseResponseType
    {
        [DataMember]
        public List<PromotionPriorityType> PromotionPriorityTypeData { get; set; }
    }
}
