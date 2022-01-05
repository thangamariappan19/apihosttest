using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS.IPaymentView;
using EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
   public class GiftvoucherDetailsPresenter
    {
       GiftvoucherDetailsview _GiftvoucherDetailsview;
       GiftvoucherDetailBLL _GiftvoucherDetailBLL = new GiftvoucherDetailBLL();

       public GiftvoucherDetailsPresenter(GiftvoucherDetailsview ViewObj)
       {
           _GiftvoucherDetailsview = ViewObj;
       }

       public void SaveGiftvoucher()
       {
           try
           {
               var RequestData = new SaveGiftvoucherDetailsRequest();
               RequestData.GiftvoucherPaymentDetails = new GiftvoucherDetail();
               RequestData.GiftvoucherPaymentDetails.ID = _GiftvoucherDetailsview.ID;
               RequestData.GiftvoucherPaymentDetails.ApplicationDate = DateTime.Now;
               RequestData.GiftvoucherPaymentDetails.InvoiceNumber = _GiftvoucherDetailsview.InvoiceNumber;
               RequestData.GiftvoucherPaymentDetails.PayMentMode = _GiftvoucherDetailsview.PayMentMode;
               //RequestData.GiftvoucherPaymentDetails.InvoiceHeaderID = _GiftvoucherDetailsview.InvoiceHeaderID;
               RequestData.GiftvoucherPaymentDetails.GiftvoucherCode = _GiftvoucherDetailsview.GiftvoucherCode;
               RequestData.GiftvoucherPaymentDetails.Amount = _GiftvoucherDetailsview.Amount;
               RequestData.GiftvoucherPaymentDetails.CreateBy = _GiftvoucherDetailsview.UserID;
               RequestData.GiftvoucherPaymentDetails.CreateOn = DateTime.Now;
               RequestData.GiftvoucherPaymentDetails.SCN = _GiftvoucherDetailsview.SCN;

               var ResponseData = _GiftvoucherDetailBLL.SaveGiftvoucherDetails(RequestData);
               _GiftvoucherDetailsview.ProcessStatus = ResponseData.StatusCode;
               _GiftvoucherDetailsview.Message = ResponseData.DisplayMessage;

           }
           catch (Exception ex)
           {
               
           }
       }

       public void SelectGiftvoucherDetailByInvoiceNo()
       {
           try
           {
               var RequestData = new SelectGiftvoucherDetailByInvoiceNoRequest();
               RequestData.InvoiceNumber = _GiftvoucherDetailsview.InvoiceNumber;
               var ResponseData = _GiftvoucherDetailBLL.SelectGiftvoucherDetailByInvoiceNo(RequestData);

               if (ResponseData.GiftvoucherDetailData != null)
               {
                   _GiftvoucherDetailsview.ID = ResponseData.GiftvoucherDetailData.ID;
                   _GiftvoucherDetailsview.GiftvoucherCode = ResponseData.GiftvoucherDetailData.GiftvoucherCode;
                   _GiftvoucherDetailsview.Amount = ResponseData.GiftvoucherDetailData.Amount;
               }
               _GiftvoucherDetailsview.ProcessStatus = ResponseData.StatusCode;

           }
           catch (Exception ex)
           {

           }
       }
    }
}
