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
    public class CouponReceiptHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CouponID { get; set; }
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public string CurrentLocation { get; set; }
        [DataMember]
        public string FromSerialNum { get; set; }
        [DataMember]
        public string ToSerialNum { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public List<CouponReceiptDetails> CouponReceiptDetailsList { get; set; }
    }
}
