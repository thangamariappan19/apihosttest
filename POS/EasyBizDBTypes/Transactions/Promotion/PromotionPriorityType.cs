using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{

    [DataContract]
    [Serializable]
    public class PromotionPriorityType:BaseType
    {

        [DataMember]
        public int ID { get; set; }
       
        [DataMember]
        public int PromotionID { get; set; }

        [DataMember]
        public string PromotionName { get; set; }
        [DataMember]
        public string PriceListCode { get; set; }
        [DataMember]
        public string PromotionCode { get; set; }

        [DataMember]
        public int PriorityNo { get; set; }
        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public List<PromotionPriorityType> PromotionPriorityTypeData { get; set; }

      
    
    }
}
