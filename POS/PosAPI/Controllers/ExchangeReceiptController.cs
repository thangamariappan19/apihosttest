using EasyBizBLL.Transactions.POS;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ExchangeReceiptController : ApiController
    {
        public IHttpActionResult GetExchangeReportData(string invoice)
        {
            try
            {
                var RequestData = new SelectExchangeByInvoiceNumRequest();
                RequestData.InvoiceNum = invoice;
                SelectExchangeByInvoiceNumResponse response = null;
                var ResponseData = new SalesExchangeBLL();
                response = ResponseData.GetExchangeReceipt(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
