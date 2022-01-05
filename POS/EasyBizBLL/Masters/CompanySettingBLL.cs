using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CompanySettingBLL
    {

        public SaveCompanySettingResponse SaveCompanySetting(SaveCompanySettingRequest objRequest)
        {

            SaveCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;               
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCompanySettings = new CompanySettings();
                    objCompanySettings = (CompanySettings)objRequest.RequestDynamicData;
                    objRequest.CompanySettingData = objCompanySettings;
                }
                objResponse = (SaveCompanySettingResponse)objBaseCompanySettingDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CompanySettingData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COMPANYSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CompanySettingBLL", "SaveCompanySetting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Company Setting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCompanySettingResponse API_SelectAllCountryMaster(SelectAllCompanySettingRequest objRequest)
        {
            SelectAllCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (SelectAllCompanySettingResponse)objBaseCompanySettingDAL.API_SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public UpdateCompanySettingResponse UpdateCompanySetting(UpdateCompanySettingRequest objRequest)
        {

            UpdateCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCompanySettings = new CompanySettings();
                    objCompanySettings = (CompanySettings)objRequest.RequestDynamicData;
                    objRequest.CompanySettingData = objCompanySettings;
                }
                objResponse = (UpdateCompanySettingResponse)objBaseCompanySettingDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CompanySettingData.ID);
                    objRequest.DocumentType = Enums.DocumentType.COMPANYSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CompanySettingBLL", "UpdateCompanySetting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Company Setting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCompanySettingResponse SelectCompanySettingLookUp(SelectAllCompanySettingRequest request)
        {
            SelectAllCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (SelectAllCompanySettingResponse)objBaseCompanySettingDAL.API_SelectCompanySettingLookUp(request);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteCompanySettingResponse DeleteCompanySetting(DeleteCompanySettingRequest objRequest)
        {

            DeleteCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (DeleteCompanySettingResponse)objBaseCompanySettingDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CompanySettingData.ID);
                    objRequest.DocumentType = Enums.DocumentType.COMPANYSETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CompanySettingBLL", "DeleteCompanySetting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Company Setting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCompanySettingResponse SelectAllCompanySettingResponse(SelectAllCompanySettingRequest objRequest)
        {
            SelectAllCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (SelectAllCompanySettingResponse)objBaseCompanySettingDAL.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }


        public SelectByIDCompanySettingResponse SelectByIDCompanySetting(SelectByIDCompanySettingRequest objRequest)
        {
            SelectByIDCompanySettingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (SelectByIDCompanySettingResponse)objBaseCompanySettingDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCompanySettingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCompanySettingsLookUpResponse SelectCompanySettingsLookUp(SelectCompanySettingsLookUpRequest objRequest)
        {
            SelectCompanySettingsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCompanySetting();
                objResponse = (SelectCompanySettingsLookUpResponse)objBaseCompanySettingDAL.SelectCompanySettingsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCompanySettingsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

     

    }
}
