using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CouponMasterRequest
{
    public class SelectCouponCouponListDetailsRequest:BaseRequestType
    {

        [DataMember]
        public int CouponID { get; set; }
    }
}
