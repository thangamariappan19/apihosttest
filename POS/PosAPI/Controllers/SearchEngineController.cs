using EasyBizBLL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizRequest.Masters.SearchEngineRequest;
using EasyBizResponse.Masters.SearchEngineResponse;

namespace PosAPI.Controllers
{
    public class SearchEngineController : ApiController
    {
        public IHttpActionResult GetCustomerSKUData(string CustSearchString)
        {
            try
            {
                var RequestData = new CustomerSkuRequest();
                RequestData.SearchString = CustSearchString;
                CustomersSkuResponse response = null;
                var ResponseData = new SearchEngineBLL();
                response = ResponseData.GetCustomerSKUSearchPOS(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCustomerData(string CustomerSearchString, int i)
        {
            try
            {
                var RequestData = new SearchCustomerRequest();
                RequestData.SearchString = CustomerSearchString;
                SearchCustomerResponse response = null;
                var ResponseData = new SearchEngineBLL();
                response = ResponseData.GetCustomerSearchPOS(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult GetSaleReturnSearchData(string SalesReturnSearchString, int StoreID, int SaleReturn)
        {
            try
            {
                var RequestData = new SearchSalesReturnExchangeRequest();
                RequestData.SearchString = SalesReturnSearchString;
                RequestData.StoreID = StoreID;

                SearchSalesReturnExchangeResponse response = null;
                var ResponseData = new SearchEngineBLL();
                response = ResponseData.GetSaleReturnSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetExchangeSearchData(string ExchangeSearchString, int StoreID, string Exchange)
        {
            try
            {
                var RequestData = new SearchExchangeRequest();
                RequestData.SearchString = ExchangeSearchString;
                RequestData.StoreID = StoreID;
                RequestData.IsActive = Exchange;

                SearchExchangeResponse response = null;
                var ResponseData = new SearchEngineBLL();
                response = ResponseData.GetExchangeSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



    }
}
