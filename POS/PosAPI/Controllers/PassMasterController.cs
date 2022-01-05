using EasyBizBLL.FCPasses;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.FCPasses;
using EasyBizRequest.FCPasses;
using EasyBizResponse.FCPasses;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    [Authorize]
    public class PassMasterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPassesList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new PassMasterRequest();

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

                PassMasterResponse response = null;
                var ResponseData = new PassMasterBLL();
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

        //Select By ID
        public IHttpActionResult GetPassMasterByID(int ID)
        {
            try
            {
                var RequestData = new PassMasterRequest();
                RequestData.ID = ID;
                PassMasterResponse response = null;
                var ResponseData = new PassMasterBLL();
                response = ResponseData.SelectPassMasterRecord(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
                //return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPassMasterLookUp()
        {
            try
            {
                var RequestData = new SelectPassMasterLookUpRequest();
                //RequestData.ID = ID;
                SelectPassMasterLookUpResponse response = null;
                var ResponseData = new PassMasterBLL();
                response = ResponseData.API_SelectPassMasterLookUp(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
                //return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPassMaster(PassMaster _ObjPassMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new PassMasterRequest();
                RequestData.PassMasterRequestData = new PassMaster();
                RequestData.PassMasterRequestData = _ObjPassMaster;
                RequestData.PassMasterRequestData.CreateBy = UserID;
                PassMasterResponse response = null;
                var ResponseData = new PassMasterBLL();
                response = ResponseData.SavePassMaster(RequestData);
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

        public IHttpActionResult PutPassMaster(PassMaster _ObjPassMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new PassMasterRequest();
                RequestData.PassMasterRequestData = new PassMaster();
                RequestData.PassMasterRequestData = _ObjPassMaster;
                RequestData.PassMasterRequestData.UpdateBy = UserID;
                PassMasterResponse response = null;
                var ResponseData = new PassMasterBLL();
                response = ResponseData.UpdatePassMaster(RequestData);
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
