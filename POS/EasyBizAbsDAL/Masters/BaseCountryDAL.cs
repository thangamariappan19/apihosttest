using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse.Masters.CountryResponse;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCountryDAL : BaseDAL
    {
        public abstract SelectCountryLookUpResponse SelectCountryLookUp(SelectCountryLookUpRequest ObjRequest);
        public abstract GetCurrencyCodeForCountryResponse GetCurrencyCodeForCountry(GetCurrencyCodeForCountryRequest ObjRequest);
        public abstract GetCurrencyByStoreResponse GetCurencyByStore(GetCurrencyByStoreRequest objRequest);

        public abstract SelectAllCountryResponse API_SelectAll(SelectAllCountryRequest objRequest);

    }
}
