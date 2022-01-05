using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizFactory;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports.DayWiseTransaction
{
   public class InvoiceTransactionReportBLL
    {
       public InvoiceHeaderTransactionReponse SelectAllInvoice(InvoiceHeaderTransactionRequest objRequest)
       {
           InvoiceHeaderTransactionReponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetInvoiceTransactionDAL();
               objResponse = (InvoiceHeaderTransactionReponse)BaseInvoice.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new InvoiceHeaderTransactionReponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public InvoiceHeaderTransactionReponse SelectAllInvoiceReport(InvoiceHeaderTransactionRequest objRequest)
       {
           InvoiceHeaderTransactionReponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetInvoiceTransactionDAL();
               objResponse = (InvoiceHeaderTransactionReponse)BaseInvoice.SelectAllReport(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new InvoiceHeaderTransactionReponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public InvoiceHeaderTransactionReponse SelectRecord(InvoiceHeaderTransactionRequest objRequest)
       {
           InvoiceHeaderTransactionReponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetInvoiceTransactionDAL();
               objResponse = (InvoiceHeaderTransactionReponse)BaseInvoice.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new InvoiceHeaderTransactionReponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
     
}
