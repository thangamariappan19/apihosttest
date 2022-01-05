using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using EasyBizResponse.Masters.CustomerGroupResponse;
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
    public class CustomerGroupController : ApiController
    {        
        [HttpGet]
        public IHttpActionResult GetEmployeeList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCustomerGroupMasterRequest();

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



                SelectAllCustomerGroupMasterResponse response = null;
                var ResponseData = new CustomerGroupBLL();
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
        public IHttpActionResult GetAllCustomerGroupData()
        {
            try
            {
                var RequestData = new SelectAllCustomerGroupMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllCustomerGroupMasterResponse response = null;
                var ResponseData = new CustomerGroupBLL();
                response = ResponseData.SelectAllCustomerGroupMasterResponse(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCustomerGroupDataByID(int ID)
        {
            try
            {
               

                var RequestData = new SelectByIDCustomerGroupRequest();
                RequestData.ID = ID;
                SelectByIDCustomerGroupResponse response = null;
                var ResponseData = new CustomerGroupBLL();
                response = ResponseData.SelectByIDCustomerGroupResponse(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCustomerGroupMaster(CustomerGroupMaster _objCustomerGroupMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCustomerGroupRequest();
                RequestData.CustomerGroupMasterData = new CustomerGroupMaster();
                RequestData.CustomerGroupMasterData = _objCustomerGroupMaster;
                RequestData.CustomerGroupMasterData.CreateBy = UserID;
                RequestData.CustomerGroupMasterData.CreateOn = DateTime.Now;
                SaveCustomerGroupResponse response = null;
                var ResponseData = new CustomerGroupBLL();
                response = ResponseData.SaveCustomerGroup(RequestData);
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
        public IHttpActionResult PutCustomerGroupeMaster(CustomerGroupMaster _objRoleMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0; 

                var RequestData = new UpdateCustomerGroupMasterRequest();
                RequestData.CustomerGroupMasterData = new CustomerGroupMaster();
                RequestData.CustomerGroupMasterData = _objRoleMaster;
                RequestData.CustomerGroupMasterData.CreateBy = UserID;
                RequestData.CustomerGroupMasterData.UpdateOn = DateTime.Now;
                UpdateCustomerGroupMasterResponse response = null;
                var ResponseData = new CustomerGroupBLL();
                response = ResponseData.UpdateCustomerGroup(RequestData);
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
