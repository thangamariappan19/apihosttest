using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ProductSearchController : ApiController
    {

        public IHttpActionResult GetProductSearch(string SearchValue,int StoreID)
        {
            try
            {
                GetProductDescSearchRequest RequestData = new GetProductDescSearchRequest();
                RequestData.SearchString = SearchValue;
                RequestData.Storeid = StoreID;              
               
                GetProductDescSearchResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetProductDescSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
