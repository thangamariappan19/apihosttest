using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CouponTransfer
{
    [Serializable]
    [DataContract]
    public class SaveCouponTransferRequest : BaseRequestType
    {
        [DataMember]
        public CouponTransferMaster CouponTransferRecord { get; set; }
        [DataMember]
        public List<CouponReceiptDetails> CouponReceiptDetailsList { get; set; }
        [DataMember]
        public List<CouponTransferDetails> CouponTransferDetailsList { get; set; }
    }
}
