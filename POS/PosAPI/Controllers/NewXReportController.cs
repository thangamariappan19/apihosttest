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
    public class NewXReportController : ApiController
    {
        public IHttpActionResult GetXReportData(int cashierID, int shiftID, DateTime businessDate, int storeID, int posID)
        {
            try
            {
                var RequestData = new SelectNewXReportByDetailsRequest();
                RequestData.CashierID = cashierID;
                RequestData.ShiftID = shiftID;
                RequestData.BusinessDate = businessDate;
                RequestData.StoreID = storeID;
                RequestData.POSID = posID;
                SelectNewXReportByDetailsReponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectNewXReportRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
