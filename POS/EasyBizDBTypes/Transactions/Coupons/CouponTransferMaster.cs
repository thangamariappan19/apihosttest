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
    public class CouponTransferMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CouponID { get; set; }
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public int FromCountryID { get; set; }
        [DataMember]
        public String FromCountryCode { get; set; }
        [DataMember]
        public string ToLocation { get; set; }
        [DataMember]
        public string FromSerialNum { get; set; }
        [DataMember]
        public string ToSerialNum { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public int ToStoreID { get; set; }
        [DataMember]
        public String ToStoreCode { get; set; }
        [DataMember]
        public String Fromloaction { get; set; }
        [DataMember]
        public List<CouponReceiptDetails> CouponTransferDetailsList { get; set; }

    }
}
