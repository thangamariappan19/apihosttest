using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CityMasterRequest;
using EasyBizResponse.Masters.CityMasterResponse;
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
    public class CityMasterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCityList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCityRequest();

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



                SelectAllCityResponse response = null;
                var ResponseData = new CityMasterBLL();
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

        //Select All
        public IHttpActionResult GetAllCityMasterData()
        {
            try
            {
                var RequestData = new SelectAllCityRequest();
                SelectAllCityResponse response = null;
                var ResponseData = new CityMasterBLL();
                response = ResponseData.SelectAllRecordCityMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
              return  InternalServerError(ex);
            }
        }

        //Select By ID
        public IHttpActionResult GetCityMasterByID(int ID)
        {
            try
            {
                var RequestData = new SelectByCityIDRequest();
                RequestData.ID = ID;
                SelectByCityIDResponse response = null;
                var ResponseData = new CityMasterBLL();
                response = ResponseData.SelectCityRecord(RequestData);
                return Ok(response);
            }
            catch(Exception  ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCityMaster(CityMaster _ObjCityMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCityRequest();
                RequestData.CityRecord = new CityMaster();
                RequestData.CityRecord = _ObjCityMaster;
                RequestData.CityRecord.CreateBy = UserID;
                SaveCityResponse response = null;                
                var ResponseData = new CityMasterBLL();
                response = ResponseData.SaveCityMaster(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutCityMaster(CityMaster _ObjCityMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCityRequest();
                RequestData.CityRecord = new CityMaster();
                RequestData.CityRecord = _ObjCityMaster;
                RequestData.CityRecord.UpdateBy = UserID;
                UpdateCityResponse response = null;
                var ResponseData = new CityMasterBLL();
                response = ResponseData.UpdateCity(RequestData);
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
