using EasyBizDBTypes.Common;
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
   public class SalesExchangeDetail:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SalesExchangeID { get; set; }
        [DataMember]
        public string SalesInvoiceNumber { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]      
        public String ModifiedSalesEmployee { get; set; }
        [DataMember]
        public String ModifiedSalesManager { get; set; }
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
        public int CountryID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int PosID { get; set; }     
       
        [DataMember]
        public int ExchangeQty { get; set; }
        [DataMember]
        public int ExchangedQty { get; set; } // Exisiting Exchange Qty
        [DataMember]
        public int InvoiceDetailID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public bool IsExchange { get; set; }
        [DataMember]
        public string ExchangeRemarks { get; set; }
        [DataMember]
        public string ExchangedSKU { get; set; }
        [DataMember]
        public bool IsExchanged { get; set; }
        [DataMember]
        public bool EnableCell { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public Decimal SellingPricePerQty { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public int NewExchangeQty { get; set; }
        [DataMember]
        public bool IsReturned { get; set; }
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public int InvoiceSerialNo { get; set; }
        [DataMember]
        public string InvoiceType { get; set; }
        [DataMember]
        public int TaxID { get; set; }
        [DataMember]
        public Decimal TaxAmount { get; set; }
        [DataMember]
        public string ExchangeSKU { get; set; }
        [DataMember]
        public string Tag_Id { get; set; }
        [DataMember]
        public bool CreditSales { get; set; }
    }
}
