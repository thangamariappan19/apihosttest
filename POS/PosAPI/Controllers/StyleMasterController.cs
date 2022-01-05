using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.StyleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StyleMasterController : ApiController
    {
        //Many Lookups needed 

        //select all
        public IHttpActionResult GetStyleData()
        {
            try
            {
                var RequestData = new SelectAllStyleRequest();
                SelectAllStyleResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectAllStyleRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetStyleData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStyleRequest();
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
                SelectAllStyleResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.API_SelectAllStyleRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetStyleDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByStyleIDRequest();
                RequestData.ID = ID;
                SelectByStyleIDResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectStyleRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetStyleCode(string StyleCode)
        {
            try
            {
                var RequestData = new StyleCodeGeneratingRequest();
                RequestData.StyleCode = StyleCode;
                StyleCodeGeneratingResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectStyleCodeRunningNum(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Insert and Update
        public IHttpActionResult PostStyleMaster(StyleMaster _objStyleMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStyleRequest();
                
                RequestData.StyleRecord = new StyleMaster();
                RequestData.StyleRecord = _objStyleMaster;
                RequestData.StyleWithScaleDetailsList = _objStyleMaster.ScaleDetailMasterList;
                RequestData.ItemImageMasterDetailsList = _objStyleMaster.ItemImageMasterList;
                RequestData.StyleWithColorDetailsList = _objStyleMaster.ColorMasterList;
                RequestData.StyleRecord.CreateBy = UserID;
                SaveStyleResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SaveStyle(RequestData);
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

        public IHttpActionResult GetStyleColorScale(string StyleCode, string X)
        {
            try
            {
                var RequestData = new SelectAllStyleRequest();
                RequestData.StyleCode = StyleCode;
                SelectAllStyleResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectstyleColorSizeTypesListRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
