using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StoreBasedCountryIDController : ApiController
    {
        public IHttpActionResult GetSelectStore(int CountryID)
        {
            try
            {
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = CountryID;
                var response = new SelectStoreMasterLookUpResponse();
                var ResponseData = new StoreMasterBLL();
                response = ResponseData.SelectStoreMasterLookUp(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Sub Brand
        public IHttpActionResult GetSubBrandList()
        {
            try
            {
                var RequestData = new SelectSubBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                //RequestData.CountryID = CountryID;
                var response = new SelectSubBrandLookUpResponse();
                var ResponseData = new SubBrandBLL();
                response = ResponseData.SubBrandLookUp(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
