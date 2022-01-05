using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.OnAccountPaymentRequest
{
    [DataContract]
    [Serializable]
    public class GetOnAccountPaymentDetailsRequest : BaseRequestType
    {
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public string SearchString { get; set; }
    }
}
