using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CouponMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDCouponMasterResponse : BaseResponseType
    {
        [DataMember]
        public CouponMaster CouponMasterRecord { get; set; }
    }
}
