using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CouponMasterRequest
{
    public class SelectCouponCustomerDetailsRequest:BaseRequestType
    {

        [DataMember]
        public int CouponID { get; set; }

        [DataMember]
        public string CouponCustomerType { get; set; } 
    }
}
