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
    public class StockReturnTransactionReportBLL
    {
   
        public StockReturnTransactionResponse SelectAllStockReturn(StockReturnTransactionRequest objRequest)
        {
            StockReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnTransactionDAL();
                objResponse = (StockReturnTransactionResponse)objBaseStockReturnDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReturnTransaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockReturnTransactionResponse SelectAllStockReturnReport(StockReturnTransactionRequest objRequest)
        {
            StockReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockReturnTransactionDAL();
                objResponse = (StockReturnTransactionResponse)BaseInvoice.SelectAllReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturnTransaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockReturnTransactionResponse SelectRecord(StockReturnTransactionRequest objRequest)
        {
            StockReturnTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockReturnTransactionDAL();
                objResponse = (StockReturnTransactionResponse)BaseInvoice.SelectDetailReturn(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReturnTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturnTransaction");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
