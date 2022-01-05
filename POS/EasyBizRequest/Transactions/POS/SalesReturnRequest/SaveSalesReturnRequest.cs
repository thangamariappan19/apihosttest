using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Sales_Return
{
    [Serializable]
    [DataContract]
    public class SaveSalesReturnRequest : BaseRequestType
    {
        [DataMember]
        public SalesReturnHeader SalesReturnHeaderData { get; set; }
        [DataMember]
        public List<InvoiceDetails> SalesReturnDetailList { get; set; }

        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }

        [DataMember]
        public long RunningNo { get; set; }

        [DataMember]
        public long DocumentNumberingID { get; set; }

        [DataMember]
        public OnAccountPayment OnAccountPaymentRecord { get; set; }
        [DataMember]
        public List<PaymentDetail> SalesReturnPaymentdetails { get; set; }
    }
}
