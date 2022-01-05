using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.WNPromotionResponse
{
   public class SelectWNPromotionLookUpResponse : BaseResponseType
    {
       [DataMember]
       public List<WNPromotion> WNPromotionList { get; set; }
    }
}
