using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.CouponDetailRequest
{
    [DataContract]
    [Serializable]
   public class GetCouponMasterByCustomerListRequest:BaseRequestType
    {
        [DataMember]
        public string CouponCode { get; set; }
    }
}
