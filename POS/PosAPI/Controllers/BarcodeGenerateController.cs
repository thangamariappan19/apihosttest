using EasyBizBLL.Masters;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BarcodeGenerateController : ApiController
    {
        public IHttpActionResult GetSKUData()
        {
            try
            {
                var RequestData = new SelectBarcodeGenerateBySKURequest();
                SelectBarcodeGenerateBySKUResponse response = null;
                var ResponseData = new BarcodeSettingsBLL();
                response = ResponseData.BarcodeGenerateBySKU(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
