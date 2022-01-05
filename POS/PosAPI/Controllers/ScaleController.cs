using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    //[Authorize]
    public class ScaleController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetScaleList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllScaleRequest();

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



                SelectAllScaleResponse response = null;
                var ResponseData = new ScaleBLL();
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

        public IHttpActionResult GetScaleList()
        {
            try
            {
                var RequestData = new SelectAllScaleRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllScaleResponse response = null;
                var ResponseData = new ScaleBLL();
                response = ResponseData.SelectAllScale(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetScaleList(int ID)
        {
            try
            {
                var RequestData = new SelectByScaleIDRequest();
                RequestData.ID = ID;
                SelectByScaleIDResponse response = null;
                var ResponseData = new ScaleBLL();
                response = ResponseData.SelectScaleRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostScale(ScaleMaster _objScaleMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveScaleRequest();                
                RequestData.ScaleRecord = new ScaleMaster();
                RequestData.ScaleRecord = _objScaleMaster;
                RequestData.ScaleDetailMasterList = _objScaleMaster.ScaleDetailMasterList;
                RequestData.BrandMasterList = _objScaleMaster.BrandMasterList;
                RequestData.ScaleRecord.CreateOn = DateTime.Now;
                RequestData.ScaleRecord.CreateBy = UserID;
                RequestData.ScaleRecord.SCN = 0;
                SaveScaleResponse response = null;
                var ResponseData = new ScaleBLL();
                response = ResponseData.SaveScale(RequestData);
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
