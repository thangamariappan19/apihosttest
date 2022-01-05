using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StockController : ApiController
    {
        public IHttpActionResult GetStockByStyleList(string SearchValue, int StoreID, string fromFormName)
        {
            try
            {
                GetStockByStyleCodeRequest RequestData = new GetStockByStyleCodeRequest();
                RequestData.SearchValue = SearchValue;
                RequestData.StoreIDs = Convert.ToString(StoreID);
                RequestData.RequestFrom = Enums.RequestFrom.Search;
                RequestData.StockWiseName = fromFormName;
                GetStockByStyleCodeResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.StyleStockOverView(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
