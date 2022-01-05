using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
    public class StockRequestBLL
    {
        public SaveStockRequestResponse SaveStockRequest(SaveStockRequestRequest objRequest)
        {
            SaveStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockRequest = new StockRequestHeader();
                    objStockRequest = (StockRequestHeader)objRequest.RequestDynamicData;
                    objRequest.StockRequestHeaderRecord = objStockRequest;
                    objRequest.StockRequestDetailsList = objStockRequest.StockRequestDetailsList;
                }
                objResponse = (SaveStockRequestResponse)objBaseStockRequestDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKREQUEST;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "SaveStockRequest");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveStockRequestResponse Saveint_stock(SaveStockRequestRequest objRequest)
        {
            SaveStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var Objint_stock = new int_stockrequestTypes();
                    Objint_stock = (int_stockrequestTypes)objRequest.RequestDynamicData;
                    objRequest.int_stockrequestTypesList = Objint_stock.int_stockrequestTypesList;
                }
                objResponse = (SaveStockRequestResponse)objTransactionLogsDAL.Saveint_stock(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKREQUEST;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "Saveint_stock");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "int_stock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateStockRequestResponse UpdateStockRequest(UpdateStockRequestRequest objRequest)
        {
            UpdateStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (UpdateStockRequestResponse)objBaseStockRequestDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKREQUEST;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "UpdateStockRequest");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteStockRequestResponse DeleteStockRequest(DeleteStockRequestRequest objRequest)
        {
            DeleteStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (DeleteStockRequestResponse)objBaseStockRequestDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKREQUEST;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockRequestBLL", "DeleteStockRequest");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockRequestResponse SelectAllInt_ConfirmTransfer(SelectAllStockRequestRequest objRequest)
        {
            SelectAllStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectAllStockRequestResponse)objBaseStockRequestDAL.SelectAllInt_ConfirmTransfer(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockRequestResponse SelectAllStockRequest(SelectAllStockRequestRequest objRequest)
        {
            SelectAllStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectAllStockRequestResponse)objBaseStockRequestDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockRequestResponse API_SelectALL(SelectAllStockRequestRequest objRequest)
        {
            SelectAllStockRequestResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectAllStockRequestResponse)objBaseStockRequestDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockRequestResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStockRequestIDResponse SelectStockRequestRecord(SelectByStockRequestIDRequest objRequest)
        {
            SelectByStockRequestIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByStockRequestIDResponse)objBaseStockRequestDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockRequestIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStockRequestDetailsResponse SelectStockRequestDetails(SelectByStockRequestDetailsRequest objRequest)
        {
            SelectByStockRequestDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDetailsDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectByStockRequestDetailsResponse)objBaseStockRequestDetailsDAL.SelectByStockRequestDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockRequestDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectByStockRequestDetailsResponse Selectint_stockreceiptDetails(SelectByStockRequestDetailsRequest objRequest)
        {
            SelectByStockRequestDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDetailsDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectByStockRequestDetailsResponse)objBaseStockRequestDetailsDAL.Selectint_stockreceiptDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockRequestDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "stockreceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectByStockRequestDetailsResponse SelectWithOutint_stockreceipt(SelectByStockRequestDetailsRequest objRequest)
        {
            SelectByStockRequestDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDetailsDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectByStockRequestDetailsResponse)objBaseStockRequestDetailsDAL.SelectWithOutint_stockreceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockRequestDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "stockreceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectByStockRequestDetailsResponse SelectStockRequestHeaderID(SelectByStockRequestDetailsRequest objRequest)
        {
            SelectByStockRequestDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDetailsDAL = objFactory.GetDALRepository().GetStockRequestDAL();
                objResponse = (SelectByStockRequestDetailsResponse)objBaseStockRequestDetailsDAL.SelectByStockRequestHeaderID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockRequestDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
