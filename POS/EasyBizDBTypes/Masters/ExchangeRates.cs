using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class ExchangeRates : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ExchangeRatesCode { get; set; }
        [DataMember]
        public string ExchangeRatesName { get; set; }
        [DataMember]
        public int BaseCurrencyID { get; set; }

        [DataMember]
        public string BaseCurrency { get; set; }

        [DataMember]
        public int TargetCurrencyID { get; set; }

        [DataMember]
        public string TargetCurrency { get; set; }

        [DataMember]
        public DateTime ExchangeRateDate { get; set; }

        [DataMember]
        public Decimal ExchangeAmount { get; set; }
        [DataMember]
        public List<ExchangeRates> ExchangeRateslist { get; set; }
    }
}
