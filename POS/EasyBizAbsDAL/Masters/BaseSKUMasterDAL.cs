using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract  class BaseSKUMasterDAL : BaseDAL
    {
        public abstract GetStylePricingBySKUCodeResponse SelectGetStylePricingBySKUCode(GetStylePricingBySKUCodeRequest RequestObj);
        public abstract SelectSKUByStyleIDResponse SelectByStyleID(SelectSKUByStyleIDRequest RequestObj);

        public abstract SaveSKUMasterResponse ImportExcelInsert(SaveSKUMasterRequest RequestObj);
        public abstract SaveSKUMasterResponse UpdateImportBarCode(SaveSKUMasterRequest RequestObj);

        public abstract SelectAllSKUImagesResponse SelectAllSKUImages(SelectByALLSKUImagesRequest RequestObj);

        public abstract SelectColorCodeResponse SelectColorCodeSKUCode(SelectColorCodeRequest RequestObj);
        public abstract SelectSizeCodeResponse SelectZizeCodeSKUCode(SelectSizeCodeRequest RequestObj);

        public abstract GetStylePricingBySKUCodeResponse GetPriceBySKUCode(GetStylePricingBySKUCodeRequest objRequest);
        public abstract GetBarCodeBySKUResponse GetBarCodeBySKU(GetBarCodeBySKURequest objRequest);
        public abstract SelectAllSKUMasterResponse GetSKUWithoutStoreID(SelectAllSKUMasterRequest objRequest);

        public abstract SelectSKUOnCountryResponse SelectSKUOnCountry(SelectSKUOnCountryRequest request);

        public abstract SelectAllSKUMasterResponse GetSKUDetails(SelectAllSKUMasterRequest objRequest);
        public abstract SelectAllSKUMasterResponse GetSKUSearchForSales(SelectAllSKUMasterRequest objRequest);
        public abstract SelectAllSKUMasterResponse GetSKUSearchForSalesCombo(SelectAllSKUMasterRequest objRequest);

        public abstract SelectAllSKUMasterResponse API_SelectALL(SelectAllSKUMasterRequest requestData);
        public abstract GetStylePricingBySKUCodeResponse SelectCurrencyStylePricingBySKUCode(GetStylePricingBySKUCodeRequest objRequest);
        public abstract SelectAllSKUMasterResponse GetSKUDetailsByBin(SelectAllSKUMasterRequest objRequest);
        public abstract SelectSKUDetailsResponse SelectSKUDetails(SelectAllSKUMasterRequest objRequest);
    }
}
