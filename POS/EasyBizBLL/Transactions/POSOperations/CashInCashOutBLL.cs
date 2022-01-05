using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.PaymentDetails;
using EasyBizFactory;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.POSOperations;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using EasyBizResponse.Transactions.POSOperations.CashInCashOut;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POSOperations
{
    public class CashInCashOutBLL
    {
        public SaveCashInCashOutResponse SaveCashInCashOut(SaveCashInCashOutRequest objRequest)
        {
            SaveCashInCashOutResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCashInCashOut = new CashInCashOutMaster();
                    objCashInCashOut = (CashInCashOutMaster)objRequest.RequestDynamicData;
                    objRequest.CashInCashOutMasterRecord = objCashInCashOut;
                    objRequest.CashInCashOutDetailsList = objCashInCashOut.CashInCashOutDetailsList;
                }
                objResponse = (SaveCashInCashOutResponse)objBaseCashInCashOutDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CASHINCASHOUT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POSOperations.CashInCashOutBLL", "SaveCashInCashOut");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCashInCashOutResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateCashInCashOutResponse UpdateCashInCashOut(UpdateCashInCashOutRequest objRequest)
        {
            UpdateCashInCashOutResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (UpdateCashInCashOutResponse)objBaseCashInCashOutDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CASHINCASHOUT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POSOperations.CashInCashOutBLL", "UpdateCashInCashOut");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCashInCashOutResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteCashInCashOutResponse DeleteCashInCashOut(DeleteCashInCashOutRequest objRequest)
        {
            DeleteCashInCashOutResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (DeleteCashInCashOutResponse)objBaseCashInCashOutDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CASHINCASHOUT;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POSOperations.CashInCashOutBLL", "DeleteCashInCashOut");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCashInCashOutResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCashInCashOutResponse SelectAllCashInCashOut(SelectAllCashInCashOutRequest objRequest)
        {
            SelectAllCashInCashOutResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (SelectAllCashInCashOutResponse)objBaseCashInCashOutDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCashInCashOutResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCashInCashoutReportResponse SelectAllCashInCashOutReport(SelectAllCashInCashoutReportRequest objRequest)
        {
            SelectAllCashInCashoutReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (SelectAllCashInCashoutReportResponse)objBaseCashInCashOutDAL.SelectCashInCashOutReportDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCashInCashoutReportResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByCashInCashOutIDResponse SelectCashInCashOutRecord(SelectByCashInCashOutIDRequest objRequest)
        {
            SelectByCashInCashOutIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (SelectByCashInCashOutIDResponse)objBaseCashInCashOutDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByCashInCashOutIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectCashInCashOutDetailsResponse SelectAllStoreGroupDetails(SelectCashInCashOutDetailsRequest objRequest)
        {
            SelectCashInCashOutDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCashInCashOutDetailsDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (SelectCashInCashOutDetailsResponse)objBaseCashInCashOutDetailsDAL.SelectCashInCashOutDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCashInCashOutDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAllCashInCashOutDateWiseReponse SelectAllCashInCashOutRecord(SelectAllCashInCashOutDateWiseRequest objRequest)
        {
            SelectAllCashInCashOutDateWiseReponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCashInCashOutDAL = objFactory.GetDALRepository().GetCashInCashOutDAL();
                objResponse = (SelectAllCashInCashOutDateWiseReponse)objBaseCashInCashOutDAL.SelectCashInCashOutRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCashInCashOutDateWiseReponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CashInCashOut Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
