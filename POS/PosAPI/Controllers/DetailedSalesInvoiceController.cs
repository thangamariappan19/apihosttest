using EasyBizBLL.Reports;
using EasyBizRequest.Reports;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DetailedSalesInvoiceController : ApiController
    {
        public IHttpActionResult GetDetailedSalesInvoiceData(string InvoiceNo,int StoreID,int Mode)
        {
            try
            {
                var RequestData = new CommonReportRequest();
                RequestData.InvoiceNo = InvoiceNo;
                RequestData.StoreID = StoreID;
                RequestData.MODE = Mode;
                CommonReportRespose response = null;
                var ResponseData = new ReportsBLL();
                response = ResponseData.GetSalesInvoiceReportData(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
