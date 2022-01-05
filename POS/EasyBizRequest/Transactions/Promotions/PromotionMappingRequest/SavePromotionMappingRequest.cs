using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionMappingRequest
{
    [DataContract]
    [Serializable]
    public class SavePromotionMappingRequest : BaseRequestType
    {
        [DataMember]
        public List<PromotionMappingTypes> PromotionMappingList { get; set; }
    }
}
