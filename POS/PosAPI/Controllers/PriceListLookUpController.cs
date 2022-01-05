using EasyBizBLL.Masters;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizResponse.Masters.PriceListResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PriceListLookUpController : ApiController
    {
        public IHttpActionResult GetPriceListLookUp()
        {
            try
            {
                var RequestData = new SelectPriceListLookUPRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Type = "Type";
                SelectPriceListLookUPResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.PriceListLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

