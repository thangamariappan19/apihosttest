using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.SalesReturn
{
    [DataContract]
    [Serializable]
    public class SalesReturnHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long InvoiceHeaderID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }

        [DataMember]
        public string DocumentDate_str { get; set; }

        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string SalesInvoiceNumber { get; set; }

        [DataMember]
        public string SalesDate_str { get; set; }

        [DataMember]
        public DateTime SalesDate { get; set; }

        [DataMember]
        public string ApplicationDate_str { get; set; }

        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public int TotalReturnQty { get; set; }
        [DataMember]
        public Decimal TotalReturnAmount { get; set; }
        [DataMember]
        public string PaymentMode { get; set; }
        [DataMember]
        public List<SalesReturnDetail> SalesReturnDetailList { get; set; }
        [DataMember]
        public bool ReturnWithOutInvoiceNo { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public string ReturnMode { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public int CashierID { get; set; }
        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string PosCode { get; set; }
        [DataMember]
        public int TaxID { get; set; }
        [DataMember]
        public Decimal TotalTaxAmount { get; set; }
        [DataMember]
        public bool CreditSales { get; set; }
        [DataMember]
        public OnAccountPayment OnAccountPaymentRecord { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DetailID { get; set; }
        [DataMember]
        public List<PaymentDetail> SalesReturnPaymentdetails { get; set; }

    }
}
