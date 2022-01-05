using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizBLL.Common;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizBLL.Transactions.POS;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class SalesReturnController : ApiController
    {
        //Test
        //Invoice Header as Response. (InvoiceDetailsList,PaymentList)
        //Salesreturn Search invoice
        public IHttpActionResult GetSearchInvoiceHeaderDetails(string InvoiceNo, int StoreID, bool ForceSKUSearch)
        {
            try
            {
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.SearchString = InvoiceNo;
                RequestData.StoreID = StoreID;
                RequestData.ForceSKUSearch = ForceSKUSearch;

                //var ResponseData = InvoiceBLL.SelectInvoiceDetailsList(RequestData);
                GetSearchInvoiceHeaderDetailsResponse response = null;
                var ResponseDate = new InvoiceBLL();
                response = ResponseDate.GetSearchInvoiceHeaderDetails(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult PostReturnInvoice(SalesReturnHeader salesReturnHeader)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                salesReturnHeader.CreateBy = UserID;


                var RequestData = new SaveSalesReturnRequest();
                var response = new SaveSalesReturnResponse();
                //SelectDocumentNumberingRecord();
                RequestData.SalesReturnHeaderData = new SalesReturnHeader();
                RequestData.SalesReturnHeaderData = salesReturnHeader;
                RequestData.OnAccountPaymentRecord = salesReturnHeader.OnAccountPaymentRecord;
                RequestData.DocumentNumberingID = 67;
                                
                SalesReturnBLL _ResponseData = new SalesReturnBLL();
                response = _ResponseData.SaveSalesReturn(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}