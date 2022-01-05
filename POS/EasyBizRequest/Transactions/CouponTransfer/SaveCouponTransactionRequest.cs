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
    public class SaveCouponTransactionRequest : BaseRequestType
    {
        [DataMember]
        public List<CouponTransaction> CouponTransactionList { get; set; }
    }
}
