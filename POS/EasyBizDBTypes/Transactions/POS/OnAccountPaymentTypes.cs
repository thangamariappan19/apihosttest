using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    public class OnAccountPayment : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public Decimal BillingAmount { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public DateTime PaymentDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public List<OnAccountPaymentDetails> OnAccountPaymentDetailsList { get; set; }
        [DataMember]
        public List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList { get; set; }
    }
    public class OnAccountPaymentDetails
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int OnAccountPaymentID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public string ChangeCurrency { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }
        [DataMember]
        public string ApprovalNumber { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
    public class OnAcInvoiceWisePayment
    {
        public int ID { get; set; }
        [DataMember]
        public int OnAccountPaymentID { get; set; }
        [DataMember]
        public int SlNo { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public int PurchaseStoreID { get; set; }
        [DataMember]
        public string PurchaseStoreCode { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public Decimal BillAmount { get; set; }
        [DataMember]
        public Decimal CashPaid { get; set; }
        [DataMember]
        public Decimal CardPaid { get; set; }
        [DataMember]
        public Decimal TotalPaid { get; set; }
        [DataMember]
        public Decimal PendingAmount { get; set; }
        [DataMember]
        public bool IsSelect { get; set; }
        [DataMember]
        public bool CloseBill { get; set; }
        [DataMember]
        public Decimal DiscountAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}

