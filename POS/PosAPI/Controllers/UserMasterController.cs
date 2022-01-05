using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.UsersResponse;
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
    public class UserMasterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetUserList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllUsersRequest();

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



                SelectAllUsersResponse response = null;
                var ResponseData = new UsersBLL();
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
        //select all

        //public IHttpActionResult GetUsersList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllUsersRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllUsersResponse response = null;
        //        var ResponseData = new UsersBLL();
        //        response = ResponseData.SelectAllUsers(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetUsersList(int ID)
        {
            try
            {
                var RequestData = new SelectByUsersIDRequest();
                RequestData.ID = ID;
                SelectByUsersIDResponse response = null;
                var ResponseData = new UsersBLL();
                response = ResponseData.SelectUserMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostUser(UsersSettings _objUserSettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveUsersRequest();
                RequestData.UsersRecord = new UsersSettings();
                RequestData.UsersRecord = _objUserSettings;
                RequestData.UsersRecord.CreateOn = DateTime.Now;
                RequestData.UsersRecord.CreateBy = UserID;
                RequestData.UsersRecord.SCN = 0;
                
                SaveUsersResponse response = null;
                var ResponseData = new UsersBLL();
                response = ResponseData.SaveUsers(RequestData);
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
        public IHttpActionResult PutUser(UsersSettings _objUserSettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new UpdateUsersRequest();
                RequestData.UsersRecord = new UsersSettings();
                RequestData.UsersRecord = _objUserSettings;
                RequestData.UsersRecord.UpdateOn = DateTime.Now;
                RequestData.UsersRecord.SCN = 0;
                RequestData.UsersRecord.UpdateBy = UserID;
                UpdateUsersResponse response = null;
                var ResponseData = new UsersBLL();
                response = ResponseData.UpdateUsers(RequestData);
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
