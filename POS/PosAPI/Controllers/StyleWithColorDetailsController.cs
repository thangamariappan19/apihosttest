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
    public class StyleWithColorDetailsController : ApiController
    {
        public IHttpActionResult GetStyleWithColorDetails(int styleid,string stylecode)
        {
            try
            {
                var RequestData = new SelectColorDetailsRequest();
                RequestData.ID = styleid;
                RequestData.StyleCode = stylecode;
                SelectColorDetailsResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectStyleWithColorDetailsRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
