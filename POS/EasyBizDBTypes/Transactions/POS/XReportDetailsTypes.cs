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
    public class XReportDetailsTypes
    {
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public Decimal DiscountTotal { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public Int32 DecimalPlaces { get; set; }
    }
}
