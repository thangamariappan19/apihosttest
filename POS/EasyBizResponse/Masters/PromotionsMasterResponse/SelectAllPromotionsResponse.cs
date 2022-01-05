using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PromotionsMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllPromotionsResponse : BaseResponseType
    {
        [DataMember]
        public List<PromotionsMaster> PromotionsList { get; set; }
    }
}
