using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ProductSubGroupController : ApiController
    {
        public IHttpActionResult GetProductGroupList()
        {
            try
            {
                var RequestData = new SelectAllProductGroupRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllProductGroupResponse response = null;
                var ResponseData = new ProductGroupBLL();
                response = ResponseData.SelectAllProductGroup(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetProductGroupList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllProductGroupRequest();
                RequestData.ShowInActiveRecords = true;
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
                SelectAllProductGroupResponse response = null;
                var ResponseData = new ProductGroupBLL();
                response = ResponseData.API_SelectAllProductGroup(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetProductGroupByID(int ID)
        {
            try
            {
                var RequestData = new SelectProductGroupListForProductSubGroupRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ProductGroupID = ID;
                SelectProductGroupListForProductSubGroupResponse response = null;
                var ResponseData = new ProductSubGroupBLL();
                response = ResponseData.ProductSubGroupByProductGroup(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostProductGroup(List<ProductSubGroupMaster> ProductSubGrouplist)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveProductSubGroupRequest();
                RequestData.ProductSubGrouplist = ProductSubGrouplist;
                SaveProductSubGroupResponse response = null;
                var ResponseData = new ProductSubGroupBLL();
                response = ResponseData.SaveProductSubGroup(RequestData);
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
