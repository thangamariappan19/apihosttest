using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizResponse.Masters.ColorMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ColorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetColorList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllColorRequest();

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



                SelectAllColorResponse response = null;
                var ResponseData = new ColorBLL();
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

        public IHttpActionResult GetColorList()
        {
            try
            {
                var RequestData = new SelectAllColorRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllColorResponse response = null;
                var ResponseData = new ColorBLL();
                response = ResponseData.SelectAllColorRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetColorByID(int ID)
        {
            try
            {
                var RequestData = new SelectByColorIDRequest();
                RequestData.ID = ID;
                SelectByColorIDResponse response = null;
                var ResponseData = new ColorBLL();
                response = ResponseData.SelectColorRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostColor(ColorMaster _objColor)
        {
            try
            {
                var RequestData = new SaveColorRequest();
                RequestData.ColorRecord = new ColorMaster();
                RequestData.ColorRecord = _objColor;
                RequestData.ColorRecord.CreateOn = DateTime.Now;
                RequestData.ColorRecord.SCN = 0;
                SaveColorResponse response = null;
                var ResponseData = new ColorBLL();
                response = ResponseData.SaveColor(RequestData);
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
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutColor(ColorMaster _objColor)
        {
            try
            {
                var RequestData = new UpdateColorRequest();
                RequestData.ColorRecord = new ColorMaster();
                RequestData.ColorRecord = _objColor;
                RequestData.ColorRecord.UpdateOn = DateTime.Now;
                RequestData.ColorRecord.SCN = 0;
                UpdateColorResponse response = null;
                var ResponseData = new ColorBLL();
                response = ResponseData.UpdateColor(RequestData);
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
