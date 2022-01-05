using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CouponReceipt
{
    [Serializable]
    [DataContract]
    public class SaveCouponReceiptRequest : BaseRequestType
    {
        [DataMember]
        public CouponReceiptHeader CouponReceiptHeaderRecord { get; set; }
        [DataMember]
        public List<CouponReceiptDetails> CouponReceiptDetailsList { get; set; }
    }
}
