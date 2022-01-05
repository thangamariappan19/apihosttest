using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.PromotionsMasterRespopnse
{


    [DataContract]
    [Serializable]
    public class SelectByPromotionsIDsResponse:BaseResponseType
    {
        [DataMember]
        public List<PromotionsMaster> PromotionsList { get; set; }
    }
}
