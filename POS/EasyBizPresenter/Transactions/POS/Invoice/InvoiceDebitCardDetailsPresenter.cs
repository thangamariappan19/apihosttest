using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS.IInvoice;
using EasyBizRequest.Transactions.POS.CardDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
   public class InvoiceDebitCardDetailsPresenter
    {
       IInvoiceCardDetails _IInvoiceCardDetails;
       InvoiceCardDetailsBLL _InvoiceCardDetailsBLL = new InvoiceCardDetailsBLL();

       public InvoiceDebitCardDetailsPresenter(IInvoiceCardDetails ViewObj)
       {
           _IInvoiceCardDetails = ViewObj;
       }
       public void SaveInvoice()
       {
           try
           {
               var RequestData = new SaveCardDetailsRequest();
               RequestData.InvoiceCardDetailsrData = new InvoiceCardDetails();
               RequestData.InvoiceCardDetailsrData.ID = _IInvoiceCardDetails.InvoiceCardRecord.ID;
               RequestData.InvoiceCardDetailsrData.ApplicationDate = DateTime.Now;
               RequestData.InvoiceCardDetailsrData.InvoiceHeaderID = _IInvoiceCardDetails.InvoiceCardRecord.InvoiceHeaderID;
               RequestData.InvoiceCardDetailsrData.InvoiceNumber = _IInvoiceCardDetails.InvoiceCardRecord.InvoiceNumber;
               RequestData.InvoiceCardDetailsrData.CardNumber = _IInvoiceCardDetails.InvoiceCardRecord.CardNumber;
               RequestData.InvoiceCardDetailsrData.CardHolderName = _IInvoiceCardDetails.InvoiceCardRecord.CardHolderName;
               //RequestData.InvoiceCardDetailsrData.Amount = _IInvoiceCardDetails.InvoiceCardRecord.Amount;
               RequestData.InvoiceCardDetailsrData.AmountToBePay = _IInvoiceCardDetails.InvoiceCardRecord.AmountToBePay;
               RequestData.InvoiceCardDetailsrData.ReceivedAmount = _IInvoiceCardDetails.InvoiceCardRecord.ReceivedAmount; ;
               RequestData.InvoiceCardDetailsrData.AmountToBePay = _IInvoiceCardDetails.InvoiceCardRecord.AmountToBePay;
               //RequestData.InvoiceCardDetailsrData.ExpiryYear = _IInvoiceCardDetails.InvoiceCardRecord.ExpiryYear;
              // RequestData.InvoiceCardDetailsrData.ExpiryMonth = _IInvoiceCardDetails.InvoiceCardRecord.ExpiryMonth;
               RequestData.InvoiceCardDetailsrData.CardType = _IInvoiceCardDetails.InvoiceCardRecord.CardType;
               //RequestData.InvoiceCardDetailsrData.PaymentType = _IInvoiceCardDetails.InvoiceCardRecord.PaymentType;
               RequestData.InvoiceCardDetailsrData.CreateBy = _IInvoiceCardDetails.UserID;
               RequestData.InvoiceCardDetailsrData.CreateOn = DateTime.Now;
               RequestData.InvoiceCardDetailsrData.SCN = _IInvoiceCardDetails.SCN;

               var ResponseData = _InvoiceCardDetailsBLL.SaveInvoiceCardDetails(RequestData);
               //_IInvoiceCardDetails.Message = ResponseData.DisplayMessage;
               //_IInvoiceCardDetails.ProcessStatus = ResponseData.StatusCode;
              
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //public void SelectDebitCardDetailsByInvoiceNo()
       //{
       //    try
       //    {
       //        var RequestData = new SelectCreditCardDetailsByInvoiceNoRequest();
       //        RequestData.InvoiceNumber = _IInvoiceCardDetails.InvoiceBillNo;
       //        RequestData.CardType = _IInvoiceCardDetails.CardType;
       //        var ResponseData = _InvoiceCardDetailsBLL.SelectCreditCardDetailsByInvoiceNo(RequestData);

       //        if (ResponseData.InvoiceNoCreditCardDetails != null)
       //        {
       //            _IInvoiceCardDetails.ID = ResponseData.InvoiceNoCreditCardDetails.ID;
       //            _IInvoiceCardDetails.CardNumber = ResponseData.InvoiceNoCreditCardDetails.CardNumber;
       //            _IInvoiceCardDetails.CardHolderName = ResponseData.InvoiceNoCreditCardDetails.CardHolderName;
       //            _IInvoiceCardDetails.CardType = ResponseData.InvoiceNoCreditCardDetails.CardType;
       //            //_IInvoiceCardDetails.Amount = ResponseData.InvoiceNoCreditCardDetails.Amount;
       //            _IInvoiceCardDetails.InvoiceTotalAmount = ResponseData.InvoiceNoCreditCardDetails.AmountToBePay;
       //            //_IInvoiceCardDetails.AmountPaid = ResponseData.InvoiceNoCreditCardDetails.AmountPaid;
       //            //_IInvoiceCardDetails.BalanceAmounttobePay = ResponseData.InvoiceNoCreditCardDetails.BalanceAmounttobepay;
       //            _IInvoiceCardDetails.CardNumber = ResponseData.InvoiceNoCreditCardDetails.CardNumber;
       //            //_IInvoiceCardDetails.ExpiryYear = ResponseData.InvoiceNoCreditCardDetails.ExpiryYear;
       //            //_IInvoiceCardDetails.ExpiryMonth = ResponseData.InvoiceNoCreditCardDetails.ExpiryMonth;
       //        }
       //        //_IInvoiceCardDetails.InvoiceCardRecord.InvoiceHeaderID = ResponseData.InvoiceNoCreditCardDetails.InvoiceHeaderID;

       //        _IInvoiceCardDetails.ProcessStatus = ResponseData.StatusCode;

       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
    }
}
