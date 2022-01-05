using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
    public class StockReceiptBLL
    {
        public SaveStockReceiptResponse SaveAndUpdateIntConfirmtransfer(SaveStockReceiptRequest objRequest)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockReceipt = new StockReceiptHeader();
                    objStockReceipt = (StockReceiptHeader)objRequest.RequestDynamicData;
                    objRequest.StockReceiptHeaderRecord = objStockReceipt;
                    objRequest.StockReceiptDetailsList = objStockReceipt.StockReceiptDetailsList;
                    objRequest.TransactionLogList = objStockReceipt.TransactionLogList;
                }                
                objResponse = (SaveStockReceiptResponse)objBaseStockReceiptDAL.Update_ConfirmTransfer(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "SaveStockReceipt");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveStockReceiptResponse SaveStockReceipt(SaveStockReceiptRequest objRequest)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockReceipt = new StockReceiptHeader();
                    objStockReceipt = (StockReceiptHeader)objRequest.RequestDynamicData;
                    objRequest.StockReceiptHeaderRecord = objStockReceipt;
                    objRequest.StockReceiptDetailsList = objStockReceipt.StockReceiptDetailsList;
                    objRequest.TransactionLogList = objStockReceipt.TransactionLogList;
                }
                objResponse = (SaveStockReceiptResponse)objBaseStockReceiptDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {

                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "SaveStockReceipt");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveStockReceiptResponse saveintStockReceipt(SaveStockReceiptRequest objRequest)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjTransactionLog = new int_stockreceipt();
                    ObjTransactionLog = (int_stockreceipt)objRequest.RequestDynamicData;
                    objRequest.int_stockreceiptList = ObjTransactionLog.int_stockreceiptList;
                }
                objResponse = (SaveStockReceiptResponse)objTransactionLogsDAL.saveintStockReceipt(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TRANSACTIONLOG;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL", "SaveTransactionLog");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateStockReceiptResponse UpdateStockReceipt(UpdateStockReceiptRequest objRequest)
        {
            UpdateStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (UpdateStockReceiptResponse)objBaseStockReceiptDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "UpdateStockReceipt");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteStockReceiptResponse DeleteStockReceipt(DeleteStockReceiptRequest objRequest)
        {
            DeleteStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (DeleteStockReceiptResponse)objBaseStockReceiptDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "DeleteStockReceipt");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReceiptResponse SelectAllStockReceipt(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReceiptResponse API_SelectALL(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReceiptResponse SelectAllStockReceiptWms(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.SelectAllStockReceiptWms(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReceiptResponse SelectAllStockReceiptWmsFlagCheck(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.SelectAllStockReceiptWmsFlagCheck(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveStockReceiptResponse Saveint_stockreceipt(SaveStockReceiptRequest objRequest)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjTransactionLog = new int_stockreceipt();
                    ObjTransactionLog = (int_stockreceipt)objRequest.RequestDynamicData;
                    objRequest.int_stockreceiptList = ObjTransactionLog.int_stockreceiptList;
                }
                objResponse = (SaveStockReceiptResponse)objBaseStockReceiptDAL.Saveint_stockreceipt(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "Saveint_stockreceipt");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStockReceiptIDResponse SelectStockReceiptRecord(SelectByStockReceiptIDRequest objRequest)
        {
            SelectByStockReceiptIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectByStockReceiptIDResponse)objBaseStockReceiptDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockReceiptIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStockReceiptResponse GetStockReceiptHeaderReport(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.GetStockReceiptHeaderReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStockReceiptResponse GetStockReceiptDetailsReport(SelectAllStockReceiptRequest objRequest)
        {
            SelectAllStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptResponse)objBaseStockReceiptDAL.GetStockReceiptDetailsReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByStockReceiptDetailsResponse SelectStockReceiptDetails(SelectByStockReceiptDetailsRequest objRequest)
        {
            SelectByStockReceiptDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDetailsDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectByStockReceiptDetailsResponse)objBaseStockReceiptDetailsDAL.SelectByStockReceiptDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockReceiptDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectByStockReceiptDetailsResponse SelectStockReceiptTransactionDetails(SelectByStockReceiptDetailsRequest objRequest)
        {
            SelectByStockReceiptDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDetailsDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectByStockReceiptDetailsResponse)objBaseStockReceiptDetailsDAL.SelectByStockReceiptTransactionDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockReceiptDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SaveStockReceiptResponse SaveStockReceiptlistWms(SaveStockReceiptRequest objRequest)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockReceipt = new StockReceiptHeader();
                    objStockReceipt = (StockReceiptHeader)objRequest.RequestDynamicData;
                    objRequest.StockReceiptHeaderRecord = objStockReceipt;
                    objRequest.StockReceiptDetailsList = objStockReceipt.StockReceiptDetailsList;
                    objRequest.TransactionLogList = objStockReceipt.TransactionLogList;
                }
                objResponse = (SaveStockReceiptResponse)objBaseStockReceiptDAL.SaveStockReceiptlistWms(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {

                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "SaveStockReceipt");
                }
            }
            catch(Exception ex)
            {

            }
            return objResponse;
        }

        public SaveStockReceiptListWmsConfirmTransferResponse SaveStockReceiptListWmsConfirmTransfer(SaveStockReceiptListWmsConfirmTransferRequest objRequest)
        {
            SaveStockReceiptListWmsConfirmTransferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockReceipt = new int_stockreceipt();
                    objStockReceipt = (int_stockreceipt)objRequest.RequestDynamicData;

                    objRequest.int_stockreceiptConfirmTransfer = objStockReceipt.int_stockreceiptConfirmTransfer;
                  
                }
                objResponse = (SaveStockReceiptListWmsConfirmTransferResponse)objBaseStockReceiptDAL.SaveStockReceiptListWmsConfirmTransfer(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {

                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "SaveStockReceipt");
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }


        public SaveStockReceiptResponse SaveStockReceiptListWmsFlagCheck(SaveStockReceiptRequest RequestData)
        {
            SaveStockReceiptResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                RequestData.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                if (RequestData.RequestDynamicData != null)
                {
                    var objStockReceipt = new StockReceiptHeader();
                    objStockReceipt = (StockReceiptHeader)RequestData.RequestDynamicData;
                    RequestData.StockReceiptHeaderRecord = objStockReceipt;
                    RequestData.StockReceiptDetailsList = objStockReceipt.StockReceiptDetailsList;
                    RequestData.TransactionLogList = objStockReceipt.TransactionLogList;
                }
                objResponse = (SaveStockReceiptResponse)objBaseStockReceiptDAL.SaveStockReceiptlistWmsFlagCheck(RequestData);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && RequestData.DataSync == false)
                {

                    RequestData.RequestFrom = RequestData.RequestFrom;
                    RequestData.DocumentIDs = Convert.ToString(objResponse.IDs);
                    RequestData.DocumentType = Enums.DocumentType.STOCKRECEIPT;
                    RequestData.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(RequestData, objResponse, "EasyBizBLL.Transactions.Stocks.StockReceiptBLL", "SaveStockReceipt");
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public SelectAllStockReceiptDetailsResponse SelectAllStockReceiptDetailsForFlaglist(SelectAllStockReceiptDetailsRequest objRequest)
        {
            SelectAllStockReceiptDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptDAL();
                objResponse = (SelectAllStockReceiptDetailsResponse)objBaseStockReceiptDAL.SelectAllStockReceiptDetailsForFlaglist(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReceiptDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
    
}
