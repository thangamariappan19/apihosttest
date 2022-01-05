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
    public class SelectByIDCouponMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }


        public String Source { get; set; }

        [DataMember]
        public String PromotionCode { get; set; }
    }
}
