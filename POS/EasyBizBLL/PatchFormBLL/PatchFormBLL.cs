using EasyBizAbsDAL.PatchForm;
using EasyBizAbsDAL.SalesTarget;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizFactory;
using EasyBizRequest.Common;
using EasyBizRequest.PatchFormRequest;
using EasyBizResponse.Common;
using EasyBizResponse.PatchFormResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.PatchFormBLL
{
   public class PatchFormBLL
    {
       public SavePatchFormResponse SavePatchForm(SavePatchFormRequest objRequest)
       {
           SavePatchFormResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;

               BasePatchFormDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetBasePatchFormDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjPatchFormTypesHeader = new PatchFormTypes();
                   ObjPatchFormTypesHeader = (PatchFormTypes)objRequest.RequestDynamicData;
                   objRequest.DocumentTypeID = ObjPatchFormTypesHeader.DocumentTypeID;
                   objRequest.PatchFormTypesRecord = ObjPatchFormTypesHeader;                   
               }
               objResponse = (SavePatchFormResponse)objBaseDocumentNumberingMasterDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.PATCHFORM;
                   objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                 

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.PatchFormBLL.PatchFormBLL", "SavePatchForm");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SavePatchFormResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Patch Form");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

      
    }
}
