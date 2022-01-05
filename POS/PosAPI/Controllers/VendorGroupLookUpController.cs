using EasyBizBLL.Masters;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class VendorGroupLookUpController : ApiController
    {
        public IHttpActionResult GetVendorGroupLookup()
        {
            try
            {
                var RequestData = new SelectVendorGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectVendorGroupLookUpResponse response = null;
                var ResponseData = new VendorGroupMasterBLL();
                response = ResponseData.VendorGroupLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
