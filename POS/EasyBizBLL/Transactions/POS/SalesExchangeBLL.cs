using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class SalesExchangeBLL
    {
       public SaveSalesExchangeResponse SaveSalesExchange(SaveSalesExchangeRequest objRequest)
       {
           SaveSalesExchangeResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
               var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();

               if (objRequest.RequestDynamicData != null)
               {
                   var _SalesExchangeHeader = new SalesExchangeHeader();
                   _SalesExchangeHeader = (SalesExchangeHeader)objRequest.RequestDynamicData;
                   objRequest.SalesExchangeHeaderRecord = _SalesExchangeHeader;
                   objRequest.SalesExchangeDetailList = _SalesExchangeHeader.SalesExchangeDetailList;
                   objRequest.ReturnList = _SalesExchangeHeader.ReturnExchangeDetailList;
                   objRequest.TransactionLogList = _SalesExchangeHeader.TransactionLogList;
               }

               objResponse = (SaveSalesExchangeResponse)objBaseSalesExchangeDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentNos = objRequest.SalesExchangeHeaderRecord.DocumentNo;
                   objRequest.DocumentType = Enums.DocumentType.SALESEXCHANGE;
                   objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.SalesExchangeBLL", "SaveSalesExchange");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveSalesExchangeResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public GetExchangeOrSalesResponse GetSalesOrExchangeList(SelectAllInvoiceRequest objRequest)
        {
            GetExchangeOrSalesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();
                objResponse = (GetExchangeOrSalesResponse)objBaseSalesExchangeDAL.GetSalesOrExchangeList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetExchangeOrSalesResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectSalesExchangeRecordResponse SelectSalesExchangeRecord(SelectSalesExchangeRecordRequest objRequest)
       {
           SelectSalesExchangeRecordResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();
               objResponse = (SelectSalesExchangeRecordResponse)objBaseSalesExchangeDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectSalesExchangeRecordResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Sales Exchange");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectAllSalesExchangeResponse SelectAllSalesExchangeList(SelectAllSalesExchangeRequest objRequest)
       {
           SelectAllSalesExchangeResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();
               objResponse = (SelectAllSalesExchangeResponse)objBaseSalesExchangeDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllSalesExchangeResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectAllSalesExchangeDetailResponse SelectAllSalesExchangeDetailList(SelectAllSalesExchangeDetailRequest objRequest)
       {
           SelectAllSalesExchangeDetailResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();
               objResponse = (SelectAllSalesExchangeDetailResponse)objBaseSalesExchangeDAL.SelectAllSalesExchangeDetailList(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllSalesExchangeDetailResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectExchangeByInvoiceNumResponse GetExchangeReceipt(SelectExchangeByInvoiceNumRequest objRequest)
       {
           SelectExchangeByInvoiceNumResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetBaseSalesExchangeDAL();
               objResponse = (SelectExchangeByInvoiceNumResponse)BaseInvoice.GetExchangeReceipt(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectExchangeByInvoiceNumResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
