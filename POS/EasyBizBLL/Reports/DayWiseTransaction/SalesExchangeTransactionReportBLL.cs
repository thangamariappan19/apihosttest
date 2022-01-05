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
    public class SalesExchangeTransactionReportBLL
    {
        public SalesExchangeTransactionResponse SalesExchangeTransactionList(SalesExchangeTransactionRequest objRequest)
        {
            SalesExchangeTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesExchangeDAL = objFactory.GetDALRepository().GetSalesExchangeTransactionDAL();
                objResponse = (SalesExchangeTransactionResponse)objBaseSalesExchangeDAL.SelectAllSalesExchangeDetailList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesExchangeTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SalesExchangeTransactionResponse SalesEchangeTransactionReportList(SalesExchangeTransactionRequest objRequest)
        {
            SalesExchangeTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetSalesExchangeTransactionDAL();
                objResponse = (SalesExchangeTransactionResponse)BaseInvoice.SelectAllSalesExchangeTransactionReportList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesExchangeTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SalesExchangeTransactionResponse SelectRecordByID(SalesExchangeTransactionRequest objRequest)
        {
            SalesExchangeTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetSalesExchangeTransactionDAL();
                objResponse = (SalesExchangeTransactionResponse)BaseInvoice.SelectRecordByID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SalesExchangeTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Exchange Detail");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
