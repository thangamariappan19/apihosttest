using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Coupons
{
    [DataContract]
    [Serializable]
    public class UpdateCouponDetailsListRequest : BaseRequestType
    {
        [DataMember]
        public CouponListDetails CouponListDetailsReq { get; set; }

    }
}
