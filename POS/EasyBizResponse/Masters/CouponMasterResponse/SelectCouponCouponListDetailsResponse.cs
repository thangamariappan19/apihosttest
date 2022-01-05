using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CouponMasterResponse
{
    public class SelectCouponCouponListDetailsResponse:BaseResponseType
    {

        [DataMember]
        public CouponMaster CouponMasterListDetails { get; set; }
    }
}
