using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizResponse.Masters.LanguageResponse;
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
    public class LanguageController : ApiController
    {

        public IHttpActionResult GetLanguage()
        {
            try
            {
                var RequestData = new SelectAllLanguageRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllLanguageResponse response = null;
                var ResponseData = new LanguageBLL();
                response = ResponseData.SelectAllLanguage(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]
        public IHttpActionResult GetLanguage(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllLanguageRequest();

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



                SelectAllLanguageResponse response = null;
                var ResponseData = new LanguageBLL();
                response = ResponseData.API_SelectAllLanguage(RequestData);
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


        public IHttpActionResult GetLanguageByID(int ID)
        {
            try
            {
                var RequestData = new SelectByLanguageIDRequest();
                RequestData.ID = ID;
                SelectByLanguageIDResponse response = null;
                var ResponseData = new LanguageBLL();
                response = ResponseData.SelectLanguage(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostLanguage(LanguageMaster _objLanguage)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveLanguageRequest();
                RequestData.LanguageMasterRecord = new LanguageMaster();
                RequestData.LanguageMasterRecord = _objLanguage;
                RequestData.LanguageMasterRecord.CreateBy = UserID;
                RequestData.LanguageMasterRecord.CreateOn = DateTime.Now;
                RequestData.LanguageMasterRecord.SCN = 0;
                SaveLanguageResponse response = null;
                var ResponseData = new LanguageBLL();
                response = ResponseData.SaveLanguage(RequestData);
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

        public IHttpActionResult PutLanguage(LanguageMaster _objLanguage)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;


                var RequestData = new UpdateLanguageRequest();
                RequestData.LanguageMasterRecord = new LanguageMaster();
                RequestData.LanguageMasterRecord = _objLanguage;
                RequestData.LanguageMasterRecord.UpdateOn = DateTime.Now;
                RequestData.LanguageMasterRecord.UpdateBy = UserID;
                RequestData.LanguageMasterRecord.SCN = 0;
                UpdateLanguageResponse response = null;
                var ResponseData = new LanguageBLL();
                response = ResponseData.UpdateLanguage(RequestData);
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
