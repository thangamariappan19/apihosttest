using EasyBizDBTypes.Common;
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
    public class UpdateCouponMasterRequest : BaseRequestType
    {
        [DataMember]
        public CouponMaster CouponMasterData { get; set; }

        [DataMember]
        public List<CommonUtil> StoreCommonUtilData { get; set; }

        [DataMember]
        public List<CommonUtil> CustomerCommonUtilData { get; set; }

        [DataMember]
        public List<CommonUtil> TotalMasterCommonUtilData { get; set; }


        
    }
}
