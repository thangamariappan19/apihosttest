using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class RetailSettingsBLL
    {
        public SelectRetailSettingsLookUpResponse SelectRetailSettingsLookUp(SelectRetailSettingsLookUpRequest objRequest)
        {
            SelectRetailSettingsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objRetailSettingDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                objResponse = (SelectRetailSettingsLookUpResponse)objRetailSettingDAL.SelectRetailSettingsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectRetailSettingsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SaveRetailResponse SaveRetail(SaveRetailRequest objRequest)
        {
            SaveRetailResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseRetailSettingsDAL objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRetail = new RetailSettingsType();
                    objRetail = (RetailSettingsType)objRequest.RequestDynamicData;
                    objRequest.RetailRecord = objRetail;
                }
                objResponse = (SaveRetailResponse)objBaseRetailSettingsDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.RetailRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.RETAILSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RetailSettingsBLL", "SaveRetail");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveRetailResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllRetailResponse API_SelectALL(SelectAllRetailRequest requestData)
        {
            SelectAllRetailResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                objResponse = (SelectAllRetailResponse)objBaseRetailSettingsDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRetailResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateRetailReponse UpdateRetail(UpdateRetailRequest objRequest)
        {
            UpdateRetailReponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRetail = new RetailSettingsType();
                    objRetail = (RetailSettingsType)objRequest.RequestDynamicData;
                    objRequest.RetailRecord = objRetail;
                }
                objResponse = (UpdateRetailReponse)objBaseRetailSettingsDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.RetailRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.RETAILSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RetailSettingsBLL", "UpdateRetail");
                }

            }
            catch (Exception ex)
            {
                objResponse = new UpdateRetailReponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteRetailResponse DeleteRetail(DeleteRetailRequest objRequest)
        {
            DeleteRetailResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                objResponse = (DeleteRetailResponse)objBaseRetailSettingsDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.RetailRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.RETAILSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RetailSettingsBLL", "DeleteRetail");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteRetailResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllRetailResponse SelectAllRetail(SelectAllRetailRequest objRequest)
        {
            SelectAllRetailResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                objResponse = (SelectAllRetailResponse)objBaseRetailSettingsDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRetailResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Retail Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByRetailIDResponse SelectRetailRecord(SelectByRetailIDRequest objRequest)
        {
            SelectByRetailIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objBaseRetailSettingsDAL = objFactory.GetDALRepository().GetRetailSettingDAL();
                objResponse = (SelectByRetailIDResponse)objBaseRetailSettingsDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByRetailIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Retail settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
