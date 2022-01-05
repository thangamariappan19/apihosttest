using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionsMasterRequest
{

    [DataContract]
    [Serializable]
   public class SelectByPromotionsIDsRequest:BaseRequestType
    {
        [DataMember]
        public string IDs { get; set; } 
    }
}
