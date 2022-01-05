using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.CardDetails
{

    [Serializable]
    [DataContract]
    public class SaveCardDetailsRequest : BaseRequestType
    {
        [DataMember]
        public InvoiceCardDetails InvoiceCardDetailsrData { get; set; }

        [DataMember]
        public List<InvoiceCardDetails> InvoiceCardList { get; set; }
        public List<PaymentProcessor> PaymentProcessorList { get; set; }
    }
}
