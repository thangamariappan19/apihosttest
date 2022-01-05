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
    public class ZReport3Controller : ApiController
    {       
        public IHttpActionResult GetZReport3Data(DateTime Businessdate, int StoreID)
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.BusinessDate = Businessdate;
                RequestData.StoreID = StoreID;
                SelectZReportByDetailsResponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectZReport2Records(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
