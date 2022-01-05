using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class OnAccountPaymentBLL
    {
        public SaveOnAccountPaymentResponse SaveOnAccountPayment(SaveOnAccountPaymentRequest objRequest)
        {
            SaveOnAccountPaymentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.RequestFrom == Enums.RequestFrom.StoreServer)
                {
                    objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                }
                else
                {
                    objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                }
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.OnAccountPaymentRecord = (OnAccountPayment)objRequest.RequestDynamicData;
                }
                var objBaseBrandDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (SaveOnAccountPaymentResponse)objBaseBrandDAL.InsertRecord(objRequest);

                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false && objRequest.RequestFrom != Enums.RequestFrom.SyncService)
                //{
                //    objRequest.DocumentNos = Convert.ToString(objResponse.IDs); //No Document Number
                //    objRequest.DocumentDate = objRequest.OnAccountPaymentRecord.PaymentDate;
                //    objRequest.BaseIntegrateStoreID = objRequest.OnAccountPaymentRecord.StoreID;
                //    objRequest.BaseIntegrateStoreCode = objRequest.OnAccountPaymentRecord.StoreCode;                   

                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.ONACCOUNTPAYMENT;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.OnAccountPaymentBLL", "SaveOnAccountPayment");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveOnAccountPaymentResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "On Account Payment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        //Rajaram - For getting On Account Details in Sales Return Screen
        public GetOnAccountPaymentDetailsResponse GetOnAccountDetails(GetOnAccountPaymentDetailsRequest objRequest)
        {
            GetOnAccountPaymentDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objOnAccountDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (GetOnAccountPaymentDetailsResponse)objOnAccountDAL.GetOnAccountDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetOnAccountPaymentDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "On Account Payment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetOnAccountPaymentPendingResponse GetPendingPayments(GetOnAccountPaymentPendingRequest objRequest)
        {
            GetOnAccountPaymentPendingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (GetOnAccountPaymentPendingResponse)objBaseBrandDAL.GetOnAccountPaymentPendingList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetOnAccountPaymentPendingResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "On Account Payment Pending");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllOnAccountPaymentResponse GetOnAccountPaymentList(SelectAllOnAccountPaymentRequest objRequest)
        {
            SelectAllOnAccountPaymentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (SelectAllOnAccountPaymentResponse)objBaseBrandDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllOnAccountPaymentResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "On Account Payment List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectOnAccountPaymentResponse GetOnAccountPaymentRecord(SelectOnAccountPaymentRequest objRequest)
        {
            SelectOnAccountPaymentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (SelectOnAccountPaymentResponse)objBaseBrandDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectOnAccountPaymentResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "On Account Payment List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllOnAccountPaymentResponse API_GetALLOnAccountPaymentRecort(SelectAllOnAccountPaymentRequest objRequest)
        {
            SelectAllOnAccountPaymentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetOnAccountPaymentDAL();
                objResponse = (SelectAllOnAccountPaymentResponse)objBaseBrandDAL.API_GetALLOnAccountDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllOnAccountPaymentResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "On Account Payment List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
