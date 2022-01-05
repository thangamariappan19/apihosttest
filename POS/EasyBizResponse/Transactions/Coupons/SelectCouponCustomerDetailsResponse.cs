using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CouponMasterResponse
{
    public class SelectCouponCustomerDetailsResponse:BaseResponseType
    {

        [DataMember]
        public List<CustomerMaster> CustomerMasterData { get; set; }

        public List<CustomerGroupMaster> CustomerGroupMasterData { get; set; }

        [DataMember]
        public List<CommonUtil> CustomerCommonUtil { get; set; }
    }
}
