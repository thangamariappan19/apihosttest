using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizResponse.Masters.PosMasterResponse;
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
    public class POSController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPOSList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPosMasterRequest();

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



                SelectAllPosMasterResponse response = null;
                var ResponseData = new PosMasterBLL();
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
        public IHttpActionResult GetPosList()
        {
            try
            {
                var RequestData = new SelectAllPosMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllPosMasterResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectAllPosMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPOSByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDPosMasterRequest();
                RequestData.ID = ID;
                SelectByIDPosMasterResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectPosMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetPOSBYStore(int StoreID, string len)
        {
            try
            {
                var RequestData = new SelectPosMasterLookUpRequest();
                RequestData.StoreID = StoreID;
                SelectPosMasterLookUpResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectPosMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult PostPos(PosMaster _objPos)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePosMasterRequest();
                RequestData.PosMasterData = new PosMaster();
                RequestData.PosMasterData = _objPos;
                RequestData.PosMasterData.CreateBy = UserID;
                RequestData.PosMasterData.CreateOn = DateTime.Now;
                RequestData.PosMasterData.SCN = 0;
                SavePosMasterResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SavePosMaster(RequestData);
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

        public IHttpActionResult PutPos(PosMaster _objPos)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdatePosMasterRequest();
                RequestData.PosMasterData = new PosMaster();
                RequestData.PosMasterData = _objPos;
                RequestData.PosMasterData.UpdateBy = UserID;
                RequestData.PosMasterData.UpdateOn = DateTime.Now;
                RequestData.PosMasterData.SCN = 0;
                UpdatePosMasterResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.UpdatePosMaster(RequestData);
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
