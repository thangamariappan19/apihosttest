using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using System.Web.Http;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System.Security.Claims;
using System.Net.Http;

namespace PosAPI.Controllers
{
    public class OpeningStockController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetOpeningStockList(int StoreID, string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllOpeningStockRequest();

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
                SelectAllOpeningStockResponse response = null;
                var ResponseData = new OpeningStockBLL();
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
        public IHttpActionResult GetOpeningStock(int StoreID)
        {
            try
            {
                var RequestData = new SelectAllOpeningStockRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = StoreID;
                SelectAllOpeningStockResponse response = null;
                var ResponseData = new OpeningStockBLL();
                response = ResponseData.SelectAllOpeningStock(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetOpeningStockByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDOpeningStockHeaderRequest();
                RequestData.ID = ID;
                SelectByIDOpeningStockHeaderResponse response = null;
                var ResponseData = new OpeningStockBLL();
                response = ResponseData.SelectStockRequestRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetAllSKUDetails(string itemcode,string storeid)
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
        public IHttpActionResult PostOpeningStockDetails(OpeningStockHeader _objOpeningStock)
        {
            try
            {
                var RequestData = new SaveOpeningStockRequest();
                RequestData.OpeningStockHeaderRecord = new OpeningStockHeader();
                RequestData.OpeningStockHeaderRecord = _objOpeningStock;
                RequestData.OpeningStockDetailsList = _objOpeningStock.OpeningStockDetailsList;
                RequestData.TransactionLogList = _objOpeningStock.TransactionLogList;
                SaveOpeningStockResponse response = null;
                var ResponseData = new OpeningStockBLL();
                response = ResponseData.SaveOpeningStock(RequestData);
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
