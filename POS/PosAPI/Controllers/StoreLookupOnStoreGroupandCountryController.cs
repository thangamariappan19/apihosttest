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
    public class StoreLookupOnStoreGroupandCountryController : ApiController
    {
        public IHttpActionResult GetStoreLookupOnStoreGroupandCountry(int StoreGroupID,int CountryId)
        {
            try
            {
                SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreGroupID = Convert.ToInt32(StoreGroupID);
                RequestData.CountryID = Convert.ToInt32(CountryId);
                SelectStoreMasterLookUpResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectStoreBasedOnStoreGroupandCountryMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
