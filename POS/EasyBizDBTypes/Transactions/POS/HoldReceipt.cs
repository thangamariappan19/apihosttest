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
    public class HoldReceipt
    {
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public String ItemCode { get; set; }
        [DataMember]
        public String Cashier { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public String POSName { get; set; }
        [DataMember]
        public string Footer { get; set; }
        [DataMember]
        public String ArabicDetails { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
