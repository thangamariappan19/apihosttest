using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.CardDetails
{
    [DataContract]
    [Serializable]
   public class SelectCreditCardDetailsByInvoiceNoRequest:BaseRequestType
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string CardType { get; set; }

        [DataMember]
        public long InvoiceHeaderID { get; set; }
    }
}
