using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizResponse.Masters.FranchiseResponse;
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
    public class FranchiseController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetFranchiseList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllFranchiseMasterRequest();

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



                SelectAllfranchiseResponse response = null;
                var ResponseData = new FranchiseBLL();
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

        //public IHttpActionResult GetFranchiseData()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllFranchiseMasterRequest();
        //        SelectAllfranchiseResponse response = null;
        //        var ResponseData = new FranchiseBLL();
        //        response = ResponseData.SelectAllFranchise(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        //Select by ID - Edit
        public IHttpActionResult GetFranchiseDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDFranchiseRequest();
                RequestData.ID = ID;
                SelectByIDFranchiseResponse response = null;
                var ResponseData = new FranchiseBLL();
                response = ResponseData.SelectFranchiseRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostFranchiseMaster(FranchiseType _objFranchiseMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveFranchiseMasterRequest();
                RequestData.FranchiseTypeData = new FranchiseType();
                RequestData.FranchiseTypeData = _objFranchiseMaster;
                RequestData.FranchiseTypeData.CreateBy = UserID;
                saveFranchiseResponse response = null;
                var ResponseData = new FranchiseBLL();
                response = ResponseData.SaveFranchise(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutFranchiseMaster(FranchiseType _objFranchiseMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateFranchiseMasterRequest();
                RequestData.FranchiseTypeRecord = new FranchiseType();
                RequestData.FranchiseTypeRecord = _objFranchiseMaster;
                RequestData.FranchiseTypeRecord.UpdateBy = UserID;
                UpdateFranchiseResponse response = null;
                var ResponseData = new FranchiseBLL();
                response = ResponseData.UpdateFranchise(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
