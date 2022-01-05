using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IOnAccountPayment;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class OnAccountPaymentPresenter
    {
        IOnAccountPayment _IOnAccountPayment;
        OnAccountPaymentBLL _OnAccountPaymentBLL = new OnAccountPaymentBLL();

        public OnAccountPaymentPresenter(IOnAccountPayment ViewObj)
        {
            _IOnAccountPayment = ViewObj;
        }
        public void GetCustomer()
        {
            try
            {
                var _CustomerMasterBLL = new CustomerMasterBLL();
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Source = "Sales";
                RequestData.ID =0;
                RequestData.CustomerInfo = _IOnAccountPayment.SearchString;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new SelectAllCustomerMasterResponse();

                ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IOnAccountPayment.CustomerMasterList = ResponseData.CustomerMasterData;
                }
                else
                {
                    var CustomerList = new List<CustomerMaster>();
                    _IOnAccountPayment.CustomerMasterList = CustomerList;
                }
            }
            catch (Exception ex)
            {
                var CustomerList = new List<CustomerMaster>();
                _IOnAccountPayment.CustomerMasterList = CustomerList;
                throw ex;
            }
        }
        public void GetPendingPayments()
        {
            try
            {
                var RequestData = new GetOnAccountPaymentPendingRequest();
                RequestData.Type = _IOnAccountPayment.Type;
                RequestData.SearchString = _IOnAccountPayment.SearchString;
                var ResponseData = _OnAccountPaymentBLL.GetPendingPayments(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IOnAccountPayment.OnAcInvoiceWisePaymentList = ResponseData.OnAcInvoiceWisePaymentList;
                }
                else
                {
                    _IOnAccountPayment.OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCurrencyExchangeRates()
        {
            try
            {
                var _ExchangeRatesBLL = new ExchangeRatesBLL();
                var RequestData = new SelectCurrecnyExchangeRatesRequest();
                RequestData.Exchangedate = DateTime.Now;
                var ResponseData = _ExchangeRatesBLL.SelectCurrenctExchangeRates(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IOnAccountPayment.CurrencyExchangeList = ResponseData.CurrencyExchangeRatesList;
                }
                else
                {
                    _IOnAccountPayment.CurrencyExchangeList = new List<ExchangeRates>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCurrencyLookUp()
        {
            try {
                var _CurrencyBLL = new CurrencyBLL();
                var RequestData = new SelectCurrencyLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IOnAccountPayment.CurrencyLookup = ResponseData.CurrencyMasterList;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void SaveOnAccountPayment()
        {
            try
            {
                var RequestData = new SaveOnAccountPaymentRequest();
                string _StoreIDs = string.Empty;
                if(_IOnAccountPayment.OnAccountPaymentRecord != null && _IOnAccountPayment.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList != null)
                {
                    var StoreWiseGroupList = _IOnAccountPayment.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList.GroupBy(x => x.StoreID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList(); 
                    
                    if (StoreWiseGroupList != null && StoreWiseGroupList.Count > 0)
                    {
                        foreach(List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList in StoreWiseGroupList)
                        {
                            _StoreIDs = _StoreIDs + "," + OnAcInvoiceWisePaymentList.FirstOrDefault().StoreID;
                        }
                    }
                    RequestData.StoreIDs = _StoreIDs.TrimStart(',');
                }
                RequestData.RequestFrom = _IOnAccountPayment.RequestFrom;
                RequestData.OnAccountPaymentRecord = _IOnAccountPayment.OnAccountPaymentRecord;

                var ResponseData = _OnAccountPaymentBLL.SaveOnAccountPayment(RequestData);
                _IOnAccountPayment.Message = ResponseData.DisplayMessage;
                _IOnAccountPayment.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class OnAccountPaymentCollectionViewPresenter
    {
        IOnAccountPaymentCollectionView _IOnAccountPaymentCollectionView;
        OnAccountPaymentBLL _OnAccountPaymentBLL = new OnAccountPaymentBLL();
        public OnAccountPaymentCollectionViewPresenter(IOnAccountPaymentCollectionView ViewObj)
        {
            _IOnAccountPaymentCollectionView = ViewObj;
        }
        public void GetOnAccountPaymentList()
        {
            //SelectAllOnAccountPaymentResponse GetOnAccountPaymentList(SelectAllOnAccountPaymentRequest objRequest)
            try
            {
                var RequestData = new SelectAllOnAccountPaymentRequest();
                RequestData.RequestFrom = _IOnAccountPaymentCollectionView.DataRequestFrom;
                RequestData.SearchString = _IOnAccountPaymentCollectionView.SearchString;
                var ResponseData = new SelectAllOnAccountPaymentResponse();
                ResponseData = _OnAccountPaymentBLL.GetOnAccountPaymentList(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IOnAccountPaymentCollectionView.OnAccountPaymentList = ResponseData.OnAccountPaymentList;
                }
                else
                {
                    _IOnAccountPaymentCollectionView.OnAccountPaymentList = new List<OnAccountPayment>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

}
