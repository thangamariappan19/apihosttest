using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.SalesExchange
{
    [DataContract]
    [Serializable]
   public class SalesExchangeHeader:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long InvoiceHeaderID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string SalesInvoiceNumber { get; set; }
        [DataMember]
        public DateTime SalesDate { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public List<SalesExchangeDetail> SalesExchangeDetailList { get; set; }
        [DataMember]
        public bool ExchangeWithOutInvoiceNo { get; set; }
        [DataMember]
        public int TotalExchangeQty { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public string ExchangeMode { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string POSName { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public int CashierID { get; set; }
        [DataMember]
        public bool CreditSales { get; set; }
        [DataMember]
        public List<SalesExchangeDetail> ReturnExchangeDetailList { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DetailID { get; set; }
    }
}
