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
    public class StockReturnTransactionController : ApiController
    {
        public IHttpActionResult GetStockReturnTransactionData(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            try
            {
                var RequestData = new StockReturnTransactionRequest();
                RequestData.FromDate = FromDate;
                RequestData.ToDate = ToDate;
                RequestData.StoreID = StoreID;
                StockReturnTransactionResponse response = null;
                var ResponseData = new StockReturnTransactionReportBLL();
                response = ResponseData.SelectAllStockReturnReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
