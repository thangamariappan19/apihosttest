using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class POSSearchSelectedInvoiceController : ApiController
    {
        // Selected Invoice Search for POS Invoice Search Screen
        public IHttpActionResult GetSelectedPOSSearchInvoice(string SearchValue, string SalesStatus, Enums.RequestFrom requestFrom, int StoreID)
        {
            try
            {
                SelectAllInvoiceRequest RequestData = new SelectAllInvoiceRequest();
                int Status = (int)Enums.InvoiceStatus.Completed;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);

                RequestData.StoreID = StoreID;
                RequestData.SalesStatus = "Completed";
                RequestData.RequestFrom = requestFrom;
                RequestData.SearchString = SearchValue;
                SelectAllInvoiceResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.SelectedPOSSearchInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        public IHttpActionResult GetPOSSearchInvoice(string SearchValue, int StoreID)
        {
            try
            {
                SearchCommonInvoiceRequest RequestData = new SearchCommonInvoiceRequest();
                RequestData.Storeid = StoreID;
                RequestData.SearchString = SearchValue;
                SearchCommonInvoiceResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.GetPOSSearchInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
