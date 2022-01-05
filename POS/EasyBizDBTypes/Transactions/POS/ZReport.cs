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
   public class ZReport
    {
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public String ShopCode { get; set; }        
        [DataMember]
        public Decimal Netamount { get; set; }
        [DataMember]
        public String Country { get; set; }
        [DataMember]
        public Int32 CreditSale { get; set; }
        [DataMember]
        public Int32 DecimalPlaces { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public String Cashier { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public String ShiftName { get; set; }
        [DataMember]
        public Decimal FloatAmount { get; set; }
        [DataMember]
        public Decimal TotalCashInBox { get; set; }
        [DataMember]
        public Decimal CashInAmount { get; set; }
        [DataMember]
        public Decimal CashOutAmount { get; set; }                                                                
    }
}
