using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Masters;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class PromotionController : ApiController
    {
        public IHttpActionResult GetPromotionList(int StoreID)
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.RequestedProcess = "SalesInvoice";
                RequestData.StoreIDs = Convert.ToString(StoreID);
                SelectAllPromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectAllPromotionsRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPromotionList()
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllPromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectAllPromotionsRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPromotionList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.ShowInActiveRecords = true;

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

                SelectAllPromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.API_SelectAllPromotionsRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPromotionList1(int ID, string qry)
        {
            try
            {
                var RequestData = new SelectByPromotionsIDRequest();
                //RequestData.ShowInActiveRecords = true;
                RequestData.ID = ID;
                SelectByPromotionsIDResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectPromotionsRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPromotionMaster(PromotionsMaster _objPromotionMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SavePromotionsRequest();
                RequestData.PromotionsRecord = new PromotionsMaster();
                RequestData.PromotionsRecord = _objPromotionMaster;
                RequestData.StoreTypeList = _objPromotionMaster.StoreList;
                RequestData.CustomerTypeList = _objPromotionMaster.CustomerList;
                RequestData.ProductTypeList = _objPromotionMaster.ProductTypeList;
                RequestData.BuyItemTypeList = _objPromotionMaster.BuyItemTypeList;
                RequestData.GetItemTypeList = _objPromotionMaster.GetItemTypeList;
                RequestData.PromotionsRecord.CreateBy = UserID;
                SavePromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SavePromotions(RequestData);
                //return Ok(response);
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