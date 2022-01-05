using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
    public class PaymentDetail : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SlNo { get; set; }
        [DataMember]
        public long InvoiceHeaderID { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public int FromCountryID { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public int PayCurrencyID { get; set; }
        [DataMember]
        public string PayCurrency { get; set; }
        [DataMember]
        public string ChangeCurrency { get; set; }
        [DataMember]
        public int ChangeCurrencyID { get; set; }
        [DataMember]
        public Decimal BaseAmount { get; set; }
        [DataMember]
        public Decimal Receivedamount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public string CardNo { get; set; }
        [DataMember]
        public string CardHolder { get; set; }
        [DataMember]
        public string ApproveNo { get; set; }
        [DataMember]
        public Decimal BalanceAmountToBePay { get; set; }
        [DataMember]
        public Decimal OnAccountReceiveAmount { get; set; }
        [DataMember]
        public Decimal PendingAmount { get; set; }
        [DataMember]
        public bool OnAcPaymentCompleted { get; set; }
        [DataMember]
        public bool FromSalesOrder { get; set; }
        [DataMember]
        public bool IsPaymentProcesser { get; set; }
        [DataMember]
        public string CardType2 { get; set; }
    }

    public class PaymentProcessor : PaymentDetail
    {
        public string CardNumber { get; set; }
        public string RRN { get; set; }
        public string StatusCode { get; set; }
        public string HostErrorCode { get; set; }
        public string AuthCode { get; set; }
        public string CardSchemes { get; set; }
        public string Stan { get; set; }
        public string TerminalID { get; set; }
        public string DateAndTime { get; set; }
        public decimal Amount { get; set; }
        public string PosCode { get; set; }
        public string User { get; set; }
        public string CustomerMobileNumber { get; set; }
        public string Currency { get; set; }
        public Boolean process { get; set; }
        public List<PaymentProcessor> PaymentProcessorList { get; set; }
    }
}
