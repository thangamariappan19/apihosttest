using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizResponse.Masters.TailoringMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class TailoringController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTailorinList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllTailoringRequest();

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



                SelectAllTailoringResponse response = null;
                var ResponseData = new TailoringMasterBLL();
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
        public IHttpActionResult GetTailoringData()
        {
            try
            {
                var RequestData = new SelectAllTailoringRequest();
                SelectAllTailoringResponse response = null;
                var ResponseData = new TailoringMasterBLL();
                response = ResponseData.SelectAllTailoringUnit(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select By ID
        public IHttpActionResult GetTailoringeDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByTailoringIDRequest();
                RequestData.ID = ID;
                SelectByTailoringIDResponse response = null;
                var ResponseData = new TailoringMasterBLL();
                response = ResponseData.SelectTailoringUnitRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostTailoringMaster(TailoringMasterTypes _objTailoringMaster)
        {
            try
            {
                var RequestData = new SaveTailoringRequest();
                RequestData.TailoringMasterRecord = new TailoringMasterTypes();
                RequestData.TailoringMasterRecord = _objTailoringMaster;
                SaveTailoringResponse response = null;
                var ResponseData = new TailoringMasterBLL();
                response = ResponseData.SaveTailoringmaster(RequestData);
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
