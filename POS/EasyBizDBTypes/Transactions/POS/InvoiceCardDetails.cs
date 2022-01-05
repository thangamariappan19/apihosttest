using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    public class InvoiceCardDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime ApplicationDate {get;set;}
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }       
        [DataMember]
        public string CardType { get; set; } 
        [DataMember]
        public Decimal ReceivedAmount { get; set; }       
        [DataMember]
        public string PaymentCurrency { get; set; }       
        [DataMember]
        public Decimal AmountToBePay { get; set; }
        [DataMember]
        public Decimal BalanceAmountToBePay { get; set; }
        [DataMember]
        public int PaymentCurrencyID { get; set; }
        [DataMember]
        public int ChangeCurrencyID { get; set; }
        public Decimal ReturnAmount { get; set; }
        public string ApprovalNumber { get; set; }
        public bool PaymentProcesser { get; set; }
        public string CardType2 { get; set; }
        
    }
    /*public class PaymentProcessor
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
        public List<PaymentProcessor> PaymentProcessorList { get; set; }
    }*/
}
