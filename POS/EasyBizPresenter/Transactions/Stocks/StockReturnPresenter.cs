using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizIView.Transactions.IStockReturn;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using Newtonsoft.Json;
using EasyBizDBTypes.Masters;

namespace EasyBizPresenter.Transactions.Stocks
{
    public class StockReturnPresenter
    {
        IStockReturnView _IStockReturnView;
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        StockReturnBLL _StockReturnBLL = new StockReturnBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        WarehouseMasterBLL _WarehouseMasterBLL = new WarehouseMasterBLL();
        int _RunningNo;
        int _DetailID;
        public StockReturnPresenter(IStockReturnView ViewObj)
        {
            _IStockReturnView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStockReturnView.DocumentNo == "")
            {
                _IStockReturnView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
            }
            else if (_IStockReturnView.DocumentDate == null)
            {
                _IStockReturnView.Message = "DocumentDate is missing Please Enter it.";
            }
            else if (_IStockReturnView.ToWareHouseID == 0)
            {
                _IStockReturnView.Message = "ToWareHouse is missing Please Enter it.";
            }
            //else if (_IStockReturnView.SKUMasterList == null)
            //{
            //    _IStockReturnView.Message = "Reason is missing Please Select it.";
            //}
            else if (_IStockReturnView.StockReturnDetailsList.Count == 0)
            {
                _IStockReturnView.Message = "StockReturnDetails is missing Please Select it.";
            }
            else if (_IStockReturnView.Status == string.Empty)
            {
                _IStockReturnView.Message = "Status is missing Please Select it.";
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
                RequestData.SearchString = _IStockReturnView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
                else
                {
                    _IStockReturnView.SKUMasterTypesList = new List<SKUMasterTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockBySKU()
        {
            try
            {
                var RequestData = new GetStockBySkuRequest();
                var ResponseData = new GetStockBySkuResponse();
                RequestData.SKUCode = _IStockReturnView.SKUMasterTypesList.FirstOrDefault().SKUCode;
                RequestData.StoreID = _IStockReturnView.StoreID;
                ResponseData = _TransactionLogBLL.GetStockBySku1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnView.ScaleWiseStockList = ResponseData.ScaleWiseStockList;
                }
                else
                {
                    _IStockReturnView.ScaleWiseStockList = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void GetSKU()
        //{
        //    try
        //    {
        //        var RequestData = new GetStockBySkuRequest();
        //        var ResponseData = new GetStockBySkuResponse();
        //        RequestData.SKUCode = _IStockReturnView.SKUSearchString;
        //        ResponseData = _TransactionLogBLL.GetStockBySku(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IStockReturnView.StockData = ResponseData.StockData;
        //        }
        //        else
        //        {
        //            _IStockReturnView.StockData = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void SaveStockReturn()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStockReturnRequest();
                    RequestData.StockReturnHeaderRecord = new StockReturnHeader();
                    var NewStockReqList = new List<StockReturnDetails>();
                    var StockReqList = _IStockReturnView.StockReturnDetailsList;
                    // var StockReqList = _IStockReturnView.StockReturnDetailsList1;
                    List<List<StockReturnDetails>> GroupByList = StockReqList.GroupBy(d => d.SKUCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();
                    foreach (List<StockReturnDetails> objStockRequestDetails in GroupByList)
                    {
                        var _StockRequestDetails = objStockRequestDetails.FirstOrDefault();
                        int Qty = objStockRequestDetails.Sum(x => x.Quantity);

                        if (_StockRequestDetails.StockQty > Qty || _StockRequestDetails.StockQty == Qty)
                        {
                            _StockRequestDetails.Quantity = Qty;
                            NewStockReqList.Add(_StockRequestDetails);
                        }

                    }
                    if (NewStockReqList.Count == GroupByList.Count)
                    {
                        //RequestData.StockReturnDetailsList1 = NewStockReqList;
                        RequestData.StockReturnDetailsList = NewStockReqList;
                        RequestData.TransactionLogList = _IStockReturnView.TransactionLogList;
                        RequestData.StockReturnHeaderRecord.ID = _IStockReturnView.ID;
                        RequestData.StockReturnHeaderRecord.DocumentNo = _IStockReturnView.DocumentNo;
                        RequestData.StockReturnHeaderRecord.DocumentDate = _IStockReturnView.DocumentDate;
                        RequestData.StockReturnHeaderRecord.TotalQuantity = _IStockReturnView.TotalQuantity;
                        RequestData.StockReturnHeaderRecord.ToWareHouseID = _IStockReturnView.ToWareHouseID;
                        RequestData.StockReturnHeaderRecord.ToWareHouseCode = _IStockReturnView.ToWareHouseCode;
                        RequestData.StockReturnHeaderRecord.Status = _IStockReturnView.Status;
                        RequestData.StockReturnHeaderRecord.CreateBy = _IStockReturnView.UserID;
                        RequestData.StockReturnHeaderRecord.FromStoreID = _IStockReturnView.StoreID;
                        RequestData.StockReturnHeaderRecord.StoreCode = _IStockReturnView.StoreCode;
                        RequestData.StockReturnHeaderRecord.Remarks = _IStockReturnView.Remarks;
                        RequestData.StockReturnHeaderRecord.CreateOn = DateTime.Now;
                        RequestData.StockReturnHeaderRecord.Active = true;
                        RequestData.StockReturnHeaderRecord.SCN = _IStockReturnView.SCN;
                        RequestData.StockReturnHeaderRecord.ReturnType = _IStockReturnView.ReturnType;
                        RequestData.StockReturnHeaderRecord.StockReturnDetailsList = new List<StockReturnDetails>();
                        RequestData.StockReturnHeaderRecord.StockReturnDetailsList = NewStockReqList;
                        RequestData.RunningNo = _RunningNo;
                        RequestData.DetailID = _DetailID;
                        RequestData.Username = _IStockReturnView.UserInformation1.UserName;
                        RequestData.Password = _IStockReturnView.UserInformation1.Password;


                        var ResponseData = _StockReturnBLL.SaveStockReturn(RequestData);
                        _IStockReturnView.Message = ResponseData.DisplayMessage;
                        _IStockReturnView.ProcessStatus = ResponseData.StatusCode;
                        _IStockReturnView.StockReturnHeaderID = Convert.ToInt32(ResponseData.IDs);
                    }
                    else
                    {
                        _IStockReturnView.ProcessStatus = Enums.OpStatusCode.QuantityMisMatched;
                    }
                    if (_IStockReturnView.ProcessStatus == Enums.OpStatusCode.Success)
                    {

                        //UpdateRunningNo();
                    }
                }
                else
                {
                    _IStockReturnView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Saveint_stockreturn()
        {
            try
            {
                var RequestData = new SaveStockReturnRequest();
                var NewIntStocketurnList = new List<int_stockreturn>();

                var Int_StockReqList = _IStockReturnView.int_stockreturnList;
                List<List<int_stockreturn>> GroupByList = Int_StockReqList.GroupBy(d => d.SKUCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                foreach (List<int_stockreturn> objInt_StockRequestDetails in GroupByList)
                {
                    var _Int_StockRequestDetails = objInt_StockRequestDetails.FirstOrDefault();
                    int Qty = objInt_StockRequestDetails.Sum(x => x.Quantity);
                    _Int_StockRequestDetails.Quantity = Qty;
                    NewIntStocketurnList.Add(_Int_StockRequestDetails);
                }

                RequestData.int_stockreturnList = NewIntStocketurnList;
                var ResponseData = _StockReturnBLL.Saveint_stock(RequestData);
                _IStockReturnView.Message = ResponseData.DisplayMessage;
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
                objUpdateRunningNumRequest.StoreID = _IStockReturnView.StoreID;

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

            RequestData.TransactionLogList = _IStockReturnView.TransactionLogList;
            var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);

        }
        public void SelectStockReturnHeaderRecord()
        {
            try
            {
                var RequestData = new SelectByStockReturnIDRequest();
                RequestData.ID = _IStockReturnView.ID;
                var ResponseData = _StockReturnBLL.SelectStockReturnRecord(RequestData);
                _IStockReturnView.ID = ResponseData.StockReturnHeaderRecord.ID;
                _IStockReturnView.TotalQuantity = ResponseData.StockReturnHeaderRecord.TotalQuantity;
                _IStockReturnView.ToWareHouseID = ResponseData.StockReturnHeaderRecord.ToWareHouseID;
                _IStockReturnView.DocumentDate = ResponseData.StockReturnHeaderRecord.DocumentDate;
                _IStockReturnView.DocumentNo = ResponseData.StockReturnHeaderRecord.DocumentNo;
                _IStockReturnView.Remarks = ResponseData.StockReturnHeaderRecord.Remarks;
                _IStockReturnView.Status = ResponseData.StockReturnHeaderRecord.Status;

                _IStockReturnView.SCN = ResponseData.StockReturnHeaderRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStockReturnView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IStockReturnView.Message = ResponseData.DisplayMessage;
                }
                _IStockReturnView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetWhareHouseLookUP()
        {
            SelectWhareHouseLookUpRequest RequestData = new SelectWhareHouseLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IStockReturnView.CountryID;
            SelectWhareouseLookUpResponse ResponseData = _WarehouseMasterBLL.SelectWhareHouseLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReturnView.WarehouseMasterLookUp = ResponseData.WarehouseMasterList;
            }
        }
        public void SelectStockReturnDetails()
        {
            var RequestData = new SelectByStockReturnDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IStockReturnView.ID;
            var ResponseData = _StockReturnBLL.SelectStockReturnDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockReturnView.StockReturnDetailsList = ResponseData.StockReturnDetailsRecord;
            }
            else
            {
                _IStockReturnView.Message = ResponseData.DisplayMessage;
                _IStockReturnView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteStockReturn()
        {
            try
            {
                var RequestData = new DeleteStockReturnRequest();
                RequestData.ID = _IStockReturnView.ID;
                var ResponseData = _StockReturnBLL.DeleteStockReturn(RequestData);
                _IStockReturnView.Message = ResponseData.DisplayMessage;
                _IStockReturnView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.STOCKRETURN;
                RequestData.StoreID = _IStockReturnView.StoreID;
                RequestData.StoreCode = _IStockReturnView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IStockReturnView.DocumentNo = DocumentNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void SelectSKULookup()
        //{
        //    var RequestData = new SelectAllSKUMasterRequest();

        //    var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
        //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //    {
        //        _IStockReturnView.SKUMasterList = ResponseData.SKUMasterTypesList;
        //    }
        //    else
        //    {
        //        _IStockReturnView.Message = ResponseData.DisplayMessage;
        //        _IStockReturnView.ProcessStatus = ResponseData.StatusCode;
        //    }
        //}
    }
    public class StockReturnListPresenter
    {
        IStockReturnCollectionView _IStockReturnCollectionView;
        StockReturnBLL _StockReturnBLL = new StockReturnBLL();

        public StockReturnListPresenter(IStockReturnCollectionView ViewObj)
        {
            _IStockReturnCollectionView = ViewObj;
        }

        public void GetStockReturnlist()
        {
            try
            {
                var RequestData = new SelectAllStockReturnRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = _IStockReturnCollectionView.StoreID;
                RequestData.StoreCode = _IStockReturnCollectionView.StoreCode;
                var ResponseData = new SelectAllStockReturnResponse();
                ResponseData = _StockReturnBLL.SelectAllStockReturn(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnCollectionView.StockReturnHeaderList = ResponseData.StockReturnHeaderList;
                }
                else
                {
                    _IStockReturnCollectionView.StockReturnHeaderList = new List<StockReturnHeader>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class StockReturnReportViewPresenter
    {
        IStockReturnHeaderReportView _IStockReturnHeaderReportView;
        public StockReturnReportViewPresenter(IStockReturnHeaderReportView ViewObj)
        {
            _IStockReturnHeaderReportView = ViewObj;
        }
        public void GetStockReceiptRecord()
        {
            try
            {
                var _StockReturnBLL = new StockReturnBLL();
                var RequestData = new SelectAllStockReturnRequest();
                RequestData.FromDate = _IStockReturnHeaderReportView.FromDate;
                RequestData.ToDate = _IStockReturnHeaderReportView.ToDate;
                var ResponseData = _StockReturnBLL.GetStockReturnHeaderReport(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnHeaderReportView.StockReturnHeaderList = ResponseData.StockReturnHeaderList;
                }
                else
                {
                    _IStockReturnHeaderReportView.StockReturnHeaderList = new List<StockReturnHeader>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class StockReturnDetailReportViewPresenter
    {
        IStockReturnDetailsReportView _IStockReturnDetailsReportView;
        public StockReturnDetailReportViewPresenter(IStockReturnDetailsReportView ViewObj)
        {
            _IStockReturnDetailsReportView = ViewObj;
        }
        public void GetStockReceiptDetailRecord()
        {
            try
            {
                var _StockReturnBLL = new StockReturnBLL();
                var RequestData = new SelectAllStockReturnRequest();
                RequestData.FromDate = _IStockReturnDetailsReportView.FromDate;
                RequestData.ToDate = _IStockReturnDetailsReportView.ToDate;
                var ResponseData = _StockReturnBLL.GetStockReturnDetailsReport(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnDetailsReportView.StockReturnDetailsList = ResponseData.StockReturnDetailsList;
                }
                else
                {
                    _IStockReturnDetailsReportView.StockReturnDetailsList = new List<StockReturnDetails>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
