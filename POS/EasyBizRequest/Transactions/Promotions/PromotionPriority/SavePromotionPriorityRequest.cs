using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionPriority
{

    [DataContract]
    [Serializable]
    public class SavePromotionPriorityRequest:BaseRequestType
    {
        [DataMember]
        public PromotionPriorityType PromotionPriorityType { get; set; }

        [DataMember]
        public List<PromotionPriorityType> PromotionPriorityTypeData { get; set; }
    }
}
