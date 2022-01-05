using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    //[Authorize]
    public class StockReceiptController : ApiController
    {        
        [HttpGet]
        public IHttpActionResult GetStockReceiptList(string StoreCode, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();

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
                RequestData.FromOrToStoreCode = StoreCode;
                SelectAllStockReceiptResponse response = null;
                var ResponseData = new StockReceiptBLL();
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
        public IHttpActionResult GetAllStockReceipt(string storeCode)
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.FromOrToStoreCode = storeCode;
                SelectAllStockReceiptResponse response = null;
                var ResponseData = new StockReceiptBLL();
                response = ResponseData.SelectAllStockReceipt(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetStockReceiptByID(int ID)
        {
            try
            {
                var RequestData = new SelectByStockReceiptIDRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ID = ID;
                SelectByStockReceiptIDResponse response = null;
                var ResponseData = new StockReceiptBLL();
                response = ResponseData.SelectStockReceiptRecord(RequestData);
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
        public IHttpActionResult PostStockReceiptDetails(StockReceiptHeader _objStockReceipt)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStockReceiptRequest();
                RequestData.StockReceiptHeaderRecord = new StockReceiptHeader();
                RequestData.StockReceiptHeaderRecord = _objStockReceipt;
                RequestData.StockReceiptHeaderRecord.CreateBy = UserID;
                RequestData.StockReceiptDetailsList = _objStockReceipt.StockReceiptDetailsList;
                RequestData.TransactionLogList = _objStockReceipt.TransactionLogList;
                RequestData.BinLogList = _objStockReceipt.BinLogList;
                RequestData.RFIDTagList = _objStockReceipt.RFIDList;
                RequestData.StockReceiptHeaderRecord.CreateBy = UserID;
                SaveStockReceiptResponse response = null;
                var ResponseData = new StockReceiptBLL();
                response = ResponseData.SaveStockReceipt(RequestData);
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
