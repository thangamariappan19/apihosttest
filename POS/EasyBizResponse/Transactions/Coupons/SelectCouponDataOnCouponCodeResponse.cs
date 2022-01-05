using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Coupons
{
    [DataContract]
    [Serializable]
    public class SelectCouponDataOnCouponCodeResponse : BaseResponseType
    {
        [DataMember]
        public CouponMaster CouponMasterRecord { get; set; }
    }
}
