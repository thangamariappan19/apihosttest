using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CouponMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectCouponMasterLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<CouponMaster> CouponMasterList = new List<CouponMaster>();
    }
}
