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
    //Controller for POS - Search Invoice Default Search. Top 20
    public class POSSearchAllInvoiceController : ApiController
    {
        //Function for POS Invoice Search - Default Search
        public IHttpActionResult GetALLPOSSearchInvoice(string SalesStatus, Enums.RequestFrom requestFrom, int StoreID)
        {
            try
            {
                SelectAllInvoiceRequest RequestData = new SelectAllInvoiceRequest();
                int Status = (int)Enums.InvoiceStatus.Completed;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);

                RequestData.StoreID = StoreID;
                RequestData.SalesStatus = "Completed";
                RequestData.RequestFrom = requestFrom;
                //RequestData.SearchString = SearchValue;
                SelectAllInvoiceResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.SelectPOSSearchAllInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
