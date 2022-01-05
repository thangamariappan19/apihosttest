using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.TillSettingRequest;
using EasyBizResponse.Masters.TillSettingsResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public  class TillSettingsBLL
    {
        public SaveTillSettingsResponse SaveTillSettings(SaveTillSettingsRequest objRequest)
        {
            SaveTillSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTillSettings = new TillSettings();
                    objTillSettings = (TillSettings)objRequest.RequestDynamicData;
                    objRequest.TillSettingsData = objTillSettings;
                }
                objResponse = (SaveTillSettingsResponse)objBaseTillSettingsDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.TillSettingsData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.TILL;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TillSettingsBLL", "SaveTillSettings");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveTillSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllTillSettingsResponse SelectAllTillSettings(SelectAllTillSettingsRequest objRequest)
        {
            SelectAllTillSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                objResponse = (SelectAllTillSettingsResponse)objBaseTillSettingsDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllTillSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDTillSettingsResponse SelectTillSettingsRecord(SelectByIDTillSettingsRequest objRequest)
        {
            SelectByIDTillSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                objResponse = (SelectByIDTillSettingsResponse)objBaseTillSettingsDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDTillSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateTillSettingsResponse UpdateTillSettings(UpdateTillSettingsRequest objRequest)
        {
            UpdateTillSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTillSettings = new TillSettings();
                    objTillSettings = (TillSettings)objRequest.RequestDynamicData;
                    objRequest.TillSettingsData = objTillSettings;
                }
                objResponse = (UpdateTillSettingsResponse)objBaseTillSettingsDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.TillSettingsData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.TILL;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TillSettingsBLL", "UpdateTillSettings");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateTillSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteTillSettingsResponse DeleteTillSettings(DeleteTillSettingsRequest objRequest)
        {
            DeleteTillSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                objResponse = (DeleteTillSettingsResponse)objBaseTillSettingsDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                  //  objRequest.DocumentIDs = Convert.ToString(objRequest.TillSettingsData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.TILL;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TillSettingsBLL", "DeleteTillSettings");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteTillSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectTillSettingsLookUpResponse SelectTillSettingsLookUp(SelectTillSettingsLookUpRequest objRequest)
        {
            SelectTillSettingsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTillSettingsDAL = objFactory.GetDALRepository().GetTillSettingsDAL();
                objResponse = (SelectTillSettingsLookUpResponse)objBaseTillSettingsDAL.SelectTillSettingsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectTillSettingsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
