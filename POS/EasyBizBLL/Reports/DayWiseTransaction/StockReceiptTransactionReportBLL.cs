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
    public class StockReceiptTransactionReportBLL
    {
        public StockReceiptTransactionResponse SelectAllStockReceipt(StockReceiptTransactionRequest objRequest)
        {
            StockReceiptTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockReceiptTransactionDAL();
                objResponse = (StockReceiptTransactionResponse)objBaseStockReceiptDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReceiptTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockReceiptTransactionResponse SelectAllStockReceiptReport(StockReceiptTransactionRequest objRequest)
        {
            StockReceiptTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockReceiptTransactionDAL();
                objResponse = (StockReceiptTransactionResponse)BaseInvoice.SelectAllReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReceiptTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public StockReceiptTransactionResponse SelectRecord(StockReceiptTransactionRequest objRequest)
        {
            StockReceiptTransactionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetStockReceiptTransactionDAL();
                objResponse = (StockReceiptTransactionResponse)BaseInvoice.SelectDetailReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockReceiptTransactionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceiptDetai");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
