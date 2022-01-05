using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class InventoryCountingController : ApiController
    {
        public IHttpActionResult GetInventoryCountingList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                //test
                var RequestData = new GetInventoryCountingInitRequest();
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
                RequestData.ShowInActiveRecords = true;
                GetInventoryCountingInitResponse response = null;
                if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                {
                    RequestData.RequestFrom = 0;
                }
                else
                {
                    RequestData.RequestFrom = EasyBizDBTypes.Common.Enums.RequestFrom.DefaultLoad;

                }
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.API_GetInventoryCountingInitList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetInventoryCountingList()
        {
            try
            {
                var RequestData = new GetInventoryCountingInitRequest();
                RequestData.ShowInActiveRecords = true;
                GetInventoryCountingInitResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetInventoryCountingInitList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetInitializeStock(string documentno)
        {
            try
            {
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.SelectionMode = "DocumentNo";
                RequestData.DocumentNo = documentno;
                GetInventoryCountingInitRecordResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetInventoryCountingInitRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetInventorySummary(string documentno,int i)
        {
            try
            {
                var RequestData = new GetInventoryManualCountRecordRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.DocumentNo = documentno;
                GetInventoryManualCountRecordResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetInventoryManualCountRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetSystemStockByStore(int storeid)
        {
            try
            {
                var RequestData = new GetSystemStockByStoreRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = storeid;
                GetSystemStockByStoreResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetSystemStockByStore(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetInventoryCountingRecord(string id, string view)
        {
            try
            {
                var RequestData = new SelectByInventoryCountingIDRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ID = Convert.ToInt32(id);
                SelectByInventoryCountingIDResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.SelectInventoryCountingRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostSystemStock(SaveSystemStockRequest _objSystemStockRequest)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveSystemStockRequest();
                RequestData.InventoryManualCountRecord = _objSystemStockRequest.InventoryManualCountRecord;
                RequestData.InventoryManualCountRecord.CreateBy = UserID;
                RequestData.RunningNo = _objSystemStockRequest.RunningNo;
                RequestData.DocumentNumberingID = _objSystemStockRequest.DocumentNumberingID;
                SaveSystemStockResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.SaveSystemStock(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
