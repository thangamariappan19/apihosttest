using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizResponse.Masters.DesignationMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public  class DesignationMasterBLL
    {
        public SaveDesignationMasterResponse SaveDesignationMaster(SaveDesignationMasterRequest objRequest)
        {
            SaveDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDesignationMaster = new DesignationMaster();
                    objDesignationMaster = (DesignationMaster)objRequest.RequestDynamicData;
                    objRequest.DesignationMasterData = objDesignationMaster;
                }
                objResponse = (SaveDesignationMasterResponse)objBaseDesignationMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DesignationMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.DESIGNATION;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignationMasterBLL", "SaveDesignationMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllDesignationMasterResponse API_SelectAllDesignationMaster(SelectAllDesignationMasterRequest requestData)
        {

            SelectAllDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                objResponse = (SelectAllDesignationMasterResponse)objBaseDesignationMasterDAL.API_SelectAll(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllDesignationMasterResponse SelectAllDesignationMaster(SelectAllDesignationMasterRequest objRequest)
        {
            SelectAllDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                objResponse = (SelectAllDesignationMasterResponse)objBaseDesignationMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDDesignationMasterResponse SelectDesignationMasterRecord(SelectByIDDesignationMasterRequest objRequest)
        {
            SelectByIDDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                objResponse = (SelectByIDDesignationMasterResponse)objBaseDesignationMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateDesignationMasterResponse UpdateDesignationMaster(UpdateDesignationMasterRequest objRequest)
        {
            UpdateDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDesignationMaster = new DesignationMaster();
                    objDesignationMaster = (DesignationMaster)objRequest.RequestDynamicData;
                    objRequest.DesignationMasterData = objDesignationMaster;
                }
                objResponse = (UpdateDesignationMasterResponse)objBaseDesignationMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DesignationMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.DESIGNATION;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignationMasterBLL", "UpdateDesignationMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteDesignationMasterResponse DeleteDesignationMaster(DeleteDesignationMasterRequest objRequest)
        {
            DeleteDesignationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                objResponse = (DeleteDesignationMasterResponse)objBaseDesignationMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.DesignationMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.DESIGNATION;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DesignationMasterBLL", "DeleteDesignationMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteDesignationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectDesignationMasterLookUpResponse SelectDesignationMasterLookUp(SelectDesignationMasterLookUpRequest objRequest)
        {
            SelectDesignationMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDesignationMasterDAL = objFactory.GetDALRepository().GetDesignationMasterDAL();
                objResponse = (SelectDesignationMasterLookUpResponse)objBaseDesignationMasterDAL.SelectDesignationMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDesignationMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
