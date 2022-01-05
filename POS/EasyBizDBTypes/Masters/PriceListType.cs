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
    public class PriceListType : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PriceListCode { get; set; }

        [DataMember]
        public string PriceListName { get; set; }

        [DataMember]

        public string Remarks { get; set; }

        [DataMember]

        public int PriceListCurrencyType { get; set; }

        [DataMember]

        public int BasePriceListID { get; set; }

        [DataMember]

        public Decimal ConversionFactore { get; set; }

        [DataMember]

        public string CurrencyName { get; set; }

        [DataMember]

        public string BaseCurrency1 { get; set; }

        [DataMember]
        public string PriceCategory { get; set; }
        [DataMember]
        public string PriceType { get; set; }

        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
    }
}
