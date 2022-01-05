using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.FCPasses;
using EasyBizResponse.FCPasses;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.FCPasses
{
    public class PassMasterBLL
    {
        public PassMasterResponse SavePassMaster(PassMasterRequest objRequest)
        {
            PassMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetPassDAL();
                //if (objRequest.RequestDynamicData != null)
                //{
                //    var objCityMaster = new CityMaster();
                //    objCityMaster = (CityMaster)objRequest.RequestDynamicData;
                //    objRequest.CityRecord = objCityMaster;
                //}
                objResponse = (PassMasterResponse)objBaseCityMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.CityRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.CITY;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CityMasterBLL", "SaveCityMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new PassMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pass Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public PassMasterResponse UpdatePassMaster(PassMasterRequest objRequest)
        {
            PassMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetPassDAL();
                //if (objRequest.RequestDynamicData != null)
                //{
                //    var objCityMaster = new CityMaster();
                //    objCityMaster = (CityMaster)objRequest.RequestDynamicData;
                //    objRequest.CityRecord = objCityMaster;
                //}
                objResponse = (PassMasterResponse)objBaseCityMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.CityRecord.ID);
                //    objRequest.DocumentType = Enums.DocumentType.CITY;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CityMasterBLL", "UpdateCity");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new PassMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Pass Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public PassMasterResponse SelectPassMasterRecord(PassMasterRequest objRequest)
        {
            PassMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetPassDAL();
                objResponse = (PassMasterResponse)objBaseCityMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new PassMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pass Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public PassMasterResponse API_SelectALL(PassMasterRequest requestData)
        {
            PassMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCityMasterDAL = objFactory.GetDALRepository().GetPassDAL();
                objResponse = (PassMasterResponse)objBaseCityMasterDAL.SelectAll(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new PassMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pass Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectPassMasterLookUpResponse API_SelectPassMasterLookUp(SelectPassMasterLookUpRequest requestData)
        {
            SelectPassMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPassDAL();
                objResponse = (SelectPassMasterLookUpResponse)objPriceListDAL.API_SelectPassMasterLookUp(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectPassMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Pass Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        //public SelectCityLookUPResponse SelectCityLookUp(SelectCityLookUPRequest objRequest)
        //{
        //    SelectCityLookUPResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    {
        //        try
        //        {
        //            var objBaseCityMasterDAL = objFactory.GetDALRepository().GetCityDAL();
        //            objResponse = (SelectCityLookUPResponse)objBaseCityMasterDAL.SelectCityLookUP(objRequest);
        //        }
        //        catch (Exception ex)
        //        {

        //            objResponse = new SelectCityLookUPResponse();
        //            objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "City Master");
        //            objResponse.ExceptionMessage = ex.Message;
        //            objResponse.StackTrace = ex.StackTrace;
        //        }
        //        return objResponse;
        //    }
        //}
    }
}
