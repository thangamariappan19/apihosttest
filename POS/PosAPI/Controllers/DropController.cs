using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.DropMasterResponse;
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
    public class DropController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDropList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllDropMasterRequest();

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



                SelectAllDropMasterResponse response = null;
                var ResponseData = new DropMasterBLL();
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
        public IHttpActionResult GetDropData()
        {
            try
            {
                var RequestData = new SelectAllDropMasterRequest();
                SelectAllDropMasterResponse response = null;
                var ResponseData = new DropMasterBLL();
                response = ResponseData.SelectAllDropMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Select by ID - Edit
        public IHttpActionResult GetdropMasterDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDDropMasterRequest();
                RequestData.ID = ID;
                SelectByIDDropMasterResponse response = null;
                var ResponseData = new DropMasterBLL();
                response = ResponseData.SelectByIDDropMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostDropMaster(DropMasterTypes _objDropMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;


                var RequestData = new SaveDropMasterRequest();
                RequestData.DropMasterTypesRecord = new DropMasterTypes();
                RequestData.DropMasterTypesRecord = _objDropMaster;
                RequestData.DropMasterTypesRecord.CreateOn = DateTime.Now;
                RequestData.DropMasterTypesRecord.CreateBy = UserID;
                RequestData.DropMasterTypesRecord.SCN = 0;
                SaveDropMasterResponse response = null;
                var ResponseData = new DropMasterBLL();
                response = ResponseData.SaveDropMaster(RequestData);
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
        public IHttpActionResult PutDropMaster(DropMasterTypes _objDropMaster)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;



                var RequestData = new UpdateDropMasterRequest();
                RequestData.DropMasterTypesRequestData = new DropMasterTypes();
                RequestData.DropMasterTypesRequestData = _objDropMaster;
                RequestData.DropMasterTypesRequestData.UpdateOn = DateTime.Now;
                RequestData.DropMasterTypesRequestData.SCN = 0;
                RequestData.DropMasterTypesRequestData.UpdateBy = UserID;
                UpdateDropMasterResponse response = null;
                var ResponseData = new DropMasterBLL();
                response = ResponseData.UpdateDropMaster(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
