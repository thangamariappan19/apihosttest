using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionCriteria
{
    [DataContract]
    [Serializable]
    public class SelectPromotionCriteriaRequest:BaseRequestType
    {
        [DataMember]
        public string PromotionCode { get; set; }
    }
}
