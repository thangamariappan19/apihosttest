using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizBLL.Transactions.Promotions;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizIView.Masters.IShift;
using EasyBizIView.Transactions.IPOS.IInvoice;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.EmployeeDiscountInfoRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizRequest.Transactions.DiscountMasterRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Common;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.EmployeeDiscountInfoResponse;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.Promotions.DiscountMasterResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
    public class InvoicePresenter
    {
        DayShiftLOGBLL _DayShiftLOGBLL = new DayShiftLOGBLL();
        IInvoiceView _IInvoiceView;        
        ShiftBLL _ShiftBLL = new ShiftBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
        CustomerSpecialPriceMasterBLL _CustomerSpecialPriceMasterBLL = new CustomerSpecialPriceMasterBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        PromotionsMasterBLL _PromotionsMasterBLL = new PromotionsMasterBLL();
        InvoiceBLL _InvoiceBLL = new InvoiceBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        TaxBLL _TaxBLL = new TaxBLL();
        DayClosingBLL _DayClosingBLL = new DayClosingBLL();      
        ExchangeRatesBLL _ExchangeRatesBLL = new ExchangeRatesBLL();
        InvoiceCashDetailsBLL _InvoiceCashDetailsBLL = new InvoiceCashDetailsBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();

        private static string _BillNo = string.Empty;
        private static int _DetailID = 0;
        private static int _RunningNo = 0;
        public InvoicePresenter(IInvoiceView ViewObj)
        {
            _IInvoiceView = ViewObj;
        }
        public bool IsValidBusinessDate()
        {
            bool objbool = true;
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;               
                RequestData.Type = "BusinessDateStatus";
                RequestData.BusinessDate = _IInvoiceView.BusinessDate;
                var ResponseData = new SelectShiftLogResponse();
                ResponseData = _ShiftBLL.SelectShiftLogRecordbyID(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if(ResponseData.ShiftTypesData != null && ResponseData.ShiftTypesData.Status == "Close")
                    {
                        objbool = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objbool;
        }
        //public void SelectShiftListForCategory()
        //{
        //    try
        //    {
        //        var RequestData = new SelectByCountryIDRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
        //        SelectByCountryIDResponse ResponseData = _ShiftBLL.SelectCountryRecord(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IShiftMasterView.ShiftList = ResponseData.ShiftMasterList;
        //        }
        //        else
        //        {
        //            var test = ResponseData.DisplayMessage;
        //            _IInvoiceView.Message = test;
        //            _IInvoiceView.ProcessStatus = ResponseData.StatusCode;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void GetCustomer()
        {
            try
            {
                //SelectAllCustomerMasterResponse SelectAllCustomerMaster(SelectAllCustomerMasterRequest objRequest)

                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Source = "Sales";
                RequestData.ID = _IInvoiceView.CustomerID;
                RequestData.CustomerInfo = _IInvoiceView.CustomerSearchString;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new SelectAllCustomerMasterResponse();

                ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.CustomerMasterList = ResponseData.CustomerMasterData;

                    if (ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupID != 0 && ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupCode != null)
                    {
                        GetDiscountRecord(ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupID, ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupCode , ResponseData.CustomerMasterData.FirstOrDefault().CustomerCode);
                    }
                }
                else
                {
                    var CustomerList = new List<CustomerMaster>();
                    _IInvoiceView.CustomerMasterList = CustomerList;
                }
            }
            catch (Exception ex)
            {
                var CustomerList = new List<CustomerMaster>();
                _IInvoiceView.CustomerMasterList = CustomerList;
                throw ex;
            }
        }
        public void GetDiscountRecord(int CustomerGroupID,string CustomerGroupCode , string CustomerCode)
        {
            var _DiscountMasterBLL = new DiscountMasterBLL();
            var RequestData = new SelectAllDiscountMasterRequest();
            RequestData.CustomerGroupID = CustomerGroupID;
            RequestData.CustomerGroupCode = CustomerGroupCode;
            var ResponseData = new SelectAllDiscountMasterResponse();
            try
            {
                ResponseData = _DiscountMasterBLL.SelectAllMappingRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.DiscountMasterRecord = ResponseData.DiscountMasterRecord;

                    GetEmployeeDiscountInfo(CustomerCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _DiscountMasterBLL = null;
                RequestData = null;
                ResponseData = null;
            }
        }

        public void GetShiftLog()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
                RequestData.StoreID = _IInvoiceView.UserInformation.StoreID;
                RequestData.POSID = _IInvoiceView.UserInformation.POSID;
                var ResponseData = new SelectShiftLogResponse();
                ResponseData = _ShiftBLL.SelectShiftLogRecordbyID(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.ShiftLOGTypesList = ResponseData.ShiftTypesData;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceReceipt()
        {
            try
            {
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceView.PrintInvoiceNo;
                RequestData.ReturnInvoiceNo = string.Empty;
                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.InvoiceReceiptList = ResponseData.InvoiceList;
                }
                else
                {
                    _IInvoiceView.InvoiceReceiptList = new List<InvoiceReceiptTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceSubReceipt()
        {
            try
            {
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceView.PrintInvoiceNo;
                RequestData.ReturnInvoiceNo = string.Empty;
                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.InvoiceSubReceiptList = ResponseData.InvoiceSubReceiptTList;
                }
                else
                {
                    _IInvoiceView.InvoiceSubReceiptList = new List<InvoiceSubReceiptTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceApprovalNum()
        {
            try
            {
                var RequestData = new SelectInvoiceApprovalNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceView.PrintInvoiceNo;
                var ResponseData = new SelectInvoiceReceiptApprovalNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt2(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.ApprovalNumReceiptList = ResponseData.ApprovalNumReceiptList;
                }
                else
                {
                    _IInvoiceView.ApprovalNumReceiptList = new List<ApprovalNumReceipt>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetHoldReceipt()
        {
            try
            {
                var RequestData = new SelectHoldReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceView.HoldBillNo;
                var ResponseData = new SelectHoldReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetHoldReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.IHoldReceiptList = ResponseData.HoldReceiptList;
                }
                else
                {
                    _IInvoiceView.IHoldReceiptList = new List<HoldReceipt>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public void GetAllSKUList()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
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
                RequestData.SKUCode = _IInvoiceView.SkuCode;
                RequestData.StoreID = _IInvoiceView.UserInformation.StoreID;
                ResponseData = _TransactionLogBLL.GetStockBySku(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.StockData = ResponseData.StockData;
                }
                else
                {
                    _IInvoiceView.StockData = null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetSKU()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _IInvoiceView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    
                    List<SKUMasterTypes> tempSKUDataList = new List<SKUMasterTypes>();
                    /*tempSKUDataList.Add(ResponseData.SKUMasterTypesList);
                    if(ResponseData.SKUMasterTypesList.Sum)*/
                    _IInvoiceView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }              
                else
                {
                    _IInvoiceView.SKUMasterTypesList = new List<SKUMasterTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSalesOrder()
        {
            try
            {
                var _SalesOrderBLL = new SalesOrderBLL();
                var RequestData = new SelectBySalesOrderIDRequest();
                RequestData.DocumentNo = _IInvoiceView.SKUSearchString;
                var ResponseData = _SalesOrderBLL.SelectSalesOrderRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.SalesOrderRecord = ResponseData.SalesOrderMasterRecord;
                }
                else
                {
                    _IInvoiceView.SalesOrderRecord = null;
                }
            }
            catch
            {
                _IInvoiceView.SalesOrderRecord = null;
            }
        }

        public void SelectCustomerSpecialPriceMasterInfo()
        {
            try
            {
                var RequestData = new SelectAllCustomerSpecialPriceMasterRequest();
                //RequestData.CustomerSpecialPriceMasterInfo = _IInvoiceView.CustomerSpecialMasterInfo;
                RequestData.Source = "Sales";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _CustomerSpecialPriceMasterBLL.SelectAllCustomerSpecialPriceMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.CustomerSpecialPriceMasterList = ResponseData.CustomerSpecialPriceMasterTypesList;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IInvoiceView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IInvoiceView.CustomerSpecialPriceMasterList = new List<CustomerSpecialPriceMasterTypes>();
                 
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectByIDSKUMaster()
        {

            try
            {
                var RequestData = new SelectByIDSKUMasterRequest();

                RequestData.SkuCode = _IInvoiceView.SkuCode;

                RequestData.Source = "Sales";

                var ResponseData = _SKUMasterBLL.SelectByIdSKUMaster(RequestData);



                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
                else
                {
                    _IInvoiceView.Message = ResponseData.DisplayMessage;
                    _IInvoiceView.SKUMasterTypesList = new List<SKUMasterTypes>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SaveInvoice(Enums.InvoiceStatus InvoiceStatus)
        {
            try
            {
                var RequestData = new SaveInvoiceRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.InvoiceHeaderData = new InvoiceHeader();

                var objInvoiceHeaderTypes = new InvoiceHeader();
                objInvoiceHeaderTypes.ID = _IInvoiceView.ID;
                objInvoiceHeaderTypes.CountryID = _IInvoiceView.UserInformation.CountryID;
                objInvoiceHeaderTypes.StoreID = _IInvoiceView.UserInformation.StoreID;
                objInvoiceHeaderTypes.PosID = _IInvoiceView.UserInformation.POSID;
                objInvoiceHeaderTypes.DocumentDate = _IInvoiceView.InvoiceDate;
                objInvoiceHeaderTypes.BusinessDate = _IInvoiceView.BusinessDate;

                if (InvoiceStatus == Enums.InvoiceStatus.ParkSale)
                {
                    GetSalesHoldDocumentNumberingRecord();
                    objInvoiceHeaderTypes.InvoiceNo = _IInvoiceView.HoldBillNo;
                    RequestData.PaymentList = new List<PaymentDetail>();
                }
                else
                {
                    SelectDocumentNumberingRecord();
                    objInvoiceHeaderTypes.InvoiceNo = _IInvoiceView.BillNo;
                    RequestData.PaymentList = CashList();
                }
                objInvoiceHeaderTypes.CustomerGroupID = 0;
                objInvoiceHeaderTypes.CustomerID = _IInvoiceView.CustomerID;
                objInvoiceHeaderTypes.ShiftID = _IInvoiceView.ShiftID;
                objInvoiceHeaderTypes.TotalQty = _IInvoiceView.TotalQty;
                objInvoiceHeaderTypes.SubTotalAmount = _IInvoiceView.SubTotalAmount;
                objInvoiceHeaderTypes.TotalDiscountType = _IInvoiceView.TotalDiscountType;
                objInvoiceHeaderTypes.TotalDiscountAmount = _IInvoiceView.TotalDiscountAmount;
                objInvoiceHeaderTypes.TotalDiscountPercentage = _IInvoiceView.TotalDiscountPercentage;
                objInvoiceHeaderTypes.TaxID = _IInvoiceView.TaxID;
                objInvoiceHeaderTypes.TaxAmount = _IInvoiceView.TaxAmount;
                objInvoiceHeaderTypes.SubTotalWithTaxAmount = _IInvoiceView.SubTotalWithTaxAmount;
                objInvoiceHeaderTypes.NetAmount = _IInvoiceView.TotalPrice;
                objInvoiceHeaderTypes.ReceivedAmount = _IInvoiceView.TotalPrice;
                objInvoiceHeaderTypes.AppliedPriceListID = 0;
                objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = 0;
                objInvoiceHeaderTypes.AppliedPromotionID = 0;
                objInvoiceHeaderTypes.SalesEmployeeID = _IInvoiceView.EmployeeID;
                objInvoiceHeaderTypes.SalesManagerID = _IInvoiceView.SalesManagerID;
                objInvoiceHeaderTypes.CashierID = _IInvoiceView.UserInformation.ID;

                objInvoiceHeaderTypes.RefNumber = _IInvoiceView.RefNumber;
                
                objInvoiceHeaderTypes.CreateBy = _IInvoiceView.UserInformation.ID;

                objInvoiceHeaderTypes.SCN = _IInvoiceView.SCN;

                objInvoiceHeaderTypes.CountryCode = _IInvoiceView.CountryCode;
                objInvoiceHeaderTypes.StoreCode = _IInvoiceView.StoreCode;
                objInvoiceHeaderTypes.PosCode = _IInvoiceView.PosCode;                
                objInvoiceHeaderTypes.CustomerCode = _IInvoiceView.CustomerCode;               
                objInvoiceHeaderTypes.SalesEmployeeCode = _IInvoiceView.SalesEmployeeCode;
                objInvoiceHeaderTypes.DiscountRemarks = _IInvoiceView.DiscountRemarks;
                objInvoiceHeaderTypes.BeforeRoundOffAmount = _IInvoiceView.BeforeRoundOffAmount;
                objInvoiceHeaderTypes.RoundOffAmount = _IInvoiceView.RoundOffAmount;

                //objInvoiceHeaderTypes.SubTotalWithOutDiscount = _IInvoiceView.SubTotalWithOutDiscount;


                int Status = (int)InvoiceStatus;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                objInvoiceHeaderTypes.SalesStatus = TypeName;

                if (_IInvoiceView.InvoiceDetailsList != null)
                {
                    RequestData.InvoiceDetailList = _IInvoiceView.InvoiceDetailsList;
                }               
                RequestData.InvoiceHeaderData = objInvoiceHeaderTypes;

                RequestData.BaseIntegrateStoreID = _IInvoiceView.UserInformation.StoreID;
                RequestData.TransactionLogList = _IInvoiceView.TransactionLogList;
                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;

                if (_IInvoiceView.SalesOrderRecord != null)
                {
                    RequestData.SalesOrderDocumentNo = _IInvoiceView.SalesOrderRecord.DocumentNo;
                }
                else
                {
                    RequestData.SalesOrderDocumentNo = string.Empty;
                }

                var ResponseData = _InvoiceBLL.SaveInvoice(RequestData);

                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.InvoiceHeaderID = Convert.ToInt32(ResponseData.IDs);
                    //UpdateRunningNo();
                }

                _IInvoiceView.InvoiceStatus = ResponseData.StatusCode;

                if (InvoiceStatus == Enums.InvoiceStatus.Completed)
                {
                    _IInvoiceView.Message = ResponseData.DisplayMessage;
                }
                else if (InvoiceStatus == Enums.InvoiceStatus.ParkSale)
                {
                    _IInvoiceView.Message = "The sales is holded !.";
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
                objUpdateRunningNumRequest.StoreID = _IInvoiceView.UserInformation.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }        
        public List<PaymentDetail> CashList()
        {
            var _CashList = new List<PaymentDetail>();

            var InVoiceCashDetailsData = new PaymentDetail();
            InVoiceCashDetailsData.BusinessDate = _IInvoiceView.BusinessDate;
            InVoiceCashDetailsData.InvoiceHeaderID = _IInvoiceView.InvoiceHeaderID;
            InVoiceCashDetailsData.InvoiceNumber = _IInvoiceView.BillNo;
            InVoiceCashDetailsData.PayCurrency = _IInvoiceView.PaymentCurrency;
            InVoiceCashDetailsData.PayCurrencyID = _IInvoiceView.UserInformation.CurrencyID;
            InVoiceCashDetailsData.ChangeCurrency = _IInvoiceView.PaymentCurrency;
            InVoiceCashDetailsData.ChangeCurrencyID = _IInvoiceView.UserInformation.CurrencyID;
            InVoiceCashDetailsData.FromCountryID = _IInvoiceView.UserInformation.CountryID;
            InVoiceCashDetailsData.FromStoreID = _IInvoiceView.UserInformation.StoreID;
            InVoiceCashDetailsData.Receivedamount = _IInvoiceView.TotalPrice;
            InVoiceCashDetailsData.CreateBy = _IInvoiceView.UserID;
            InVoiceCashDetailsData.Mode = "Cash";

            _CashList.Add(InVoiceCashDetailsData);

            return _CashList;
        }
        public void SaveTransactionsLog()
        {
            try
            {
                var RequestData = new SaveTransactionLogRequest();
                RequestData.TransactionLogList = _IInvoiceView.TransactionLogList;
                var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }       
        public void GetStylePricingBySKUCode()
        {

            try
            {
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.SKUCode = _IInvoiceView.SkuCode;

                var ResponseData = _SKUMasterBLL.SelectGetStylePricingBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.StylePricingList = ResponseData.StylePricingList;
                }
                else
                {
                    _IInvoiceView.Message = ResponseData.DisplayMessage;
                    _IInvoiceView.StylePricingList = new List<StylePricing>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetPriceListByPriceListIds()
        {
            try
            {
                var RequestData = new SelectByIDsPriceListRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.PriceListIDS = _IInvoiceView.PriceListIDs;
                var ResponseData = _PriceListBLL.SelectByIDsPriceList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.PriceListType = ResponseData.PriceList;
                }
                else
                {
                    _IInvoiceView.PriceListType = new List<PriceListType>();
                    _IInvoiceView.Message = ResponseData.DisplayMessage;
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
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALES;
                RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
                RequestData.StateID = _IInvoiceView.UserInformation.StateID;
                RequestData.StoreID = _IInvoiceView.UserInformation.StoreID;
                //RequestData.POSID = _IInvoiceView.UserInformation.POSID;

                RequestData.StoreCode = _IInvoiceView.UserInformation.StoreCode;
                //RequestData.POSCode = _IInvoiceView.UserInformation.POSCode;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BillNo = string.Empty;
                    BillNo = BillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IInvoiceView.BillNo = BillNo;
                    _IInvoiceView.PrintInvoiceNo = BillNo;

                    _BillNo = BillNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
                else
                {
                    _IInvoiceView.BillNo = string.Empty;
                    _IInvoiceView.PrintInvoiceNo = string.Empty; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        public void GetSalesHoldDocumentNumberingRecord()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALESHOLD;
                RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
                RequestData.StateID = _IInvoiceView.UserInformation.StateID;
                RequestData.StoreID = _IInvoiceView.UserInformation.StoreID;
                //RequestData.POSID = _IInvoiceView.UserInformation.POSID;

                RequestData.StoreCode = _IInvoiceView.UserInformation.StoreCode;
                //RequestData.POSCode = _IInvoiceView.UserInformation.POSCode;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string HoldBillNo = string.Empty;
                    HoldBillNo = HoldBillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                    _IInvoiceView.HoldBillNo = HoldBillNo;
                    //_BillNo = HoldBillNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
                else
                {
                    _IInvoiceView.HoldBillNo = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTaxRecordByStoreID()
        {
            try
            {
                var RequestData = new SelectTaxDetailsByCountryIDRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
                RequestData.Type = true;
                var ResponseData = _TaxBLL.SelectTaxDetailsByCountryID(RequestData);
                _IInvoiceView.TaxList = ResponseData.TaxDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveDayClosing()
        {
            try
            {
                    DateTime BusinessDate = DateTime.Now;

                    SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                    RequestData.DayClosingRecord = new DayClosing();

                    RequestData.DayClosingRecord.BuisnessDate = BusinessDate;
                    RequestData.DayClosingRecord.ID = 0;
                    RequestData.DayClosingRecord.CountryID = _IInvoiceView.UserInformation.CountryID;
                    RequestData.DayClosingRecord.StoreID = _IInvoiceView.UserInformation.StoreID;                    
                    RequestData.DayClosingRecord.POSID = _IInvoiceView.UserInformation.POSID;
                    RequestData.DayClosingRecord.ShiftID = _IInvoiceView.ShiftID;
                    RequestData.DayClosingRecord.ShiftInUserID = _IInvoiceView.UserInformation.EmployeeID;
                    RequestData.DayClosingRecord.Status = "Open";
                    RequestData.DayClosingRecord.StartingTime = BusinessDate;
                    RequestData.DayClosingRecord.ClosingTime = BusinessDate;
                    SaveDayClosingResponse ResponseData = _DayClosingBLL.SaveDayClosing(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IInvoiceView.BusinessDate = BusinessDate;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.DuplicateRecordFound)
                    {
                        _IInvoiceView.BusinessDate = DateTime.Now.AddDays(1);
                    }
                    else if (ResponseData.StatusCode == 0)
                    {
                       _IInvoiceView.BusinessDate = BusinessDate;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateDayClosing()
        {
            try
            {             
                   SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                    RequestData.DayClosingRecord = new DayClosing();

                    RequestData.DayClosingRecord.BuisnessDate =  _IInvoiceView.BusinessDate;
                    RequestData.DayClosingRecord.StartingTime = _IInvoiceView.BusinessDate;
                    RequestData.DayClosingRecord.StoreID = _IInvoiceView.UserInformation.StoreID;
                    RequestData.DayClosingRecord.POSID = _IInvoiceView.UserInformation.POSID;
                    RequestData.DayClosingRecord.ShiftID = _IInvoiceView.ShiftID;
                    RequestData.DayClosingRecord.ShiftInUserID = 0;
                    RequestData.DayClosingRecord.Status = "Closed";
                    RequestData.DayClosingRecord.ClosingTime = DateTime.Now;
                    SaveDayClosingResponse ResponseData = _DayClosingBLL.UpdateDayClosing(RequestData);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSkuImageList()
        {
            var RequestData = new SelectByALLSKUImagesRequest();
            var ResponseData = new SelectAllSKUImagesResponse();
            try
            {
                RequestData.SKUID = _IInvoiceView.SKUID;
                RequestData.StyleID = _IInvoiceView.StyleID;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                ResponseData = _SKUMasterBLL.SelectAllSKUImages(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.SKUImageList = ResponseData.SKUImageList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetWNPromotionList()
        {
            var _WNPromotionBLL = new WNPromotionBLL();
            try
            {                
                var RequestData = new SelectAllWNPromotionRequest();
                var ResponseData = new SelectAllWNPromotionResponse();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.PriceListID = _IInvoiceView.UserInformation.PriceListID;
                RequestData.CountryID = _IInvoiceView.UserInformation.CountryID;
                ResponseData = _WNPromotionBLL.SelectAllWNPromotion(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.WNPromotionList = ResponseData.WNPromotionList;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCurrencyList()
        {
            try
            {
                var RequestData = new SelectCurrencyLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.CurrencyMasterList = ResponseData.CurrencyMasterList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SelectRetailSettings()
        {
            try
            {
                var _RetailSettingsBLL = new RetailSettingsBLL();
                var RequestData = new SelectByRetailIDRequest();
                RequestData.ID = _IInvoiceView.UserInformation.RetailID;
                var ResponseData = _RetailSettingsBLL.SelectRetailRecord(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.RetailSetting = ResponseData.RetailRecord;
                }
                else
                {
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public void GetEmployeeMasterList()
        {
            try
            {
                var _EmployeeMasterBLL = new EmployeeMasterBLL();
                var RequestData = new SelectAllEmployeeMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.StoreID = _IInvoiceView.UserInformation.StoreID;
                var ResponseData = new SelectAllEmployeeMasterResponse();               
                ResponseData = _EmployeeMasterBLL.SelectAllEmployeeMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.EmployeeList = ResponseData.EmployeeMasterList;
                }
            }
            catch(Exception ex)
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
                RequestData.ID = _IInvoiceView.ManagerOverrideID;
                var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                if (Source == "PAGELOAD")
                {
                    _IInvoiceView.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;                    
                }
                else
                {
                    _IInvoiceView.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
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
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.RequestedProcess = "SalesInvoice";
                var ResponseData = _PromotionsMasterBLL.SelectAllPromotionsRecords(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.PromotionsMasterList = ResponseData.PromotionsList;
                }
                else
                {
                    _IInvoiceView.PromotionsMasterList = new List<PromotionsMaster>();
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void SelectPromotionCriteriaDetails()
        {
            try
            {
                var RequestData = new SelectPromotionCriteriaRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.PromotionCode = _IInvoiceView.PromotionCode;
                var ResponseData = _PromotionsMasterBLL.SelectPromotionCriteria(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.PromotionCriteriaList = ResponseData.PromotionCriteriaList;
                }
                else
                {
                    _IInvoiceView.PromotionCriteriaList = new List<PromotionCriteria>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SelectBrandListByStoreID()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ID = _IInvoiceView.UserInformation.StoreID;
                var ResponseData = _StoreMasterBLL.SelectByIDStoreMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BrandNames = ResponseData.StoreMasterData.Brand;
                    _IInvoiceView.BrandNames = BrandNames.Split(';');
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetEmployeeDiscountInfo(string CustomerCode)
        {            
            try
            {
                var RequestData = new SelectEmployeeDiscountInfoByCustCode();
                RequestData.CustomerCode = CustomerCode;
                var ResponseData = new SelectEmployeeDiscountInfoResponseByCustCode();
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceView.EmployeeDiscountInfoList = ResponseData.EmployeeDiscountInfoList;
                }
                else
                {
                    _IInvoiceView.EmployeeDiscountInfoList = new List<EmployeeDiscountInfo>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
