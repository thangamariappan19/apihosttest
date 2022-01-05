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
    public class XReport2Controller : ApiController
    {
        public IHttpActionResult GetXReport2Data(int cashierID, int shiftID, DateTime businessDate, int storeID, int posID)
        {
            try
            {
                var RequestData = new SelectXReportByDetailsRequest();
                RequestData.CashierID = cashierID;
                RequestData.ShiftID = shiftID;
                RequestData.BusinessDate = businessDate;
                RequestData.StoreID = storeID;
                RequestData.POSID = posID;
                SelectXReportByDetailsResponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectXReport2Records(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
