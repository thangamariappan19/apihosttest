using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CityMasterRequest;
using EasyBizResponse.Masters.CityMasterResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CityMasterBLL
    {

        public SaveCityResponse SaveCityMaster(SaveCityRequest objRequest)
        {
            SaveCityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCityMaster = new CityMaster();
                    objCityMaster = (CityMaster)objRequest.RequestDynamicData;
                    objRequest.CityRecord = objCityMaster;
                }
                objResponse = (SaveCityResponse)objBaseCityMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CityRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CITY;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CityMasterBLL", "SaveCityMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCityResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCityResponse UpdateCity(UpdateCityRequest objRequest)
        {
            UpdateCityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCityMaster = new CityMaster();
                    objCityMaster = (CityMaster)objRequest.RequestDynamicData;
                    objRequest.CityRecord = objCityMaster;
                }
                objResponse = (UpdateCityResponse)objBaseCityMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CityRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.CITY;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CityMasterBLL", "UpdateCity");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCityResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectByCityIDResponse SelectCityRecord(SelectByCityIDRequest objRequest)
        {
            SelectByCityIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                objResponse = (SelectByCityIDResponse)objBaseCityMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByCityIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCityResponse SelectAllRecordCityMaster(SelectAllCityRequest objRequest)
        {
            SelectAllCityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                objResponse = (SelectAllCityResponse)objBaseCityMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCityResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCityResponse API_SelectALL(SelectAllCityRequest requestData)
        {
            SelectAllCityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                objResponse = (SelectAllCityResponse)objBaseCityMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCityResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectCityLookUPResponse SelectCityLookUp(SelectCityLookUPRequest objRequest)
        {
            SelectCityLookUPResponse objResponse = null;
            var objFactory = new DALFactory();
            {
            try
                { 
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
                objResponse = (SelectCityLookUPResponse)objBaseCityMasterDAL.SelectCityLookUP(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectCityLookUPResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "City Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
    }
}
