using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
using EasyBizResponse.Masters.AgentMasterResponse;
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
    public class PriceTypeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPriceTypeList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPriceTypeRequest();

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



                SelectAllPriceTypeResponse response = null;
                var ResponseData = new PriceTypeBLL();
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
        //public IHttpActionResult GetPriceTypeData()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllPriceTypeRequest();
        //        SelectAllPriceTypeResponse response = null;
        //        var ResponseData = new PriceTypeBLL();
        //        response = ResponseData.SelectAllPriceType(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        //Select by ID - Edit
        public IHttpActionResult GetPriceTypeDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDPriceTypeRequest();
                RequestData.ID = ID;
                SelectByIDPriceTypeResponse response = null;
                var ResponseData = new PriceTypeBLL();
                response = ResponseData.SelectByIDPriceType(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPriceTypeMaster(PriceTypeMasterTypes _objPriceTypeMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePriceTypeRequest();
                RequestData.PriceTypesRecord = new PriceTypeMasterTypes();
                RequestData.PriceTypesRecord = _objPriceTypeMaster;
                RequestData.PriceTypesRecord.CreateBy = UserID;
                SavePriceTypeResponse response = null;
                var ResponseData = new PriceTypeBLL();
                response = ResponseData.SavePriceType(RequestData);
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

        public IHttpActionResult PutPriceTypeMaster(PriceTypeMasterTypes _objPriceTypeMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdatePriceTypeRequest();
                RequestData.PriceTypeData = new PriceTypeMasterTypes();
                RequestData.PriceTypeData = _objPriceTypeMaster;
                RequestData.PriceTypeData.UpdateBy = UserID;
                UpdatePriceTypeResponse response = null;
                var ResponseData = new PriceTypeBLL();
                response = ResponseData.UpdatePriceType(RequestData);
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
