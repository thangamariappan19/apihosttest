using EasyBizBLL.Transactions.Pricing;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PricePointController : ApiController
    {

        //select all
        public IHttpActionResult GetPricePointData()
        {
            try
            {
                var RequestData = new SelectAllPricePointRequest();
                SelectAllPricePointResponse response = null;
                RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                var ResponseData = new PricePointBLL();
                response = ResponseData.GetPricePointList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPricePointData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPricePointRequest();

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

                SelectAllPricePointResponse response = null;
                RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                var ResponseData = new PricePointBLL();
                response = ResponseData.API_GetPricePointList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetPricePointByID(string PricePointCode)
        {
            try
            {
                var RequestData = new SelectPricePointByIDRequest();
                RequestData.PricePointCode = PricePointCode;
                SelectPricePointByIDResponse response = null;
                var ResponseData = new PricePointBLL();
                response = ResponseData.GetPricePointRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Insert and Update
        public IHttpActionResult PostRoleMaster(PricePoint _objPricePointMaster)
        {
            try
            {
                var RequestData = new SavePricePointRequest();
                //RequestData.PricePointList = new List<PricePoint>();
                RequestData.PricePointCode = _objPricePointMaster.PricePointCode;
                RequestData.PricePointName = _objPricePointMaster.PricePointName;
                if (_objPricePointMaster.ID == 0)
                    RequestData.Mode = 1;
                else
                    RequestData.Mode = 2;
                RequestData.PricePointList = _objPricePointMaster.PricePointList;
                RequestData.PricePointRange = _objPricePointMaster.PricePointRangeList;
                SavePricePointResponse response = null;
                var ResponseData = new PricePointBLL();
                response = ResponseData.SavePricePointList(RequestData);
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
