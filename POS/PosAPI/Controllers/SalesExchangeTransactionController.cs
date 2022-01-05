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
    public class SalesExchangeTransactionController : ApiController
    {
        public IHttpActionResult GetSalesExchangeTransactionData(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            try
            {
                var RequestData = new SalesExchangeTransactionRequest();
                RequestData.FromDate = FromDate;
                RequestData.ToDate = ToDate;
                RequestData.StoreID = StoreID;
                SalesExchangeTransactionResponse response = null;
                var ResponseData = new SalesExchangeTransactionReportBLL();
                response = ResponseData.SalesEchangeTransactionReportList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
