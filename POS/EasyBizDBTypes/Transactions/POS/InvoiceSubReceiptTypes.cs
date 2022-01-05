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
    public class InvoiceSubReceiptTypes
    {
        [DataMember]
        public Decimal CASH { get; set; }
        [DataMember]
        public Decimal KNET { get; set; }
        [DataMember]
        public Decimal VISA { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }
        [DataMember]
        public Decimal CREDITCARD { get; set; }
        [DataMember]
        public Decimal PaymentCash { get; set; }
        [DataMember]
        public String PaymentCurrency { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
    }
}
