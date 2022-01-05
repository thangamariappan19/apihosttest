using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
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
    public class InvoiceHeader : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public String SalesStatus { get; set; }
        [DataMember]
        public int AppliedPriceListID { get; set; }
        [DataMember]
        public int AppliedCustomerSpecialPriceID { get; set; }
        [DataMember]
        public int AppliedPromotionID { get; set; }
        [DataMember]
        public bool IsDataSyncToCountryServer { get; set; }
        [DataMember]
        public bool IsDataSyncToMainServer { get; set; }
        [DataMember]
        public DateTime CountryServerSyncTime { get; set; }
        [DataMember]
        public DateTime MainServerSyncTime { get; set; }
        [DataMember]
        public string SyncFailedReason { get; set; }
        [DataMember]
        public Decimal TaxAmount { get; set; }
        [DataMember]
        public int TaxID { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string TotalDiscountType { get; set; }
        [DataMember]
        public Decimal TotalDiscountAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscountPercentage { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public int CustomerGroupID { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public Decimal SubTotalAmount { get; set; }



        [DataMember]
        public Decimal SubTotalWithTaxAmount { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }

        [DataMember]
        public int SalesEmployeeID { get; set; }
        [DataMember]
        public string SalesEmployeeName { get; set; }
        [DataMember]
        public int SalesManagerID { get; set; }
        [DataMember]
        public bool UnHold { get; set; }
        [DataMember]
        public string RefNumber { get; set; }
        [DataMember]
        public List<InvoiceDetails> InvoiceDetailList { get; set; }
        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        //[DataMember]
        //public string CustomerName { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string SalesEmployeeCode { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public int CashierID { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }
        [DataMember]
        public List<PaymentProcessor> PaymentProcessorList { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string PosCode { get; set; }

        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public string DiscountRemarks { get; set; }

        [DataMember]
        public Decimal BeforeRoundOffAmount { get; set; }


        [DataMember]
        public Decimal RoundOffAmount { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public bool IsCreditSale { get; set; }
        [DataMember]
        public string Mode { get; set; }
        //List<PaymentDetail> PayList { get; set; }

        //// New field added by Senthamil selvan @ 04.09.2018
        //[DataMember]
        //public Decimal SubTotalWithOutDiscount { get; set; }        


        // "Senthamil_Changes"
        [DataMember]
        public int CouponID { get; set; }
        [DataMember]
        public string RedeemCouponCode { get; set; }
        [DataMember]
        public int RedeemCouponLineNo { get; set; }
        [DataMember]
        public string RedeemCouponSerialCode { get; set; }
        [DataMember]
        public string RedeemCouponDiscountType { get; set; }
        [DataMember]
        public decimal RedeemCouponDiscountValue { get; set; }
        [DataMember]
        public decimal RedeemValue { get; set; }
        [DataMember]
        public string IssuedCouponCode { get; set; }
        [DataMember]
        public string IssuedCouponLineNo { get; set; }
        [DataMember]
        public string IssuedCouponSerialCode { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerMobileNo { get; set; }

    }
}
