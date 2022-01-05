using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse.Masters.CountryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    [Authorize]
    public class CountryController : ApiController
    {
        public IHttpActionResult GetCountryData()
        {
            try
            {
                var RequestData = new SelectAllCountryRequest();
                SelectAllCountryResponse response = null;
                var ResponseData = new CountryBLL();
                response = ResponseData.SelectAllCountryMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]

        public IHttpActionResult GetCountryList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCountryRequest();

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



                SelectAllCountryResponse response = null;
                var ResponseData = new CountryBLL();
                response = ResponseData.API_SelectAllCountryMaster(RequestData);
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

        //[HttpGet]
        public IHttpActionResult GetCountryData(int ID)
        {
            try
            {
                var RequestData = new SelectByIDCountryRequest();
                RequestData.ID = ID;
                SelectByIDCountryResponse response = null;
                var ResponseData = new CountryBLL();
                response = ResponseData.SelectCountryMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCountryData(CountryMaster _CountryMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCountryRequest();
                var response = new SaveCountryResponse();

                RequestData.CountryMasterData = new CountryMaster();
                RequestData.CountryMasterData.ID = _CountryMaster.ID;
                RequestData.CountryMasterData.CountryCode = _CountryMaster.CountryCode;
                RequestData.CountryMasterData.CountryName = _CountryMaster.CountryName;
                RequestData.CountryMasterData.LanguageName = _CountryMaster.LanguageName;
                RequestData.CountryMasterData.DecimalDigit = _CountryMaster.DecimalDigit;
                RequestData.CountryMasterData.DecimalPlaces = _CountryMaster.DecimalPlaces;
                RequestData.CountryMasterData.DateFormat = _CountryMaster.DateFormat;
                RequestData.CountryMasterData.DateSeparator = _CountryMaster.DateSeparator;
                RequestData.CountryMasterData.NearByRoundOff = _CountryMaster.NearByRoundOff;
                RequestData.CountryMasterData.TaxID = _CountryMaster.TaxID;
                RequestData.CountryMasterData.NegativeSign = _CountryMaster.NegativeSign;
                RequestData.CountryMasterData.CurrencySeparator = _CountryMaster.CurrencySeparator;
                RequestData.CountryMasterData.Currency = _CountryMaster.Currency;
                RequestData.CountryMasterData.CurrencyID = _CountryMaster.CurrencyID;
                RequestData.CountryMasterData.EmailID = _CountryMaster.EmailID;
                RequestData.CountryMasterData.CreditLimitCheck = _CountryMaster.CreditLimitCheck;
                RequestData.CountryMasterData.AllowMultipleTransaction = _CountryMaster.AllowMultipleTransaction;
                RequestData.CountryMasterData.AllowPartialReceiving = _CountryMaster.AllowPartialReceiving;
                RequestData.CountryMasterData.AllowSaleAndRedemption = _CountryMaster.AllowSaleAndRedemption;
                RequestData.CountryMasterData.CurrencyCode = _CountryMaster.CurrencyCode;
                RequestData.CountryMasterData.TaxCode = _CountryMaster.TaxCode;
                RequestData.CountryMasterData.Active = _CountryMaster.Active;
                RequestData.CountryMasterData.OrginCountry = _CountryMaster.OrginCountry;
                RequestData.CountryMasterData.POSTitle = _CountryMaster.POSTitle;
                RequestData.CountryMasterData.PromotionRoundOff = _CountryMaster.PromotionRoundOff;
                RequestData.CountryMasterData.CreateBy = UserID;
                RequestData.CountryMasterData.CreateOn = DateTime.Now;
                RequestData.CountryMasterData.SCN = _CountryMaster.SCN;
                CountryBLL _ResponseData = new CountryBLL();
                response = _ResponseData.SaveCountryMaster(RequestData);

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
                throw ex;
                //return InternalServerError(ex);
            }
        }


        public IHttpActionResult PutCountryData(CountryMaster _CountryMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCountryRequest();
                var response = new UpdateCountryResponse();

                RequestData.CountryMasterData = new CountryMaster();
                RequestData.CountryMasterData.ID = _CountryMaster.ID;
                RequestData.CountryMasterData.CountryCode = _CountryMaster.CountryCode;
                RequestData.CountryMasterData.CountryName = _CountryMaster.CountryName;
                RequestData.CountryMasterData.LanguageName = _CountryMaster.LanguageName;
                RequestData.CountryMasterData.DecimalDigit = _CountryMaster.DecimalDigit;
                RequestData.CountryMasterData.DecimalPlaces = _CountryMaster.DecimalPlaces;
                RequestData.CountryMasterData.DateFormat = _CountryMaster.DateFormat;
                RequestData.CountryMasterData.DateSeparator = _CountryMaster.DateSeparator;
                RequestData.CountryMasterData.NegativeSign = _CountryMaster.NegativeSign;
                RequestData.CountryMasterData.NearByRoundOff = _CountryMaster.NearByRoundOff;
                RequestData.CountryMasterData.TaxID = _CountryMaster.TaxID;
                RequestData.CountryMasterData.CurrencySeparator = _CountryMaster.CurrencySeparator;
                RequestData.CountryMasterData.Currency = _CountryMaster.Currency;
                RequestData.CountryMasterData.CurrencyID = _CountryMaster.CurrencyID;
                RequestData.CountryMasterData.EmailID = _CountryMaster.EmailID;
                RequestData.CountryMasterData.CreditLimitCheck = _CountryMaster.CreditLimitCheck;
                RequestData.CountryMasterData.AllowMultipleTransaction = _CountryMaster.AllowMultipleTransaction;
                RequestData.CountryMasterData.AllowPartialReceiving = _CountryMaster.AllowPartialReceiving;
                RequestData.CountryMasterData.AllowSaleAndRedemption = _CountryMaster.AllowSaleAndRedemption;
                RequestData.CountryMasterData.CurrencyCode = _CountryMaster.CurrencyCode;
                RequestData.CountryMasterData.TaxCode = _CountryMaster.TaxCode;
                RequestData.CountryMasterData.Active = _CountryMaster.Active;
                RequestData.CountryMasterData.OrginCountry = _CountryMaster.OrginCountry;
                RequestData.CountryMasterData.POSTitle = _CountryMaster.POSTitle;
                RequestData.CountryMasterData.PromotionRoundOff = _CountryMaster.PromotionRoundOff;
                RequestData.CountryMasterData.UpdateBy = UserID;
                RequestData.CountryMasterData.UpdateOn = DateTime.Now;
                RequestData.CountryMasterData.SCN = _CountryMaster.SCN;
                CountryBLL _ResponseData = new CountryBLL();
                response = _ResponseData.UpdateCountryMaster(RequestData);

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
