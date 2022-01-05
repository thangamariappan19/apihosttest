using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class InvoiceCashDetailsBLL
    {
       public SaveInvoiceCashDetailsResponse SaveInvoiceCardDetails(SaveInVoiceCashDetailsRequest objRequest)
       {
           SaveInvoiceCashDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseInvoiceCashDetailsDAL = objFactory.GetDALRepository().GetBaseInvoiceCashDetailsDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjInvoiceCashDetails = new InVoiceCashDetails();
                   ObjInvoiceCashDetails = (InVoiceCashDetails)objRequest.RequestDynamicData;
                   objRequest.InVoiceCashDetailsData = ObjInvoiceCashDetails;
               }
               objResponse = (SaveInvoiceCashDetailsResponse)objBaseInvoiceCashDetailsDAL.InsertRecord(objRequest);
               
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                   objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentIDs);
                   objRequest.DocumentType = Enums.DocumentType.PAYMENTS;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceCashDetailsBLL", "SaveInvoiceCardDetails");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveInvoiceCashDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Save Invoice Cash Details");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectByInvoiceNoCashDetailsResponse SelectByInvoiceNoCashDetails(SelectByInvoiceNoCashDetailsRequest objRequest)
       {
           SelectByInvoiceNoCashDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseInvoiceCashDetailsDAL = objFactory.GetDALRepository().GetBaseInvoiceCashDetailsDAL();
               objResponse = (SelectByInvoiceNoCashDetailsResponse)objBaseInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByInvoiceNoCashDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice Cash Details");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
