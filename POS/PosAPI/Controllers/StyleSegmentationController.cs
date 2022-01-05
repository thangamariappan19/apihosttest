using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StyleSegmentationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStyleSegmentationList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllAFSegamationMasterRequest();

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



                SelectAllAFSegamationMasterResponse response = null;
                var ResponseData = new AFSegamationMasterBLL();
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
        public IHttpActionResult GetStyleSegmentationData()
        {
            try
            {
                var RequestData = new SelectAllAFSegamationMasterRequest();
                SelectAllAFSegamationMasterResponse response = null;
                var ResponseData = new AFSegamationMasterBLL();
                response = ResponseData.SelectAllAFSegamationMaster(RequestData);
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
        public IHttpActionResult GetStyleSegmentationDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDAFSegamationMasterRequest();
                RequestData.ID = ID;
                SelectByIDAFSegamationMasterResponse response = null;
                var ResponseData = new AFSegamationMasterBLL();
                response = ResponseData.SelectByIDAFSegamationMaster(RequestData);
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

        //Insert Or Update
        public IHttpActionResult PoststyleSegmentationMaster(AFSegamationMasterTypes _objStyleSegmentationMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveAFSegamationMasterRequest();
                RequestData.AFSegamationMasterTypesRecord = new AFSegamationMasterTypes();
                RequestData.AFSegamationMasterTypesRecord = _objStyleSegmentationMaster;
                RequestData.AFSegmentationDetailMasterList = _objStyleSegmentationMaster.SegmentList;
                RequestData.AFSegamationMasterTypesRecord.CreateBy = UserID;
                SaveAFSegamationMasterResponse response = null;
                var ResponseData = new AFSegamationMasterBLL();
                response = ResponseData.SaveAFSegamationMaster(RequestData);
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
