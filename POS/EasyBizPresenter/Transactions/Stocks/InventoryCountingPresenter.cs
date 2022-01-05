using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizIView.Transactions.IInventoryCounting;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizBLL.SyncSettings;
using EasyBizRequest.SyncSettings;
using EasyBizResponse.SyncSettings;
using EasyBizRequest.Masters.SKUMasterRequest;

namespace EasyBizPresenter.Transactions.Stocks
{
    
    public class InventoryCountingPresenter
    {
         IInventoryCountingEntry _IInventoryCountingView;
        InventoryCountingBLL _InventoryCountingBLL = new InventoryCountingBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();

        int _RunningNo;
        int _DetailID;
         public InventoryCountingPresenter(IInventoryCountingEntry ViewObj)
        {
            _IInventoryCountingView = ViewObj;
        }
         public bool IsValidForm()
         {
             bool objBool = false;
             if (_IInventoryCountingView.DocumentNumber == "")
             {
                 _IInventoryCountingView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen..";
             }
             else if (_IInventoryCountingView.DocumentDate == null)
             {
                 _IInventoryCountingView.Message = "DocumentDate is missing Please Enter it.";
             }
             //else if (_IInventoryCountingView.SKUMasterList == null)
             //{
             //    _IInventoryCountingView.Message = "Reason is missing Please Select it.";
             //}
             //else if (_IInventoryCountingView.InventoryCountingDetailsList.Count == 0)
             //{
             //    _IInventoryCountingView.Message = "InventoryCountingDetails is missing Please Select it.";
             //}           
             else
             {
                 objBool = true;
             }
             return objBool;
         }
        public void SaveInventoryCounting()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveInventoryCountingRequest();
                    RequestData.InventoryCountingHeaderRecord = new InventoryCountingHeader();
                    RequestData.InventoryCountingDetailsList = _IInventoryCountingView.InventoryCountingDetailsList;                   
                    RequestData.InventoryCountingHeaderRecord.ID = _IInventoryCountingView.InventoryCountingID;
                    RequestData.InventoryCountingHeaderRecord.DocumentNumber = _IInventoryCountingView.DocumentNumber;
                    RequestData.InventoryCountingHeaderRecord.DocumentDate = _IInventoryCountingView.DocumentDate;                   
                    RequestData.InventoryCountingHeaderRecord.CreateBy = _IInventoryCountingView.UserID;
                    RequestData.InventoryCountingHeaderRecord.StoreID = _IInventoryCountingView.StoreID;
                    RequestData.InventoryCountingHeaderRecord.StoreCode = _IInventoryCountingView.StoreCode;
                    RequestData.InventoryCountingHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.InventoryCountingHeaderRecord.Active = true;
                    RequestData.InventoryCountingHeaderRecord.SCN = _IInventoryCountingView.SCN;
                    var ResponseData = _InventoryCountingBLL.SaveInventoryCounting(RequestData);
                    _IInventoryCountingView.Message = ResponseData.DisplayMessage;
                    _IInventoryCountingView.ProcessStatus = ResponseData.StatusCode;
                    _IInventoryCountingView.InventoryCountingID = Convert.ToInt32(ResponseData.IDs);

