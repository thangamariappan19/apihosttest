using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizIView.Transactions.IPOS.ISalesOrder;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class SalesOrderPresenter
    {
        ISalesOrderView _ISalesOrderView;
        SalesOrderBLL _SalesOrderBLL = new SalesOrderBLL();
        long _RunningNo = 0;
        long _DetailID = 0;

        public SalesOrderPresenter(ISalesOrderView ViewObj)
        {
            _ISalesOrderView = ViewObj;
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var _DocumentNumberingBLL = new DocumentNumberingBLL();
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALESORDER;
                RequestData.CountryID = _ISalesOrderView.UserInformation.CountryID;
                RequestData.StateID = _ISalesOrderView.UserInformation.StateID;
                RequestData.StoreID = _ISalesOrderView.UserInformation.StoreID;
                RequestData.POSID = _ISalesOrderView.UserInformation.POSID;

                RequestData.StoreCode = _ISalesOrderView.UserInformation.StoreCode;
                RequestData.POSCode = _ISalesOrderView.UserInformation.POSCode;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BillNo = string.Empty;
                    BillNo = BillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _ISalesOrderView.DocumentNo = BillNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                _ISalesOrderView.DocumentNo = string.Empty;
                throw ex;
            }
        }
        public void GetCustomer()
        {
            try
            {
                var _CustomerMasterBLL = new CustomerMasterBLL();
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Source = "Sales";
                RequestData.ID = _ISalesOrderView.CustomerID;
                RequestData.CustomerInfo = _ISalesOrderView.CustomerSearchString;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new SelectAllCustomerMasterResponse();

                ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesOrderView.CustomerMasterList = ResponseData.CustomerMasterData;

                    if (ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupID != 0 && ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupCode != null)
                    {
                        //GetDiscountRecord(ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupID, ResponseData.CustomerMasterData.FirstOrDefault().CustomerGroupCode, ResponseData.CustomerMasterData.FirstOrDefault().CustomerCode);
                    }
                }
                else
                {
                    var CustomerList = new List<CustomerMaster>();
                    _ISalesOrderView.CustomerMasterList = CustomerList;
                }
            }
            catch (Exception ex)
            {
                var CustomerList = new List<CustomerMaster>();
                _ISalesOrderView.CustomerMasterList = CustomerList;
            }
        }
        public void GetSKU()
        {
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _ISalesOrderView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    if (ResponseData.SKUMasterTypesList != null && ResponseData.SKUMasterTypesList.Count > 0)
                    {
                        _ISalesOrderView.SKURecord = ResponseData.SKUMasterTypesList.FirstOrDefault();
                    }
                    else
                    {
                        _ISalesOrderView.SKURecord = null;
                    }
                }
                else
                {
                    _ISalesOrderView.SKURecord = null;
                    _ISalesOrderView.Message = ResponseData.DisplayMessage;
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
                    _ISalesOrderView.StylePricingList = ResponseData.StylePricingList;
                }
                else
                {
                    _ISalesOrderView.Message = ResponseData.DisplayMessage;
                    _ISalesOrderView.StylePricingList = new List<StylePricing>();
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
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectByIDsPriceListRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.PriceListIDS = _ISalesOrderView.PriceListIDs;
                var ResponseData = _PriceListBLL.SelectByIDsPriceList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesOrderView.PriceListType = ResponseData.PriceList;
                }
                else
                {
                    _ISalesOrderView.PriceListType = new List<PriceListType>();
                    _ISalesOrderView.Message = ResponseData.DisplayMessage;
                }

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
                RequestData.ID = _ISalesOrderView.ManagerOverrideID;
                var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                if (Source == "PAGELOAD")
                {
                    _ISalesOrderView.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
                else
                {
                    _ISalesOrderView.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveSalesOrder()
        {
            try
            {
                if (_ISalesOrderView.ProcessMode == Enums.ProcessMode.New)
                {
                    SelectDocumentNumberingRecord();
                }
                var RequestData = new SaveSalesOrderRequest();
                var SalesOrderRecord = _ISalesOrderView.SalesOrderRecord;

                RequestData.SalesOrderHeaderRecord = new SalesOrderHeader();
                RequestData.SalesOrderHeaderRecord = SalesOrderRecord;
                RequestData.SalesOrderDetailsList = SalesOrderRecord.SalesOrderDetailsList;


                RequestData.PaymentList = new List<PaymentDetail>();
                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;

                var ResponseData = _SalesOrderBLL.SaveSalesOrder(RequestData);
                _ISalesOrderView.Message = ResponseData.DisplayMessage;
                _ISalesOrderView.ProcessStatus = ResponseData.StatusCode;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectsalesOrderMasterRecord()
        {
            try
            {
                var RequestData = new SelectBySalesOrderIDRequest();
                RequestData.ID = _ISalesOrderView.ID;
                var ResponseData = _SalesOrderBLL.SelectSalesOrderRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesOrderView.SalesOrderRecord = ResponseData.SalesOrderMasterRecord;
                }
                else
                {
                    _ISalesOrderView.Message = ResponseData.DisplayMessage;
                    _ISalesOrderView.SalesOrderRecord = null;
                }
            }
            catch
            {
                _ISalesOrderView.SalesOrderRecord = null;
            }
        }
        public void DeleteSalesOrder()
        {
            try
            {
                var RequestData = new DeleteSalesOrderRequest();
                RequestData.ID = _ISalesOrderView.ID;
                var ResponseData = _SalesOrderBLL.DeleteSalesOrder(RequestData);
                _ISalesOrderView.Message = ResponseData.DisplayMessage;
                _ISalesOrderView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SalesOrderCollectionPresenter
    {
        ISalesOrderCollectionView _ISalesOrderCollectionView;
        public SalesOrderCollectionPresenter(ISalesOrderCollectionView ViewObj)
        {
            _ISalesOrderCollectionView = ViewObj;
        }
        public void GetSalesOrderList()
        {
            try
            {
                var _SalesOrderBLL = new SalesOrderBLL();
                var RequestData = new SelectAllSalesOrderRequest();
                var ResponseData = new SelectAllSalesOrderResponse();
                RequestData.DataMode = _ISalesOrderCollectionView.DataMode;
                ResponseData = _SalesOrderBLL.SelectAllSalesRecord(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesOrderCollectionView.SalesOrderMasterList = ResponseData.SalesOrderHeaderList;
                }
                else
                {
                    _ISalesOrderCollectionView.SalesOrderMasterList = new List<SalesOrderHeader>();
                }
            }
            catch(Exception ex)
            {
                _ISalesOrderCollectionView.SalesOrderMasterList = new List<SalesOrderHeader>();
                throw ex;
            }
        }
    }
}
