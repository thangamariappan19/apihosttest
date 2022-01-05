using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse.Masters.CurrencyResponse;
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
    public class CurrencyController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetCurrencyList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCurrencyRequest();

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



                SelectAllCurrencyResponse response = null;
                var ResponseData = new CurrencyBLL();
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

        public IHttpActionResult GetCurrencyeData()
        {
            try
            {
                var RequestData = new SelectAllCurrencyRequest();
                SelectAllCurrencyResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.SelectAllCurrencyMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select by ID - Edit
        public IHttpActionResult GetCurrencyDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDCurrencyRequest();
                RequestData.ID = ID;
                SelectByIDCurrencyResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.SelectCurrencyMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCurrencyMaster(CurrencyMaster _objCurrencyMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCurrencyRequest();
                RequestData.CurrencyMasterData = new CurrencyMaster();
                RequestData.CurrencyMasterData = _objCurrencyMaster;
                RequestData.CurrencyMasterData.CreateBy = UserID;
                SaveCurrencyResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.SaveCurrencyMaster(RequestData);
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

        public IHttpActionResult PutcurrencyMaster(CurrencyMaster _objCurrencyMaster)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCurrencyRequest();
                RequestData.CurrencyMasterData = new CurrencyMaster();
                RequestData.CurrencyMasterData = _objCurrencyMaster;
                RequestData.CurrencyMasterData.UpdateBy = UserID;
                UpdateCurrencyResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.UpdateCurrencyMaster(RequestData);
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