                    if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        UpdateRunningNo();
                    }
                }
                else
                {
                    _IInventoryCountingView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InventoryPosting()
        {
            try
            {
                var RequestData = new SaveInventoryCountingRequest();
                RequestData.InventoryCountingHeaderRecord = new InventoryCountingHeader();
                RequestData.InventoryCountingHeaderRecord = _IInventoryCountingView.InventoryCountingRecord;
                var ResponseData = _InventoryCountingBLL.InventoryPosting(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var SaveTransactionLogRequestData = new SaveTransactionLogRequest();
                    SaveTransactionLogRequestData.TransactionLogList = _IInventoryCountingView.TransactionLogList;
                    var SaveTransactionLogResponseData = _TransactionLogBLL.SaveTransactionLog(SaveTransactionLogRequestData);
                    if (SaveTransactionLogResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IInventoryCountingView.Message = ResponseData.DisplayMessage;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SaveTransactionsLog()
        {
           

        }
        public void GetInventoryCountingRecord()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();

                var RequestData = new SelectByInventoryCountingIDRequest();
                RequestData.ID = _IInventoryCountingView.InventoryCountingID;
                var ResponseData = _InventoryCountingBLL.SelectInventoryCountingRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingView.InventoryCountingRecord = ResponseData.InventoryCountingHeaderRecord;
                }
                else
                {
                    _IInventoryCountingView.InventoryCountingRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public void DeleteInventoryCounting()
        {
            try
            {
                var RequestData = new DeleteInventoryCountingRequest();
                RequestData.ID = _IInventoryCountingView.InventoryCountingID;
                var ResponseData = _InventoryCountingBLL.DeleteInventoryCounting(RequestData);
                _IInventoryCountingView.Message = ResponseData.DisplayMessage;
                _IInventoryCountingView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetDocumentNumber()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.INVENTORYCOUNTING;
                RequestData.StoreID = _IInventoryCountingView.StoreID;
                RequestData.StoreCode = _IInventoryCountingView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNumber = string.Empty;
                    DocumentNumber = DocumentNumber.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IInventoryCountingView.DocumentNumber = DocumentNumber;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
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
                objUpdateRunningNumRequest.StoreID = _IInventoryCountingView.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        
        public void GetStockBySKU(string SkuOrBarcode)
        {
            try
            {
                var RequestData = new GetStockBySkuRequest();
                var ResponseData = new GetStockBySkuResponse();
                RequestData.SKUCode = SkuOrBarcode;
                RequestData.StoreID = _IInventoryCountingView.StoreID;
                ResponseData = _TransactionLogBLL.GetStockBySku(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingView.StockData = ResponseData.StockData;
                }
                else
                {
                    _IInventoryCountingView.StockData = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class InventoryCountingListPresenter
    {
        IInventoryCountingCollectionView _IInventoryCountingCollectionView;
        InventoryCountingBLL _InventoryCountingBLL = new InventoryCountingBLL();

        public InventoryCountingListPresenter(IInventoryCountingCollectionView ViewObj)
        {
            _IInventoryCountingCollectionView = ViewObj;
        }

        public void GetInventoryCountinglist()
        {
            try
            {
                var RequestData = new GetInventoryCountingInitRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new GetInventoryCountingInitResponse();
                ResponseData = _InventoryCountingBLL.GetInventoryCountingInitList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingCollectionView.InventoryInitList = ResponseData.InventoryInitList;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
    public class InventoryCountingViewPresenter
    {
        IInventoryCountingView _IInventoryCountingView;
        public InventoryCountingViewPresenter(IInventoryCountingView ViewObj)
        {
            _IInventoryCountingView = ViewObj;
        }
        public void GetInventoryCountingRecord()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();

                var RequestData = new SelectByInventoryCountingIDRequest();
                RequestData.ID = _IInventoryCountingView.InventoryCountingID;
                var ResponseData = _InventoryCountingBLL.SelectInventoryCountingRecord(RequestData);   
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingView.InventoryCountingRecord = ResponseData.InventoryCountingHeaderRecord;
                }
                else
                {
                    _IInventoryCountingView.InventoryCountingRecord = null;
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class InventoryCountingInitPresenter
    {
        IInventoryCountingInitView _IInventoryCountingInitView;
        int _RunningNo;
        int _DetailID;
        public InventoryCountingInitPresenter(IInventoryCountingInitView ViewObj)
        {
            _IInventoryCountingInitView = ViewObj;
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var _DocumentNumberingBLL = new DocumentNumberingBLL();
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.INVENTORYCOUNTING;
                RequestData.StoreID = _IInventoryCountingInitView.StoreID;
                RequestData.StoreCode = _IInventoryCountingInitView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IInventoryCountingInitView.DocumentNo = DocumentNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSystemStockByStore()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();

                var RequestData = new GetSystemStockByStoreRequest();
                RequestData.StoreID = _IInventoryCountingInitView.StoreID;
                var ResponseData = _InventoryCountingBLL.GetSystemStockByStore(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingInitView.InventorySysCountList = ResponseData.InventorySysCountList;
                }
                else
                {
                    _IInventoryCountingInitView.InventorySysCountList = new List<InventorySysCount>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveSystemStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new SaveSystemStockRequest();
                RequestData.InventoryInitRecord = _IInventoryCountingInitView.InventoryInitRecord;
                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;
                var ResponseData = _InventoryCountingBLL.SaveSystemStock(RequestData);                
                _IInventoryCountingInitView.ProcessStatus = ResponseData.StatusCode;
                _IInventoryCountingInitView.Message = ResponseData.DisplayMessage;

                if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    throw new Exception(ResponseData.ExceptionMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetInitializeStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "DocumentNo";
                RequestData.DocumentNo = _IInventoryCountingInitView.DocumentNo;

                var ResponseData = _InventoryCountingBLL.GetInventoryCountingInitRecord(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingInitView.InventoryInitRecord = ResponseData.InventoryInitRecord;
                }
                else
                {
                    _IInventoryCountingInitView.InventoryInitRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetStylePricingBySKUCode(string SkuCode , int PriceListID)
        {
            decimal SKUPrice = 0;
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.SKUCode = SkuCode;

                var ResponseData = _SKUMasterBLL.SelectGetStylePricingBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var PriceData = ResponseData.StylePricingList.Where(x => x.PriceListID == PriceListID).FirstOrDefault();
                    if (PriceData != null)
                    {
                        SKUPrice = PriceData.Price;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SKUPrice;
        }
    }
    public class InventoryDocUploadPresenter
    {
        IInventoryDocUploadView _IInventoryDocUploadView;
        public InventoryDocUploadPresenter(IInventoryDocUploadView ViewObj)
        {
            _IInventoryDocUploadView = ViewObj;
        }
        public void GetDocumentByDate()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "Date";
                RequestData.DocumentNo = _IInventoryDocUploadView.DocumentNo;
                RequestData.DocumentDate = _IInventoryDocUploadView.DocumentDate;
                
                var ResponseData = _InventoryCountingBLL.GetInventoryCountingInitRecord(RequestData);


                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryDocUploadView.InventoryInitRecord = ResponseData.InventoryInitRecord;
                    GetManualStockCountRecord();
                }
                else
                {
                    _IInventoryDocUploadView.InventoryInitRecord = null;
                    _IInventoryDocUploadView.ProcessStatus = ResponseData.StatusCode;
                    _IInventoryDocUploadView.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsValidSKU(string SKUCode)
        {
            bool ValidSKU = false;
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = SKUCode;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if(ResponseData.SKUMasterTypesList != null && ResponseData.SKUMasterTypesList.Count > 0)
                    {
                        _IInventoryDocUploadView.SKURecord = ResponseData.SKUMasterTypesList.FirstOrDefault();
                        ValidSKU = true;
                    }
                }
            }
            catch 
            {
                ValidSKU = false;
            }
            return ValidSKU;
        }
        public void SaveManualStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new SaveManualStockRequest();
                RequestData.InventoryManualCountRecord = _IInventoryDocUploadView.InventoryManualCountRecord;
                RequestData.Status = _IInventoryDocUploadView.Status;
                var ResponseData = _InventoryCountingBLL.SaveManualStock(RequestData);
                _IInventoryDocUploadView.ProcessStatus = ResponseData.StatusCode;
                _IInventoryDocUploadView.Message = ResponseData.DisplayMessage;
                if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    throw new Exception(ResponseData.ExceptionMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetManualStockCountRecord()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryManualCountRecordRequest();               
                RequestData.DocumentNo = _IInventoryDocUploadView.DocumentNo;
                var ResponseData = _InventoryCountingBLL.GetInventoryManualCountRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryDocUploadView.InventoryManualCountRecord = ResponseData.InventoryManualCountRecord;
                }
                else
                {
                    _IInventoryDocUploadView.InventoryManualCountRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class InventorySummaryPresenter
    {
        IInventorySummaryView _IInventorySummaryView;
        public InventorySummaryPresenter(IInventorySummaryView ViewObj)
        {
            _IInventorySummaryView = ViewObj;
        }
        public void GetInventorySummary()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryManualCountRecordRequest();
                RequestData.DocumentNo = _IInventorySummaryView.DocumentNo;
                var ResponseData = _InventoryCountingBLL.GetInventoryManualCountRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventorySummaryView.InventoryManualCountRecord = ResponseData.InventoryManualCountRecord;
                }
                else
                {
                    _IInventorySummaryView.InventoryManualCountRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInitializeStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "DocumentNo";
                RequestData.DocumentNo = _IInventorySummaryView.DocumentNo;           

                var ResponseData = _InventoryCountingBLL.GetInventoryCountingInitRecord(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                   // _IInventorySummaryView.InventoryInitRecord = ResponseData.InventoryInitRecord;                   
                }
                else
                {
                    //_IInventorySummaryView.InventoryInitRecord = null;                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class InventoryCountingApprovePresenter
    {
        IInventoryCountingApproveView _IInventoryCountingApproveView;
        public InventoryCountingApprovePresenter(IInventoryCountingApproveView ViewObj)
        {
            _IInventoryCountingApproveView = ViewObj;
        }
        public void GetInventoryCountinglist()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = new GetInventoryCountingInitResponse();
                ResponseData = _InventoryCountingBLL.GetInventoryCountingInitList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingApproveView.InventoryInitList = ResponseData.InventoryInitList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInventorySummary()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryManualCountRecordRequest();
                RequestData.DocumentNo = _IInventoryCountingApproveView.DocumentNo;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.GetInventoryManualCountRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingApproveView.InventoryManualCountRecord = ResponseData.InventoryManualCountRecord;
                }
                else
                {
                    _IInventoryCountingApproveView.InventoryManualCountRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInitializeStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "DocumentNo";
                RequestData.DocumentNo = _IInventoryCountingApproveView.DocumentNo;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.GetInventoryCountingInitRecord(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingApproveView.InventoryInitRecord = ResponseData.InventoryInitRecord;
                }
                else
                {
                    _IInventoryCountingApproveView.InventoryInitRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCountryList()
        {
            var _CountryBLL = new CountryBLL();
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryCountingApproveView.CountryList = ResponseData.CountryMasterList;
            }
            else
            {
                _IInventoryCountingApproveView.CountryList = new List<CountryMaster>();
            }
        }
        public void GetStoreList()
        {
            var _StoreMasterBLL = new StoreMasterBLL();
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IInventoryCountingApproveView.CountryID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryCountingApproveView.StoreList = ResponseData.StoreMasterList;
            }
            else
            {
                _IInventoryCountingApproveView.StoreList = new List<StoreMaster>();
            }
        }
        public string GetStoreConnection()
        {
            string ConString = string.Empty;
            try
            {
                var _SyncSettingsBLL = new SyncSettingsBLL();
                GetDBConnectionsRequest RequestData = new GetDBConnectionsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.SyncMode = Enums.SyncMode.EnterpriseToStore;
                RequestData.BaseStoreID = _IInventoryCountingApproveView.StoreID;
                GetDBConnectionsResponse ResponseData = _SyncSettingsBLL.GetDBConnectionList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    ConString = ResponseData.DBConnectionList.FirstOrDefault().ConnectionString;
                }
                else
                {
                    _IInventoryCountingApproveView.StoreList = new List<StoreMaster>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ConString;
        }
        public void InventoryFinalize()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new InventoryFinalizeRequest();

                RequestData.DocumentNo = _IInventoryCountingApproveView.DocumentNo;
                RequestData.Status = _IInventoryCountingApproveView.Status;
                RequestData.RARemarks = _IInventoryCountingApproveView.RARemarks;
                RequestData.RequestedByUserID = _IInventoryCountingApproveView.UserID;
                RequestData.TransactionLogList = _IInventoryCountingApproveView.TransactionLogList;
                RequestData.ConnectionString = GetStoreConnection();

                var ResponseData = _InventoryCountingBLL.InventoryFinalize(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if (_IInventoryCountingApproveView.Status == "Completed")
                    {
                        SyncInventoryDataToServer();
                    }
                    else
                    {
                        _IInventoryCountingApproveView.ProcessStatus = ResponseData.StatusCode;
                        _IInventoryCountingApproveView.Message = "Inventory count is rejected successfully !.";
                    }
                }
                else
                {
                    _IInventoryCountingApproveView.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SyncInventoryDataToServer()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new InventorySyncRequest();

                RequestData.DocumentNo = _IInventoryCountingApproveView.InventoryInitRecord.DocumentNo;
                RequestData.DocumentDate = _IInventoryCountingApproveView.InventoryInitRecord.DocumentDate;
                RequestData.StoreID = _IInventoryCountingApproveView.InventoryInitRecord.StoreID;
                RequestData.Remarks = _IInventoryCountingApproveView.InventoryInitRecord.Remarks;
                RequestData.RARemarks = _IInventoryCountingApproveView.RARemarks;
                RequestData.CountingType = _IInventoryCountingApproveView.InventoryManualCountRecord.CountingType;
                RequestData.RequestedByUserID = _IInventoryCountingApproveView.UserID;

                RequestData.InventorySysCountList = _IInventoryCountingApproveView.InventoryInitRecord.InventorySysCountList;
                RequestData.ManualCountList = _IInventoryCountingApproveView.InventoryManualCountRecord.InventoryManualCountDetailList;
                RequestData.TransactionLogList = _IInventoryCountingApproveView.TransactionLogList;

                RequestData.ConnectionString = null;

                var ResponseData = _InventoryCountingBLL.InventorySyncToServer(RequestData);
                _IInventoryCountingApproveView.ProcessStatus = ResponseData.StatusCode;

                if(ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    _IInventoryCountingApproveView.ProcessStatus = Enums.OpStatusCode.SyncFailed;
                    _IInventoryCountingApproveView.Message = "Inventory count sync to server is failed.Please do manually !.";
                }
                else
                {
                    _IInventoryCountingApproveView.Message = "Inventory count successfully saved and sync to the server !.";
                }
               
            }
            catch(Exception ex)
            {               
                _IInventoryCountingApproveView.ProcessStatus = Enums.OpStatusCode.SyncFailed;
                _IInventoryCountingApproveView.Message = ex.Message + Environment.NewLine + ex.Source;
            }
        }
    }

    public class InventoryDocumentEditPresenter
    {
        IInventoryDocumentEditView _IInventoryDocumentEditView;
        public InventoryDocumentEditPresenter(IInventoryDocumentEditView ViewObj)
        {
            _IInventoryDocumentEditView = ViewObj;
        }
        public void GetManualStockCountRecord()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryManualCountRecordRequest();
                RequestData.DocumentNo = _IInventoryDocumentEditView.DocumentNo;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.GetInventoryManualCountRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryDocumentEditView.InventoryManualCountRecord = ResponseData.InventoryManualCountRecord;
                }
                else
                {
                    _IInventoryDocumentEditView.InventoryManualCountRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInventoryCountinglist()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = new GetInventoryCountingInitResponse();
                ResponseData = _InventoryCountingBLL.GetInventoryCountingInitList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryDocumentEditView.InventoryInitList = ResponseData.InventoryInitList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsValidSKU(string SKUCode)
        {
            bool ValidSKU = false;
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = SKUCode;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if (ResponseData.SKUMasterTypesList != null && ResponseData.SKUMasterTypesList.Count > 0)
                    {
                        _IInventoryDocumentEditView.SKURecord = ResponseData.SKUMasterTypesList.FirstOrDefault();
                        ValidSKU = true;
                    }
                }
            }
            catch
            {
                ValidSKU = false;
            }
            return ValidSKU;
        }        
        public void GetCountryList()
        {
            var _CountryBLL = new CountryBLL();
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryDocumentEditView.CountryList = ResponseData.CountryMasterList;
            }
            else
            {
                _IInventoryDocumentEditView.CountryList = new List<CountryMaster>();
            }
        }
        public void GetStoreList()
        {
            var _StoreMasterBLL = new StoreMasterBLL();
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IInventoryDocumentEditView.CountryID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryDocumentEditView.StoreList = ResponseData.StoreMasterList;
            }
            else
            {
                _IInventoryDocumentEditView.StoreList = new List<StoreMaster>();
            }
        }
        public string GetStoreConnection()
        {
            string ConString = string.Empty;
            try
            {
                var _SyncSettingsBLL = new SyncSettingsBLL();
                GetDBConnectionsRequest RequestData = new GetDBConnectionsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.SyncMode = Enums.SyncMode.EnterpriseToStore;
                RequestData.BaseStoreID = _IInventoryDocumentEditView.StoreID;
                GetDBConnectionsResponse ResponseData = _SyncSettingsBLL.GetDBConnectionList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    ConString = ResponseData.DBConnectionList.FirstOrDefault().ConnectionString;
                }
                else
                {
                    _IInventoryDocumentEditView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConString;
        }
        public void SaveManualStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new SaveManualStockRequest();
                RequestData.InventoryManualCountRecord = _IInventoryDocumentEditView.InventoryManualCountRecord;
                RequestData.Status = "Pending for Approval";
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.SaveManualStock(RequestData);
                _IInventoryDocumentEditView.ProcessStatus = ResponseData.StatusCode;
                _IInventoryDocumentEditView.Message = ResponseData.DisplayMessage;

                if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    throw new Exception(ResponseData.ExceptionMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class InventoryReportPresenter
    {
        IInventoryReportView _IInventoryReportView;
        public InventoryReportPresenter(IInventoryReportView ViewObj)
        {
            _IInventoryReportView = ViewObj;
        }
        public void GetInventoryCountinglist()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = new GetInventoryCountingInitResponse();
                ResponseData = _InventoryCountingBLL.GetInventoryCountingInitList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryReportView.InventoryInitList = ResponseData.InventoryInitList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInventorySummary()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryManualCountRecordRequest();
                RequestData.DocumentNo = _IInventoryReportView.DocumentNo;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.GetInventoryManualCountRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryReportView.InventoryManualCountRecord = ResponseData.InventoryManualCountRecord;
                }
                else
                {
                    _IInventoryReportView.InventoryManualCountRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInitializeStock()
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "DocumentNo";
                RequestData.GroupByMode = _IInventoryReportView.GroupByMode;
                RequestData.DocumentNo = _IInventoryReportView.DocumentNo;
                RequestData.ConnectionString = GetStoreConnection();
                var ResponseData = _InventoryCountingBLL.GetInventoryCountingInitRecord(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInventoryReportView.InventoryInitRecord = ResponseData.InventoryInitRecord;
                }
                else
                {
                    _IInventoryReportView.InventoryInitRecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCountryList()
        {
            var _CountryBLL = new CountryBLL();
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryReportView.CountryList = ResponseData.CountryMasterList;
            }
            else
            {
                _IInventoryReportView.CountryList = new List<CountryMaster>();
            }
        }
        public void GetStoreList()
        {
            var _StoreMasterBLL = new StoreMasterBLL();
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IInventoryReportView.CountryID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IInventoryReportView.StoreList = ResponseData.StoreMasterList;
            }
            else
            {
                _IInventoryReportView.StoreList = new List<StoreMaster>();
            }
        }
        public string GetStoreConnection()
        {
            string ConString = string.Empty;
            try
            {
                var _SyncSettingsBLL = new SyncSettingsBLL();
                GetDBConnectionsRequest RequestData = new GetDBConnectionsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.SyncMode = Enums.SyncMode.EnterpriseToStore;
                RequestData.BaseStoreID = _IInventoryReportView.StoreID;
                GetDBConnectionsResponse ResponseData = _SyncSettingsBLL.GetDBConnectionList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    ConString = ResponseData.DBConnectionList.FirstOrDefault().ConnectionString;
                }
                else
                {
                    _IInventoryReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConString;
        }
    }
}
