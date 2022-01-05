using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.TransactionLogs
{
    public class TransactionLogBLL
    {
        public GetStockByStyleCodeResponse GetStockByStyleCode(GetStockByStyleCodeRequest objRequest)
        {
            GetStockByStyleCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetStockByStyleCodeResponse)objTransactionLogsDAL.GetStockByStyleCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockByStyleCodeResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetProductDescSearchResponse GetPOSProductDescSearch(GetProductDescSearchRequest objRequest)
        {
            GetProductDescSearchResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetProductDescSearchResponse)objTransactionLogsDAL.GetPOSProductDescSearch(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetProductDescSearchResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetProductDescSearchResponse GetProductDescSearch(GetProductDescSearchRequest objRequest)
        {
            GetProductDescSearchResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetProductDescSearchResponse)objTransactionLogsDAL.GetProductDescSearch(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetProductDescSearchResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public FindStockByCountryResponse GetFindStockByCountry(FindStockRequest objRequest)
        {
            FindStockByCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (FindStockByCountryResponse)objTransactionLogsDAL.GetFindStockByCountry(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new FindStockByCountryResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetNonTradingStockBySKUResponse GetNonTradingStockBySku(GetNonTradingStockBySKURequest requestData)
        {
            GetNonTradingStockBySKUResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetNonTradingStockBySKUResponse)objTransactionLogsDAL.GetNonTradingStockBySku(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new GetNonTradingStockBySKUResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Non-Trading Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveTransactionLogResponse SaveTransactionLog(SaveTransactionLogRequest objRequest)
        {
            SaveTransactionLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjTransactionLog = new TransactionLog();
                    ObjTransactionLog = (TransactionLog)objRequest.RequestDynamicData;
                    objRequest.TransactionLogList = ObjTransactionLog.TransactionLogList;
                }
                objResponse = (SaveTransactionLogResponse)objTransactionLogsDAL.InsertRecord(objRequest);
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
                objResponse = new SaveTransactionLogResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveTransactionLogResponse SetOpeningStock(SaveTransactionLogRequest objRequest)
        {
            SaveTransactionLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                objRequest.BaseIntegrateStoreID = objRequest.TransactionLogList.FirstOrDefault().StoreID;
 
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjTransactionLog = new TransactionLog();
                    ObjTransactionLog = (TransactionLog)objRequest.RequestDynamicData;
                    objRequest.TransactionLogList = ObjTransactionLog.TransactionLogList;
                }
                objResponse = (SaveTransactionLogResponse)objTransactionLogsDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TRANSACTIONLOG;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL", "SetOpeningStock");
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL", "SetOpeningStock");

                   // BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL", "SetOpeningStock");

                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveTransactionLogResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "SetOpeningStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetStockByStyleCodeResponse StyleStockOverView(GetStockByStyleCodeRequest objRequest)
        {
            GetStockByStyleCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetStockByStyleCodeResponse)objTransactionLogsDAL.GetStoreStockByStyleOverView(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockByStyleCodeResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Transaction List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetStockBySkuResponse GetStockBySku(GetStockBySkuRequest objRequest)
        {
            GetStockBySkuResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetStockBySkuResponse)objTransactionLogsDAL.GetStockBySku(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockBySkuResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetStockBySkuResponse GetStockBySku1(GetStockBySkuRequest objRequest)
        {
            GetStockBySkuResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetStockBySkuResponse)objTransactionLogsDAL.GetStockBySku1(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockBySkuResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public FindStockResponse GetStoreStockByCountry(FindStockRequest objRequest)
        {
            FindStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (FindStockResponse)objTransactionLogsDAL.GetStoreStockByCountry(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new FindStockResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public FindStockResponse GetStyleSummary(FindStockRequest objRequest)
        {
            FindStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (FindStockResponse)objTransactionLogsDAL.GetStyleSummary(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new FindStockResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetQuantityBySKUResponse GetQuantityBySKU(GetQuantityBySKURequest objRequest)
        {
            GetQuantityBySKUResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetQuantityBySKUResponse)objTransactionLogsDAL.GetQuantityBySku(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetQuantityBySKUResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetStockByStyleCodeResponse GetStockPivotByStyle(GetStockByStyleCodeRequest objRequest)
        {
            GetStockByStyleCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetStockByStyleCodeResponse)objTransactionLogsDAL.GetStockPivotByStyle(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockByStyleCodeResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetProductCommonSearchResponse GetProductSearch(GetProductCommonSearchRequest objRequest)
        {
            GetProductCommonSearchResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetTransactionLogsDAL();
                objResponse = (GetProductCommonSearchResponse)objTransactionLogsDAL.GetProductCommonSearch(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetProductCommonSearchResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Data");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


    }
}
