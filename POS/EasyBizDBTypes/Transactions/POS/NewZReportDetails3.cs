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
    public  class NewZReportDetails3
    {
        [DataMember]
        public String ShiftID { get; set; }
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public Decimal CASH { get; set; }
        [DataMember]
        public Decimal KNET { get; set; }
        [DataMember]
        public Decimal VISA { get; set; }
        [DataMember]
        public Decimal CREDITCARD { get; set; }
        [DataMember]
        public Decimal PaymentCash { get; set; }
        [DataMember]
        public Int32 DecimalPlaces { get; set; }
        [DataMember]
        public String CurrencyName { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public Decimal TotalCash { get; set; }
        [DataMember]
        public Decimal ForeignTotal { get; set; }
        [DataMember]
        public String POSID { get; set; }
        [DataMember]
        public Decimal Credit { get; set; }
    }
}
