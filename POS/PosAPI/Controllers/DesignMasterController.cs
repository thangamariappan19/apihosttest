using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DesignMasterController : ApiController
    {
        //still some lookups for design master need to be developed. 
        //select all
        [Authorize]
        public IHttpActionResult GetDesignMasterData()
        {
            try
            {
                var RequestData = new SelectAllDesignMasterRequest();
                SelectAllDesignMasterResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.SelectAllDesignMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetDesignMasterData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllDesignMasterRequest();

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

                SelectAllDesignMasterResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.API_SelectAllDesignMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetDesignMasterDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDDesignMasterRequest();
                RequestData.ID = ID;
                SelectByIDDesignMasterResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.SelectByIDDesignMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostDesignMaster(DesignMasterTypes _objDesignMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveDesignMasterRequest();
                RequestData.DesignMasterData = new DesignMasterTypes();
                RequestData.DesignMasterData = _objDesignMaster;
                RequestData.DesignMasterData.CreateBy = UserID;
                SaveDesignMasterResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.SaveDesignMaster(RequestData);
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

        public IHttpActionResult PutRoleMaster(DesignMasterTypes _objDesignMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateDesignMasterRequest();
                RequestData.DesignMasterData = new DesignMasterTypes();
                RequestData.DesignMasterData = _objDesignMaster;
                RequestData.DesignMasterData.UpdateBy = UserID;
                UpdateDesignMasterResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.UpdateDesignMaster(RequestData);
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
