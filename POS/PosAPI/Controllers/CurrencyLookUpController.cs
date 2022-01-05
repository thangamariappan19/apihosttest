using EasyBizBLL.Masters;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse.Masters.CurrencyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CurrencyLookUpController : ApiController
    {
        //Currency Lookup
        public IHttpActionResult GetCurrencyLookup()
        {
            try
            {
                var RequestData = new SelectCurrencyLookUpRequest();
                SelectCurrencyLookUpResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.SelectCurrencyLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
