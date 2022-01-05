using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.CouponTransfer
{
    [Serializable]
    [DataContract]
    public class SelectByIDCouponTransferResponse : BaseResponseType
    {
        [DataMember]
        public CouponTransferMaster CouponTransferMasterRecord { get; set; }
    }
}
