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
    public class FindStockController : ApiController
    {

        public IHttpActionResult GetFindStock(string SearchValue,int CountryID)
        {
            try
            {
                FindStockRequest RequestData = new FindStockRequest();
                RequestData.SearchString = SearchValue;
                RequestData.RequestFrom = Enums.RequestFrom.Search;
                RequestData.CountryID = CountryID;
                FindStockByCountryResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetFindStockByCountry(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
