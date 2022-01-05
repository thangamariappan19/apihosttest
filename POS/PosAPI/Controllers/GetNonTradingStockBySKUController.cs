using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class GetNonTradingStockBySKUController : ApiController
    {
        public IHttpActionResult GetNonTradingStock(string itemcode,int storeid)
        {
            try
            {
                var RequestData = new GetNonTradingStockBySKURequest();
                RequestData.SKUCode = itemcode;
                RequestData.StoreID = storeid;
                GetNonTradingStockBySKUResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetNonTradingStockBySku(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
