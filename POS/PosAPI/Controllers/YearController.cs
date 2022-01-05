using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.YearMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class YearController : ApiController
    {

        //select all
        public IHttpActionResult GetYearData()
        {
            try
            {
                var RequestData = new SelectAllYearRequest();
                SelectAllYearResponse response = null;
                var ResponseData = new YearBLL();
                response = ResponseData.SelectAllYear(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetYearData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllYearRequest();

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

                SelectAllYearResponse response = null;
                var ResponseData = new YearBLL();
                response = ResponseData.API_SelectAllYear(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select by ID - Edit
        public IHttpActionResult GetYearDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByYearIDRequest();
                RequestData.ID = ID;
                SelectByYearIDResponse response = null;
                var ResponseData = new YearBLL();
                response = ResponseData.SelectYearRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostYearMaster(YearMaster _objYearMaster)
        {
            try
            {
                var RequestData = new SaveYearRequest();
                RequestData.YearRecord = new YearMaster();
                RequestData.YearRecord = _objYearMaster;
                SaveYearResponse response = null;
                var ResponseData = new YearBLL();
                response = ResponseData.SaveYear(RequestData);
                //return Ok(response);
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

        public IHttpActionResult PutYearMaster(YearMaster _objYearMaster)
        {
            try
            {
                var RequestData = new UpdateYearRequest();
                RequestData.YearRecord = new YearMaster();
                RequestData.YearRecord = _objYearMaster;
                UpdateYearResponse response = null;
                var ResponseData = new YearBLL();
                response = ResponseData.UpdateYear(RequestData);
                //return Ok(response);
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
