using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.StockStaging;
using EasyBizResponse.Transactions.Stocks.StockStaging;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
    public class StockStagingBLL
    {
        public GetStockStagingRecordsByStyleCodeResponse GetStockByStyleCode(GetStockStagingRecordsByStyleCodeRequest objRequest)
        {
            GetStockStagingRecordsByStyleCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockStagingDAL();
                objResponse = (GetStockStagingRecordsByStyleCodeResponse)objBaseStockReceiptDAL.GetStockByStyleCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStockStagingRecordsByStyleCodeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Stock Staging");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
