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
    public class SelectCouponTransferLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<CouponTransferMaster> CouponMasterList = new List<CouponTransferMaster>();
    }
}
