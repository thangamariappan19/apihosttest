using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockReturn;
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
    public class StockReturnController : ApiController
    {        
        [HttpGet]
        public IHttpActionResult GetStockReturnList(int StoreID, string StoreCode, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStockReturnRequest();

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
                SelectAllStockReturnResponse response = null;
                var ResponseData = new StockReturnBLL();
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
        public IHttpActionResult GetAllStockReturn(int storeID,string storeCode)
        {
            try
            {
                var RequestData = new SelectAllStockReturnRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID= storeID;
                RequestData.StoreCode = storeCode;
                //RequestData.RequestFrom = Enums.RequestFrom.MainServer;
                SelectAllStockReturnResponse response = null;
                var ResponseData = new StockReturnBLL();
                response = ResponseData.SelectAllStockReturn(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetAllStockReturnbyID(int ID)
        {
            try
            {
                var RequestData = new SelectByStockReturnIDRequest();
                RequestData.ID = ID;
                SelectByStockReturnIDResponse response = null;
                var ResponseData = new StockReturnBLL();
                response = ResponseData.SelectStockReturnRecord(RequestData);
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
        public IHttpActionResult PostStockRetuenDetails(StockReturnHeader _objStockReturn)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStockReturnRequest();
                RequestData.StockReturnHeaderRecord = new StockReturnHeader();
                RequestData.StockReturnHeaderRecord = _objStockReturn;
                RequestData.StockReturnHeaderRecord.CreateBy = UserID;
                RequestData.StockReturnDetailsList = _objStockReturn.StockReturnDetailsList;
                RequestData.TransactionLogList = _objStockReturn.TransactionLogList;
                SaveStockReturnResponse response = null;
                var ResponseData = new StockReturnBLL();
                response = ResponseData.SaveStockReturn(RequestData);
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
