using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ManagerOverrideController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetManagerOverride()
        {
            try
            {
                var RequestData = new SelectAllManagerOverrideRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllManagerOverrideResponse response = null;
                var ResponseData = new ManagerOverrideBLL();
                response = ResponseData.SelectAllManagerOverride(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCountryList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllManagerOverrideRequest();

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



                SelectAllManagerOverrideResponse response = null;
                var ResponseData = new ManagerOverrideBLL();
                response = ResponseData.API_SelectAllManagerOverride(RequestData);
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

        public IHttpActionResult GetManagerOverride(int ID)
        {
            try
            {
                var RequestData = new SelectByIDManagerOverrideRequest();
                RequestData.ID = ID;
                SelectByIDManagerOverrideResponse response = null;
                var ResponseData = new ManagerOverrideBLL();
                response = ResponseData.SelectManagerOverride(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostManagerOverride(ManagerOverride _objManager)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveManagerOverrideRequest();
                RequestData.ManagerOverrideData = new ManagerOverride();
                RequestData.ManagerOverrideData = _objManager;                
                RequestData.ManagerOverrideData.CreateOn = DateTime.Now;
                RequestData.ManagerOverrideData.CreateBy = UserID;
                RequestData.ManagerOverrideData.SCN = 0;
                SaveManagerOverrideResponse response = null;
                var ResponseData = new ManagerOverrideBLL();
                response = ResponseData.SaveManagerOverride(RequestData);
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

        public IHttpActionResult PutManagerOverride(ManagerOverride _objManager)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveManagerOverrideRequest();
                RequestData.ManagerOverrideData = new ManagerOverride();
                RequestData.ManagerOverrideData = _objManager;
                RequestData.ManagerOverrideData.UpdateOn = DateTime.Now;
                RequestData.ManagerOverrideData.CreateBy = UserID;
                RequestData.ManagerOverrideData.SCN = 0;
                SaveManagerOverrideResponse response = null;
                var ResponseData = new ManagerOverrideBLL();
                response = ResponseData.SaveManagerOverride(RequestData);
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
