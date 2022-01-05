using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.StyleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StyleMasterLookUpController : ApiController
    {
        public IHttpActionResult GetStyleMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStyleLookUpRequest();
                SelectStyleLookUpResponse response = null;
                RequestData.ShowInActiveRecords = false;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectStyleLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
