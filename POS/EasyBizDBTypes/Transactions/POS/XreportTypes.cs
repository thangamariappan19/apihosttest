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
    public class XreportTypes
    {
        [DataMember]
        public Decimal SubTotal { get; set; }
        [DataMember]
        public Decimal DiscountTotal { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Int32 CreditSale { get; set; }
        [DataMember]
        public bool IsReturn { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public String Cashier { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public String PosName { get; set; }
        [DataMember]
        public String ShiftName { get; set; }
        [DataMember]
        public Decimal NetSales { get; set; }
        [DataMember]
        public Decimal FloatAmount { get; set; }
        [DataMember]
        public Decimal TotalCashInBox { get; set; }
        [DataMember]
        public Decimal CashInAmount { get; set; }
        [DataMember]
        public Decimal CashOutAmount { get; set; }
        [DataMember]
        public Decimal IsReturn1 { get; set; }
        [DataMember]
        public Int32 DecimalPlaces { get; set; }                                                
    }
}
