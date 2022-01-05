using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.ReasonMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ReasonController : ApiController
    {

        //public IHttpActionResult GetReasonLookUp()
        //{
        //    try
        //    {
        //        var RequestData = new SelectReasonMasterLookUpRequest();
        //        SelectReasonMasterLookUpResponse response = null;
        //        var ResponseData = new ReasonMasterBLL();
        //        response = ResponseData.SelectReasonMasterLookUp(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //select all
        public IHttpActionResult GetReasonData()
        {
            try
            {
                var RequestData = new SelectAllReasonMasterRequest();
                SelectAllReasonMasterResponse response = null;
                var ResponseData = new ReasonMasterBLL();
                response = ResponseData.SelectAllReasonMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetReasonData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllReasonMasterRequest();

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

                SelectAllReasonMasterResponse response = null;
                var ResponseData = new ReasonMasterBLL();
                response = ResponseData.API_SelectAllReasonMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select by ID - Edit
        public IHttpActionResult GetReasonDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDReasonMasterRequest();
                RequestData.ID = ID;
                SelectByIDReasonMasterResponse response = null;
                var ResponseData = new ReasonMasterBLL();
                response = ResponseData.SelectReasonMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostReasonMaster(ReasonMaster _objReasonMaster)
        {
            try
            {
                var RequestData = new SaveReasonMasterRequest();
                RequestData.ReasonMasterData = new ReasonMaster();
                RequestData.ReasonMasterData = _objReasonMaster;
                SaveReasonMasterResponse response = null;
                var ResponseData = new ReasonMasterBLL();
                response = ResponseData.SaveReasonMaster(RequestData);
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

        public IHttpActionResult PutReasonMaster(ReasonMaster _objReasonMaster)
        {
            try
            {
                var RequestData = new UpdateReasonMasterRequest();
                RequestData.ReasonMasterData = new ReasonMaster();
                RequestData.ReasonMasterData = _objReasonMaster;
                UpdateReasonMasterResponse response = null;
                var ResponseData = new ReasonMasterBLL();
                response = ResponseData.UpdateReasonMaster(RequestData);
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
