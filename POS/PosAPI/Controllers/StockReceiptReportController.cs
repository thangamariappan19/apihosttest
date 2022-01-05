using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using EasyBizBLL.Transactions.Stocks;

namespace PosAPI.Controllers
{
    public class StockReceiptReportController : ApiController
    {
        public IHttpActionResult GetStockReturnReport(DateTime fromDate, DateTime toDate, string reportselectedvalue)
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();

                RequestData.FromDate = fromDate;
                RequestData.ToDate = toDate;
                SelectAllStockReceiptResponse response = null;
                var ResponseData = new StockReceiptBLL();
                string report = reportselectedvalue;
                if (report == "Stock Receipt Header")
                {
                    //_StockReturnReportViewPresenter = new StockReturnReportViewPresenter(this);

                    response = ResponseData.GetStockReceiptHeaderReport(RequestData);
                }
                else if (report == "Stock Receipt Details")
                {
                    response = ResponseData.GetStockReceiptDetailsReport(RequestData);
                }
                // response = ResponseData.GetStockReturnHeaderReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
