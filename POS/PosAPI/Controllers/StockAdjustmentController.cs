using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Transactions.StockStaging;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class StockAdjustmentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStockAdjustnentList(int StoreID, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new GetAllStockAdjustmentRecordRequest();

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
                GetAllStockAdjustmentRecordResponse response = null;
                var ResponseData = new StockAdjustmentBLL();
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
        public IHttpActionResult GetAllStockAdjustment(int StoreID)
        {
            try
            {
                var RequestData = new GetAllStockAdjustmentRecordRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = StoreID;
                GetAllStockAdjustmentRecordResponse response = null;
                var ResponseData = new StockAdjustmentBLL();
                response = ResponseData.SelectStockAdjustment(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetStockAdjustment(int ID)
        {
            try
            {
                var RequestData = new SelectRecordStockAdjustmentRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ID = ID;
                SelectRecordStockAdjustmentResponse response = null;
                var ResponseData = new StockAdjustmentBLL();
                response = ResponseData.SelectStockAdjustmentRecord(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
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
        public IHttpActionResult PostStockAdjustmentDetails(StockAdjustmentHeader _objStockAdjustment)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveStockAdjustmentRequest();
                RequestData.StockAdjustmentRecord = new StockAdjustmentHeader();
                RequestData.RequestedByUserID = UserID;
                RequestData.StockAdjustmentRecord = _objStockAdjustment;
                RequestData.TransactionLogList = _objStockAdjustment.TransactionLogList;
                SaveStockAdjustmentResponse response = null;
                var ResponseData = new StockAdjustmentBLL();
                response = ResponseData.SaveStockAdjustment(RequestData);
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
