using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseBarcodeSettingsDAL:BaseDAL
    {
        public abstract SelectBarcodeSettingsLookUpResponse SelectBarcodeSettingsLookUp(SelectBarcodeSettingsLookUpRequest RequestObj);
        public abstract SelectBarcodeGenerateBySKUResponse SelectBarcodeGenerateBySKU(SelectBarcodeGenerateBySKURequest RequestObj);
        public abstract SelectAllBarcodeSettingsResponse API_SelectALL(SelectAllBarcodeSettingsRequest requestData);
    }
}
