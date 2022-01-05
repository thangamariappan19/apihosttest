using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest;
using EasyBizResponse.Transactions.POS.GiftvoucherDetailsResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class GiftvoucherDetailBLL
    {
       public SaveGiftvoucherDetailsResponse SaveGiftvoucherDetails(SaveGiftvoucherDetailsRequest objRequest)
       {
           SaveGiftvoucherDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseGiftvoucherDetailsDAL = objFactory.GetDALRepository().GetBaseGiftvoucherDetailsDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjGiftVoucherDetail = new GiftvoucherDetail();
                   ObjGiftVoucherDetail = (GiftvoucherDetail)objRequest.RequestDynamicData;
                   objRequest.GiftvoucherPaymentDetails = ObjGiftVoucherDetail;
               }
               objResponse = (SaveGiftvoucherDetailsResponse)objBaseGiftvoucherDetailsDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   //objRequest.DocumentType = Enums.DocumentType.GIFTVOUCHERDETAIL;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.GiftvoucherDetailBLL", "SaveGiftvoucherDetails");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveGiftvoucherDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectGiftvoucherDetailByInvoiceNoResponse SelectGiftvoucherDetailByInvoiceNo(SelectGiftvoucherDetailByInvoiceNoRequest objRequest)
       {
           SelectGiftvoucherDetailByInvoiceNoResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseGiftvoucherDetailsDAL = objFactory.GetDALRepository().GetBaseGiftvoucherDetailsDAL();
               objResponse = (SelectGiftvoucherDetailByInvoiceNoResponse)objBaseGiftvoucherDetailsDAL.SelectGiftvoucherDetailByInvoiceNo(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectGiftvoucherDetailByInvoiceNoResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
