using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizResponse.Masters.DivisionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DivisionController : ApiController
    {

        //select all
        public IHttpActionResult GetDivisionData()
        {
            try
            {
                var RequestData = new SelectAllDivisionRequest();
                SelectAllDivisionResponse response = null;
                var ResponseData = new DivisionBLL();
                response = ResponseData.SelectAllDivision(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetDivisionData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllDivisionRequest();

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

                SelectAllDivisionResponse response = null;
                var ResponseData = new DivisionBLL();
                response = ResponseData.API_SelectAllDivision(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetDivisionleDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByDivisionIDRequest();
                RequestData.ID = ID;
                SelectByDivisionIDResponse response = null;
                var ResponseData = new DivisionBLL();
                response = ResponseData.SelectDivisionRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostDivisionMaster(DivisionMaster _objDivisionMaster)
        {
            try
            {
                var RequestData = new SaveDivisionRequest();
                RequestData.DivisionRecord = new DivisionMaster();
                RequestData.DivisionRecord = _objDivisionMaster;
                SaveDivisionResponse response = null;
                var ResponseData = new DivisionBLL();
                response = ResponseData.SaveDivision(RequestData);
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

        public IHttpActionResult PutDivisionMaster(DivisionMaster _objDivisionMaster)
        {
            try
            {
                var RequestData = new UpdateDivisionRequest();
                RequestData.DivisionRecord = new DivisionMaster();
                RequestData.DivisionRecord = _objDivisionMaster;
               UpdateDivisionResponse response = null;
                var ResponseData = new DivisionBLL();
                response = ResponseData.UpdateDivision(RequestData);
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
