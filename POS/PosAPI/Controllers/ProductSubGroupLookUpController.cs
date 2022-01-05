using EasyBizBLL.Masters;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ProductSubGroupLookUpController : ApiController
    {
        public IHttpActionResult GetProductGroupList()
        {
            try
            {
                var RequestData = new SelectProductSubGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectProductSubGroupLookUpResponse response = null;
                var ResponseData = new ProductSubGroupBLL();
                response = ResponseData.ProductSubGroupLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
