using EasyBizBLL.Transactions.POS;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ReturnReceiptController : ApiController
    {
        public IHttpActionResult GetReturnReportData(string invoice)
        {
            try
            {
                var RequestData = new SelectInvoiceReturnReceiptByInvoiceNumRequest();
                RequestData.InvoiceNum = invoice;
                SelectInvoiceReturnReceiptByInvoiceNumResponse response = null;
                var ResponseData = new SalesReturnBLL();
                response = ResponseData.GetInvoiceReturnReceipt(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
