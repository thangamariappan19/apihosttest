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
    public class SelectAllCouponMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<CouponMaster> CouponMasterList { get; set; }
    }
}
