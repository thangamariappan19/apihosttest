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
    public class SelectCouponStoreDetails:BaseRequestType
    {
        [DataMember]
        public int CouponID { get; set; }  
    }
}
