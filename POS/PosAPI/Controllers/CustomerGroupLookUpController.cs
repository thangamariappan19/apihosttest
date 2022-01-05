using EasyBizBLL.Masters;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizResponse.Masters.CustomerGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CustomerGroupLookUpController : ApiController
    {
        public IHttpActionResult GetCustomerGroup()
        {
            try
            {
                var RequestData = new SelectCustomerGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectCustomerGroupLookUpResponse response = null;
                var ResponseData = new CustomerGroupBLL();
                response = ResponseData.SelectCustomerGroupLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
