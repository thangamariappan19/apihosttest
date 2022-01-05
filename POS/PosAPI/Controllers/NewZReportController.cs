using EasyBizBLL.Masters;
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
    public class NewZReportController : ApiController
    {
        public IHttpActionResult GetZReportData(DateTime Businessdate, int StoreID)
        {
            try
            {
                var RequestData = new SelectNewZReportByDetailsRequest();
                RequestData.BusinessDate = Businessdate;
                RequestData.StoreID = StoreID;
                SelectNewZReportByDetailsResponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectNewZReportRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
