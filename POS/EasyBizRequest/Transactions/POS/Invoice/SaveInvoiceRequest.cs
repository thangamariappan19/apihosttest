using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    [Serializable]
    [DataContract]
    public class SaveInvoiceRequest : BaseRequestType
    {
        [DataMember]
        public InvoiceHeader InvoiceHeaderData { get; set; }
        [DataMember]
        public List<InvoiceDetails> InvoiceDetailList { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }

        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }

        [DataMember]
        public long RunningNo { get; set; }

        [DataMember]
        public long DocumentNumberingID { get; set; }

        public string SalesOrderDocumentNo { get; set; }

        public List<PaymentProcessor> PaymentProcessorList { get; set; }
    }
}
