using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
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
    public class BrandController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetBrandList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllBrandRequest();

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
                SelectAllBrandResponse response = null;
                var ResponseData = new BrandBLL();
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





        public IHttpActionResult GetBrandList()
        {
            try
            {
                var RequestData = new SelectAllBrandRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllBrandResponse response = null;
                var ResponseData = new BrandBLL();
                response = ResponseData.SelectAllBrandRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetBrandID(int ID)
        {
            try
            {
                var RequestData = new SelectByBrandIDRequest();
                RequestData.ID = ID;
                SelectByBrandIDResponse response = null;
                var ResponseData = new BrandBLL();
                response = ResponseData.SelectBrandRecord(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostBrand(BrandMaster _objBrand)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveBrandRequest();
                RequestData.BrandRecord = new BrandMaster();
                RequestData.BrandRecord = _objBrand;
                RequestData.BrandRecord.CreateOn = DateTime.Now;
                RequestData.BrandRecord.CreateBy = UserID;
                RequestData.BrandRecord.SCN = 0;
                SaveBrandResponse response = null;
                var ResponseData = new BrandBLL();
                response = ResponseData.SaveBrand(RequestData);
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

        public IHttpActionResult PutBrand(BrandMaster _objBrand)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateBrandRequest();
                RequestData.BrandRecord = new BrandMaster();
                RequestData.BrandRecord = _objBrand;
                RequestData.BrandRecord.UpdateOn = DateTime.Now;
                RequestData.BrandRecord.UpdateBy = UserID;
                RequestData.BrandRecord.SCN = 0;
                UpdateBrandResponse response = null;
                var ResponseData = new BrandBLL();
                response = ResponseData.UpdateBrand(RequestData);
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
