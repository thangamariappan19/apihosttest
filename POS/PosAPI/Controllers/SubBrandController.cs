using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SubBrandController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetSubBrandList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllSubBrandRequest();

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



                SelectAllSubBrandResponse response = null;
                var ResponseData = new SubBrandBLL();
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

        //public IHttpActionResult GetSubBrand()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllSubBrandRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllSubBrandResponse response = null;
        //        var ResponseData = new SubBrandBLL();
        //        response = ResponseData.SelectAllSubBrandRecords(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        public IHttpActionResult GetSubBrand(int BrandID)
        {
            try
            {
                var RequestData = new SelectSubBrandListForCategoryRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.BrandID = BrandID;
                SelectSubBrandListForCategoryResponse response = null;
                var ResponseData = new SubBrandBLL();
                response = ResponseData.SubBrandByBrand(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostSubBrand(List<SubBrandMaster> SubBrandList)
        {
            try
            {
                var RequestData = new SaveSubBrandRequest();
                RequestData.SubBrandlist = SubBrandList;
                SaveSubBrandResponse response = null;
                var ResponseData = new SubBrandBLL();
                response = ResponseData.SaveSubBrand(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
