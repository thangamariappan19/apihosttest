using EasyBizBLL.Masters;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class EmployeeMasterLookUpController : ApiController
    {
        public IHttpActionResult GetEmployeeMasterLookUp()
        {
            try
            {
                var RequestData = new SelectEmployeeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectEmployeeLookUpResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.SelectEmployeeLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

