using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizIView.Transactions.IPOS.ISalesReturns;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TaxMasterRequest;

namespace EasyBizPresenter.Transactions.POS
{
    public class SalesReturnPresenter
    {
        ISalesReturnView _ISalesReturnView;
        SalesReturnBLL _SalesReturnBLL = new SalesReturnBLL();
        InvoiceBLL _InvoiceBLL = new InvoiceBLL();

        int _RunningNo;
        int _DetailID;
        public SalesReturnPresenter(ISalesReturnView ViewObj)
        {
            _ISalesReturnView = ViewObj;
        }
        public void GetInvoiceList()
        {
            try
            {
                var RequestData = new SelectAllInvoiceRequest();                
                var ResponseData = _InvoiceBLL.SelectAllInvoice(RequestData);
                //_ISalesReturnView.InvoiceHeaderList = ResponseData.InvoiceHeaderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectInvoiceDetailsList(string Mode,string InvoiceNo)
        {
            try
            {
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.InvoiceHeaderID = _ISalesReturnView.InvoiceHeaderID;
                RequestData.RequestFrom = Enums.RequestFrom.Search;
                if (InvoiceNo != null && InvoiceNo != string.Empty)
                {
                    RequestData.SearchString = InvoiceNo;
                }
                else
                {
                    RequestData.SearchString = _ISalesReturnView.SearchString;
                }
                var ResponseData = _InvoiceBLL.SelectInvoiceDetailsList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //if (Mode == "SalesInvoice")
                    //{
                        _ISalesReturnView.InvoiceDetailsList = ResponseData.InvoiceDetailsList;
                        _ISalesReturnView.ExchangeInvoiceDetailsList = null;
                    //}
                    //else
                    //{
                    //    _ISalesReturnView.ExchangeInvoiceDetailsList = ResponseData.InvoiceDetailsList;
                    //}
                }
                else
                {
                    var InvoiceDetailsList = new List<InvoiceDetails>();
                    _ISalesReturnView.InvoiceDetailsList = InvoiceDetailsList;

                    GetExchangeList();
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
                var _DocumentNumberingBLL = new DocumentNumberingBLL();
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALESRETURN;
                RequestData.CountryID = _ISalesReturnView.CountryID;
                RequestData.StateID = _ISalesReturnView.StateID;
                RequestData.StoreID = _ISalesReturnView.StoreID;
                RequestData.POSID = _ISalesReturnView.POSID;

                RequestData.StoreCode = _ISalesReturnView.UserInfo.StoreCode;
                RequestData.POSCode = _ISalesReturnView.UserInfo.POSCode;

                //var ResponseData = _DocumentNumberingBLL.DocumentNumberingBillNoGenerate(RequestData);
                //_ISalesExchangeView.DocumentNo = ResponseData;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                    _ISalesReturnView.DocumentNo = DocumentNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
                else
                {
                    _ISalesReturnView.DocumentNo = string.Empty;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceReturnReceipt()
        {
            try
            {
                var RequestData = new SelectInvoiceReturnReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _ISalesReturnView.PrintInvoiceNo;
                var ResponseData = new SelectInvoiceReturnReceiptByInvoiceNumResponse();
                ResponseData = _SalesReturnBLL.GetInvoiceReturnReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.InvoiceReturnReceipt = ResponseData.InvoiceReturnList;
                }
                else
                {
                    _ISalesReturnView.InvoiceReturnReceipt = new List<InvoiceReturnReceipt>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetInvoiceReceipt(string PrintInvoiceNo,string ReturnInvoiceNo)
        {
            try
            {
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = PrintInvoiceNo;

                RequestData.ReturnInvoiceNo = ReturnInvoiceNo;

                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.InvoiceReceiptList = ResponseData.InvoiceList;
                }
                else
                {
                    _ISalesReturnView.InvoiceReceiptList = new List<InvoiceReceiptTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceSubReceipt(string PrintInvoiceNo)
        {
            try
            {
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = PrintInvoiceNo;
               
                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.InvoiceSubReceiptList = ResponseData.InvoiceSubReceiptTList;
                }
                else
                {
                    _ISalesReturnView.InvoiceSubReceiptList = new List<InvoiceSubReceiptTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceApprovalNum(string PrintInvoiceNo)
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceApprovalNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = PrintInvoiceNo;
                var ResponseData = new SelectInvoiceReceiptApprovalNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt2(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.ApprovalNumReceiptList = ResponseData.ApprovalNumReceiptList;
                }
                else
                {
                    _ISalesReturnView.ApprovalNumReceiptList = new List<ApprovalNumReceipt>();
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
                var _DocumentNumberingBLL = new DocumentNumberingBLL();
                UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
                UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

                objUpdateRunningNumRequest.RunningNo = _RunningNo;
                objUpdateRunningNumRequest.DetailID = _DetailID;
                objUpdateRunningNumRequest.StoreID = _ISalesReturnView.StoreID;

                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveSalesReurn()
        {
            var RequestData = new SaveSalesReturnRequest();
            var ResponseData = new SaveSalesReturnResponse();
            try
            {
                SelectDocumentNumberingRecord();
                RequestData.SalesReturnHeaderData = _ISalesReturnView.SalesReturnData;
                RequestData.SalesReturnHeaderData.DocumentNo = _ISalesReturnView.DocumentNo;
                if(_ISalesReturnView.OnAccountPaymentRecord != null)
                {
                    RequestData.SalesReturnHeaderData.CreditSales = true;
                }

                RequestData.BaseIntegrateStoreID = _ISalesReturnView.StoreID;
                RequestData.TransactionLogList = _ISalesReturnView.TransactionLogList;
                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;
                RequestData.OnAccountPaymentRecord = _ISalesReturnView.OnAccountPaymentRecord;

                ResponseData = _SalesReturnBLL.SaveSalesReturn(RequestData);
                _ISalesReturnView.Message = ResponseData.DisplayMessage;
                _ISalesReturnView.ProcessStatus = ResponseData.StatusCode;

                if (ResponseData.IDs != null && ResponseData.IDs != string.Empty)
                {
                    _ISalesReturnView.ID = Convert.ToInt32(ResponseData.IDs);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SelectPromotionsRecord()
        {
            try
            {
                var _PromotionsMasterBLL = new PromotionsMasterBLL();
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.RequestedProcess = "SalesReturn";
                var ResponseData = _PromotionsMasterBLL.SelectAllPromotionsRecords(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.PromotionsMasterList = ResponseData.PromotionsList;
                }
                else
                {
                    _ISalesReturnView.PromotionsMasterList = new List<PromotionsMaster>();
                }

            }
            catch
            {
                _ISalesReturnView.PromotionsMasterList = new List<PromotionsMaster>();
            }
        }
        public void SaveTransactionsLog()
        {
            try
            {
                var _TransactionLogBLL = new TransactionLogBLL();
                var RequestData = new SaveTransactionLogRequest();
                RequestData.TransactionLogList = _ISalesReturnView.TransactionLogList;
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
                RequestData.ID = _ISalesReturnView.ManagerOverrideID;
                var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                if (Source == "PAGELOAD")
                {
                    _ISalesReturnView.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
                else
                {
                    _ISalesReturnView.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
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
                var _SalesExchangeBLL = new SalesExchangeBLL();

                var RequestData = new SelectAllSalesExchangeDetailRequest();

                RequestData.Mode = "ReturnInvoice";
                RequestData.InvoiceNo = _ISalesReturnView.SearchString;

                var ResponseData = new SelectAllSalesExchangeDetailResponse();
                ResponseData = _SalesExchangeBLL.SelectAllSalesExchangeDetailList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //_ISalesReturnView.ExchangeList = ResponseData.SalesExchangeDetailList;
                    //For On-Account Checking - Start
                    if (ResponseData.SalesExchangeDetailList != null && ResponseData.SalesExchangeDetailList.Count > 0)
                    {
                        //_ISalesReturnView.CreditSales = ResponseData.SalesExchangeDetailList.FirstOrDefault().CreditSales;
                        //if (ResponseData.SalesExchangeDetailList.FirstOrDefault().CreditSales)
                        //{
                            SelectInvoiceDetailsList("ExchangeInvoice", ResponseData.SalesExchangeDetailList.FirstOrDefault().SalesInvoiceNumber);
                        //}
                    }
                    //For On-Account Checking - End
                }
                else
                {
                    var ExchangeList = new List<SalesExchangeDetail>();
                    _ISalesReturnView.ExchangeList = ExchangeList;
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
                RequestData.SearchString = _ISalesReturnView.SearchString;
                RequestData.Count = 1;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var SkuMaster = ResponseData.SKUMasterTypesList.FirstOrDefault();
                    
                    GetStylePricingBySKUCode(SkuMaster.SKUCode);

                    _ISalesReturnView.SkuRecord = SkuMaster;
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
                    _ISalesReturnView.StylePricingList = ResponseData.StylePricingList;
                }
                else
                {
                    _ISalesReturnView.Message = ResponseData.DisplayMessage;
                    _ISalesReturnView.StylePricingList = new List<StylePricing>();
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
                RequestData.CountryID = _ISalesReturnView.CountryID;
                RequestData.Type = true;
                var ResponseData = _TaxBLL.SelectTaxDetailsByCountryID(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnView.TaxList = ResponseData.TaxDetailList;
                }
                else
                {
                    _ISalesReturnView.TaxList = new List<TaxMaster>();
                }
            }
            catch (Exception ex)
            {
                _ISalesReturnView.TaxList = new List<TaxMaster>();
            }
        }
    }
    public class SalesReturnCollectionViewPresenter
    {
        ISalesReturnCollectionView _ISalesReturnCollectionView;
        public SalesReturnCollectionViewPresenter(ISalesReturnCollectionView ObjView)
        {
            _ISalesReturnCollectionView = ObjView;
        }
        public void GetSalesReturnList()
        {
            var _SalesReturnBLL = new SalesReturnBLL();
            var RequestData = new SelectAllSalesReturnRequest();
            var ResponseData = new SelectAllSalesReturnResponse();
            try
            {
                ResponseData = _SalesReturnBLL.SelectAllSalesReturn(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnCollectionView.SalesReturnHeaderList = ResponseData.SalesReturnHeaderList;
                }
                else
                {
                    _ISalesReturnCollectionView.SalesReturnHeaderList = new List<SalesReturnHeader>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
