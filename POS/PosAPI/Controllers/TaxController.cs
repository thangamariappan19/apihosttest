using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.TaxMasterResponse;
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
    public class TaxController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTaxList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllTaxRequest();

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



                SelectAllTaxResponse response = null;
                var ResponseData = new TaxBLL();
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
        //public IHttpActionResult GetTaxData()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllTaxRequest();
        //        SelectAllTaxResponse response = null;
        //        var ResponseData = new TaxBLL();
        //        response = ResponseData.SelectAllTaxRecords(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}



        public IHttpActionResult GetTaxDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByTaxIDRequest();
                RequestData.ID = ID;
                SelectByTaxIDResponse response = null;
                var ResponseData = new TaxBLL();
                response = ResponseData.SelectTaxRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        //Select taxcode,names for lookups
        /*public IHttpActionResult GetTaxLookUp()
        {
            try
            {
                var RequestData = new SelectTaxLookUpRequest();
                SelectTaxLookUpResponse response = null;
                var ResponseData = new TaxBLL();
                response = ResponseData.SelectTaxLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }*/



        //Insert or update in same func. if id = 0, insert orelse update
        public IHttpActionResult PostTaxMaster(List<TaxMaster> _objTaxMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveTaxRequest();
                RequestData.Taxlist = new List<TaxMaster>();
                RequestData.Taxlist = _objTaxMaster;
                SaveTaxResponse response = null;
                var ResponseData = new TaxBLL();
                response = ResponseData.SaveTax(RequestData);
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
