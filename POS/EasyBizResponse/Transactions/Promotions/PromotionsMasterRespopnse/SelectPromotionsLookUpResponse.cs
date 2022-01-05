using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PromotionsMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectPromotionsLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<PromotionsMaster> PromotionsMasterList { get; set; }
    }
}
