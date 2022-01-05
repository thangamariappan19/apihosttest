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
    public class SearchInvoiceController : ApiController
    {
        
        public IHttpActionResult GetSearchInvoice(string SearchValue, string SalesStatus, Enums.RequestFrom requestFrom, int StoreID)
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
                response = ResponseData.SelectAllInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        #region GetSearchBillCompletedInvoice

        public IHttpActionResult GetSearchBillCompletedInvoice(string InvoiceNo)
        {
            try
            {
                SelectAllInvoiceRequest RequestData = new SelectAllInvoiceRequest();
                int Status = (int)Enums.InvoiceStatus.Completed;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);

               // RequestData.StoreID = StoreID;
                RequestData.SalesStatus = "BillCompleted";
                // RequestData.RequestFrom = requestFrom;
                //RequestData.SearchString = SearchValue;
                RequestData.InvoiceNo = InvoiceNo;
                SelectAllInvoiceResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.SelectBillCompletedSalesInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        #endregion

    }
}
