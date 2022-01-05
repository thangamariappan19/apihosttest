using EasyBizBLL.Transactions.PriceChange;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.PriceChange;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PriceChangeController : ApiController
    {
        List<PriceChangeDetails> ValidatingPriceChangeDetailsList = new List<PriceChangeDetails>();

        [HttpGet]
        public IHttpActionResult GetPriceChangeList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPriceChangeRequest();

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



                SelectAllPriceChangeResponse response = null;
                var ResponseData = new PriceChangeBLL();
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
        public IHttpActionResult GetPriceChangeList()
        {
            try
            {
                SelectAllPriceChangeRequest request = new SelectAllPriceChangeRequest();
                SelectAllPriceChangeResponse response = null;
                request.ShowInActiveRecords = true;
                var bll = new PriceChangeBLL();
                response = bll.GetAllPriceChange(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPriceChangeList(int ID)
        {
            try
            {
                SelectByIDPriceChangeRequest request = new SelectByIDPriceChangeRequest();
                SelectByIDPriceChangeResponse response = null;
                request.ID = ID;
                var bll = new PriceChangeBLL();
                response = bll.GetPriceChangeRecord(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutValidate(List<PriceChangeDetails> ValidatingPriceChangeDetailsList)
        {
            try
            {
                ValidatePriceChangeRequest request = new ValidatePriceChangeRequest();
                ValidatePriceChangeResponse response = null;
                request.ValidatingPriceChangeDetailsList = ValidatingPriceChangeDetailsList;
                request.SourceCountryID = ValidatingPriceChangeDetailsList[0].BaseCurrencyID;
                request.PriceChangeType = ValidatingPriceChangeDetailsList[0].PriceType;
                var bll = new PriceChangeBLL();
                response = bll.ValidatePriceChangeDetails(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPos(PriceChange _objPriceChange)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePriceChangeRequest();
                RequestData.PriceChangeRecord = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
                RequestData.PriceChangeRecord = _objPriceChange;
                RequestData.PriceChangeDetailsList = _objPriceChange.PriceChangeDetailsList;
                RequestData.PriceChangeCountriesList = _objPriceChange.PriceChangeCountriesList;
                RequestData.PriceChangeRecord.CreateBy = UserID;
                SavePriceChangeResponse response = null;
                var ResponseData = new PriceChangeBLL();
                response = ResponseData.SavePriceChange(RequestData);
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
