using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizFactory;
using EasyBizRequest.Import.StylePricingRequest;
using EasyBizResponse.Import.StylePricingResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Import
{
   public class ImportStylePricingBLL
    {
       public SaveStylePricingMasterResponse SaveImportExcelStylePricingMaster(SaveStylePricingMasterRequest RequestObj)
       {

           SaveStylePricingMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseImportStylePricingDAL = objFactory.GetDALRepository().GetBaseImportStylePricingDAL();
               if (RequestObj.RequestDynamicData != null)
               {
                   var ObjImportStylePricing = new StylePricing();
                   ObjImportStylePricing = (StylePricing)RequestObj.RequestDynamicData;
                   RequestObj.ImportStylePricingExcelList = ObjImportStylePricing.ImportStylePricingExcelList;
               }
               objResponse = (SaveStylePricingMasterResponse)objBaseImportStylePricingDAL.InsertRecord(RequestObj);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && RequestObj.DataSync == false)
               {
                   RequestObj.RequestFrom = RequestObj.RequestFrom;
                   RequestObj.DocumentIDs = Convert.ToString(objResponse.IDs);
                   RequestObj.DocumentType = Enums.DocumentType.IMPORTSTYLEPRICING;
                   RequestObj.ProcessMode = Enums.ProcessMode.BulkNew;

                   BackgroundServices _BackgroundServices = new BackgroundServices(RequestObj, objResponse, "EasyBizBLL.Import.ImportStylePricingBLL", "SaveImportExcelStylePricingMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveStylePricingMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Pricing Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
