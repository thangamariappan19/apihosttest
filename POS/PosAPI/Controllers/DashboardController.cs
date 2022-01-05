using EasyBizBLL.Dashboard;
using EasyBizRequest.Masters.DashboardRequest;
using EasyBizResponse.Masters.DashboardReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DashboardController : ApiController
    {
        public IHttpActionResult GetDashboard(DateTime Fromdate, DateTime Todate, string report_type, int country_id)
        {
            try
            {
                var RequestData = new SelectDashboardRequest();
                RequestData.FromDate = Fromdate;
                RequestData.ToDate = Todate;
                RequestData.report_type = report_type;
                RequestData.country_id = country_id;
                SelectDashboardResponse response = null;
                var ResponseData = new RegisterDashBoardBLL();
                response = ResponseData.SelectInbetweendateDetail(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
