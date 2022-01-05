using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PaymentDetails
{
    [DataContract]
    [Serializable]
    public class SelectByCashInCashOutIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
