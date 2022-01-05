using EasyBizBLL.Transactions.TransactionLogs;
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
    // New Product search Controller for POS Product Search Screen. 
    public class POSProductSearchController : ApiController
    {
        
        public IHttpActionResult GetPOSProductSearch(string SearchValue, int StoreID,int PriceListID)
        {
            try
            {
                GetProductDescSearchRequest RequestData = new GetProductDescSearchRequest();
                RequestData.SearchString = SearchValue;
                RequestData.Storeid = StoreID;
                RequestData.PriceListID = PriceListID;

                GetProductDescSearchResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetPOSProductDescSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        public IHttpActionResult GetProductSearch(string SearchValue, int StoreID)
        {
            try
            {
                GetProductCommonSearchRequest RequestData = new GetProductCommonSearchRequest();
                RequestData.SearchString = SearchValue;
                RequestData.Storeid = StoreID;


                GetProductCommonSearchResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetProductSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
