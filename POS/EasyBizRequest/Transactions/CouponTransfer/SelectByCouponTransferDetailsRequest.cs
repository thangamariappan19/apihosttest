using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CouponTransfer
{

    [DataContract]
    [Serializable]
    public class SelectByCouponTransferDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
