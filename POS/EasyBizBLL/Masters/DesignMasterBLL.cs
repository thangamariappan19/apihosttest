using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizResponse.Masters.DesignMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public  class DesignMasterBLL
    {

       public SaveDesignMasterResponse SaveDesignMaster(SaveDesignMasterRequest objRequest)
       {

           SaveDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               //objRequest.BaseIntegrateStoreID = objRequest.DesignMasterData.BrandID;

               // Changed by Senthamil @ 07.09.2018
                if (objRequest.RequestDynamicData != null && objRequest.RequestDynamicData.BrandID != null && objRequest.RequestDynamicData.BrandID > 0)
                {
                    objRequest.BaseIntegrateStoreID = objRequest.RequestDynamicData.BrandID;
                }
               else
               {
                   objRequest.BaseIntegrateStoreID = objRequest.DesignMasterData.BrandID;
               }  

               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objDesignMasterTypes = new DesignMasterTypes();
                   objDesignMasterTypes = (DesignMasterTypes)objRequest.RequestDynamicData;
                   objRequest.DesignMasterData = objDesignMasterTypes;
                   objRequest.DesignWithItemImageList = objDesignMasterTypes.DesignWithItemImageList;
                   objRequest.ImportExcelList = objDesignMasterTypes.ImportExcelList;
               }
               objResponse = (SaveDesignMasterResponse)objBaseDesignMasterDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DesignMasterData.ID = Convert.ToInt32(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.DESIGNMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignMasterBLL", "SaveDesignMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public UpdateDesignMasterResponse UpdateDesignMaster(UpdateDesignMasterRequest objRequest)
       {

           UpdateDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

               // Changed by Senthamil @ 07.09.2018
               if (objRequest.RequestDynamicData != null && objRequest.RequestDynamicData.BrandID != null && objRequest.RequestDynamicData.BrandID > 0)
               {
                   objRequest.BaseIntegrateStoreID = objRequest.RequestDynamicData.BrandID;
               }
               else
               {
                   objRequest.BaseIntegrateStoreID = objRequest.DesignMasterData.BrandID;
               }                

               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objDesignMasterTypes = new DesignMasterTypes();
                   objDesignMasterTypes = (DesignMasterTypes)objRequest.RequestDynamicData;
                   objRequest.DesignMasterData = objDesignMasterTypes;
                   objRequest.DesignWithItemImageList = objDesignMasterTypes.DesignWithItemImageList;                   
               }
               objResponse = (UpdateDesignMasterResponse)objBaseDesignMasterDAL.UpdateRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objRequest.DesignMasterData.ID);
                   objRequest.DocumentType = Enums.DocumentType.DESIGNMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignMasterBLL", "UpdateDesignMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new UpdateDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public DeleteDesignMasterResponse DeleteDesignMaster(DeleteDesignMasterRequest objRequest)
       {

           DeleteDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               objRequest.BaseIntegrateStoreID = objRequest.BrandID;

               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (DeleteDesignMasterResponse)objBaseDesignMasterDAL.DeleteRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                  // objRequest.DocumentIDs = Convert.ToString(objRequest.DesignMasterData.ID);
                   objRequest.DocumentType = Enums.DocumentType.DESIGNMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.Delete;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignMasterBLL", "DeleteDesignMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new DeleteDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectAllDesignMasterResponse SelectAllDesignMaster(SelectAllDesignMasterRequest objRequest)
       {

           SelectAllDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectAllDesignMasterResponse)objBaseDesignMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public SelectAllDesignMasterResponse API_SelectAllDesignMaster(SelectAllDesignMasterRequest objRequest)
        {

            SelectAllDesignMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
                objResponse = (SelectAllDesignMasterResponse)objBaseDesignMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDesignMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDDesignMasterResponse SelectByIDDesignMaster(SelectByIDDesignMasterRequest objRequest)
       {

           SelectByIDDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }   
               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectByIDDesignMasterResponse)objBaseDesignMasterDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }


       public SelectDesignMasterLookUpResponse SelectDesignLookUP(SelectDesignMasterLookUpRequest objRequest)
       {

           SelectDesignMasterLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectDesignMasterLookUpResponse)objBaseDesignMasterDAL.DesignResponseLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectDesignMasterLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectDesignGradeLookUpResponse GradeLookUp(SelectDesignGradeLookUpRequest objRequest)
       {
           SelectDesignGradeLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseCollection = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectDesignGradeLookUpResponse)BaseCollection.SelectDesignGradeLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectDesignGradeLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectDesignDevelopmentOfficeLookUpResponse DevelopmentOfficeLookUp(SelectDesignDevelopmentOfficeLookUpRequest objRequest)
       {
           SelectDesignDevelopmentOfficeLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseCollection = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectDesignDevelopmentOfficeLookUpResponse)BaseCollection.SelectDesignDevelopmentOfficeLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectDesignDevelopmentOfficeLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SaveDesignMasterResponse SaveImportExcelDesignMaster(SaveDesignMasterRequest objRequest)
       {

           SaveDesignMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
               var objBaseDesignMasterDAL = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SaveDesignMasterResponse)objBaseDesignMasterDAL.ImportExcelInsert(objRequest);

               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.DESIGNMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignMasterBLL", "SaveImportExcelDesignMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveDesignMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
      
    }
}
