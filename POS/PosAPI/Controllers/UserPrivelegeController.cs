using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class UserPrivelegeController : ApiController
    {
        [Authorize]
        //select all
        public IHttpActionResult GetPOSScreenNamesData()
        {
            try
            {
                var RequestData = new GetScreenNamesRequest();
                GetScreenNamesResponse response = null;
                var ResponseData = new PrevilegesBLL();
                response = ResponseData.POSScreenNames(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by Role - Edit
        public IHttpActionResult GetUserPrivelegesByID(int ID)
        {
            try
            {
                var RequestData = new SelectByUserIDPrivilagesRequest();
                RequestData.ID = ID;
                SelectByUserIDPrivilagesResponse response = null;
                var ResponseData = new PrevilegesBLL();
                response = ResponseData.SelectUserIDPrivilagesResponse(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostUserPrivelegesMaster(UserPrivilagesTypes _objUserPrivelegesMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePrevilegesRequestt();
                RequestData.UserPrivilagesData = new UserPrivilagesTypes();
                RequestData.UserPrivilagesData = _objUserPrivelegesMaster;
                RequestData.UserPrivilagesData.CreateBy = UserID;
                SavePrevilegesResponse response = null;
                var ResponseData = new PrevilegesBLL();
                response = ResponseData.SaveMASUserprivilagesResponse(RequestData);
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
