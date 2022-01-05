using EasyBizBLL.Masters;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StyleWithScaleDetailsController : ApiController
    {
        public IHttpActionResult GetStyleWithScaleDetails(int styleid, string stylecode)
        {
            try
            {
                var RequestData = new SelectStyleWithScaleforStockRequest();
                RequestData.ID = styleid;
                RequestData.StyleCode = stylecode;
                SelectStyleWithScaleforStockResponse response = null;
                var ResponseData = new StyleMasterBLL();
                response = ResponseData.SelectStyleWithScaleRecordForStock(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
