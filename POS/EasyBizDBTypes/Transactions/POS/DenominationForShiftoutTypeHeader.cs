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
    public class DenominationForShiftoutTypeHeader
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ShifLogId  { get; set; }
        [DataMember]
        public String StoreCode   { get; set; }
        [DataMember]
        public String POSCode  { get; set; }
        [DataMember]
        public String ShiftCode  { get; set; }
        [DataMember]
        public Decimal ShiftInAmount  { get; set; }
        [DataMember]
        public Decimal ShiftOutAmount  { get; set; }
        [DataMember]
        public String remarks { get; set; }
        [DataMember]
        public Decimal TotalValueCount { get; set; }
        [DataMember]
        public Decimal TotalCardValue { get; set; }
        [DataMember]
        public Decimal GrandTotalValue { get; set; }

    }
}
