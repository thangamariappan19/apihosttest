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
    public class ExchangeRatesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetExchangeRatesList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllExchangeRatesRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllExchangeRatesResponse response = null;
                var ResponseData = new ExchangeRatesBLL();
                response = ResponseData.API_SelectALL(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public IHttpActionResult GetExchangeRatesList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllExchangeRatesRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllExchangeRatesResponse response = null;
        //        var ResponseData = new ExchangeRatesBLL();
        //        response = ResponseData.SelectAllExchangeRatesRecords(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetExchangeRatesByID(string ExchangeCode)
        {
            try
            {
                var RequestData = new SelectByIDExchangeRatesRequest();
                RequestData.ExchangeRatesCode = ExchangeCode;
                SelectByIDExchangeRatesResponse response = null;
                var ResponseData = new ExchangeRatesBLL();
                response = ResponseData.SelectExchangeRatesRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostExchangeRate(ExchangeRates _objexchange)
        {
            try
            {
                var RequestData = new SaveExchangeRatesRequest();
                RequestData.IDs = _objexchange.ID;
                RequestData.ExchangeRatesDate = _objexchange.ExchangeRateDate;
                RequestData.ExchangeRateslist = _objexchange.ExchangeRateslist;
                SaveExchangeRatesResponse response = null;
                var ResponseData = new ExchangeRatesBLL();
                response = ResponseData.SaveExchangeRates(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
