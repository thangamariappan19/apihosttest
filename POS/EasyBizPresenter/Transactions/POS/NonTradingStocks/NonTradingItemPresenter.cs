using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizIView.Transactions.INonTradingItem;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.NonTradingStocks
{
    public class NonTradingItemPresenter
    {
        INonTradingItemView _INonTradingItemView;
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        NonTradingItemStockBLL _NonTradingItemStockBLL = new NonTradingItemStockBLL();
        NonTradingStockHeaderTypes _NonTradingItemStock = new NonTradingStockHeaderTypes();

        int _RunningNo;
        int _DetailID;

        public NonTradingItemPresenter(INonTradingItemView ViewObj)
        {
            _INonTradingItemView = ViewObj;
        }

        public void GetNonTradingStockBySKU()
        {
            try
            {
                var RequestData = new GetNonTradingStockBySKURequest();
                var ResponseData = new GetNonTradingStockBySKUResponse();
                RequestData.SKUCode = _INonTradingItemView.ItemCode;
                RequestData.StoreID = _INonTradingItemView.UserInformation.StoreID;
                ResponseData = _TransactionLogBLL.GetNonTradingStockBySku(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //_INonTradingItemView.NonTradingStockData = ResponseData.NonTradingStockData;
                    _INonTradingItemView.NonTradingStockList = ResponseData.NonTradingStockList;
                }
                else if (ResponseData.NonTradingStockList == null && ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    _INonTradingItemView.Message = "Item not found or Item is not a Non-TradingItem";
                    _INonTradingItemView.NonTradingStockList.Clear();
                }
                else
                {
                    _INonTradingItemView.Message = "Item not found";
                    //_INonTradingItemView.NonTradingStockData = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetEmployeeByStore()
        {
            try
            {
                var RequestData = new GetEmployeeByStoreRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreID = _INonTradingItemView.UserInformation.StoreID;
                var ResponseData = _EmployeeMasterBLL.GetEmployeeByStore(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _INonTradingItemView.EmployeeLookUp = ResponseData.EmployeeList;
                }
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
                RequestData.DocumentTypeID = (int)Enums.DocumentType.NONTRADINGITEMDISTRIBUTION;
                RequestData.StoreID = _INonTradingItemView.UserInformation.StoreID;
                RequestData.StoreCode = _INonTradingItemView.UserInformation.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                    _INonTradingItemView.DocumentNo = DocumentNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveNonTradingItem()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveNonTradingItemRequest();
                    RequestData.NonTradingItemRecord = new NonTradingStockHeaderTypes();

                    var NewnonTradingSTockList = new List<NonTradingStockDetailsTypes>();
                    var NonTradingStockList = _INonTradingItemView.NonTradingStockDetailsList;
                    // var StockReqList = _INonTradingItemView.StockReturnDetailsList1;
                    //List<List<NonTradingStockDetailsTypes>> GroupByList = NonTradingStockList.GroupBy(d => d.SKUCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();
                    //foreach (List<NonTradingStockDetailsTypes> objnonTradingStockDetails in GroupByList)
                    //{
                    //    var _NonTradingStockDetails = objnonTradingStockDetails.FirstOrDefault();
                    //    int RecQty = objnonTradingStockDetails.Sum(x => x.ReceivedQty);
                    //    int RetQty = objnonTradingStockDetails.Sum(x => x.ReturnQty);
                    //    int Qty = 0;
                    //    if (_NonTradingStockDetails.ReceivedQty > Qty || _NonTradingStockDetails.ReturnQty > Qty)
                    //    {
                    //        //_StockRequestDetails.Quantity = Qty;
                    //        NewnonTradingSTockList.Add(_NonTradingStockDetails);
                    //    }
                    //}
                    //if (NewnonTradingSTockList.Count == GroupByList.Count)
                    //{
                    //RequestData.StockReturnDetailsList1 = NewStockReqList;

                    RequestData.RefDocumentNo = _INonTradingItemView.RefDocumentNo;
                    if (RequestData.RefDocumentNo == "" || RequestData.RefDocumentNo == null)
                    {
                        RequestData.NonTradingStockDetailsList = NonTradingStockList;
                        RequestData.TransactionLogList = _INonTradingItemView.TransactionLogList;
                       
                    }
                    else
                    {
                        NonTradingStockList.RemoveAll(p => p.ReturnQty == 0);
                        _INonTradingItemView.TransactionLogList.RemoveAll(p => p.InQty == 0);
                        RequestData.NonTradingStockDetailsList = NonTradingStockList;
                        RequestData.TransactionLogList = _INonTradingItemView.TransactionLogList;
                    }
                    RequestData.NonTradingItemRecord.ID = _INonTradingItemView.ID;
                    RequestData.NonTradingItemRecord.DocumentNo = _INonTradingItemView.DocumentNo;
                    RequestData.NonTradingItemRecord.DocumentDate = _INonTradingItemView.DocumentDate;
                    RequestData.NonTradingItemRecord.CountryID = _INonTradingItemView.CountryID;
                    RequestData.NonTradingItemRecord.StoreID = _INonTradingItemView.StoreID;
                    RequestData.NonTradingItemRecord.ReceivedType = _INonTradingItemView.ReceivedType;
                    RequestData.NonTradingItemRecord.TransactionType = _INonTradingItemView.TransactionType;
                    RequestData.NonTradingItemRecord.ReceivedQty = Convert.ToInt32(_INonTradingItemView.Quantity);
                    RequestData.NonTradingItemRecord.RefDocumentNo = _INonTradingItemView.RefDocumentNo;
                    RequestData.NonTradingItemRecord.CreateOn = DateTime.Now;
                    RequestData.NonTradingItemRecord.CreateBy = _INonTradingItemView.UserID;
                    RequestData.NonTradingItemRecord.EmployeeID = _INonTradingItemView.EmployeeID;
                    RequestData.NonTradingItemRecord.EmployeeCode = _INonTradingItemView.EmployeeCode;
                    RequestData.NonTradingItemRecord.EmployeeName = _INonTradingItemView.EmployeeName;
                    RequestData.NonTradingItemRecord.StoreCode = _INonTradingItemView.StoreCode;
                    //RequestData.NonTradingItemRecord.NonTradingStockDetailsList = new List<NonTradingStockDetailsTypes>();
                    //RequestData.NonTradingItemRecord.NonTradingStockDetailsList = NewnonTradingSTockList; 
                    RequestData.RunningNo = _RunningNo;
                    RequestData.DocumentNumberingID = _DetailID;

                    var ResponseData = _NonTradingItemStockBLL.SaveNonTradingItemStock(RequestData);
                    _INonTradingItemView.Message = ResponseData.DisplayMessage;
                    _INonTradingItemView.ProcessStatus = ResponseData.StatusCode;
                    _INonTradingItemView.NonTradingStockHeaderID = Convert.ToInt32(ResponseData.IDs);
                    //}
                    if (_INonTradingItemView.ProcessStatus == Enums.OpStatusCode.Success)
                    {
                        //UpdateRunningNo();
                    }
                }
                else
                {
                    _INonTradingItemView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //2020-02-29 Old Normal save without Details Table
        //public void SaveNonTradingItem()
        //{
        //    try
        //    {
        //        if (IsValidForm())
        //        {
        //            var RequestData = new SaveNonTradingItemRequest();
        //            RequestData.NonTradingItemRecord = new NonTradingStockHeaderTypes();
        //            //RequestData.NonTradingItemRecord.ID = _INonTradingItemView.ID;
        //            RequestData.NonTradingItemRecord.DocumentNo = _INonTradingItemView.DocumentNo;
        //            RequestData.NonTradingItemRecord.DocumentDate = _INonTradingItemView.DocumentDate;
        //            RequestData.NonTradingItemRecord.CountryID = _INonTradingItemView.CountryID;
        //            RequestData.NonTradingItemRecord.StoreID = _INonTradingItemView.StoreID;
        //            RequestData.NonTradingItemRecord.EmployeeID = _INonTradingItemView.EmployeeID;
        //            RequestData.NonTradingItemRecord.EmployeeName = _INonTradingItemView.EmployeeName;
        //            RequestData.NonTradingItemRecord.EmployeeCode = _INonTradingItemView.EmployeeCode;
        //            RequestData.NonTradingItemRecord.ReceivedQty = _INonTradingItemView.ReceivedQty;
        //            RequestData.NonTradingItemRecord.ReturnQty = _INonTradingItemView.ReturnQty;
        //            RequestData.NonTradingItemRecord.ReceivedType = _INonTradingItemView.ReceivedType;
        //            RequestData.NonTradingItemRecord.TransactionType = _INonTradingItemView.TransactionType;
        //            //RequestData.NonTradingItemRecord.SKUID = _INonTradingItemView.SKUID;
        //            RequestData.NonTradingItemRecord.SKUCode = _INonTradingItemView.SkuCode;
        //            RequestData.NonTradingItemRecord.BarCode = _INonTradingItemView.BarCode;
        //            //RequestData.NonTradingItemRecord.AvailableStock = _INonTradingItemView.NonTradingStockData.StockQty;
        //            RequestData.NonTradingItemRecord.CreateOn = DateTime.Now;
        //            RequestData.NonTradingItemRecord.CreateBy = _INonTradingItemView.UserID;
        //            RequestData.RunningNo = _RunningNo;
        //            RequestData.DocumentNumberingID = _DetailID;
        //            //RequestData.NonTradingItemRecord.SCN = _INonTradingItemView.SCN;
        //            RequestData.RequestFrom = _INonTradingItemView.RequestFrom;
        //            var ResponseData = _NonTradingItemStockBLL.SaveNonTradingItemStock(RequestData);
        //            _INonTradingItemView.Message = ResponseData.DisplayMessage;
        //            _INonTradingItemView.ProcessStatus = ResponseData.StatusCode;
        //        }
        //        else
        //        {
        //            _INonTradingItemView.ProcessStatus = Enums.OpStatusCode.GeneralError;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_INonTradingItemView.DocumentNo.Trim() == string.Empty)
            {
                _INonTradingItemView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
            }
            else if (_INonTradingItemView.DocumentDate == null)
            {
                _INonTradingItemView.Message = "DocumentDate is missing Please Enter it.";
            }
            //else if (_INonTradingItemView.ReceivedQty == 0)
            //{
            //    _INonTradingItemView.Message = "Please Enter Quantity...!";
            //}
            else if (_INonTradingItemView.NonTradingStockDetailsList.Count == 0)
            {
                _INonTradingItemView.Message = "NonTradingStockDetails is missing Please Select it.";
            }
            //else if (_INonTradingItemView.TransactionType == "Issue")
            //{
            //    if (_INonTradingItemView.SkuCode.Trim() == string.Empty)
            //    {
            //        _INonTradingItemView.Message = " Please Enter SKUcode/BarCode.";
            //    }
            //}
            //else if (_INonTradingItemView.BarCode.Trim() == string.Empty)
            //{
            //    _INonTradingItemView.Message = " Please Enter SKUcode/BarCode.";
            //}
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public bool IsValidForm1()
        {
            bool objBool = false;
            if (_INonTradingItemView.DocumentNo.Trim() == string.Empty)
            {
                _INonTradingItemView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
            }
            else if (_INonTradingItemView.DocumentDate == null)
            {
                _INonTradingItemView.Message = "DocumentDate is missing Please Enter it.";
            }
            else if (_INonTradingItemView.ReceivedQty == 0)
            {
                _INonTradingItemView.Message = "Please Enter Quantity...!";
            }
            //else if (_INonTradingItemView.NonTradingStockDetailsList.Count == 0)
            //{
            //    _INonTradingItemView.Message = "NonTradingStockDetails is missing Please Select it.";
            //}
            //else if (_INonTradingItemView.SkuCode.Trim() == string.Empty)
            //{
            //    _INonTradingItemView.Message = " Please Enter SKUcode/BarCode.";
            //}
            //else if (_INonTradingItemView.BarCode.Trim() == string.Empty)
            //{
            //    _INonTradingItemView.Message = " Please Enter SKUcode/BarCode.";
            //}
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public class NonTradingStockListPresenter
        {
            INonTradingStockCollectionView _INonTradingStockCollectionView;
            NonTradingItemStockBLL _NonTradingItemStockBLL = new NonTradingItemStockBLL();

            public NonTradingStockListPresenter(INonTradingStockCollectionView ViewObj)
            {
                _INonTradingStockCollectionView = ViewObj;
            }
            public void GetNonTradingStockList()
            {
                try
                {
                    var RequestData = new SelectALLNonTradingStockRequest();
                    RequestData.ShowInActiveRecords = true;
                    RequestData.StoreID = _INonTradingStockCollectionView.StoreID;
                    RequestData.StoreCode = _INonTradingStockCollectionView.StoreCode;
                    var ResponseData = new SelectALLNonTradingStockResponse();
                    ResponseData = _NonTradingItemStockBLL.SelectALLNonTradingStock(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _INonTradingStockCollectionView.NonTradingStockHeaderList = ResponseData.NonTradingStockHeaderList;
                    }
                    else
                    {
                        _INonTradingStockCollectionView.NonTradingStockHeaderList = new List<NonTradingStockHeaderTypes>();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SelectNonTradingStockHeader()
        {
            try
            {
                var RequestData = new SelectByNonTradingHeaderIDRequest();
                RequestData.ID = _INonTradingItemView.ID;
                RequestData.DocumentNo = _INonTradingItemView.RefDocumentNo;
                //RequestData.RefDocumentNo = _INonTradingItemView.RefDocumentNo;
                var ResponseData = _NonTradingItemStockBLL.SelectNonTradingHeaderRecord(RequestData);
                if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    RequestData.RefDocumentNo = _INonTradingItemView.RefDocumentNo;
                    _INonTradingItemView.ID = ResponseData.NonTradingStockHeaderRecord.ID;
                    if (RequestData.RefDocumentNo == null || RequestData.RefDocumentNo == "")
                        _INonTradingItemView.DocumentNo = ResponseData.NonTradingStockHeaderRecord.DocumentNo;
                    else
                        _INonTradingItemView.RefDocumentNo = ResponseData.NonTradingStockHeaderRecord.DocumentNo;
                    _INonTradingItemView.DocumentDate = ResponseData.NonTradingStockHeaderRecord.DocumentDate;
                    _INonTradingItemView.ReceivedType = ResponseData.NonTradingStockHeaderRecord.ReceivedType;
                    if (RequestData.RefDocumentNo != null || RequestData.RefDocumentNo != "")
                        _INonTradingItemView.TransactionType = "Return";
                    else
                        _INonTradingItemView.TransactionType = ResponseData.NonTradingStockHeaderRecord.TransactionType;
                    //_INonTradingItemView.Quantity = ResponseData.NonTradingStockHeaderRecord.ReceivedQty;
                    _INonTradingItemView.EmployeeID = ResponseData.NonTradingStockHeaderRecord.EmployeeID;
                    _INonTradingItemView.EmployeeCode = ResponseData.NonTradingStockHeaderRecord.EmployeeCode;
                    _INonTradingItemView.EmployeeName = ResponseData.NonTradingStockHeaderRecord.EmployeeName;
                    _INonTradingItemView.SCN = ResponseData.NonTradingStockHeaderRecord.SCN;
                }
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _INonTradingItemView.Message = ResponseData.DisplayMessage;
                    //_INonTradingItemView.Message = "It is a Returned Document";
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _INonTradingItemView.Message = ResponseData.DisplayMessage;
                }
                _INonTradingItemView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectNonTradingStockDetails()
        {
            var RequestData = new SelectByNonTraddingDetailsIDRequest();
            RequestData.ID = _INonTradingItemView.ID;
            RequestData.RefDocumentNo = _INonTradingItemView.RefDocumentNo;
            if (RequestData.RefDocumentNo == null || RequestData.RefDocumentNo == "")               
            RequestData.Mode = "Edit";
            else
                RequestData.Mode = "Reference";
            var ResponseData = _NonTradingItemStockBLL.SelectNonTradingStockDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _INonTradingItemView.NonTradingStockDetailsList = ResponseData.NonTradingStockDetailsRecord;
            }
            else
            {
                /*_INonTradingItemView.Message = ResponseData.DisplayMessage;
                _INonTradingItemView.ProcessStatus = ResponseData.StatusCode;*/
            }
        }
        //public void GetStoreListByID()
        //{
        //    try
        //    {
        //        var _StoreMasterBLL = new StoreMasterBLL();
        //        var RequestData = new SelectByIDStoreMasterRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        var ResponseData = new SelectByIDStoreMasterResponse();
        //        RequestData.ID = _INonTradingItemView.SelectedStoreId;
        //        ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _INonTradingItemView.StoreMasterRecord = ResponseData.StoreMasterData;
        //        }
        //        else
        //        {
        //            _INonTradingItemView.StoreList = new List<StoreMaster>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}