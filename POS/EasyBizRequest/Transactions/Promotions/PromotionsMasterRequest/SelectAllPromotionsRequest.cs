using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PromotionsMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectAllPromotionsRequest : BaseRequestType
    {
        [DataMember]
        public String PromotionCode { get; set; }
        [DataMember]
        public string RequestedProcess { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
