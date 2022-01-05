using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizResponse.Masters.SeasonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SeasonController : ApiController
    {

        //select all
        public IHttpActionResult GetSeasonData()
        {
            try
            {
                var RequestData = new SelectAllSeasonRequest();
                SelectAllSeasonResponse response = null;
                var ResponseData = new SeasonBLL();
                response = ResponseData.SelectAllSeasonMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetSeasonData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllSeasonRequest();

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

                SelectAllSeasonResponse response = null;
                var ResponseData = new SeasonBLL();
                response = ResponseData.API_SelectAllSeasonMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetSeasonDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectBySeasonIDRequest();
                RequestData.ID = ID;
                SelectBySeasonIDResponse response = null;
                var ResponseData = new SeasonBLL();
                response = ResponseData.SelectSeasonMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostSeasonMaster(SeasonMaster _objSeasonMaster)
        {
            try
            {
                var RequestData = new SaveSeasonRequest();
                RequestData.SeasonRecord = new SeasonMaster();
                RequestData.SeasonRecord = _objSeasonMaster;
                SaveSeasonResponse response = null;
                var ResponseData = new SeasonBLL();
                response = ResponseData.SaveSeasonMaster(RequestData);
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

        public IHttpActionResult PutSeasonMaster(SeasonMaster _objSeasonMaster)
        {
            try
            {
                var RequestData = new UpdateSeasonRequest();
                RequestData.SeasonMasterData = new SeasonMaster();
                RequestData.SeasonMasterData = _objSeasonMaster;
                UpdateSeasonResponse response = null;
                var ResponseData = new SeasonBLL();
                response = ResponseData.UpdateSeasonMaster(RequestData);
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
