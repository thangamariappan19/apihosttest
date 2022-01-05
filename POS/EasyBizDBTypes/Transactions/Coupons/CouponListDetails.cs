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
    public class CouponListDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CouponListHeaderID { get; set; }
        [DataMember]
        public string CouponSerialCode { get; set; }
        [DataMember]
        public string IssuedStatus { get; set; }
        [DataMember]
        public string PhysicalStore { get; set; }
        [DataMember]
        public string RemainingAmount { get; set; }
        [DataMember]
        public string RedeemedStatus { get; set; }
        [DataMember]
        public bool IsSaved { get; set; }
        [DataMember]
        public string ToStore { get; set; }
        //[DataMember]
        //public int CouponTransferHeaderID { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public int RedeemCount { get; set; }
        [DataMember]
        public DateTime ExpiredDate { get; set; }
        [DataMember]
        public string CouponHeaderCode { get; set; }
        [DataMember]
        public bool Active { get; set; }

    }
}
