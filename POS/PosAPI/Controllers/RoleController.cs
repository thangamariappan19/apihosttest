using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.RoleResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class RoleController : ApiController
    {

        //Only 2 fields for lookup values
        public IHttpActionResult GetRoleData()
        {
            try
            {
                var RequestData = new SelectRoleMasterLookUpRequest();
                SelectRoleMasterLookUpResponse response = null;
                var ResponseData = new RoleBLL();
                response = ResponseData.SelectRoleLookUP(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //select all

        [HttpGet]
        public IHttpActionResult GetRoleList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllRoleRequest();

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



                SelectAllRoleResponse response = null;
                var ResponseData = new RoleBLL();
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
        //public IHttpActionResult GetRoleData()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllRoleRequest();
        //        SelectAllRoleResponse response = null;
        //        var ResponseData = new RoleBLL();
        //        response = ResponseData.SelectAllRoleMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        //Select by ID - Edit
        public IHttpActionResult GetRoleDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDRoleRequest();
                RequestData.ID = ID;
                SelectByIDRoleResponse response = null;
                var ResponseData = new RoleBLL();
                response = ResponseData.SelectRoleMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostRoleMaster (RoleMaster _objRoleMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveRoleRequest();
                RequestData.RoleMasterData = new RoleMaster();
                RequestData.RoleMasterData = _objRoleMaster;
                RequestData.RoleMasterData.CreateBy = UserID;
                SaveRoleResponse response = null;
                var ResponseData = new RoleBLL();
                response = ResponseData.SaveRoleMaster(RequestData);
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
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutRoleMaster(RoleMaster _objRoleMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateRoleRequest();
                RequestData.RoleMasterData = new RoleMaster();
                RequestData.RoleMasterData = _objRoleMaster;
                RequestData.RoleMasterData.UpdateBy = UserID; 
                UpdateRoleResponse response = null;
                var ResponseData = new RoleBLL();
                response = ResponseData.UpdateRoleMaster(RequestData);
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
