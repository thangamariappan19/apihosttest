using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse.Masters.StoreGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StoreGroupController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStoreGroupList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStoreGroupRequest();

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



                SelectAllStoreGroupResponse response = null;
                var ResponseData = new StoreGroupBLL();
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
        public IHttpActionResult GetStoreGroupData()
        {
            try
            {
                var RequestData = new SelectAllStoreGroupRequest();
                SelectAllStoreGroupResponse response = null;
                var ResponseData = new StoreGroupBLL();
                response = ResponseData.SelectAllStoreGroupMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select by ID - Edit
        public IHttpActionResult GetStoreGroupDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDStoreGroupRequest();
                RequestData.ID = ID;
                SelectByIDStoreGroupResponse response = null;
                var ResponseData = new StoreGroupBLL();
                response = ResponseData.SelectStoreGroupMasterRecord(RequestData);
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

        public IHttpActionResult PostStoreGroupMaster(StoreGroupMaster _objStoreGroupMaster)
        {
            try
            {
                var RequestData = new SaveStoreGroupRequest();
                RequestData.StoreGroupMasterData = new StoreGroupMaster();
                RequestData.StoreGroupMasterData = _objStoreGroupMaster;
                RequestData.StoreGroupDetailsList = _objStoreGroupMaster.StoreGroupDetailsList;
                SaveStoreGroupResponse response = null;
                var ResponseData = new StoreGroupBLL();
                response = ResponseData.SaveStoreGroupMaster(RequestData);
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
