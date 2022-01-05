using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS.IPaymentView;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
   public class InvoiceCashDetailsPresenter
    {
       IInvoiceCashDetailsView _InvoiceCashDetailsView;
       InvoiceCashDetailsBLL _InvoiceCashDetailsBLL = new InvoiceCashDetailsBLL();
       CurrencyBLL _CurrencyBLL = new CurrencyBLL();
       public InvoiceCashDetailsPresenter(IInvoiceCashDetailsView ViewObj)
       {
           _InvoiceCashDetailsView = ViewObj;
       }
       public void SaveCashDetails()
       {
           try
           {
               var RequestData = new SaveInVoiceCashDetailsRequest();
               RequestData.InVoiceCashDetailsData = new InVoiceCashDetails();
               RequestData.InVoiceCashDetailsData.BusinessDate = DateTime.Now;
               RequestData.InVoiceCashDetailsData.ID = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.ID;
               RequestData.InVoiceCashDetailsData.InvoiceHeaderID = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.InvoiceHeaderID;
               RequestData.InVoiceCashDetailsData.InvoiceNumber = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.InvoiceNumber;
               //RequestData.InVoiceCashDetailsData.PaymentMode = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.PaymentMode;
               //RequestData.InVoiceCashDetailsData.PaymentType = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.PaymentType;
               RequestData.InVoiceCashDetailsData.PaymentCurrency = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.PaymentCurrency;
               RequestData.InVoiceCashDetailsData.ChangeCurrency = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.ChangeCurrency;
               //RequestData.InVoiceCashDetailsData.DenominationValue = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.DenominationValue;
               //RequestData.InVoiceCashDetailsData.DenominationNumber = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.DenominationNumber;
               RequestData.InVoiceCashDetailsData.FromCountryID = 0;
               RequestData.InVoiceCashDetailsData.FromStoreID = 0;
               RequestData.InVoiceCashDetailsData.ReceivedAmount = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.ReceivedAmount;
               RequestData.InVoiceCashDetailsData.BalanceAmountToBePay = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.BalanceAmountToBePay;
               RequestData.InVoiceCashDetailsData.AmountToBePay = _InvoiceCashDetailsView.InvoiceCashDetailsRecord.AmountToBePay;
               RequestData.InVoiceCashDetailsData.CreateBy = _InvoiceCashDetailsView.UserID;
               RequestData.InVoiceCashDetailsData.CreateOn = DateTime.Now;
               RequestData.InVoiceCashDetailsData.SCN = _InvoiceCashDetailsView.SCN;

               var ResponseData = _InvoiceCashDetailsBLL.SaveInvoiceCardDetails(RequestData);

               _InvoiceCashDetailsView.ProcessStatus = ResponseData.StatusCode;
               //_InvoiceCashDetailsView.Message = ResponseData.DisplayMessage;
           }
           catch
           {

           }
       }
       //public void SelectCashDetailsByInvoiceNo()
       //{
       //    try
       //    {
       //        var RequestData = new SelectByInvoiceNoCashDetailsRequest();
       //        RequestData.InvoiceNo = _InvoiceCashDetailsView.InvoiceBillNo;
       //        var ResponseData = _InvoiceCashDetailsBLL.SelectByInvoiceNoCashDetails(RequestData);
       //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
       //        {
       //            _InvoiceCashDetailsView.ID = ResponseData.InvoiceNoCashDetails.ID;                  
       //            _InvoiceCashDetailsView.PayCurrencyID = ResponseData.InvoiceNoCashDetails.PaymentCurrencyID;
       //            _InvoiceCashDetailsView.ChangeCurrencyID = ResponseData.InvoiceNoCashDetails.ChangeCurrencyID;                   
       //            _InvoiceCashDetailsView.ReceivedCashAmount = ResponseData.InvoiceNoCashDetails.ReceivedAmount;
       //            //_InvoiceCashDetailsView.CashBalance = ResponseData.InvoiceNoCashDetails.BalanceAmount;
       //            //_InvoiceCashDetailsView.Amounttobebepay = ResponseData.InvoiceNoCashDetails.AmounttobePay;
                  
       //        }
       //        _InvoiceCashDetailsView.ProcessStatus = ResponseData.StatusCode;
       //    }
       //    catch(Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
       public void SelectCurrencyLookUp()
       {
           var RequestData = new SelectCurrencyLookUpRequest();
           RequestData.ShowInActiveRecords = false;
           var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _InvoiceCashDetailsView.CurrencyLookup = ResponseData.CurrencyMasterList;
           }
       }
       public void SelectCurrencyExchangeRates()
       {
           try
           {
               var _ExchangeRatesBLL = new ExchangeRatesBLL();
               var RequestData = new SelectCurrecnyExchangeRatesRequest();
               RequestData.Exchangedate = _InvoiceCashDetailsView.BusinessDate;
               var ResponseData = _ExchangeRatesBLL.SelectCurrenctExchangeRates(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _InvoiceCashDetailsView.CurrencyExchangeList = ResponseData.CurrencyExchangeRatesList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
