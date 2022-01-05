using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizIView.Transactions.IStockReceipt;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.StockRequest;

namespace EasyBizPresenter.Transactions.Stocks
{
    public class StockReceiptPresenter
    {
        IStockReceiptView _IStockReceiptView;
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        StockReceiptBLL _StockReceiptBLL = new StockReceiptBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        StockRequestBLL _StockRequestBLL = new StockRequestBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        int _RunningNo;
        int _DetailID;
        public StockReceiptPresenter(IStockReceiptView ViewObj)
        {
            _IStockReceiptView = ViewObj;
        }

        public StockReceiptPresenter()
        {
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStockReceiptView.Type == true  )
            {
                if (_IStockReceiptView.DocumentNo == "")
                {
                    _IStockReceiptView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
                }
                else if (_IStockReceiptView.DocumentDate == null)
                {
                    _IStockReceiptView.Message = "DocumentDate is missing Please Enter it.";
                }               
                else if (_IStockReceiptView.StockReceiptDetailsList.Count == 0)
                {
                    _IStockReceiptView.Message = "StockReceiptDetails is missing Please Select it.";
                }
                else if (_IStockReceiptView.Status == string.Empty)
                {
                    _IStockReceiptView.Message = "Status is missing Please Select it.";
                }
                else if (_IStockReceiptView.ReceivedType == string.Empty)
                {
                    _IStockReceiptView.Message = "Please select the received type.";
                }
                else
                {
                    objBool = true;
                }
                return objBool;

            }
            else if (_IStockReceiptView.DocumentDate == null)
            {
                _IStockReceiptView.Message = "DocumentDate is missing Please Enter it.";
            }
            else if (_IStockReceiptView.WithOutBaseDoc==false)
            {
                if (_IStockReceiptView.StockRequestID == 0)
                {
                    _IStockReceiptView.Message = "StockRequest DocumentNo is missing. Please Select it.";
                }
                else if (_IStockReceiptView.StockReceiptDetailsList.Count == 0)
                {
                    _IStockReceiptView.Message = "StockReceiptDetails is missing Please Select it.";
                }
                else if (_IStockReceiptView.Status == string.Empty)
                {
                    _IStockReceiptView.Message = "Status is missing Please Select it.";
                }
                else if (_IStockReceiptView.ReceivedType == string.Empty)
                {
                    _IStockReceiptView.Message = "Please select the received type.";
                }
                else
                {
                    objBool = true;
                }
            }

