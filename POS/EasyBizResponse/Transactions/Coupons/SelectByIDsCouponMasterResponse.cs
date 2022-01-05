using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Coupons
{

    [Serializable]
    [DataContract]
    public class SelectByIDsCouponMasterResponse:BaseResponseType
    {
        [DataMember]
        public List<CouponMaster> CouponMasterList { get; set; }
    }
}
