using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.StateMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StateMasterLookUpController : ApiController
    {
        //Get State based on country id
        public IHttpActionResult GetStateLookupOnCountry(int CountryID)
        {
            try
            {
                SelectStateLookUpRequest RequestData = new SelectStateLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = CountryID;
                SelectStateLookUpResponse response = null;
                var ResponseData = new StateMasterBLL();
                response = ResponseData.SelectStateLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
