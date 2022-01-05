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
    public class StockAdjustmentTransactionReportBLL
    {
        public StockAdjustmentTransactionResponse SelectAll(StockAdjustmentTransactionRequest objRequest)
        {
            StockAdjustmentTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockAdjustmentTransactionDAL();
                objResponse = (StockAdjustmentTransactionResponse)BaseInvoice.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockAdjustmentTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockAdjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockAdjustmentTransactionResponse SelectAllReport(StockAdjustmentTransactionRequest objRequest)
        {
            StockAdjustmentTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockAdjustmentTransactionDAL();
                objResponse = (StockAdjustmentTransactionResponse)BaseInvoice.SelectAllReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockAdjustmentTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockAdjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockAdjustmentTransactionResponse SelectDetailAdjustment(StockAdjustmentTransactionRequest objRequest)
        {
            StockAdjustmentTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockAdjustmentTransactionDAL();
                objResponse = (StockAdjustmentTransactionResponse)BaseInvoice.SelectDetailAdjustment(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockAdjustmentTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockAdjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }

}
