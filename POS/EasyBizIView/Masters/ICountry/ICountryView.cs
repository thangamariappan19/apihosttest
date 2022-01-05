using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Country
{
    public interface ICountryView : IBaseView
    {
        int ID { get; set; }
        string CountryCode { get; set; }
        string CountryName { get; set; }
        string LanguageName { get; set; }
        Decimal DecimalDigit { get; set; }
        int DecimalPlaces { get; set; }
        string DateFormat { get; set; }
        string DateSeparator { get; set; }
        string NegativeSign { get; set; }
        string CurrencySeparator { get; set; }
        string Currency { get; set; }
        int CurrencyID { get; set; }
        string EmailID { get; set; }
        Boolean CreditLimitCheck { get; set; }
        Boolean AllowMultipleTransaction { get; set; }
        Boolean AllowPartialReceiving { get; set; }
        Boolean AllowSaleAndRedemption { get; set; }
        Boolean OrginCountry { get; set; }
        Boolean Active { get; set; }
        List<LanguageMaster> LanguageLookUp { set; }
        List<CountryMaster> CountryMasterList { get; set; }
        List<CurrencyMaster> CurrencyLookup { set; }
        List<TaxMaster> TaxMasterLookUp { set; }
        int TaxID { get; set; }
        Decimal NearByRoundOff { get; set; }
        string CurrencyCode { get; }
        string TaxCode { get; }
        string POSTitle { get; set; }
        Decimal PromotionRoundOff { get; set; }
    }
}
