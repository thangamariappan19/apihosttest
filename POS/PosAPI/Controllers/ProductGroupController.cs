using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ProductGroupController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProductGroupList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllProductGroupRequest();

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

        public IHttpActionResult GetProductGroupByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDProductGroupRequest();
                RequestData.ID = ID;
                SelectByIDProductGroupResponse response = null;
                var ResponseData = new ProductGroupBLL();
                response = ResponseData.SelectProductGroup(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostProductGroup(ProductGroupMaster _objProductGroup)
        {
            try
            {
                var RequestData = new SaveProductGroupRequest();
                RequestData.ProductGroupRecord = new ProductGroupMaster();
                RequestData.ProductGroupRecord = _objProductGroup;
                RequestData.ProductGroupRecord.CreateOn = DateTime.Now;
                RequestData.ProductGroupRecord.SCN = 0;
                SaveProductGroupResponse response = null;
                var ResponseData = new ProductGroupBLL();
                response = ResponseData.SaveProductGroup(RequestData);
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

        public IHttpActionResult PutProductGroup(ProductGroupMaster _objProductGroup)
        {
            try
            {
                var RequestData = new UpdateProductGroupRequest();
                RequestData.ProductGroupRecord = new ProductGroupMaster();
                RequestData.ProductGroupRecord = _objProductGroup;
                RequestData.ProductGroupRecord.UpdateOn = DateTime.Now;
                RequestData.ProductGroupRecord.SCN = 0;
                UpdateProductGroupResponse response = null;
                var ResponseData = new ProductGroupBLL();
                response = ResponseData.UpdateProductGroup(RequestData);
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
