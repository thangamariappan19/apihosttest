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
    public class NewZReportDetails2
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public Decimal DiscountAmount { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public String Category { get; set; }
    }
}
