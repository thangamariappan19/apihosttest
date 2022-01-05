using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizResponse.Masters.DesignationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DesignationController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetDesignationData()
        {
            try
            {
                var RequestData = new SelectAllDesignationMasterRequest();
                SelectAllDesignationMasterResponse response = null;
                var ResponseData = new DesignationMasterBLL();
                response = ResponseData.SelectAllDesignationMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
          public IHttpActionResult GetDesignationData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllDesignationMasterRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllDesignationMasterResponse response = null;
                var ResponseData = new DesignationMasterBLL();
                response = ResponseData.API_SelectAllDesignationMaster(RequestData);
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

        //Select by ID - Edit
        public IHttpActionResult GetDesignationDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDDesignationMasterRequest();
                RequestData.ID = ID;
                SelectByIDDesignationMasterResponse response = null;                
                var ResponseData = new DesignationMasterBLL();
                response = ResponseData.SelectDesignationMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostDesignationMaster(DesignationMaster _objDesignationMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveDesignationMasterRequest();
                RequestData.DesignationMasterData = new DesignationMaster();
                RequestData.DesignationMasterData = _objDesignationMaster;
                RequestData.DesignationMasterData.CreateBy = UserID;
                SaveDesignationMasterResponse response = null;
                var ResponseData = new DesignationMasterBLL();
                response = ResponseData.SaveDesignationMaster(RequestData);
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

        public IHttpActionResult PutDesignationeMaster(DesignationMaster _objDesignationMaster)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateDesignationMasterRequest();
                RequestData.DesignationMasterData = new DesignationMaster();
                RequestData.DesignationMasterData = _objDesignationMaster;
                RequestData.DesignationMasterData.UpdateBy = UserID;
                UpdateDesignationMasterResponse response = null;
                var ResponseData = new DesignationMasterBLL();
                response = ResponseData.UpdateDesignationMaster(RequestData);
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
