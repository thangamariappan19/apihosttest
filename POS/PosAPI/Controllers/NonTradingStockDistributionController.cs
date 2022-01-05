using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class NonTradingStockDistributionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetNonTradingStockDistributionList(int StoreID, string StoreCode, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectALLNonTradingStockRequest();

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


                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = StoreID;
                RequestData.StoreCode = StoreCode;
                SelectALLNonTradingStockResponse response = null;
                var ResponseData = new NonTradingItemStockBLL();
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
        public IHttpActionResult GetNonTradingStockDistributionList(int StoreID,string StoreCode)
        {
            try
            {
                var RequestData = new SelectALLNonTradingStockRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = StoreID;
                RequestData.StoreCode = StoreCode;
                SelectALLNonTradingStockResponse response = null;
                var ResponseData = new NonTradingItemStockBLL();
                response = ResponseData.SelectALLNonTradingStock(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetSelectNonTradingStockHeader(int ID, string DocumentNo)
        {
            try
            {
                var RequestData = new SelectByNonTradingHeaderIDRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ID = ID;
                RequestData.DocumentNo = DocumentNo;
                SelectByNonTradingStockIDResponse response = null;
                var ResponseData = new NonTradingItemStockBLL();
                response = ResponseData.SelectNonTradingHeaderRecord(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //public IHttpActionResult GetSelectNonTradingStockDetails(int ID, string DocumentNo)
        //{
        //    try
        //    {
        //        var RequestData = new SelectByNonTraddingDetailsIDRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        RequestData.ID = ID;
        //        RequestData.RefDocumentNo = DocumentNo;
        //        SelectByNonTradingStockIDResponse response = null;
        //        var ResponseData = new NonTradingItemStockBLL();
        //        response = ResponseData.SelectNonTradingStockDetails(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        public IHttpActionResult PostNonTradingStockDetails(NonTradingStockHeaderTypes _objNonTradingStock)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveNonTradingItemRequest();
                RequestData.NonTradingItemRecord = new NonTradingStockHeaderTypes();
                
                RequestData.RunningNo = _objNonTradingStock.RunningNo;
                RequestData.DocumentNumberingID = _objNonTradingStock.DocumentNumberingID;
                RequestData.NonTradingItemRecord = _objNonTradingStock;
                RequestData.NonTradingItemRecord.CreateBy = UserID;
                RequestData.NonTradingStockDetailsList = _objNonTradingStock.NonTradingStockDetailsList;
                RequestData.TransactionLogList = _objNonTradingStock.TransactionLogList;
                SaveNonTradingItemResponse response = null;
                var ResponseData = new NonTradingItemStockBLL();
                response = ResponseData.SaveNonTradingItemStock(RequestData);
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
