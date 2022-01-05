using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizIView.Transactions.IStockRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;

namespace EasyBizPresenter.Transactions.Stocks
{
    public class StockRequestPresenter
    {
          IStockRequestView _IStockRequestView;
        StockRequestBLL _StockRequestBLL = new StockRequestBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        WarehouseMasterBLL _WarehouseMasterBLL = new WarehouseMasterBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        int _RunningNo;
        int _DetailID;
        public StockRequestPresenter(IStockRequestView ViewObj)
        {
            _IStockRequestView = ViewObj;
        }
         public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStockRequestView.DocumentNo == "")
            {
                _IStockRequestView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
            }
            else if (_IStockRequestView.DocumentDate == null)
            {
                _IStockRequestView.Message = "DocumentDate is missing Please Enter it.";
            }           
            else if (_IStockRequestView.WareHouseID == 0)
            {
                _IStockRequestView.Message = "ToWareHouse is missing Please Select it.";
            }
            //else if (_IStockRequestView.SKUMasterList == null)
            //{
            //    _IStockRequestView.Message = "SKU Master List is missing Please Select it.";
            //}
            else if (_IStockRequestView.StockRequestDetailsList.Count == 0)
            {
                _IStockRequestView.Message = "StockRequest Line Details is missing Please Select it.";
            }
            else if(_IStockRequestView.TotalQuantity == 0)
            {
                _IStockRequestView.Message = "Please Give Correct Quantity ";
            }
            else if (_IStockRequestView.Status == string.Empty)
            {
                _IStockRequestView.Message = "Status is missing Please Select it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveStockRequest()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStockRequestRequest();
                    RequestData.StockRequestHeaderRecord = new StockRequestHeader();
                    
                    var NewStockReqList = new List<StockRequestDetails>();

                    var StockReqList = _IStockRequestView.StockRequestDetailsList;
                    List<List<StockRequestDetails>> GroupByList = StockReqList.GroupBy(d => d.SKUCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();


                    //List<List<StyleMaster>> BrandWiseStyleList = BrandStyleList.GroupBy(d => d.BrandID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    foreach(List<StockRequestDetails> objStockRequestDetails in GroupByList)
                    {
                        var _StockRequestDetails = objStockRequestDetails.FirstOrDefault();
                        int Qty = objStockRequestDetails.Sum(x => x.Quantity);
                        _StockRequestDetails.Quantity = Qty;
                        NewStockReqList.Add(_StockRequestDetails);
                    }

                    RequestData.StockRequestDetailsList = NewStockReqList; // _IStockRequestView.StockRequestDetailsList;                   
                    RequestData.StockRequestHeaderRecord.ID = _IStockRequestView.ID;
                    RequestData.StockRequestHeaderRecord.DocumentNo = _IStockRequestView.DocumentNo;
                    RequestData.StockRequestHeaderRecord.DocumentDate = _IStockRequestView.DocumentDate;
                    RequestData.StockRequestHeaderRecord.TotalQuantity = _IStockRequestView.TotalQuantity;
                    RequestData.StockRequestHeaderRecord.FromStore = _IStockRequestView.StoreID;
                    RequestData.StockRequestHeaderRecord.StoreCode = _IStockRequestView.StoreCode;
                    RequestData.StockRequestHeaderRecord.WareHouseID = _IStockRequestView.WareHouseID;
                    RequestData.StockRequestHeaderRecord.Remarks = _IStockRequestView.Remarks;
                    RequestData.StockRequestHeaderRecord.Status = _IStockRequestView.Status;
                   // RequestData.StockRequestHeaderRecord.StoreCode = _IStockRequestView.StoreCode;
                    RequestData.StockRequestHeaderRecord.CreateBy = _IStockRequestView.UserID;
                    RequestData.StockRequestHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.StockRequestHeaderRecord.Active = true;
                    RequestData.StockRequestHeaderRecord.SCN = _IStockRequestView.SCN;
                    var ResponseData = _StockRequestBLL.SaveStockRequest(RequestData);
                    _IStockRequestView.Message = ResponseData.DisplayMessage;
                    _IStockRequestView.ProcessStatus = ResponseData.StatusCode;

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        UpdateRunningNo();
                    }
                }
                else
                {
                    _IStockRequestView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Saveint_stockrequest()
        {
            try
            {
                var RequestData = new SaveStockRequestRequest();

                var NewIntStockReqList = new List<int_stockrequestTypes>();

                var Int_StockReqList = _IStockRequestView.int_stockrequestTypesList;
                List<List<int_stockrequestTypes>> GroupByList = Int_StockReqList.GroupBy(d => d.ItemCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                //List<List<StyleMaster>> BrandWiseStyleList = BrandStyleList.GroupBy(d => d.BrandID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                foreach (List<int_stockrequestTypes> objInt_StockRequestDetails in GroupByList)
                {
                    var _Int_StockRequestDetails = objInt_StockRequestDetails.FirstOrDefault();
                    int Qty = objInt_StockRequestDetails.Sum(x => x.Quantity);
                    _Int_StockRequestDetails.Quantity = Qty;
                    NewIntStockReqList.Add(_Int_StockRequestDetails);
                }
                RequestData.int_stockrequestTypesList = NewIntStockReqList;
                var ResponseData = _StockRequestBLL.Saveint_stock(RequestData);
                _IStockRequestView.Message = ResponseData.DisplayMessage;
            }
            catch(Exception ex)
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
                objUpdateRunningNumRequest.StoreID = _IStockRequestView.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectStockRequestHeaderRecord()
        {
            try
            {
                var RequestData = new SelectByStockRequestIDRequest();
                RequestData.ID = _IStockRequestView.ID;
                var ResponseData = _StockRequestBLL.SelectStockRequestRecord(RequestData);
                _IStockRequestView.ID = ResponseData.StockRequestHeaderRecord.ID;
                _IStockRequestView.TotalQuantity = ResponseData.StockRequestHeaderRecord.TotalQuantity;
                _IStockRequestView.DocumentDate = ResponseData.StockRequestHeaderRecord.DocumentDate;
                _IStockRequestView.DocumentNo = ResponseData.StockRequestHeaderRecord.DocumentNo;
                _IStockRequestView.Status = ResponseData.StockRequestHeaderRecord.Status;                
                _IStockRequestView.WareHouseID = ResponseData.StockRequestHeaderRecord.WareHouseID;
                _IStockRequestView.Remarks = ResponseData.StockRequestHeaderRecord.Remarks;
                          
                _IStockRequestView.SCN = ResponseData.StockRequestHeaderRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStockRequestView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IStockRequestView.Message = ResponseData.DisplayMessage;
                }
                _IStockRequestView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectStockRequestDetails()
        {
            SelectByStockRequestDetailsRequest RequestData = new SelectByStockRequestDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IStockRequestView.ID;
            var ResponseData = _StockRequestBLL.SelectStockRequestDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockRequestView.StockRequestDetailsList = ResponseData.StockRequestDetailsRecord;
            }
            else
            {
                _IStockRequestView.Message = ResponseData.DisplayMessage;
                _IStockRequestView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteStockRequest()
        {
            try
            {
                var RequestData = new DeleteStockRequestRequest();
                RequestData.ID = _IStockRequestView.ID;
                var ResponseData = _StockRequestBLL.DeleteStockRequest(RequestData);
                _IStockRequestView.Message = ResponseData.DisplayMessage;
                _IStockRequestView.ProcessStatus = ResponseData.StatusCode;
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
            RequestData.CountryID = _IStockRequestView.CountryID;
            SelectWhareouseLookUpResponse ResponseData = _WarehouseMasterBLL.SelectWhareHouseLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockRequestView.WarehouseMasterLookUp = ResponseData.WarehouseMasterList;
            }
        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IStockRequestView.StoreID;
            RequestData.StoreCode = _IStockRequestView.StoreCode;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockRequestView.StoreMasterRecord = ResponseData.StoreMasterData;
            }
        }
        //public void GetToStoreMasterLookUP()
        //{
        //    SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
        //    RequestData.ShowInActiveRecords = false;
        //    RequestData.StateID = _IStockRequestView.StateID;
        //    SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
        //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //    {
        //        _IStockRequestView.ToStoreMasterLookUp = ResponseData.StoreMasterList;
        //    }
        //}
        public void GetSKU()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _IStockRequestView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockRequestView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
                else
                {
                    _IStockRequestView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
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
                RequestData.DocumentTypeID = (int)Enums.DocumentType.STOCKREQUEST;
                RequestData.StoreID = _IStockRequestView.StoreID;
                RequestData.StoreCode = _IStockRequestView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                   
                    _IStockRequestView.DocumentNo = DocumentNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectSKULookup()
        {
            var RequestData = new SelectAllSKUMasterRequest();

            var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStockRequestView.SKUMasterList = ResponseData.SKUMasterTypesList;
            }
            else
            {
                _IStockRequestView.Message = ResponseData.DisplayMessage;
                _IStockRequestView.ProcessStatus = ResponseData.StatusCode;
            }
        }
    }
    public class StockRequestListPresenter
    {
        IStockRequestCollectionView _IStockRequestCollectionView;
        StockRequestBLL _StockRequestBLL = new StockRequestBLL();

        public StockRequestListPresenter(IStockRequestCollectionView ViewObj)
        {
            _IStockRequestCollectionView = ViewObj;
        }

        public void GetStockRequestlist()
        {
            try
            {
                var RequestData = new SelectAllStockRequestRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStockRequestResponse();
                ResponseData = _StockRequestBLL.SelectAllStockRequest(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockRequestCollectionView.StockRequestHeaderList = ResponseData.StockRequestHeaderList;
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
    } 
}
