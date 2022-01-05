using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CouponMasterResponse
{
    public class SelectCouponProductCategoryDetailsResponse:BaseResponseType
    {


        [DataMember]
        public List<CommonUtil> ProductCategoryCommonUtil { get; set; }
    }
}
