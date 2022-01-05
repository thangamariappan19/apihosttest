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
    public class InvoiceReturnReceipt
    {
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public String TaxNo { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public String SalesMan { get; set; }
        [DataMember]
        public String CustomerName { get; set; }
        [DataMember]
        public String ItemCode { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public string SalesInvoice { get; set; }
        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public Decimal item_tax { get; set; }
        [DataMember]
        public string POSTitle { get; set; }
        [DataMember]
        public Decimal item_total { get; set; }
        [DataMember]
        public string Cashier { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal TaxAmount { get; set; }
        [DataMember]
        public string Footer { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public String ArabicDetails { get; set; }            
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
        [DataMember]
        public Decimal CustomerBalance { get; set; }
        [DataMember]
        public Decimal CASH { get; set; }
        [DataMember]
        public Decimal KNET { get; set; }
        [DataMember]
        public Decimal VISA { get; set; }
        [DataMember]
        public Decimal GrossAmt { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        
      
     
       
    }
}
