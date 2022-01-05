using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    public class ReceivedDenomination : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string DenominationType { get; set; }
        [DataMember]
        public Decimal DenominationValue { get; set; }
        [DataMember]
        public int DenominationNumber { get; set; }
        [DataMember]
        public Decimal TotalDenominationValue { get; set; }
    }
}
