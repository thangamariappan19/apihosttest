using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
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
    public class StyleStatusController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPriceTypeList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStyleStatusMasterRequest();

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



                SelectAllStyleStatusMasterResponse response = null;
                var ResponseData = new StyleStatusBLL();
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

        public IHttpActionResult GetStyleStatusList()
        {
            try
            {
                var RequestData = new SelectAllStyleStatusMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllStyleStatusMasterResponse response = null;
                var ResponseData = new StyleStatusBLL();
                response = ResponseData.SelectAllStyleStatus(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetStyleStatusByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDStyleStatusMasterRequest();
                RequestData.ID = ID;
                SelectByIDStyleStatusMasterResponse response = null;
                var ResponseData = new StyleStatusBLL();
                response = ResponseData.SelectByIDStyleStatus(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostStyleStatus(StyleStatusMasterType _objStyleStatus)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStyleStatusMasterRequest();
                RequestData.StyleStatusMasterTypeRecord = new StyleStatusMasterType();
                RequestData.StyleStatusMasterTypeRecord = _objStyleStatus;
                RequestData.StyleStatusMasterTypeRecord.CreateOn = DateTime.Now;
                RequestData.StyleStatusMasterTypeRecord.CreateBy = UserID;
                RequestData.StyleStatusMasterTypeRecord.SCN = 0;
                SaveStyleStatusMasterResponse response = null;
                var ResponseData = new StyleStatusBLL();
                response = ResponseData.SaveStyleStatus(RequestData);
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

        public IHttpActionResult PutStyleStatus(StyleStatusMasterType _objStyleStatus)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateStyleStatusMasterRequest();
                RequestData.StyleStatusMasterTypeRecord = new StyleStatusMasterType();
                RequestData.StyleStatusMasterTypeRecord = _objStyleStatus;
                RequestData.StyleStatusMasterTypeRecord.UpdateOn = DateTime.Now;
                RequestData.StyleStatusMasterTypeRecord.UpdateBy = UserID;
                RequestData.StyleStatusMasterTypeRecord.SCN = 0;
                UpdateStyleStatusMasterResponse response = null;
                var ResponseData = new StyleStatusBLL();
                response = ResponseData.UpdateStyleStatus(RequestData);
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
