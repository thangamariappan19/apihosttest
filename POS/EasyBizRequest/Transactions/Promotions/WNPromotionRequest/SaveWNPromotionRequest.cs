using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.WNPromotionRequest
{
    [DataContract]
    [Serializable]
    public class SaveWNPromotionRequest :BaseRequestType
    {
        [DataMember]
        public WNPromotion WNPromotionData { get; set; }

        public int Mode { get; set; }
    }
}
