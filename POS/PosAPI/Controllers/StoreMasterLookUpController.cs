using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StoreMasterLookUpController : ApiController
    {
        public IHttpActionResult GetStoreLookupOnCountry(int CountryID)
        {
            try
            {
                SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = CountryID;
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
        public IHttpActionResult GetStoreLookupOnStoreIDandCode(int StoreID,string StoreCode)
        {
            try
            {
                SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreID = StoreID;
                RequestData.StoreCode = StoreCode;
                SelectStoreMasterLookUpResponse response = null;
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SelectStorename(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
