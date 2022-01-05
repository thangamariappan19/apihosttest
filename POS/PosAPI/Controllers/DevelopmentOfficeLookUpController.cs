using EasyBizBLL.Masters;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizResponse.Masters.DesignMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DevelopmentOfficeLookUpController : ApiController
    {
        public IHttpActionResult GetDevelopmentOffice()
        {
            try
            {
                var RequestData = new SelectDesignDevelopmentOfficeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectDesignDevelopmentOfficeLookUpResponse response = null;
                var ResponseData = new DesignMasterBLL();
                response = ResponseData.DevelopmentOfficeLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}