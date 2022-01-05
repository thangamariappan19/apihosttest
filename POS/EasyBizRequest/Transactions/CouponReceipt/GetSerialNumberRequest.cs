using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CouponReceipt
{
    [DataContract]
    [Serializable]
    public class GetSerialNumberRequest : BaseRequestType
    {
        [DataMember]
        public int CouponID { get; set; }
        [DataMember]
        public String FromSerialNum { get; set; }
        [DataMember]
        public String ToSerialNum { get; set; }
        [DataMember]
        public String CouponCode { get; set; }
    }
}
