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
    public class GetEmployeeByStoreController : ApiController
    {
        public IHttpActionResult GetEmployeeBYStoreID(int storeID)
        {
            try
            {
                var RequestData = new GetEmployeeByStoreRequest();
                RequestData.StoreID = storeID;
                SelectEmployeeLookUpResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.GetEmployeeByStore(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
