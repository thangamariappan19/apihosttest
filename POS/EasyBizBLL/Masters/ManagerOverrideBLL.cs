using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using EasyBizTypes.Masters;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class ManagerOverrideBLL
    {
       public SaveManagerOverrideResponse SaveManagerOverride(SaveManagerOverrideRequest objRequest)
       {
           SaveManagerOverrideResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               BaseManagerOverrideDAL objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objManagerOverride = new ManagerOverride();
                   objManagerOverride = (ManagerOverride)objRequest.RequestDynamicData;
                   objRequest.ManagerOverrideData = objManagerOverride;
               }
               objResponse = (SaveManagerOverrideResponse)objBaseManagerOverrideDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.ManagerOverrideData.returnIDS = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.MANAGEROVERRIDE;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ManagerOverrideBLL", "SaveManagerOverride");
               }

           }
           catch (Exception ex)
           {
               objResponse = new SaveManagerOverrideResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       
        public SelectAllManagerOverrideResponse API_SelectAllManagerOverride(SelectAllManagerOverrideRequest objRequest)
        {
            SelectAllManagerOverrideResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
                objResponse = (SelectAllManagerOverrideResponse)objBaseManagerOverrideDAL.API_SelectAllManagerOverride(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllManagerOverrideResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Manager Override");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateManagerOverrideResponse UpdateManagerOverride(UpdateManagerOverrideRequest objRequest)
       {
           UpdateManagerOverrideResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               BaseManagerOverrideDAL objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objManagerOverride = new ManagerOverride();
                   objManagerOverride = (ManagerOverride)objRequest.RequestDynamicData;
                   objRequest.ManagerOverrideData = objManagerOverride;
               }
               objResponse = (UpdateManagerOverrideResponse)objBaseManagerOverrideDAL.UpdateRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   //objRequest.DocumentIDs = Convert.ToString(objRequest.ManagerOverrideData.ID);
                   objRequest.DocumentIDs = objResponse.IDs;
                   objRequest.DocumentType = Enums.DocumentType.MANAGEROVERRIDE;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;
                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ManagerOverrideBLL", "UpdateManagerOverride");
               }

           }
           catch (Exception ex)
           {
               objResponse = new UpdateManagerOverrideResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public DeleteManagerOverrideResponse DeleteManagerOverride(DeleteManagerOverrideRequest objRequest)
       {
           DeleteManagerOverrideResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               BaseManagerOverrideDAL objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               objResponse = (DeleteManagerOverrideResponse)objBaseManagerOverrideDAL.DeleteRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                  // objRequest.DocumentIDs = Convert.ToString(objRequest.ManagerOverrideData.ID);
                   objRequest.DocumentType = Enums.DocumentType.MANAGEROVERRIDE;
                   objRequest.ProcessMode = Enums.ProcessMode.Delete;
                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ManagerOverrideBLL", "DeleteCountryMaster");
               }
              
           }
           catch (Exception ex)
           {
               objResponse = new DeleteManagerOverrideResponse();
               objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectAllManagerOverrideResponse SelectAllManagerOverride(SelectAllManagerOverrideRequest objRequest)
       {
           SelectAllManagerOverrideResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               objResponse = (SelectAllManagerOverrideResponse)objBaseManagerOverrideDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllManagerOverrideResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectByIDManagerOverrideResponse SelectManagerOverride(SelectByIDManagerOverrideRequest objRequest)
       {
           SelectByIDManagerOverrideResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }
               BaseManagerOverrideDAL objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               objResponse = (SelectByIDManagerOverrideResponse)objBaseManagerOverrideDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {

               objResponse = new SelectByIDManagerOverrideResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

      

       public SelectManagerOverrideLookUpResponse SelectManagerOverrideLookUp(SelectManagerOverrideLookUpRequest objRequest)
       {
           SelectManagerOverrideLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               BaseManagerOverrideDAL objBaseManagerOverrideDAL = objFactory.GetDALRepository().GetBaseManagerOverrideDAL();
               objResponse = (SelectManagerOverrideLookUpResponse)objBaseManagerOverrideDAL.SelectManagerOverrideLookUp(objRequest);
           }
           catch (Exception ex)
           {

               objResponse = new SelectManagerOverrideLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Manager Override");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
