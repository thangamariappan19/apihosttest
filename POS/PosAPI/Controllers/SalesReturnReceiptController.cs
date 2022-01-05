using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizBLL.Reports;
using EasyBizBLL.Transactions.POS;
using EasyBizRequest.Reports;
using EasyBizResponse.Reports;

namespace PosAPI.Controllers
{
    public class SalesReturnReceiptController : ApiController
    {

        public IHttpActionResult GetSearchInvoiceHeaderDetails(string InvoiceNo, int StoreID, int Mode)
        {
            try
            {
                var RequestData = new CommonReportRequest();
                RequestData.SearchString = InvoiceNo;
                RequestData.StoreID = StoreID;
                RequestData.MODE = Mode;

                //var ResponseData = InvoiceBLL.SelectInvoiceDetailsList(RequestData);
                CommonReportRespose response = null;
                var ResponseDate = new ReportsBLL();
                response = ResponseDate.GetSalesInvoiceReportData(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
