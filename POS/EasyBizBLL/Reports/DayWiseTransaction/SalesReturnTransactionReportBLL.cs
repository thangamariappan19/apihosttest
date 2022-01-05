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
    public class SalesReturnTransactionReportBLL
    {
        public SalesReturnTransactionResponse SelectAllInvoice(SalesReturnTransactionRequest objRequest)
        {
            SalesReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetSalesReturnTransactionDAL();
                objResponse = (SalesReturnTransactionResponse)BaseInvoice.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SalesReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SalesReturnTransactionResponse SelectAllSalesReturnReport(SalesReturnTransactionRequest objRequest)
        {
            SalesReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetSalesReturnTransactionDAL();
                objResponse = (SalesReturnTransactionResponse)BaseInvoice.SelectAllReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SalesReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SalesReturnTransactionResponse SelectRecord(SalesReturnTransactionRequest objRequest)
        {
            SalesReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetSalesReturnTransactionDAL();
                objResponse = (SalesReturnTransactionResponse)BaseInvoice.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SalesReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }

}
