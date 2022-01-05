using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Pricing
{
    [DataContract]
    [Serializable]
    public class StylePricing : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public long SKUID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public int PriceListCurrency { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public bool IsManualEntry { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }

        public string PriceCategory { get; set; }

        public string PriceType { get; set; }
        public string StyleCode { get; set; }
        [DataMember]
        public List<EasyBizDBTypes.Transactions.Pricing.StylePricing> ImportStylePricingExcelList { get; set; }
    }
}
