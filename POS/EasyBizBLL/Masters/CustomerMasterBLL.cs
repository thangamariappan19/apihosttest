using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CustomerMasterBLL
    {
        public SaveCustomerMasterResponse SaveCustomerMaster(SaveCustomerMasterRequest objRequest)
        {

            SaveCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCustomerMasterDAL = objFactory.GetDALRepository().GetCustomerMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerMaster = new CustomerMaster();
                    objCustomerMaster = (CustomerMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerMasterData = objCustomerMaster;
                }
                objResponse = (SaveCustomerMasterResponse)objBaseCustomerMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentNos = objRequest.CustomerMasterData.CustomerCode;
                    objRequest.CustomerMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerMasterBLL", "SaveCustomerMaster");


                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCustomerMasterResponse GetCustomerSearchPOS(SelectAllCustomerMasterRequest requestData)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.GetCustomerSearchPOS(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectAllCustomerMasterResponse GetCommonCustomerData(SelectAllCustomerMasterRequest requestData)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.GetCommonCustomerData(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAllCustomerMasterResponse GetCommonCustomerDetailsData(SelectAllCustomerMasterRequest requestData)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.GetCommonCustomerDetailsData(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAllCustomerMasterResponse GetCommonCustomerDetailsLastBYID(SelectAllCustomerMasterRequest requestData)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.GetCommonCustomerDetailsDataID(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateCustomerMasterResponse UpdateCustomerMaster(UpdateCustomerMasterRequest objRequest)
        {

            UpdateCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCustomerMasterDAL = objFactory.GetDALRepository().GetCustomerMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerMaster = new CustomerMaster();
                    objCustomerMaster = (CustomerMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerMasterData = objCustomerMaster;
                }
                objResponse = (UpdateCustomerMasterResponse)objBaseCustomerMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CustomerMasterData.ID);
                    objRequest.DocumentNos = Convert.ToString(objRequest.CustomerMasterData.CustomerCode);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerMasterBLL", "UpdateCustomerMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteCustomerMasterResponse DeleteCustomerMaster(DeleteCustomerMasterRequest objRequest)
        {

            DeleteCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerMaster = new CustomerMaster();
                    objCustomerMaster = (CustomerMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerMasterData = objCustomerMaster;
                }
                var objBaseCustomerMasterDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (DeleteCustomerMasterResponse)objBaseCustomerMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CustomerMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerMasterBLL", "DeleteCustomerMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCustomerMasterResponse SelectAllCustomerMaster(SelectAllCustomerMasterRequest objRequest)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }

        public SelectAllCustomerMasterResponse API_SelectAllCustomerMaster(SelectAllCustomerMasterRequest objRequest)
        {
            SelectAllCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerMaster = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerMasterResponse)objBaseCustomerMaster.API_SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }

        public SelectByIDCustomerMasterResponse SelectByIDCustomerMaster(SelectByIDCustomerMasterRequest objRequest)
        {
            SelectByIDCustomerMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectByIDCustomerMasterResponse)objBaseCompanySettingDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCustomerMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCustomerMasterLookUpResponse SelectCustomerMasterLookUp(SelectCustomerMasterLookUpRequest objRequest)
        {
            SelectCustomerMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCustomerMasterDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectCustomerMasterLookUpResponse)objBaseCustomerMasterDAL.SelectCustomerMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCustomerMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectCustomerByPhoneNoResponse SelectCustomerByPhoneNo(SelectCustomerByPhoneNoRequest objRequest)
        {
            SelectCustomerByPhoneNoResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //if (objRequest.ID == 0)
                //{
                //    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                //}
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectCustomerByPhoneNoResponse)objBaseCompanySettingDAL.SelectCustomerByPhoneNo(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCustomerByPhoneNoResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        //API_CustomerSalesTransactionAll

        public SelectAllCustomerSaleTransactionResponse SelectALLCustomerSalesTransaction(SelectAllCustomerSalesTransactionRequest ObjRequest)
        {
            SelectAllCustomerSaleTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
             
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerSaleTransactionResponse)objBaseCompanySettingDAL.API_CustomerSalesTransactionAll(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerSaleTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAllCustomerSaleTransactionResponse SelectALLCustomerReturnTransaction(SelectAllCustomerSalesTransactionRequest ObjRequest)
        {
            SelectAllCustomerSaleTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
               
                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerSaleTransactionResponse)objBaseCompanySettingDAL.API_CustomerReturnTransactionAll(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerSaleTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectAllCustomerSaleTransactionResponse SelectALLCustomerReturnExchange(SelectAllCustomerSalesTransactionRequest ObjRequest)
        {
            SelectAllCustomerSaleTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCompanySettingDAL = objFactory.GetDALRepository().GetCustomerMaster();
                objResponse = (SelectAllCustomerSaleTransactionResponse)objBaseCompanySettingDAL.API_CustomerReturnExchange(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerSaleTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }

}
