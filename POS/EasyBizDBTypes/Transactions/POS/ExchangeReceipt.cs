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
    public class ExchangeReceipt
    {
        [DataMember]
        public String Currency { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public String SKUCode { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public String SalesInvoice { get; set; }
        [DataMember]
        public String POSName { get; set; }
        [DataMember]
        public String Cashier { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Footer { get; set; }
        [DataMember]
        public String ArabicDetails { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
        [DataMember]
        public String SalesInvoice1 { get; set; }
        [DataMember]
        public String CustomerName { get; set; }
    }
}
