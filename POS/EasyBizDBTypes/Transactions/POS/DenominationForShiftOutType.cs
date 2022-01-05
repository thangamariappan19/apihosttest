using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
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
    public class DenominationForShiftOutType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public String CurrencyCode { get; set; }
        [DataMember]
        public decimal CurrencyValue { get; set; }
        [DataMember]
        public String PaymentType { get; set; }
        [DataMember]
        public decimal PaymemtValue { get; set; }
        [DataMember]
        public String CardType { get; set; }
        [DataMember]
        public decimal TotalValue { get; set; }
        [DataMember]
        public int ValueCount { get; set; }
        [DataMember]
        public List<PaymentTypeMasterType> LookUpListForDenomination { get; set; }

    }   
}
