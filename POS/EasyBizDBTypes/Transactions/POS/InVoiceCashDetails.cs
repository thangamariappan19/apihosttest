using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [Serializable]
    [DataContract]
    public class InVoiceCashDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public int FromCountryID { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        [DataMember]
        public Decimal AmountToBePay { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmountToBePay { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }            
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public string ChangeCurrency { get; set; }
        [DataMember]
        public int PaymentCurrencyID { get; set; }
        [DataMember]
        public int ChangeCurrencyID { get; set; }
        [DataMember]
        public int BaseAmount { get; set; }
    }
    
}
