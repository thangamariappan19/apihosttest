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
    public class InvoiceHeaderTransactionController : ApiController
    {
        public IHttpActionResult GetInvoiceHeaderTransactionData(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            try
            {
                var RequestData = new InvoiceHeaderTransactionRequest();
                RequestData.FromDate = FromDate;
                RequestData.ToDate = ToDate;
                RequestData.StoreID = StoreID;
                InvoiceHeaderTransactionReponse response = null;
                var ResponseData = new InvoiceTransactionReportBLL();
                response = ResponseData.SelectAllInvoiceReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
