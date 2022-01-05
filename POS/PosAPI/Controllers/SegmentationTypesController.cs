using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.SegmentationMasterResponse;
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
    public class SegmentationTypesController : ApiController
    {
        
        [HttpGet]
        public IHttpActionResult GetStyleSegamationList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllSegmentRequest();

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



                SelectAllSegmentResponse response = null;
                var ResponseData = new SegmentMasterBLL();
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
        public IHttpActionResult GetSegmentationTypesData()
        {
            try
            {
                var RequestData = new SelectAllSegmentRequest();
                SelectAllSegmentResponse response = null;
                var ResponseData = new SegmentMasterBLL();
                response = ResponseData.SelectAllSegmentMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetSegmentationTypeDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectBySegmentIDRequest();
                RequestData.ID = ID;
                SelectBySegmentIDResponse response = null;
                var ResponseData = new SegmentMasterBLL();
                response = ResponseData.SelectByIDSegmentMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostRoleMaster(SegmentMaster _objSegmentatioTypeMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveSegmentRequest();
                RequestData.SegmentationRecord = new SegmentMaster();
                RequestData.SegmentationRecord = _objSegmentatioTypeMaster;
                RequestData.SegmentationRecord.CreateBy = UserID;
                SaveSegmentResponse response = null;
                var ResponseData = new SegmentMasterBLL();
                response = ResponseData.SaveSegementMaster(RequestData);
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

        public IHttpActionResult PutsegmentationTypeMaster(SegmentMaster _objSegmentatioTypeMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateSegmentRequest();
                RequestData.SegmentMasterData = new SegmentMaster();
                RequestData.SegmentMasterData = _objSegmentatioTypeMaster;
                RequestData.SegmentMasterData.CreateBy = UserID;
                UpdateSegmentResponse response = null;
                var ResponseData = new SegmentMasterBLL();
                response = ResponseData.UpdateSegmentMaster(RequestData);
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
