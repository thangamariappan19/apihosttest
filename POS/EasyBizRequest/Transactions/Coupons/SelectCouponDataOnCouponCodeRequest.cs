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
    public class SelectCouponDataOnCouponCodeRequest : BaseRequestType
    {
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public bool ReturnUpdate { get; set; }
    }
}
