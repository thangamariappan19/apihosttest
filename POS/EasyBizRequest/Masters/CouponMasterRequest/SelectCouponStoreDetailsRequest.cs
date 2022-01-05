using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CouponMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectCouponStoreDetailsRequest:BaseRequestType
    {
        [DataMember]
        public int CouponID { get; set; }

        [DataMember]
        public string CouponStoreType { get; set; }

        
    }
}
