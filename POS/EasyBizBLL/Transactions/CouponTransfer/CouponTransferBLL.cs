using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizFactory;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponTransfer;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.CouponTransfer
{
   public class CouponTransferBLL
    {
       public SaveCouponTransferResponse SaveCouponTransfer(SaveCouponTransferRequest objRequest)
        {
            SaveCouponTransferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCouponMaster = new CouponTransferMaster();
                    objCouponMaster = (CouponTransferMaster)objRequest.RequestDynamicData;
                   
                }
                objResponse = (SaveCouponTransferResponse)objBaseCouponTransferDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.COUPON;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;                   
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.CouponTransfer.CouponTransferBLL", "SaveCouponTransfer");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveCouponTransferResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Transfer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
       public SaveCouponTransactionResponse SaveCouponTransactionLog(SaveCouponTransactionRequest objRequest)
       {
           SaveCouponTransactionResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
               var objTransactionLogsDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjTransactionLog = new CouponTransaction();
                   ObjTransactionLog = (CouponTransaction)objRequest.RequestDynamicData;
                   objRequest.CouponTransactionList = ObjTransactionLog.CouponTransactionList;
               }
               objResponse = (SaveCouponTransactionResponse)objTransactionLogsDAL.SaveCouponTransactionDetails(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
               //    objRequest.DocumentType = Enums.DocumentType.COUPONTRANSACTION;
               //    objRequest.ProcessMode = Enums.ProcessMode.New;

               //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.CouponTransfer.CouponTransferBLL", "SaveCouponTransactionLog");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new SaveCouponTransactionResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectAllCouponTransferResponse SelectAllCouponTransfer(SelectAllCouponTransferRequest objRequest)
       {
           SelectAllCouponTransferResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               objResponse = (SelectAllCouponTransferResponse)objBaseCouponTransferDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllCouponTransferResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Transfer");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
        public SelectAllCouponTransferResponse API_SelectAllCouponTransfer(SelectAllCouponTransferRequest objRequest)
        {
            SelectAllCouponTransferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
                objResponse = (SelectAllCouponTransferResponse)objBaseCouponTransferDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCouponTransferResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Transfer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCouponTransferResponse SelectCouponTransferRecord(SelectByIDCouponTransferRequest objRequest)
       {
           SelectByIDCouponTransferResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               if (objRequest.ID == 0)
               {
                   int doc_id;
                   int.TryParse(objRequest.DocumentIDs, out doc_id);
                   objRequest.ID = doc_id;
               }
               objResponse = (SelectByIDCouponTransferResponse)objBaseCouponTransferDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDCouponTransferResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Transfer");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByCouponTransferDetailsResponse SelectCouponTransferDetails(SelectByCouponTransferDetailsRequest objRequest)
       {
           SelectByCouponTransferDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStockReceiptDetailsDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               objResponse = (SelectByCouponTransferDetailsResponse)objBaseStockReceiptDetailsDAL.SelectByCouponTransferDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByCouponTransferDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CouponReceipt");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }

           return objResponse;
       }
       public UpdateCouponTransferResponse UpdateCouponTransfer(UpdateCouponTransferRequest objRequest)
       {
           UpdateCouponTransferResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               objResponse = (UpdateCouponTransferResponse)objBaseCouponTransferDAL.UpdateRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.COUPON;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;

                   
                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.CouponTransfer.CouponTransferBLL", "UpdateCouponMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new UpdateCouponTransferResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Transfer");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectCouponTransferLookUpResponse SelectCouponTransferLookUp(SelectCouponTransferLookUpRequest objRequest)
       {
           SelectCouponTransferLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponTransferDAL = objFactory.GetDALRepository().GetCouponTransferDAL();
               objResponse = (SelectCouponTransferLookUpResponse)objBaseCouponTransferDAL.SelectCouponMasterLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectCouponTransferLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Transfer");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }

           return objResponse;
       }
    }
}
