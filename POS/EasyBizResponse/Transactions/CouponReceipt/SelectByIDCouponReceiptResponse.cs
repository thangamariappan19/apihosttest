using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.CouponReceipt
{
    [Serializable]
    [DataContract]
    public class SelectByIDCouponReceiptResponse : BaseResponseType
    {
        [DataMember]
        public CouponReceiptHeader CouponReceiptHeaderRecord { get; set; }
    }
}
