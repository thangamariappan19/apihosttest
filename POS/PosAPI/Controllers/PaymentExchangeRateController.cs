using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizResponse.Masters.ExchangeRatesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PaymentExchangeRateController : ApiController
    {
        public IHttpActionResult GetExchangeRatesList(DateTime exchangeDate)
        {
            try
            {
                var RequestData = new SelectCurrecnyExchangeRatesRequest();
                RequestData.Exchangedate = exchangeDate;
                RequestData.ShowInActiveRecords = true;
                SelectCurrecnyExchangeRatesResponse response = null;
                var ResponseData = new ExchangeRatesBLL();
                response = ResponseData.SelectAllExchangeRatesCurrencyRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
