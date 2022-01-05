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
    public class Zreport2
    {
        [DataMember]
        public Decimal SubTotal { get; set; }
        [DataMember]
        public Decimal DiscountTotal { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Int32 CreditSale { get; set; }
        [DataMember]
        public Boolean IsReturn { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public string Cashier { get; set; }
        [DataMember]
        public string ShopName { get; set; }
        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public string ShiftName { get; set; }
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
        public Int32 IsReturn1 { get; set; }
        [DataMember]
        public Int32 DecimalPlaces { get; set; }
        [DataMember]
        public Decimal CASH { get; set; }
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public Decimal Debit { get; set; }
        [DataMember]
        public Decimal Credit { get; set; }
    }
}
