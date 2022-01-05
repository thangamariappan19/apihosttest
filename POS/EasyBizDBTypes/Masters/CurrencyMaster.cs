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
    public class CurrencyMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public string InternationalCode { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }

        [DataMember]
        public string CurrencyType { get; set; }

        [DataMember]
        public Decimal MRoundValue { get; set; }
        [DataMember]
        public string CurrencySymbol { get; set; }

        [DataMember]
        public string InterDescription { get; set; }

        [DataMember]
        public string HundredthName { get; set; }

        [DataMember]
        public string English { get; set; }

        [DataMember]
        public string EngHundredthName { get; set; }

        [DataMember]
        public bool IsBaseCurrency { get; set; }
		[DataMember]
		public List<CurrencyDetails> CurrencyDetailsList { get; set; }
		[DataMember]
		public List<CurrencyDetails> ViewCurrencyDetailsList { get; set; }
    }
}
