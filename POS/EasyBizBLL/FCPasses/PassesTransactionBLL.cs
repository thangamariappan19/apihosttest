using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.FCPasses;
using EasyBizFactory;
using EasyBizRequest.FCPasses;
using EasyBizResponse.FCPasses;
using ResourceStrings;

namespace EasyBizBLL.FCPasses
{
    public class PassesTransactionBLL
    {
        public PassesTransactionResponse API_SelectALL(PassesTransactionRequest requestData)
        {
            PassesTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePassesTransactionDAL = objFactory.GetDALRepository().GetPassesTransactionDAL();
                objResponse = (PassesTransactionResponse)objBasePassesTransactionDAL.SelectAll(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new PassesTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Passes Transaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public PassesTransactionResponse SavePassesTransaction(PassesTransactionRequest objRequest)
        {
            PassesTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objPassesTransactionRequestDAL = objFactory.GetDALRepository().GetPassesTransactionDAL();
                //if (objRequest.RequestDynamicData != null)
                //{
                //    var objStockRequest = new PassesTransaction();
                //    objStockRequest = (PassesTransaction)objRequest.RequestDynamicData;
                //    objRequest.PassesTransactionHeaderData = objStockRequest;
                //    objRequest.PassesTransactionDetailsList = objStockRequest.PassesTransactionDetailsList;
                //}
                objResponse = (PassesTransactionResponse)objPassesTransactionRequestDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PASSESTRANSACTION;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "SaveStockRequest");
                }
            }
            catch (Exception ex)
            {
                objResponse = new PassesTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Passes Transaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectPassesTransactionResponse SelectPassesTransactionDetails(SelectPassesTransactionRequest objRequest)
        {
            SelectPassesTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objPassesTransactionRequestDAL = objFactory.GetDALRepository().GetPassesTransactionDAL();
                //if (objRequest.RequestDynamicData != null)
                //{
                //    var objStockRequest = new PassesTransaction();
                //    objStockRequest = (PassesTransaction)objRequest.RequestDynamicData;
                //    objRequest.PassesTransactionHeaderData = objStockRequest;
                //    objRequest.PassesTransactionDetailsList = objStockRequest.PassesTransactionDetailsList;
                //}
                objResponse = (SelectPassesTransactionResponse)objPassesTransactionRequestDAL.SelectRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PASSESTRANSACTION;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "SaveStockRequest");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectPassesTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Passes Transaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
