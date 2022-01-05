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
    public class SalesReturnTransactionController : ApiController
    {
        public IHttpActionResult GetSalesReturnTransactionData(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            try
            {
                var RequestData = new SalesReturnTransactionRequest();
                RequestData.FromDate = FromDate;
                RequestData.ToDate = ToDate;
                RequestData.StoreID = StoreID;
                SalesReturnTransactionResponse response = null;
                var ResponseData = new SalesReturnTransactionReportBLL();
                response = ResponseData.SelectAllSalesReturnReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
