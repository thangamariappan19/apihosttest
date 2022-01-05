using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.ShiftMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    [Authorize]
    public class ShiftController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetShiftList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllShiftRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllShiftResponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.API_SelectALL(RequestData);
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

        //public IHttpActionResult GetCountryShiftList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllShiftRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllShiftResponse response = null;
        //        var ResponseData = new ShiftBLL();
        //        response = ResponseData.SelectAllShiftRecords(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetShiftByCountry(int CountryID)
        {
            try
            {
                var RequestData = new SelectShiftListForCategoryRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CountryID = CountryID;
                SelectShiftListForCategoryResponse response = null;
                var ResponseData = new ShiftBLL();
                response = ResponseData.ShiftByCountry(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostShift(List<ShiftMaster> ShiftList)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveShiftRequest();
              
                RequestData.Shiftlist = ShiftList;
                SaveShiftResponse response = null;
                var ResponseData = new ShiftBLL();

                response = ResponseData.SaveShift(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
