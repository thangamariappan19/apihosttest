using EasyBizBLL.Reports.DayWiseTransaction;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StockAdjustmentTransactionController : ApiController
    {
        public IHttpActionResult GetStockAdjustmentTransactionData(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            try
            {
                var RequestData = new StockAdjustmentTransactionRequest();
                RequestData.FromDate = FromDate;
                RequestData.ToDate = ToDate;
                RequestData.StoreID = StoreID;
                StockAdjustmentTransactionResponse response = null;
                var ResponseData = new StockAdjustmentTransactionReportBLL();
                response = ResponseData.SelectAllReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
