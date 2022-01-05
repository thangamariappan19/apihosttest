using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Common;
using EasyBizResponse.Common;
using EasyBizBLL.Common;
using EasyBizDBTypes.Masters;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class DayInController : ApiController
    {
        public IHttpActionResult GetShiftLog(int StoreID, int UserID, int CountryID)
        {
            try
            {
                DateTime? BusinessDate = null;
                var RequestData = new SelectDayInRequest();
                RequestData.UserID = UserID;
                RequestData.StoreID = StoreID;
                RequestData.CountryID = CountryID;
                SelectDayInResponse response = null;
                var ResponseData = new DayShiftLOGBLL();
                response = ResponseData.GetSelectDayIn(RequestData);
                if (response.ShiftMasterList != null)
                {
                    //foreach (ShiftMaster objShiftList in response.ShiftList)
                    //{
                    //    if(objShiftList.Status=="Open")
                    //        response.DayIn = true;
                    //    if(objShiftList.ShiftStatus == "Open")
                    //        response.ShiftIn = true;
                    //    if (objShiftList.Status == "Close")
                    //        response.DayIn = false;
                    //    if (objShiftList.ShiftStatus == "Close")
                    //        response.ShiftIn = false;

                    //    response.BusinessDate = objShiftList.BusinessDate;
                    //}
                    if (response.ShiftMasterList.Count > 0)
                    {
                        var ShiftInData = response.ShiftMasterList.Where(x => x.ShiftStatus == "" || x.ShiftStatus == string.Empty).OrderBy(x => x.SortOrder).FirstOrDefault();
                        var objshift = new List<ShiftMaster>();

                        var objshiftmasterlist = new ShiftMaster();
                        objshiftmasterlist.ID = ShiftInData.ID;
                        objshiftmasterlist.ShiftCode = ShiftInData.ShiftCode;
                        //response.ShiftMasterList = null;
                        objshift.Add(objshiftmasterlist);
                        response.ShiftMasterList = objshift;
                    }
                    else
                    {
                        response.DisplayMessage = "Shift Master Record Not Found";
                    }
                    response.ShiftIn = response.LogShiftList.Shiftin;
                    response.DayIn = response.LogShiftList.Dayin;
                    response.BusinessDate = response.LogShiftList.BusinessDate;
                }

                if (response.StatusCode == Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //public IHttpActionResult GetDayIn(int UserID, int StoreID, DateTime date, int POSID)
        //{
        //    try
        //    {
        //        var RequestData = new SelectDayInRequest();
        //        RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
        //        RequestData.UserName = "";
        //        RequestData.UserID = UserID;
        //        RequestData.POSID = POSID;
        //        RequestData.StoreID = StoreID;
        //        RequestData.BusinessDate = date;
        //        SelectDayInResponse response = null;
        //        var ResponseData = new DayShiftLOGBLL();
        //        response = ResponseData.GetSelectDayIn(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetSelectPOSUser(int CountryID, int StoreID, int POSID, int UserID,DateTime BusinessDate)
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = Convert.ToDateTime(BusinessDate);
                RequestData.CountryID = CountryID;
                RequestData.StoreID = StoreID;
                RequestData.POSID =POSID;
                RequestData.RequestedByUserID = UserID;
                RequestData.Type = "Shift";
                var response = new SelectShiftLogResponse();
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectJoinShiftMasterandLog(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        public IHttpActionResult PostSaveDayClosing(DayClosing _ObjDay)
        {
            try
            {
                SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                RequestData.DayClosingRecord = new DayClosing();

                RequestData.DayClosingRecord.ID = _ObjDay.ID;
                RequestData.DayClosingRecord.CountryID = _ObjDay.CountryID;
                RequestData.DayClosingRecord.CountryCode = _ObjDay.CountryCode;
                RequestData.DayClosingRecord.StoreID = _ObjDay.StoreID;
                RequestData.DayClosingRecord.StoreCode = _ObjDay.StoreCode;
                RequestData.DayClosingRecord.POSID = _ObjDay.POSID;
                RequestData.DayClosingRecord.PosCode = _ObjDay.PosCode;
                RequestData.DayClosingRecord.ShiftID = _ObjDay.ShiftID;
                RequestData.DayClosingRecord.ShiftCode = _ObjDay.ShiftCode;
                RequestData.DayClosingRecord.ShiftInUserID = _ObjDay.ShiftInUserID;
                RequestData.DayClosingRecord.ShiftInUserCode = _ObjDay.ShiftInUserCode;
                RequestData.DayClosingRecord.Status = "Open";
                RequestData.DayClosingRecord.StartingTime = _ObjDay.StartingTime;
                RequestData.DayClosingRecord.ClosingTime = _ObjDay.ClosingTime;
                RequestData.DayClosingRecord.Amount = _ObjDay.Amount;
                var response = new SaveDayClosingResponse();
                var responseData = new DayClosingBLL();
                response = responseData.SaveDayClosing(RequestData);
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

        public IHttpActionResult PutShiftOut(DayClosing _ObjDay)
        {           
            try
            {                
                var RequestData = new SelectDayInRequest();
                RequestData.UserID = Convert.ToInt32(_ObjDay.ShiftInUserID);
                RequestData.StoreID = Convert.ToInt32(_ObjDay.StoreID);
                RequestData.BusinessDate = _ObjDay.StartingTime;
                RequestData.POSID = Convert.ToInt32(_ObjDay.POSID);
                RequestData.Amount = _ObjDay.Amount;
                SelectDayInResponse response = null;
                var ResponseData = new DayShiftLOGBLL();
                response = ResponseData.ShiftOut(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //call when user click dayout
        public IHttpActionResult GetShiftLog(int StoreID, int CountryID)
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                /*ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;*/
                //RequestData.UserID = UserID;
                RequestData.ShowInActiveRecords = false;
                //RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                SelectShiftLogResponse response = null;
                RequestData.CountryID = CountryID;
                RequestData.StoreID = StoreID;
                RequestData.POSID = 0;
                var ResponseData = new ShiftBLL();
                response = ResponseData.SelectJoinShiftMasterandLog(RequestData);
                if (response.AllShiftLOGandTypesList != null)
                {
                    var DayInData1 = response.AllShiftLOGandTypesList.Where(x => x.ShiftStatus == "Open" && x.Status == "Open").FirstOrDefault();
                    if (DayInData1 == null)
                    {
                        response.Dayout = true;
                        //dayout update
                    }
                    else
                    {
                        response.Dayout = false;
                        //dayout and shifotout is pending
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /* public IHttpActionResult PostInsertShiftLog(ShiftLOGTypes _ShiftLog)
         {
             try
             {
                 var RequestData = new SaveShiftLOGRequest();                
                 SaveShiftLOGResponse response = new SaveShiftLOGResponse();
                 var ResponseDate = new DayShiftLOGBLL();
                 response = ResponseDate.SaveDayClosing(RequestData);
                 return null;
             }
             catch (Exception ex)
             {
                 return InternalServerError(ex);
             }            
         }*/
    }
}