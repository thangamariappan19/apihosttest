using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.StateMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StateController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetState(int ID)
        {
            try
            {
                var RequestData = new SelectByStateIDRequest();
                RequestData.ID = ID;
                SelectByStateIDResponse response = null;
                var ResponseData = new StateMasterBLL();
                response = ResponseData.SelectStateRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public IHttpActionResult GetAllState()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllStateRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllStateResponse response = null;
        //        var ResponseData = new StateMasterBLL();
        //        response = ResponseData.SelectAllRecordStateMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpGet]
        public IHttpActionResult GetAllState(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStateRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllStateResponse response = null;
                var ResponseData = new StateMasterBLL();
                response = ResponseData.API_SelectAllStateMaster(RequestData);
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

        public IHttpActionResult PostState(StateMaster _objStateMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStateRequest();
                RequestData.StateRecord = new StateMaster();
                RequestData.StateRecord = _objStateMaster;
                RequestData.StateRecord.CreateOn = DateTime.Now;
                RequestData.StateRecord.CreateBy = UserID;
                RequestData.StateRecord.SCN = 0;
                SaveStateResponse response = null;
                var ResponseData = new StateMasterBLL();
                response = ResponseData.SaveStateMaster(RequestData);
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

        public IHttpActionResult Putstate(StateMaster _objStateMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateStateRequest();
                RequestData.StateRecord = new StateMaster();
                RequestData.StateRecord = _objStateMaster;
                RequestData.StateRecord.UpdateOn = DateTime.Now;
                RequestData.StateRecord.UpdateBy = UserID;
                RequestData.StateRecord.SCN = 0;
                UpdateStateResponse response = null;
                var ResponseData = new StateMasterBLL();
                response = ResponseData.UpdateState(RequestData);
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
