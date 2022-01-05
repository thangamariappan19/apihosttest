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
    public class InvoiceReceipt2Controller : ApiController
    {
        public IHttpActionResult GetInvoiceReport2Data(string invoice)
        {
            try
            {
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.InvoiceNum = invoice;
                SelectInvoiceReceiptByInvoiceNumResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.GetInvoiceReceipt1(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
