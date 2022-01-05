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
    public class SalesSummaryBySalesmanController : ApiController
    {
        public IHttpActionResult GetSalesSummaryBySalesmanData(DateTime FromDate, int StoreID)
        {
            try
            {
                var RequestData = new CommonReportRequest();
                RequestData.FromDate = FromDate;
                RequestData.StoreID = StoreID;
                CommonReportRespose response = null;
                var ResponseData = new ReportsBLL();
                response = ResponseData.GetSalesManWiseReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
