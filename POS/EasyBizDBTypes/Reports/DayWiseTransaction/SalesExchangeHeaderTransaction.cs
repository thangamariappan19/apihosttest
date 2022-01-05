using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
     public class SalesExchangeHeaderTransaction : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long InvoiceHeaderID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public String DocumentDate { get; set; }
        [DataMember]
        public string SalesInvoiceNumber { get; set; }
        [DataMember]
        public String SalesDate { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        //public List<SalesExchangeDetailTransaction> SalesExchangeDetailTransactionList { get; set; }
        public bool ExchangeWithOutInvoiceNo { get; set; }
        public int TotalExchangeQty { get; set; }
        public int CountryID { get; set; }
        public int StoreID { get; set; }
        public int PosID { get; set; }
        public string ExchangeMode { get; set; }
        public string StoreName { get; set; }
        public string POSName { get; set; }
        public string CountryName { get; set; }
        public int CashierID { get; set; }
        public string CountryCode { get; set; }
        public string StoreCode { get; set; }
        public string POSCode { get; set; }
        public string SalesManName { get; set; }
        public string CustomerName { get; set; }
    }
}
