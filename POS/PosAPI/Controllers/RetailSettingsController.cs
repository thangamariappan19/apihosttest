using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
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
    public class RetailSettingsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetRetailSettingsList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllRetailRequest();

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



                SelectAllRetailResponse response = null;
                var ResponseData = new RetailSettingsBLL();
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
        public IHttpActionResult GetRetailList()
        {
            try
            {
                var RequestData = new SelectAllRetailRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllRetailResponse response = null;
                var ResponseData = new RetailSettingsBLL();
                response = ResponseData.SelectAllRetail(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetRetailList(int ID)
        {
            try
            {
                var RequestData = new SelectByRetailIDRequest();
                RequestData.ID = ID;
                SelectByRetailIDResponse response = null;
                var ResponseData = new RetailSettingsBLL();
                response = ResponseData.SelectRetailRecord(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostRetailSettings(RetailSettingsType _objRetail)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveRetailRequest();
                RequestData.RetailRecord = new RetailSettingsType();
                RequestData.RetailRecord = _objRetail;
                RequestData.RetailRecord.CreateOn = DateTime.Now;
                RequestData.RetailRecord.CreateBy = UserID;
                RequestData.RetailRecord.SCN = 0;
                SaveRetailResponse response = null;
                var ResponseData = new RetailSettingsBLL();
                response = ResponseData.SaveRetail(RequestData);
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

        public IHttpActionResult PutRetailSettings(RetailSettingsType _objRetail)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveRetailRequest();
                RequestData.RetailRecord = new RetailSettingsType();
                RequestData.RetailRecord = _objRetail;
                RequestData.RetailRecord.UpdateOn = DateTime.Now;
                RequestData.RetailRecord.CreateBy = UserID;
                RequestData.RetailRecord.SCN = 0;
                SaveRetailResponse response = null;
                var ResponseData = new RetailSettingsBLL();
                response = ResponseData.SaveRetail(RequestData);
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
