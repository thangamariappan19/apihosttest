using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{
    [Serializable]
    [DataContract]
   public class PromotionMappingTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long WNPromotionID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }

        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public List<PromotionMappingTypes> PromotionMappingList { get; set; }

        [DataMember]
        public string WNPromotionCode { get; set; }

    }
}
