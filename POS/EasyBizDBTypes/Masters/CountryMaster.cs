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
    public class CountryMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string LanguageName { get; set; }
        [DataMember]
        public Decimal DecimalDigit { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }
        [DataMember]
        public string DateFormat { get; set; }
        [DataMember]
        public string DateSeparator { get; set; }
        [DataMember]
        public string NegativeSign { get; set; }
        [DataMember]
        public string CurrencySeparator { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public Boolean CreditLimitCheck { get; set; }
        [DataMember]
        public Boolean AllowMultipleTransaction { get; set; }
        [DataMember]
        public Boolean AllowPartialReceiving { get; set; }
        [DataMember]
        public Boolean AllowSaleAndRedemption { get; set; }
        [DataMember]
        public Boolean OrginCountry { get; set; }

        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public int TaxID { get; set; }

        [DataMember]
        public Decimal NearByRoundOff { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string TaxCode { get; set; }
        public List<ShiftMaster> Shiftlist { get; set; }
        [DataMember]
        public string POSTitle { get; set; }
        [DataMember]
        public Decimal PromotionRoundOff { get; set; }
    }
}
