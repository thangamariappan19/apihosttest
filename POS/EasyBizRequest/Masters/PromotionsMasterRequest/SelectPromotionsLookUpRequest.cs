using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PromotionsMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectPromotionsLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<PromotionsMaster> PromotionsMasterList = new List<PromotionsMaster>();
    }
}
