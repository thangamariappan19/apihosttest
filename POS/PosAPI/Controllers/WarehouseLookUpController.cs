using EasyBizBLL.Masters;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class WarehouseLookUpController : ApiController
    {
        public IHttpActionResult GetWarehouseLookup(int CountryID)
        {
            try
            {
                var RequestData = new SelectWhareHouseLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectWhareouseLookUpResponse response = null;
                RequestData.CountryID = CountryID;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.SelectWhareHouseLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
