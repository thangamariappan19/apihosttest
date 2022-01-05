using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.WNPromotionResponse
{
    [DataContract]
    [Serializable]
    public class SelectWNPromotionDetailsResponse :BaseResponseType 
    {
        [DataMember]
        public List<WNPromotionDetails> WNPromotionDetailsList { get; set; }

        public WNPromotionDetails WNPriceData { get; set; }
    }
}
