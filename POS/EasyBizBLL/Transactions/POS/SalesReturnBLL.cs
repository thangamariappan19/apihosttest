using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizRequest.Transactions.POS.SalesReturnRequest;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class SalesReturnBLL
    {
       public SelectAllSalesReturnResponse SelectAllSalesReturn(SelectAllSalesReturnRequest objRequest)
       {
           SelectAllSalesReturnResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseSalesReturn = objFactory.GetDALRepository().GetSalesReturnDAL();
               objResponse = (SelectAllSalesReturnResponse)BaseSalesReturn.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllSalesReturnResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SaveSalesReturnResponse SaveSalesReturn(SaveSalesReturnRequest objRequest)
       {
            SaveSalesReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseSalesReturnDAL = objFactory.GetDALRepository().GetSalesReturnDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var _SalesReturnHeader = new SalesReturnHeader();
                    _SalesReturnHeader = (SalesReturnHeader)objRequest.RequestDynamicData;
                    objRequest.SalesReturnHeaderData = _SalesReturnHeader;
                    objRequest.OnAccountPaymentRecord = _SalesReturnHeader.OnAccountPaymentRecord;
                    objRequest.TransactionLogList = _SalesReturnHeader.TransactionLogList;
                }

                objResponse = (SaveSalesReturnResponse)objBaseSalesReturnDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentNos = objRequest.SalesReturnHeaderData.DocumentNo;
                    objRequest.DocumentType = Enums.DocumentType.SALESRETURN;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.SalesReturnBLL", "SaveSalesReturn");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSalesReturnResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
       public SelectByIDSalesReturnResponse SelectByIDInvoiceHeader(EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest objRequest)
       {
           SelectByIDSalesReturnResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseSalesReturn = objFactory.GetDALRepository().GetSalesReturnDAL();
               objResponse = (SelectByIDSalesReturnResponse)BaseSalesReturn.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDSalesReturnResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectSalesReturnDetailsByIDResponse SelectInvoiceDetailsByID(SelectSalesReturnDetailsByIDRequest ReqObj)
       {
           SelectSalesReturnDetailsByIDResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseSalesReturn = objFactory.GetDALRepository().GetSalesReturnDAL();
               objResponse = (SelectSalesReturnDetailsByIDResponse)BaseSalesReturn.SelectByIDSalesReturnDetails(ReqObj);
           }
           catch (Exception ex)
           {
               objResponse = new SelectSalesReturnDetailsByIDResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectInvoiceReturnReceiptByInvoiceNumResponse GetInvoiceReturnReceipt(SelectInvoiceReturnReceiptByInvoiceNumRequest objRequest)
       {
           SelectInvoiceReturnReceiptByInvoiceNumResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseSalesReturn = objFactory.GetDALRepository().GetSalesReturnDAL();
               objResponse = (SelectInvoiceReturnReceiptByInvoiceNumResponse)BaseSalesReturn.GetInvoiceReturnReceipt(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectInvoiceReturnReceiptByInvoiceNumResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
