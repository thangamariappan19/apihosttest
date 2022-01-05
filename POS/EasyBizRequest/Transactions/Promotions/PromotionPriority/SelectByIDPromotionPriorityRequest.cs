using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionPriority
{
    [Serializable]
    [DataContract]
    public class SelectByIDPromotionPriorityRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
