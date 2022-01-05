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
    public class SelectWNPromotionByIDRequest :BaseRequestType 
    {
        [DataMember]
        public int ID { get; set; }
    }
}
