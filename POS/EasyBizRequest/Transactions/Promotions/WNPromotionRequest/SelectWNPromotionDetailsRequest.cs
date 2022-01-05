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
    public class SelectWNPromotionDetailsRequest :BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int WNPromotionID { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
    }
}
