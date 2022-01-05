using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using EasyBizBLL.Transactions.Stocks;


namespace PosAPI.Controllers
{
    public class StockReturnReportController : ApiController
    {
        public IHttpActionResult GetStockReturnReport(DateTime fromDate, DateTime toDate,string reportselectedvalue)
        {
            try
            {
                var RequestData = new SelectAllStockReturnRequest();
              
                RequestData.FromDate = fromDate;
                RequestData.ToDate = toDate;
                SelectAllStockReturnResponse response = null;
                var ResponseData = new StockReturnBLL();
                string report  = reportselectedvalue;
                if (report == "Stock Return Header")
                {
                    //_StockReturnReportViewPresenter = new StockReturnReportViewPresenter(this);

                    response = ResponseData.GetStockReturnHeaderReport(RequestData);
                }
                else if (report == "Stock Return Details")
                {
                    response = ResponseData.GetStockReturnDetailsReport(RequestData);
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
