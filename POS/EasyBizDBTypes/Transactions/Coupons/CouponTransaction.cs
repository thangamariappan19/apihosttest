using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Coupons
{
    [DataContract]
    [Serializable]
    public class CouponTransaction : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CouponSerialCode { get; set; }
        [DataMember]
        public bool IssuedStatus { get; set; }
        [DataMember]
        public string PhysicalStore { get; set; }
        [DataMember]
        public string RemainingAmount { get; set; }
        [DataMember]
        public bool RedeemedStatus { get; set; }
        [DataMember]
        public bool IsSaved { get; set; }
        [DataMember]
        public string ToStore { get; set; }
        [DataMember]
        public int CouponID { get; set; }
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public string FromLocation { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        [DataMember]
        public int DocumentID { get; set; }

        public List<CouponTransaction> CouponTransactionList { get; set; }
    }
}
