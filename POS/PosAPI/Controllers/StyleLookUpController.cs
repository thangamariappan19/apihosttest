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
    public class StyleLookUpController : ApiController
    {
        public IHttpActionResult GetStyleList()
        {
            try
            {
                var RequestData = new SelectStyleLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectStyleLookUpResponse response = null;
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
