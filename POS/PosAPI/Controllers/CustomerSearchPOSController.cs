using EasyBizBLL.Masters;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CustomerSearchPOSController : ApiController
    {

        public IHttpActionResult GetCustomerData(string CustSearchString)
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.SearchString = CustSearchString;
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.GetCustomerSearchPOS(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
