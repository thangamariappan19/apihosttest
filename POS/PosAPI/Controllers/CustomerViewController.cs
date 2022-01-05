using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CustomerViewController : ApiController
    {
        public IHttpActionResult GetCustomerSalestransaction(int ID)
        {
            try
            {
                var RequestData = new SelectAllCustomerSalesTransactionRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CustomerID = ID;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllCustomerSaleTransactionResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SelectALLCustomerSalesTransaction(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCustomerReturntransaction(string CustomerID)
        {
            try
            {
                var RequestData = new SelectAllCustomerSalesTransactionRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CustomerID = Convert.ToInt32(CustomerID);
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllCustomerSaleTransactionResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SelectALLCustomerReturnTransaction(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCustomerReturnExchange(string CustomerID,string type)
        {
            try
            {
                var RequestData = new SelectAllCustomerSalesTransactionRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CustomerID = Convert.ToInt32(CustomerID);
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllCustomerSaleTransactionResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SelectALLCustomerReturnExchange(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
