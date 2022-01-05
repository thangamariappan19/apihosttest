using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizFactory;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponTransfer;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.CouponReceipt
{
   public class CouponreceiptBLL
    {
       public SaveCouponReceiptResponse SaveCouponReceipt(SaveCouponReceiptRequest objRequest)
       {
           SaveCouponReceiptResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseCouponReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objCouponMaster = new CouponReceiptHeader();
                   objCouponMaster = (CouponReceiptHeader)objRequest.RequestDynamicData;

               }
               objResponse = (SaveCouponReceiptResponse)objBaseCouponReceiptDAL.InsertRecord(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
               //    objRequest.DocumentType = Enums.DocumentType.COUPON;
               //    objRequest.ProcessMode = Enums.ProcessMode.New;
               //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.CouponReceipt.CouponreceiptBLL", "SaveCouponReceipt");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new SaveCouponReceiptResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Receipt");
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
               //    objRequest.DocumentType = Enums.DocumentType.COUPONRECEIPT;
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
       public SelectAllCouponReceiptResponse SelectAllCouponReceipt(SelectAllCouponReceiptRequest objRequest)
       {
           SelectAllCouponReceiptResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
               objResponse = (SelectAllCouponReceiptResponse)objBaseCouponReceiptDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllCouponReceiptResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
        public SelectAllCouponReceiptResponse API_SelectAllCouponReceipt(SelectAllCouponReceiptRequest objRequest)
        {
            SelectAllCouponReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
                objResponse = (SelectAllCouponReceiptResponse)objBaseCouponReceiptDAL.API_SelectALLCouponReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCouponReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCouponReceiptResponse SelectCouponReceiptRecord(SelectByIDCouponReceiptRequest objRequest)
       {
           SelectByIDCouponReceiptResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
               objResponse = (SelectByIDCouponReceiptResponse)objBaseStockReceiptDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDCouponReceiptResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CouponReceipt");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public GetSerialNumberResponse SelectCouponSerialNum(GetSerialNumberRequest objRequest)
       {
           GetSerialNumberResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
               objResponse = (GetSerialNumberResponse)objBaseStockReceiptDAL.SelectByCouponReceiptDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new GetSerialNumberResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CouponReceipt");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
        public GetSerialNumberResponse API_SelectCouponSerialNum(GetSerialNumberRequest objRequest)
        {
            GetSerialNumberResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
                objResponse = (GetSerialNumberResponse)objBaseStockReceiptDAL.API_GetSerialNumberDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSerialNumberResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CouponReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByCouponReceiptDetailsResponse SelectCouponReceiptDetails(SelectByCouponReceiptDetailsRequest objRequest)
       {
           SelectByCouponReceiptDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStockReceiptDetailsDAL = objFactory.GetDALRepository().GetCouponReceiptDAL();
               objResponse = (SelectByCouponReceiptDetailsResponse)objBaseStockReceiptDetailsDAL.SelectByCouponReceiptDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByCouponReceiptDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CouponReceipt");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }

           return objResponse;
       }
    }
}
