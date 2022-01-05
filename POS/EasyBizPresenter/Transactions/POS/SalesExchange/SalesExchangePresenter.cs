using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizIView.Transactions.IPOS.ISalesExchange;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizDBTypes.Transactions.POS;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TaxMasterRequest;

namespace EasyBizPresenter.Transactions.POS.SalesExchange
{
    public class SalesExchangePresenter
    {
        ISalesExchangeView _ISalesExchangeView;
        InvoiceBLL _InvoiceBLL = new InvoiceBLL();
        SalesExchangeBLL _SalesExchangeBLL = new SalesExchangeBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();

        int _RunningNo;
        int _DetailID;

        public SalesExchangePresenter(ISalesExchangeView ViewObj)
        {
            _ISalesExchangeView = ViewObj;
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALESEXCHANGE;
                RequestData.CountryID = _ISalesExchangeView.CountryID;
                RequestData.StateID = _ISalesExchangeView.StateID;
                RequestData.StoreID = _ISalesExchangeView.StoreID;
                RequestData.POSID = _ISalesExchangeView.POSID;
                RequestData.StoreCode = _ISalesExchangeView.UserInfo.StoreCode;
                RequestData.POSCode = _ISalesExchangeView.UserInfo.POSCode;

                //var ResponseData = _DocumentNumberingBLL.DocumentNumberingBillNoGenerate(RequestData);
                //_ISalesExchangeView.DocumentNo = ResponseData;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                    _ISalesExchangeView.DocumentNo = DocumentNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
                else
                {
                    _ISalesExchangeView.DocumentNo = string.Empty;
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
                objUpdateRunningNumRequest.StoreID = _ISalesExchangeView.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetAllInvoiceList()
        {
            try
            {
                var RequestData = new SelectAllInvoiceRequest();

                RequestData.SearchCriteria = _ISalesExchangeView.SearchCriteria;
                RequestData.SearchString = _ISalesExchangeView.ReturnSearchString;

                var ResponseData = _InvoiceBLL.SelectAllInvoice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.InvoiceHeaderList = ResponseData.InvoiceHeaderList;
                }
                else
                {
                    var InvoiceList = new List<InvoiceHeader>();
                    _ISalesExchangeView.InvoiceHeaderList = InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectInvoiceDetailsList()
        {
            try
            {
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.InvoiceHeaderID = _ISalesExchangeView.InvoiceHeaderID;
                var ResponseData = _InvoiceBLL.SelectInvoiceDetailsList(RequestData);
                _ISalesExchangeView.InvoiceDetailsByIDList = ResponseData.InvoiceDetailsList;

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    GetExchangeList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveSalesExchange()
        {
            try
            {
                var RequestData = new SaveSalesExchangeRequest();
                RequestData.SalesExchangeHeaderRecord = new SalesExchangeHeader();

                var ReturnList = new List<SalesExchangeDetail>();
                ReturnList = _ISalesExchangeView.ReturnList.Where(x => x.ExchangeQty > 0 && x.IsExchange == true).ToList();

                int TotalExchangeQty = _ISalesExchangeView.ExchangeList.Sum(x => x.Qty);

                RequestData.SalesExchangeDetailList = _ISalesExchangeView.ExchangeList;
                RequestData.ReturnList = ReturnList;//For Updatiing Invoice Details

                SelectDocumentNumberingRecord();

                RequestData.SalesExchangeHeaderRecord.DocumentNo = _ISalesExchangeView.DocumentNo;
                RequestData.SalesExchangeHeaderRecord.DocumentDate = _ISalesExchangeView.DocumentDate;
                RequestData.SalesExchangeHeaderRecord.SalesInvoiceNumber = _ISalesExchangeView.SalesInvoiceNo;
                RequestData.SalesExchangeHeaderRecord.InvoiceHeaderID = _ISalesExchangeView.InvoiceHeaderID;

                RequestData.SalesExchangeHeaderRecord.CountryID = _ISalesExchangeView.CountryID;
                RequestData.SalesExchangeHeaderRecord.StoreID = _ISalesExchangeView.StoreID;
                RequestData.SalesExchangeHeaderRecord.PosID = _ISalesExchangeView.POSID;
                RequestData.SalesExchangeHeaderRecord.ExchangeWithOutInvoiceNo = _ISalesExchangeView.WithOutInvoiceNo;

                RequestData.SalesExchangeHeaderRecord.CreateBy = _ISalesExchangeView.UserID;
                RequestData.SalesExchangeHeaderRecord.CashierID = _ISalesExchangeView.UserInfo.EmployeeID;
                RequestData.SalesExchangeHeaderRecord.CreditSales = _ISalesExchangeView.CreditSales;

                RequestData.SalesExchangeHeaderRecord.TotalExchangeQty = TotalExchangeQty;

                RequestData.SalesExchangeHeaderRecord.ExchangeMode = _ISalesExchangeView.ExchangeMode;
                RequestData.BaseIntegrateStoreID = _ISalesExchangeView.StoreID;
                RequestData.TransactionLogList = _ISalesExchangeView.TransactionLogList;
                RequestData.SalesExchangeHeaderRecord.CountryCode = _ISalesExchangeView.CountryCode;
                RequestData.SalesExchangeHeaderRecord.StoreCode = _ISalesExchangeView.StoreCode;
                RequestData.SalesExchangeHeaderRecord.POSCode = _ISalesExchangeView.PosCode;

                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;

                var ResponseData = _SalesExchangeBLL.SaveSalesExchange(RequestData);

                _ISalesExchangeView.Message = ResponseData.DisplayMessage;
                _ISalesExchangeView.ProcessStatus = ResponseData.StatusCode;

                if (_ISalesExchangeView.ProcessStatus == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.HeaderID = ResponseData.IDs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SaveTransactionsLog()
        {
            try
            {
                var RequestData = new SaveTransactionLogRequest();
                RequestData.TransactionLogList = _ISalesExchangeView.TransactionLogList;
                var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectManagerOverride(string Source)
        {
            try
            {
                var _ManagerOverrideBLL = new ManagerOverrideBLL();
                var RequestData = new SelectByIDManagerOverrideRequest();
                RequestData.ID = _ISalesExchangeView.ManagerOverrideID;
                var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                if (Source == "PAGELOAD")
                {
                    _ISalesExchangeView.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
                else
                {
                    _ISalesExchangeView.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void GetSKURecordByExchange()
       {
           try
           {
               var _SKUMasterBLL = new SKUMasterBLL();
               var RequestData = new SelectAllSKUMasterRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.RequestFrom = Enums.RequestFrom.Search;
               RequestData.Mode = "SALES";
               RequestData.SearchString = _ISalesExchangeView.ExchangeSearchString;
               RequestData.Count = 1;
               var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var SkuMaster = ResponseData.SKUMasterTypesList.FirstOrDefault();
                    if (SkuMaster != null)
                    {                       
                        GetStockBySKU(SkuMaster.SKUCode);                        
                        if (_ISalesExchangeView.StockData != null && _ISalesExchangeView.StockData.StockQty > 0)
                        {
                            _ISalesExchangeView.SkuRecord = SkuMaster;
                        }
                        else
                        {
                            _ISalesExchangeView.Message = "Negative Stock." + _ISalesExchangeView.ExchangeSearchString + " is not available for exchange !.";
                        }
                    }
                }
                else
                {
                    _ISalesExchangeView.SkuRecord = null;
                }
            }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        public void GetExchangeList()
        {
            try
            {
                var RequestData = new SelectAllSalesExchangeDetailRequest();
                RequestData.Mode = _ISalesExchangeView.ExchangeMode;
                RequestData.InvoiceNo = _ISalesExchangeView.SalesInvoiceNo;
                var ResponseData = new SelectAllSalesExchangeDetailResponse();
                ResponseData = _SalesExchangeBLL.SelectAllSalesExchangeDetailList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if (_ISalesExchangeView.ExchangeMode == "SalesInvoice")
                    {
                        _ISalesExchangeView.ExchangeList = ResponseData.SalesExchangeDetailList;
                    }
                    else
                    {
                        _ISalesExchangeView.ReturnList = ResponseData.SalesExchangeDetailList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetExchangeReceipt()
        {
            try
            {
                var RequestData = new SelectExchangeByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _ISalesExchangeView.DocumentNo;
                var ResponseData = new SelectExchangeByInvoiceNumResponse();
                ResponseData = _SalesExchangeBLL.GetExchangeReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.ExchangeReceiptList = ResponseData.ExchangeReceiptList;
                }
                else
                {
                    _ISalesExchangeView.ExchangeReceiptList = new List<ExchangeReceipt>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSKURecordByReturn()
        {
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.Search;
                RequestData.Mode = "SALES";
                RequestData.SearchString = _ISalesExchangeView.ReturnSearchString;
                RequestData.Count = 1;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var SkuMaster = ResponseData.SKUMasterTypesList.FirstOrDefault();
                    
                    GetStylePricingBySKUCode(SkuMaster.SKUCode);

                    _ISalesExchangeView.SkuRecord = SkuMaster;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStylePricingBySKUCode(string SKUCode)
        {

            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.SKUCode = SKUCode;

                var ResponseData = _SKUMasterBLL.SelectGetStylePricingBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.StylePricingList = ResponseData.StylePricingList;
                }
                else
                {
                    _ISalesExchangeView.Message = ResponseData.DisplayMessage;
                    _ISalesExchangeView.StylePricingList = new List<StylePricing>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStockBySKU(string SKUCode)
        {
            try
            {
                var RequestData = new GetStockBySkuRequest();
                var ResponseData = new GetStockBySkuResponse();
                RequestData.SKUCode = SKUCode;
                RequestData.StoreID = _ISalesExchangeView.UserInfo.StoreID;
                ResponseData = _TransactionLogBLL.GetStockBySku(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.StockData = ResponseData.StockData;
                }
                else
                {
                    _ISalesExchangeView.StockData = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTaxRecordByCountry()
        {
            try
            {
                var _TaxBLL = new TaxBLL();
                var RequestData = new SelectTaxDetailsByCountryIDRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.CountryID = _ISalesExchangeView.CountryID;
                RequestData.Type = true;
                var ResponseData = _TaxBLL.SelectTaxDetailsByCountryID(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeView.TaxList = ResponseData.TaxDetailList;
                }
                else
                {
                    _ISalesExchangeView.TaxList = new List<TaxMaster>();
                }
            }
            catch 
            {
                _ISalesExchangeView.TaxList = new List<TaxMaster>();
            }
        }
    }

    public class SalesExchangeCollectionViewPresenter
    {
        SalesExchangeBLL _SalesExchangeBLL = new SalesExchangeBLL();
        ISalesExchangeCollectionView _ISalesExchangeCollectionView;
        public SalesExchangeCollectionViewPresenter(ISalesExchangeCollectionView ViewObj)
        {
            _ISalesExchangeCollectionView = ViewObj;
        }
        public void GetSalesExchangeList()
        {
            try
            {
                //SelectAllSalesExchangeResponse SelectAllSalesExchangeList(SelectAllSalesExchangeRequest objRequest)
                var RequestData = new SelectAllSalesExchangeRequest();
                var ResponseData = new SelectAllSalesExchangeResponse();
                ResponseData = _SalesExchangeBLL.SelectAllSalesExchangeList(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeCollectionView.SalesExchangeList = ResponseData.SalesExchangeList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        
    }
}
