using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizResponse.Masters.PriceListResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PriceListController : ApiController
    {

        //select all
        [Authorize]
        public IHttpActionResult GetPriceListData()
        {
            try
            {
                var RequestData = new SelectAllPriceListRequest();
                SelectAllPriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.SelectAllPriceList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPriceListData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPriceListRequest();

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

                SelectAllPriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.API_SelectAllPriceList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetPriceListDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDPriceListRequest();
                RequestData.ID = ID;
                SelectByIDPriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.SelectByIDPriceList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult GetPriceListData(String sdata )
        {
            try
            {
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectPriceListLookUPRequest();
                var ResponseData = new SelectPriceListLookUPResponse();
                RequestData.Type = "WNPROMOTION";
                ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                return Ok(ResponseData.PriceListTypeData);
                //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                //{
                //    ResponseData.PriceListTypeData;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IHttpActionResult PostPriceListMaster(PriceListType _objPriceListMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePriceListRequest();
                RequestData.PriceListTypeRecords = new PriceListType();
                RequestData.PriceListTypeRecords = _objPriceListMaster;
                RequestData.PriceListTypeRecords.CreateBy = UserID;
                SavePriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.SavePriceList(RequestData);
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

        public IHttpActionResult PutPriceListMaster(PriceListType _objPriceListMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdatePriceListRequest();
                RequestData.PriceListTypeRecords = new PriceListType();
                RequestData.PriceListTypeRecords = _objPriceListMaster;
                RequestData.PriceListTypeRecords.CreateBy = UserID;
                UpdatePriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.UpdatePriceList(RequestData);
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
