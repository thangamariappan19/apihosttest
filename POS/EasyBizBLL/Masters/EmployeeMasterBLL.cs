using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.EmployeeDiscountInfoRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.EmployeeDiscountInfoResponse;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class EmployeeMasterBLL
    {
        public SaveEmployeeMasterResponse SaveEmployeeMaster(SaveEmployeeMasterRequest objRequest)
        {
            SaveEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objEmployeeMaster = new EmployeeMaster();
                    objEmployeeMaster = (EmployeeMaster)objRequest.RequestDynamicData;
                    objRequest.EmployeeMasterRecord = objEmployeeMaster;
                }
                objResponse = (SaveEmployeeMasterResponse)objBaseEmployeeMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.EmployeeMasterRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.EMPLOYEES;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.EmployeeMasterBLL", "SaveEmployeeMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllEmployeeMasterResponse API_SelectALL(SelectAllEmployeeMasterRequest requestData)

        {
            SelectAllEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectAllEmployeeMasterResponse)objBaseEmployeeMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllEmployeeMasterResponse SelectSalesEmployeeForPOS(SelectAllEmployeeMasterRequest objRequest)
        {
            SelectAllEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectAllEmployeeMasterResponse)objBaseEmployeeMasterDAL.SelectSalesEmployeeForPOS(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateEmployeeMasterResponse UpdateEmployeeMaster(UpdateEmployeeMasterRequest objRequest)
        {
            UpdateEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objEmployeeMaster = new EmployeeMaster();
                    objEmployeeMaster = (EmployeeMaster)objRequest.RequestDynamicData;
                    objRequest.EmployeeMasterRecord = objEmployeeMaster;
                }
                objResponse = (UpdateEmployeeMasterResponse)objBaseEmployeeMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.EmployeeMasterRecord.ID);
                //    objRequest.DocumentType = Enums.DocumentType.EMPLOYEES;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.EmployeeMasterBLL", "UpdateEmployeeMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdateEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectEmployeeLookUpResponse GetEmployeeByStore(GetEmployeeByStoreRequest objRequest)
        {
            SelectEmployeeLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectEmployeeLookUpResponse)objBaseEmployeeMasterDAL.GetEmployeeByStore(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectEmployeeLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteEmployeeMasterResponse DeleteEmployeeMaster(DeleteEmployeeMasterRequest objRequest)
        {
            DeleteEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (DeleteEmployeeMasterResponse)objBaseEmployeeMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                 //   objRequest.DocumentIDs = Convert.ToString(objRequest.EmployeeMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.EMPLOYEES;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.EmployeeMasterBLL", "DeleteEmployeeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllEmployeeMasterResponse SelectAllEmployeeMaster(SelectAllEmployeeMasterRequest objRequest)
        {
            SelectAllEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectAllEmployeeMasterResponse)objBaseEmployeeMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetEmployeeByStoreResponse SelectEmployeeMaster(SelectByIDEmployeeMasterRequest objRequest)
        {
            GetEmployeeByStoreResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (GetEmployeeByStoreResponse)objBaseEmployeeMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new GetEmployeeByStoreResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectEmployeeLookUpResponse SelectEmployeeLookUp(SelectEmployeeLookUpRequest objRequest)
        {
            SelectEmployeeLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectEmployeeLookUpResponse)objBaseEmployeeMasterDAL.SelectCountryLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectEmployeeLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest objRequest)
        {
            SelectStoreMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectStoreMasterLookUpResponse)objBaseEmployeeMasterDAL.SelectStoreMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectEmployeeDiscountInfoResponseByCustCode SelectEmployeediscountInfoByCustCode(SelectEmployeeDiscountInfoByCustCode objRequest)
        {
            SelectEmployeeDiscountInfoResponseByCustCode objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectEmployeeDiscountInfoResponseByCustCode)objBaseEmployeeMasterDAL.SelectEmployeediscountInfoByCustCode(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectEmployeeDiscountInfoResponseByCustCode();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllEmployeeMasterResponse GetSelectFilterRecord(SelectCountryStoreFilterEmployeeMaster requestData)

        {
            SelectAllEmployeeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeMasterDAL();
                objResponse = (SelectAllEmployeeMasterResponse)objBaseEmployeeMasterDAL.API_SelectFilterData(requestData);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllEmployeeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}

