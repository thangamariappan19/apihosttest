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
    public class FamilyDiscountDetailTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int DiscountHeaderID { get; set; }      
        [DataMember]
        public int BrandID { get; set; }      
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public Decimal DiscountValue { get; set; }
        public string StoreCode { get; set; }
    }
}
