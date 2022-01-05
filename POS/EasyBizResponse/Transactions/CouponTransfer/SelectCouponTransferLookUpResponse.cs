using System;
using EasyBizDBTypes.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Transactions.Coupons;

namespace EasyBizResponse.Transactions.CouponTransfer
{
    [Serializable]
    [DataContract]
    public class SelectCouponTransferLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<CouponTransferMaster> CouponTransferList { get; set; }
    }
}
