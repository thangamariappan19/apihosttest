using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PromotionMappingController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPromotionMappingList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectWNPromotionLookUpRequest();

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



                SelectWNPromotionLookUpResponse response = null;
                var ResponseData = new WNPromotionBLL();
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
        public IHttpActionResult GetWNPromotion()
        {
            try
            {
                SelectWNPromotionLookUpRequest RequestData = new SelectWNPromotionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectWNPromotionLookUpResponse response = null;
                var ResponseData = new WNPromotionBLL();
                response = ResponseData.WNPromotionLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetWNPromotion(string CountryID, int WNPromotionID)
        {
            try
            {
                SelectAllPromotionMappingRequest RequestData = new SelectAllPromotionMappingRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.WNPromotionID = WNPromotionID;
                RequestData.Countries = CountryID;
                SelectAllPromotionMappingResponse response = null;
                var ResponseData = new PromotionMappingBLL();
                response = ResponseData.SelectAllPromotionMapping(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPromotion(PromotionMappingTypes _objPromotion)
        {
            try
            {
                var RequestData = new SavePromotionMappingRequest();
                RequestData.PromotionMappingList = _objPromotion.PromotionMappingList;
                SavePromotionMappingResponse response = null;
                var ResponseData = new PromotionMappingBLL();
                response = ResponseData.SavePromotionMapping(RequestData);
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
