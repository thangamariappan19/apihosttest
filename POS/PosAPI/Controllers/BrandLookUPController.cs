using EasyBizBLL.Masters;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BrandLookUPController : ApiController
    {
        public IHttpActionResult GetBrandLookup()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                SelectBrandLookUpResponse response = null;
                var ResponseData = new BrandBLL();
                response = ResponseData.BrandLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