            else if (_IStockReceiptView.StockReceiptDetailsList.Count == 0)
            {
                _IStockReceiptView.Message = "StockReceiptDetails is missing Please Select it.";
            }
            else if (_IStockReceiptView.Status == string.Empty)
            {
                _IStockReceiptView.Message = "Status is missing Please Select it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void GetSKU()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _IStockReceiptView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //_IStockReceiptView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
                else
                {
                    //_IStockReceiptView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveAndUpdateIntConfirmtransfer()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStockReceiptRequest();
                    RequestData.StockReceiptHeaderRecord = new StockReceiptHeader();
                    RequestData.StockReceiptDetailsList = _IStockReceiptView.StockReceiptDetailsList;
                    RequestData.TransactionLogList = _IStockReceiptView.TransactionLogList;
                    RequestData.int_stockreceiptList = _IStockReceiptView.int_stockreceiptList;
                    RequestData.StockReceiptHeaderRecord.ID = _IStockReceiptView.ID;                  
                    RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
                    RequestData.StockReceiptHeaderRecord.StockRequestID = _IStockReceiptView.StockRequestID;
                    RequestData.StockReceiptHeaderRecord.DocumentNo = _IStockReceiptView.DocumentNo;
                    RequestData.StockReceiptHeaderRecord.DocumentDate = _IStockReceiptView.DocumentDate;
                    RequestData.StockReceiptHeaderRecord.TotalQuantity = _IStockReceiptView.TotalQuantity;
                    RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity = _IStockReceiptView.TotalReceivedQuantity;
                    RequestData.StockReceiptHeaderRecord.FromWareHouseID = _IStockReceiptView.FromWareHouseID;
                    RequestData.StockReceiptHeaderRecord.FromWarehouseCode = _IStockReceiptView.FromWarehouseCode;
                    RequestData.StockReceiptHeaderRecord.Fromwarehousename = _IStockReceiptView.Fromwarehousename;
                    RequestData.StockReceiptHeaderRecord.WithOutBaseDoc = _IStockReceiptView.WithOutBaseDoc;
                    RequestData.StockReceiptHeaderRecord.StockRequestStatus = "Closed";
                    RequestData.StockReceiptHeaderRecord.Status = _IStockReceiptView.Status;
                    RequestData.StockReceiptHeaderRecord.StoreID = _IStockReceiptView.StoreID;
                    RequestData.StockReceiptHeaderRecord.StoreCode = _IStockReceiptView.StoreCode;
                    RequestData.StockReceiptHeaderRecord.Remarks = _IStockReceiptView.Remarks;
                    RequestData.StockReceiptHeaderRecord.CreateBy = _IStockReceiptView.UserID;
                    RequestData.StockReceiptHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.StockReceiptHeaderRecord.Active = true;
                    RequestData.StockReceiptHeaderRecord.Type = _IStockReceiptView.Type;
                    RequestData.StockReceiptHeaderRecord.SCN = _IStockReceiptView.SCN;
                    var ResponseData = _StockReceiptBLL.SaveAndUpdateIntConfirmtransfer(RequestData);
                    _IStockReceiptView.Message = ResponseData.DisplayMessage;
                    _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.);
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.IDs);
                    if (_IStockReceiptView.ProcessStatus == Enums.OpStatusCode.Success)
                    {
                        _IStockReceiptView.StockReceiptHeaderID = Convert.ToInt32(ResponseData.IDs);
                        UpdateRunningNo();
                        
                    }
                    else
                    {
                        _IStockReceiptView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
                else
                {

                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveStockReceipt()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStockReceiptRequest();
                    RequestData.StockReceiptHeaderRecord = new StockReceiptHeader();
                    RequestData.StockReceiptDetailsList = _IStockReceiptView.StockReceiptDetailsList;
                    RequestData.TransactionLogList = _IStockReceiptView.TransactionLogList;
                    RequestData.StockReceiptHeaderRecord.ID = _IStockReceiptView.ID;
                    RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
                    RequestData.StockReceiptHeaderRecord.StockRequestID = _IStockReceiptView.StockRequestID;
                    RequestData.StockReceiptHeaderRecord.DocumentNo = _IStockReceiptView.DocumentNo;
                    RequestData.StockReceiptHeaderRecord.DocumentDate = _IStockReceiptView.DocumentDate;
                    RequestData.StockReceiptHeaderRecord.TotalQuantity = _IStockReceiptView.TotalQuantity;
                    RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity = _IStockReceiptView.TotalReceivedQuantity;
                    RequestData.StockReceiptHeaderRecord.FromWareHouseID = _IStockReceiptView.FromWareHouseID;
                    RequestData.StockReceiptHeaderRecord.FromWarehouseCode = _IStockReceiptView.FromWarehouseCode;
                    RequestData.StockReceiptHeaderRecord.Fromwarehousename = _IStockReceiptView.Fromwarehousename;
                    RequestData.StockReceiptHeaderRecord.WithOutBaseDoc = _IStockReceiptView.WithOutBaseDoc;
                    if (_IStockReceiptView.IsReceiptComplete != true)
                    {
                        RequestData.StockReceiptHeaderRecord.StockRequestStatus = "Open";
                    }
                    else
                    {
                        RequestData.StockReceiptHeaderRecord.StockRequestStatus = "Closed";
                    }
                    RequestData.StockReceiptHeaderRecord.Status = _IStockReceiptView.Status;
                    RequestData.StockReceiptHeaderRecord.StoreID = _IStockReceiptView.StoreID;
                    RequestData.StockReceiptHeaderRecord.StoreCode = _IStockReceiptView.StoreCode;
                    RequestData.StockReceiptHeaderRecord.Remarks = _IStockReceiptView.Remarks;
                    RequestData.StockReceiptHeaderRecord.CreateBy = _IStockReceiptView.UserID;
                    RequestData.StockReceiptHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.StockReceiptHeaderRecord.Active = true;
                    RequestData.StockReceiptHeaderRecord.Type = _IStockReceiptView.Type;
                    RequestData.StockReceiptHeaderRecord.SCN = _IStockReceiptView.SCN;
                    RequestData.StockReceiptHeaderRecord.ReceivedType = _IStockReceiptView.ReceivedType;

                    var ResponseData = _StockReceiptBLL.SaveStockReceipt(RequestData);
                    _IStockReceiptView.Message = ResponseData.DisplayMessage;
                    _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.);
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.IDs);
                    if (_IStockReceiptView.ProcessStatus == Enums.OpStatusCode.Success)
                    {
                        _IStockReceiptView.StockReceiptHeaderID = Convert.ToInt32(ResponseData.IDs);
                        UpdateRunningNo();
                    }
                    else
                    {
                        _IStockReceiptView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStockReceipt()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStockReceiptRequest();
                    RequestData.StockReceiptHeaderRecord = new StockReceiptHeader();
                    RequestData.StockReceiptDetailsList = _IStockReceiptView.StockReceiptDetailsList;
                    RequestData.TransactionLogList = _IStockReceiptView.TransactionLogList;
                    RequestData.RFIDTagList = _IStockReceiptView.RFIDList;

                    RequestData.StockReceiptHeaderRecord.ID = _IStockReceiptView.ID;
                    RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
                    RequestData.StockReceiptHeaderRecord.StockRequestID = _IStockReceiptView.StockRequestID;
                    RequestData.StockReceiptHeaderRecord.DocumentNo = _IStockReceiptView.DocumentNo;
                    RequestData.StockReceiptHeaderRecord.DocumentDate = _IStockReceiptView.DocumentDate;
                    RequestData.StockReceiptHeaderRecord.TotalQuantity = _IStockReceiptView.TotalQuantity;
                    RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity = _IStockReceiptView.TotalReceivedQuantity;
                    RequestData.StockReceiptHeaderRecord.WithOutBaseDoc = _IStockReceiptView.WithOutBaseDoc;
                    RequestData.StockReceiptHeaderRecord.FromWareHouseID = _IStockReceiptView.FromWareHouseID;
                    RequestData.StockReceiptHeaderRecord.FromWarehouseCode = _IStockReceiptView.FromWarehouseCode;
                    RequestData.StockReceiptHeaderRecord.Fromwarehousename = _IStockReceiptView.Fromwarehousename;                    
                    RequestData.StockReceiptHeaderRecord.StoreID = _IStockReceiptView.StoreID;
                    RequestData.StockReceiptHeaderRecord.StoreCode = _IStockReceiptView.StoreCode;
                    RequestData.StockReceiptHeaderRecord.Remarks = _IStockReceiptView.Remarks;
                    RequestData.StockReceiptHeaderRecord.Type = _IStockReceiptView.Type;
                    RequestData.StockReceiptHeaderRecord.Status = "Closed";
                    RequestData.StockReceiptHeaderRecord.StockRequestStatus = "Closed";
                    RequestData.StockReceiptHeaderRecord.CreateBy = _IStockReceiptView.UserID;
                    RequestData.StockReceiptHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.StockReceiptHeaderRecord.Active = true;
                    RequestData.StockReceiptHeaderRecord.SCN = _IStockReceiptView.SCN;
                    RequestData.StockReceiptHeaderRecord.ReceivedType = _IStockReceiptView.ReceivedType;

                    var ResponseData = _StockReceiptBLL.SaveStockReceipt(RequestData);
                    _IStockReceiptView.Message = ResponseData.DisplayMessage;
                    _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.);
                    //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.IDs);
                }
                if (_IStockReceiptView.ProcessStatus == Enums.OpStatusCode.Success)
                {
                    UpdateRunningNo();
                }
                else
                {
                    _IStockReceiptView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRunningNo()
        {
            try
            {
                UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
                UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

                objUpdateRunningNumRequest.RunningNo = _RunningNo;
                objUpdateRunningNumRequest.DetailID = _DetailID;
                objUpdateRunningNumRequest.StoreID = _IStockReceiptView.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTransactionsLog()
        {
            var RequestData = new SaveTransactionLogRequest();

            RequestData.TransactionLogList = _IStockReceiptView.TransactionLogList;
            var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);
           
        }
        public void saveintStockReceipt()
        {
            var RequestData = new SaveStockReceiptRequest();

            RequestData.int_stockreceiptList = _IStockReceiptView.int_stockreceiptList;
            var ResponseData = _StockReceiptBLL.saveintStockReceipt(RequestData);

        }

        
        public void Saveint_stockreceipt()
        {
            var RequestData = new SaveStockReceiptRequest();
            RequestData.StockReceiptHeaderRecord = new StockReceiptHeader();
            RequestData.int_stockreceiptList = _IStockReceiptView.int_stockreceiptList;
            RequestData.StockReceiptHeaderRecord.ID = _IStockReceiptView.ID;
            RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
            RequestData.StockReceiptHeaderRecord.StockRequestID = _IStockReceiptView.StockRequestID;
            RequestData.StockReceiptHeaderRecord.DocumentNo = _IStockReceiptView.DocumentNo;
            RequestData.StockReceiptHeaderRecord.DocumentDate = _IStockReceiptView.DocumentDate;
            RequestData.StockReceiptHeaderRecord.TotalQuantity = _IStockReceiptView.TotalQuantity;
            RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity = _IStockReceiptView.TotalReceivedQuantity;
            RequestData.StockReceiptHeaderRecord.FromWareHouseID = _IStockReceiptView.FromWareHouseID;
            RequestData.StockReceiptHeaderRecord.FromWarehouseCode = _IStockReceiptView.FromWarehouseCode;
            RequestData.StockReceiptHeaderRecord.Fromwarehousename = _IStockReceiptView.Fromwarehousename;

            RequestData.StockReceiptHeaderRecord.Status = _IStockReceiptView.Status;
            RequestData.StockReceiptHeaderRecord.StoreID = _IStockReceiptView.StoreID;
            RequestData.StockReceiptHeaderRecord.StoreCode = _IStockReceiptView.StoreCode;
            RequestData.StockReceiptHeaderRecord.Remarks = _IStockReceiptView.Remarks;
            RequestData.StockReceiptHeaderRecord.CreateBy = _IStockReceiptView.UserID;
            RequestData.StockReceiptHeaderRecord.CreateOn = DateTime.Now;
            RequestData.StockReceiptHeaderRecord.Active = true;
            RequestData.StockReceiptHeaderRecord.Type = _IStockReceiptView.Type;
            RequestData.StockReceiptHeaderRecord.SCN = _IStockReceiptView.SCN;
            


          
            var ResponseData = _StockReceiptBLL.Saveint_stockreceipt(RequestData);
            _IStockReceiptView.Message = ResponseData.DisplayMessage;
            _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;

            if (_IStockReceiptView.ProcessStatus == Enums.OpStatusCode.Success)
            {
                UpdateRunningNo();
            }
            else
            {
                _IStockReceiptView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }

        }

       
        public void SelectStockReceiptHeaderRecord()
        {
            try
            {
                var RequestData = new SelectByStockReceiptIDRequest();
                RequestData.ID = _IStockReceiptView.ID;
                var ResponseData = _StockReceiptBLL.SelectStockReceiptRecord(RequestData);              

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptView.ID = ResponseData.StockReceiptHeaderRecord.ID;
                    _IStockReceiptView.HeaderID = ResponseData.StockReceiptHeaderRecord.ID;
                    _IStockReceiptView.StockRequestID = ResponseData.StockReceiptHeaderRecord.StockRequestID;
                    _IStockReceiptView.StockRequestDocumentNo = ResponseData.StockReceiptHeaderRecord.StockRequestDocumentNo;
                    _IStockReceiptView.DataFrom = ResponseData.StockReceiptHeaderRecord.DataFrom;
                    _IStockReceiptView.FromWareHouseID = ResponseData.StockReceiptHeaderRecord.FromWareHouseID;
                    _IStockReceiptView.FromWarehouseCode = ResponseData.StockReceiptHeaderRecord.FromWarehouseCode;
                    _IStockReceiptView.Fromwarehousename = ResponseData.StockReceiptHeaderRecord.Fromwarehousename;
                    _IStockReceiptView.TotalReceivedQuantity = ResponseData.StockReceiptHeaderRecord.TotalReceivedQuantity;
                    _IStockReceiptView.WithOutBaseDoc = ResponseData.StockReceiptHeaderRecord.WithOutBaseDoc;
                    _IStockReceiptView.DocumentDate = ResponseData.StockReceiptHeaderRecord.DocumentDate;
                    _IStockReceiptView.fromApplication = ResponseData.StockReceiptHeaderRecord.fromApplication;
                    _IStockReceiptView.DocumentNo = ResponseData.StockReceiptHeaderRecord.DocumentNo;
                    _IStockReceiptView.Remarks = ResponseData.StockReceiptHeaderRecord.Remarks;
                    _IStockReceiptView.StockRequestID = ResponseData.StockReceiptHeaderRecord.StockRequestID;
                    _IStockReceiptView.Status = ResponseData.StockReceiptHeaderRecord.Status;
                    _IStockReceiptView.Type = ResponseData.StockReceiptHeaderRecord.Type;
                    _IStockReceiptView.SCN = ResponseData.StockReceiptHeaderRecord.SCN;
                    _IStockReceiptView.ReceivedType = ResponseData.StockReceiptHeaderRecord.ReceivedType;

                    //SelectStockReceiptDetails();
                    _IStockReceiptView.StockReceiptDetailsList = ResponseData.StockReceiptHeaderRecord.StockReceiptDetailsList;
                    _IStockReceiptView.RFIDList = ResponseData.StockReceiptHeaderRecord.RFIDList;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStockReceiptView.Message = ResponseData.DisplayMessage;
                }
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public void SelectStock2ndReceiptHeaderRecord()
        {
            try
            {
                var RequestData = new SelectByStockReceiptIDRequest();
                RequestData.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
                var ResponseData = _StockReceiptBLL.SelectStockReceiptRecord(RequestData);
                _IStockReceiptView.ID = ResponseData.StockReceiptHeaderRecord.ID;
                _IStockReceiptView.HeaderID = ResponseData.StockReceiptHeaderRecord.ID;                
                _IStockReceiptView.FromWareHouseID = ResponseData.StockReceiptHeaderRecord.FromWareHouseID;
                _IStockReceiptView.FromWarehouseCode = ResponseData.StockReceiptHeaderRecord.FromWarehouseCode;
                _IStockReceiptView.Fromwarehousename = ResponseData.StockReceiptHeaderRecord.Fromwarehousename;
                _IStockReceiptView.TotalReceivedQuantity = ResponseData.StockReceiptHeaderRecord.TotalReceivedQuantity;
                _IStockReceiptView.DocumentDate = ResponseData.StockReceiptHeaderRecord.DocumentDate;
                _IStockReceiptView.fromApplication = ResponseData.StockReceiptHeaderRecord.fromApplication;
                _IStockReceiptView.DocumentNo = ResponseData.StockReceiptHeaderRecord.DocumentNo;
                _IStockReceiptView.Remarks = ResponseData.StockReceiptHeaderRecord.Remarks;
                _IStockReceiptView.StockRequestID = ResponseData.StockReceiptHeaderRecord.StockRequestID;
                _IStockReceiptView.Status = ResponseData.StockReceiptHeaderRecord.Status;
                _IStockReceiptView.Type = ResponseData.StockReceiptHeaderRecord.Type;

                _IStockReceiptView.SCN = ResponseData.StockReceiptHeaderRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStockReceiptView.Message = ResponseData.DisplayMessage;
                }              
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTransactionLog()
        {
            var RequestData = new SelectByStockReceiptDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.DocumentNumber = _IStockReceiptView.DocumentNo;
            var ResponseData = _StockReceiptBLL.SelectStockReceiptTransactionDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockReceiptTransactionDetailsList = ResponseData.StockReceiptHeaderList;
            }
            else
            {
                _IStockReceiptView.StockReceiptTransactionDetailsList = ResponseData.StockReceiptHeaderList;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }



        public void SelectStockReceiptDetails()
        {
            var RequestData = new SelectByStockReceiptDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IStockReceiptView.ID;
            var ResponseData = _StockReceiptBLL.SelectStockReceiptDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockReceiptDetailsList = ResponseData.StockReceiptDetailsRecord;
            }
            else
            {
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStockRequestDetails()
        {
            var RequestData = new SelectByStockRequestDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IStockReceiptView.StockRequestID;
            var ResponseData = _StockRequestBLL.SelectStockRequestDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockRequestDetailsList = ResponseData.StockRequestDetailsRecord;
            }
            else
            {
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void Selectint_stockreceipt()
        {
            var RequestData = new SelectByStockRequestDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
            RequestData.StoreCode = _IStockReceiptView.StoreCode;
            var ResponseData = _StockRequestBLL.Selectint_stockreceiptDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.int_stockreceiptList = ResponseData.int_stockreceiptRecord;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;             
            }
            else
            {
                _IStockReceiptView.int_stockreceiptList = ResponseData.int_stockreceiptRecord;
               // _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectWithOutint_stockreceipt()
        {
            var RequestData = new SelectByStockRequestDetailsRequest();
            RequestData.WithOutBaseDoc = _IStockReceiptView.WithOutBaseDoc;
            RequestData.StoreCode = _IStockReceiptView.StoreCode;
            var ResponseData = _StockRequestBLL.SelectWithOutint_stockreceipt(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.int_stockreceiptList = ResponseData.int_stockreceiptRecord;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IStockReceiptView.int_stockreceiptList = ResponseData.int_stockreceiptRecord;
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStockRequestHeaderID()
        {
            var RequestData = new SelectByStockRequestDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.StockRequestDocumentNo = _IStockReceiptView.StockRequestDocumentNo;
            var ResponseData = _StockRequestBLL.SelectStockRequestHeaderID(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockRequestHeaderRecord = ResponseData.StockRequestHeaderRecord;
            }
            else
            {
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void Int_ConfirmTransfer()
        {
            SelectAllStockRequestRequest RequestData = new SelectAllStockRequestRequest();
            RequestData.Mode = "New";
            RequestData.StoreCode = _IStockReceiptView.StoreCode;
            var ResponseData = _StockRequestBLL.SelectAllInt_ConfirmTransfer(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockRequestHeaderLookUp = ResponseData.StockRequestHeaderList;
            }
            else
            {
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStockRequestHeaderList()
        {
            SelectAllStockRequestRequest RequestData = new SelectAllStockRequestRequest();
            RequestData.Mode = "New";
            //RequestData.ID = _IStockReceiptView.ID;
            var ResponseData = _StockRequestBLL.SelectAllStockRequest(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockRequestHeaderLookUp = ResponseData.StockRequestHeaderList;
            }
            else
            {
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStockRequestHeaderListEditMode()
        {
            SelectAllStockRequestRequest RequestData = new SelectAllStockRequestRequest();
            RequestData.Mode = "Edit";
            //RequestData.ID = _IStockReceiptView.ID;
            var ResponseData = _StockRequestBLL.SelectAllStockRequest(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StockRequestHeaderLookUp = ResponseData.StockRequestHeaderList;
            }
            else
            {
                _IStockReceiptView.StockRequestHeaderLookUp = new List<StockRequestHeader>();
                //_IStockReceiptView.Message = ResponseData.DisplayMessage;
                //_IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteStockReceipt()
        {
            try
            {
                var RequestData = new DeleteStockReceiptRequest();
                RequestData.ID = _IStockReceiptView.ID;
                var ResponseData = _StockReceiptBLL.DeleteStockReceipt(RequestData);
                _IStockReceiptView.Message = ResponseData.DisplayMessage;
                _IStockReceiptView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectAllSKUMaster()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();

                RequestData.ShowInActiveRecords = true;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //_IStockReceiptView.SKUMasterList = ResponseData.SKUMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IStockReceiptView.StoreID;
            RequestData.StoreCode = _IStockReceiptView.StoreCode;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReceiptView.StoreMasterRecord = ResponseData.StoreMasterData;
            }
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.STOCKRECEIPT;
                RequestData.StoreID = _IStockReceiptView.StoreID;
                RequestData.StoreCode = _IStockReceiptView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IStockReceiptView.DocumentNo = DocumentNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class StockReceiptListPresenter
    {
        IStockReceiptCollectionView _IStockReceiptCollectionView;
        StockReceiptBLL _StockReceiptBLL = new StockReceiptBLL();
        public StockReceiptListPresenter(IStockReceiptCollectionView ViewObj)
        {
            _IStockReceiptCollectionView = ViewObj;
        }

        public void GetStockReceiptlist()
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStockReceiptResponse();
                ResponseData = _StockReceiptBLL.SelectAllStockReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptCollectionView.StockReceiptHeaderList = ResponseData.StockReceiptHeaderList;
                }
                else
                {
                    _IStockReceiptCollectionView.StockReceiptHeaderList = new List<StockReceiptHeader>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveStockReceiptListWms()
        {
            var RequestData = new SaveStockReceiptRequest();
            var NewStockReceiptList = new List<StockReceiptHeader>();
            var StockReceiptHeaderListWms1 = _IStockReceiptCollectionView.StockReceiptHeaderListWms;
            List<List<StockReceiptHeader>> GroupByList = StockReceiptHeaderListWms1.GroupBy(d => d.DocumentNo).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();


            foreach (List<StockReceiptHeader> objStockReceipHeader in GroupByList)
            {
                var _StockReceipHeader = objStockReceipHeader.FirstOrDefault();

                NewStockReceiptList.Add(_StockReceipHeader);
            }
            RequestData.StockReceiptHeaderListWms = NewStockReceiptList;
            RequestData.StockReceiptDetailsListWms = _IStockReceiptCollectionView.StockReceiptDetailsListWms;
            RequestData.StoreCode = _IStockReceiptCollectionView.StoreCode;
            RequestData.RFIDTagList = _IStockReceiptCollectionView.RFIDTagList;
            var ResponseData = _StockReceiptBLL.SaveStockReceiptlistWms(RequestData);

            if(ResponseData.StatusCode != Enums.OpStatusCode.Success)
            {

            }
        }
        public void SaveStockReceiptListWmsConfirmTransfer()
        {
            var RequestData = new SaveStockReceiptListWmsConfirmTransferRequest();
            RequestData.int_stockreceiptConfirmTransfer = _IStockReceiptCollectionView.int_stockreceiptConfirmTransfer;
            var ResponseData = _StockReceiptBLL.SaveStockReceiptListWmsConfirmTransfer(RequestData);
            //if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
            //{
            //    throw new Exception(ResponseData.DisplayMessage);
            //}
        }
        public void GetStockReceiptlistWms()
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreCode = _IStockReceiptCollectionView.StoreCode;
                var ResponseData = new SelectAllStockReceiptResponse();
                ResponseData = _StockReceiptBLL.SelectAllStockReceiptWms(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptCollectionView.int_stockreceiptListWms = ResponseData.int_stockreceiptRecord;
                    _IStockReceiptCollectionView.RFIDTagList = ResponseData.RFIDTagList;
                }
                //else
                //{
                //    _IStockReceiptCollectionView.Message = ResponseData.DisplayMessage;
                //    _IStockReceiptCollectionView.int_stockreceiptListWms = new List<int_stockreceipt>();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockReceiptlistWmsFlagCheck()
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStockReceiptResponse();
                ResponseData = _StockReceiptBLL.SelectAllStockReceiptWmsFlagCheck(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptCollectionView.StockReceiptHeaderListWmsFlagCheck = ResponseData.StockReceiptHeaderListwmsFlag;                   
                    SaveStockReceiptListWmsFlagCheck();
                }
                //else
                //{
                //    _IStockReceiptCollectionView.StockReceiptHeaderListWmsFlagCheck = new List<StockReceiptHeader>();
                //    _IStockReceiptCollectionView.Message = ResponseData.DisplayMessage;
                //}
            }
            catch (Exception ex)
            {
                _IStockReceiptCollectionView.Message = ex.Message;
            }
        }
        public void SaveStockReceiptListWmsFlagCheck()
        {
            var RequestData = new SaveStockReceiptRequest();
            RequestData.StockReceiptHeaderListWmsFlagCheck = _IStockReceiptCollectionView.StockReceiptHeaderListWmsFlagCheck;            
            var ResponseData = _StockReceiptBLL.SaveStockReceiptListWmsFlagCheck(RequestData);
            if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
            {
                throw new Exception(ResponseData.DisplayMessage);
            }
        }
        public void GetStockReceiptDetailsForFlaglist()
        {
            try
            {
                var RequestData = new SelectAllStockReceiptDetailsRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStockReceiptDetailsResponse();
                ResponseData = _StockReceiptBLL.SelectAllStockReceiptDetailsForFlaglist(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptCollectionView.StockReceiptDetailsListWmsFlag = ResponseData.StockReceiptDetailswmsFlag;
                }
                else
                {
                    _IStockReceiptCollectionView.StockReceiptDetailsListWmsFlag = ResponseData.StockReceiptDetailswmsFlag;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
    public class StockReceiptReportViewPresenter
    {
        IStockReceiptHeaderReportView _IStockReceiptHeaderReportView;
        public StockReceiptReportViewPresenter(IStockReceiptHeaderReportView ViewObj)
        {
            _IStockReceiptHeaderReportView = ViewObj;
        }
        public void GetStockReceiptRecord()
        {
            try
            {
                var _StockReceiptBLL = new StockReceiptBLL();
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.FromDate = _IStockReceiptHeaderReportView.FromDate;
                RequestData.ToDate = _IStockReceiptHeaderReportView.ToDate;
                var ResponseData = _StockReceiptBLL.GetStockReceiptHeaderReport(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptHeaderReportView.StockReceiptHeaderList = ResponseData.StockReceiptHeaderList;
                }
                else
                {
                    _IStockReceiptHeaderReportView.StockReceiptHeaderList = new List<StockReceiptHeader>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class StockReceiptDetailReportViewPresenter
    {
        IStockReceiptDetailsReportView _IStockReceiptDetailsReportView;
        public StockReceiptDetailReportViewPresenter(IStockReceiptDetailsReportView ViewObj)
        {
            _IStockReceiptDetailsReportView = ViewObj;
        }
        public void GetStockReceiptDetailRecord()
        {
            try
            {
                var _StockReceiptBLL = new StockReceiptBLL();
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.FromDate = _IStockReceiptDetailsReportView.FromDate;
                RequestData.ToDate = _IStockReceiptDetailsReportView.ToDate;
                var ResponseData = _StockReceiptBLL.GetStockReceiptDetailsReport(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptDetailsReportView.StockReceiptDetailsList = ResponseData.StockReceiptDetailsList;
                }
                else
                {
                    _IStockReceiptDetailsReportView.StockReceiptDetailsList = new List<StockReceiptDetails>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
