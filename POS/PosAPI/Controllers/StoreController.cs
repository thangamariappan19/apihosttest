using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace PosAPI.Controllers
{
    //[Authorize]
    public class StoreController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStoreList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStoreMasterRequest();

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



                SelectAllStoreMasterResponse response = null;
                var ResponseData = new StoreMasterBLL();
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

        //public IHttpActionResult GetAllStoreData()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllStoreMasterRequest();
        //        SelectAllStoreMasterResponse response = null;
        //        var ResponseData = new StoreMasterBLL();
        //        response = ResponseData.SelectAllStoreMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        //Get Store List based on CountryID
        public IHttpActionResult GetStoreDataByCountry(int countryID)
        {
            try
            {
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.CountryID = countryID;
                RequestData.type = "";
                SelectStoreMasterLookUpResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SelectStoreMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetStoreDataList(string ID,string dummy)
        {
            try
            {
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ID = Convert.ToInt32(ID);
                SelectByIDStoreMasterResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SelectedStoreId(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Get Store Data by StoreD ID
        public IHttpActionResult GetStoreDataByID(string ID)
        {
            try
            {
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ID = Convert.ToInt32(ID);            
                SelectByIDStoreMasterResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SelectByIDStoreMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostStoreMaster(StoreMaster _objStoreMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStoreMasterRequest();
                RequestData.StoreMasterRecord = new StoreMaster();
                RequestData.StoreMasterRecord = _objStoreMaster;
                RequestData.StoreBrandMappingList = _objStoreMaster.SelectStoreBrandMappingList;
                RequestData.StoreMasterRecord.CreateBy = UserID;
                SaveStoreMasterResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SaveStoreMaster(RequestData);
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

        public IHttpActionResult PutStoreMaster(StoreMaster _objStoreMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateStoreMasterRequest();
                RequestData.StoreMasterRecord = new StoreMaster();
                RequestData.StoreMasterRecord = _objStoreMaster;
                RequestData.StoreBrandMappingList = _objStoreMaster.SelectStoreBrandMappingList;
                RequestData.StoreMasterRecord.UpdateBy = UserID;
                UpdateStoreMasterResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.UpdateStoreMaster(RequestData);
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