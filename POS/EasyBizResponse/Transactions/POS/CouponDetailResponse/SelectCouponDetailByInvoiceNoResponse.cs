using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.CouponDetailResponse
{
    [DataContract]
    [Serializable]
   public class SelectCouponDetailByInvoiceNoResponse:BaseResponseType
    {
        [DataMember]
        public CouponDetail CouponDetailData { get; set; }
    }
}
