using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
   public class CouponDetail:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public DateTime ValidityStartDate { get; set; }
        [DataMember]
        public DateTime ValidityEndDate { get; set; }

        [DataMember]
        public string StoreGroupCode { get; set; }
        [DataMember]
        public string DiscountType { get; set; }
        [DataMember]
        public Decimal Discountvalue { get; set; }
        [DataMember]
        public string PayMentMode { get; set; }
    }
}
