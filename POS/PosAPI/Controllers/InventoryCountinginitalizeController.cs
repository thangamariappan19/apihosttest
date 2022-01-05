using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class InventoryCountinginitalizeController : ApiController
    {
        public IHttpActionResult GetSystemStockByStore(int storeid)
        {
            try
            {
                var RequestData = new GetSystemStockByStoreRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = storeid;
                GetSystemStockByStoreResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetSystemStockByStoreCount(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetInventoryCountingList(string limit, string offset, int storeid)
        {
            try
            {
                var RequestData = new GetSystemStockByStoreRequest();
                //int lmt = 0, ofset = 0;
                //int.TryParse(limit, out lmt);
                //int.TryParse(offset, out ofset);
                //lmt = lmt <= 0 ? 10 : lmt;
                //ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = limit;
                RequestData.Offset = offset;
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = storeid;
                RequestData.ShowInActiveRecords = true;
                GetSystemStockByStoreResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetSystemStockByStoreLimit(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
