using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizRequest.Common;
using EasyBizResponse.Common;
using System;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DayOutController : ApiController
    {
        public IHttpActionResult PutShiftOut(DayClosing _ObjDay)
        {

            try
            {
                SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                RequestData.DayClosingRecord = new DayClosing();
                RequestData.DayClosingRecord.BuisnessDate = _ObjDay.BuisnessDate;
                RequestData.DayClosingRecord.StartingTime = _ObjDay.BuisnessDate;
                RequestData.DayClosingRecord.BuisnessDateStr = _ObjDay.BuisnessDateStr;
                RequestData.DayClosingRecord.StoreID = _ObjDay.StoreID;
                RequestData.DayClosingRecord.CountryID = _ObjDay.CountryID;
                RequestData.DayClosingRecord.POSID = _ObjDay.POSID;
                RequestData.DayClosingRecord.ShiftID = 0;
                RequestData.DayClosingRecord.Amount = _ObjDay.Amount;
                RequestData.DayClosingRecord.ShiftOutUserID = _ObjDay.ShiftOutUserID;
                RequestData.DayClosingRecord.Status = "Close";
                RequestData.DayClosingRecord.ClosingTime = DateTime.Now;
                SaveDayClosingResponse response = null;
                var ResponseData = new DayClosingBLL();
                response = ResponseData.UpdateDayClosing(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
