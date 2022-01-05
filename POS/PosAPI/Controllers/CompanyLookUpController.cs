using EasyBizBLL.Masters;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CompanyLookUpController : ApiController
    {
        //Company look up based on country id
        public IHttpActionResult GetCompanyLookup(int CountryID)
        {
            try
            {
                var RequestData = new SelectCompanySettingsLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectCompanySettingsLookUpResponse response = null;
                RequestData.CountryID = CountryID;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.SelectCompanySettingsLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
