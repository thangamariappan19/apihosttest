using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.StockRequest;
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
    public class StockRequestController : ApiController
    {        
        [HttpGet]
        public IHttpActionResult GetStockRequestList(int StoreID, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStockRequestRequest();

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
                SelectAllStockRequestResponse response = null;
                var ResponseData = new StockRequestBLL();
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
        public IHttpActionResult GetStockRequest(int StoreID)
        {
            try
            {
                var RequestData = new SelectAllStockRequestRequest();
                RequestData.StoreID = StoreID;
                RequestData.ShowInActiveRecords = true;
                SelectAllStockRequestResponse response = null;
                var ResponseData = new StockRequestBLL();
                response = ResponseData.SelectAllStockRequest(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetStockRequestbyID(int ID)
        {
            try
            {
                var RequestData = new SelectByStockRequestIDRequest();
                RequestData.ID = ID;
                SelectByStockRequestIDResponse response = null;
                var ResponseData = new StockRequestBLL();
                response = ResponseData.SelectStockRequestRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetAllSKUDetails(string itemcode, string storeid)
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = itemcode;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.StoreIDs = storeid;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectAllSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostStockRequestDetails(StockRequestHeader _objOpeningStock)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStockRequestRequest();
                RequestData.StockRequestHeaderRecord = new StockRequestHeader();
                RequestData.StockRequestHeaderRecord = _objOpeningStock;
                RequestData.StockRequestHeaderRecord.CreateBy = UserID;
                RequestData.StockRequestDetailsList = _objOpeningStock.StockRequestDetailsList;
                //RequestData.TransactionLogList = _objOpeningStock.TransactionLogList;
                SaveStockRequestResponse response = null;
                var ResponseData = new StockRequestBLL();
                response = ResponseData.SaveStockRequest(RequestData);
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
