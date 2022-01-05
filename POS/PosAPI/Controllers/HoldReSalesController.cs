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
    public class HoldReSalesController : ApiController
    {
        public IHttpActionResult GetHoldSalesInvoice(DateTime Businessdate)
        {
            try
            {
                var RequestData = new SelectAllInvoiceRequest();

                int Status = (int)Enums.InvoiceStatus.ParkSale;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                RequestData.SalesStatus = TypeName;
                RequestData.BusinessDate = Businessdate;
                SelectAllInvoiceResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.SelectHoldSalesInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        //Before Optimize - Old One
        //public IHttpActionResult GetHoldSalesInvoice(DateTime Businessdate)
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllInvoiceRequest();

        //        int Status = (int)Enums.InvoiceStatus.ParkSale;
        //        string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
        //        RequestData.SalesStatus = TypeName;
        //        RequestData.BusinessDate = Businessdate;
        //        SelectAllInvoiceResponse response = null;
        //        var ResponseData = new InvoiceBLL();
        //        response = ResponseData.SelectAllInvoice(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }

        //}


        public IHttpActionResult GetRecallSales(int ID)
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.InvoiceHeaderID = ID;
                RequestData.SalesStatus = "ParkSale";
                SelectInvoiceDetailsListResponse response = null;
                var ResponseData = new InvoiceBLL();
                response = ResponseData.GetSelectedRecallInvoice(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        // Before Optimization - Old One
        //public IHttpActionResult GetRecallSales(int ID)
        //{
        //    try
        //    {
        //        var _InvoiceBLL = new InvoiceBLL();
        //        var RequestData = new SelectInvoiceDetailsListRequest();
        //        RequestData.InvoiceHeaderID = ID;
        //        RequestData.SalesStatus = "ParkSale";
        //        SelectInvoiceDetailsListResponse response = null;
        //        var ResponseData = new InvoiceBLL();
        //        response = ResponseData.SelectInvoiceDetailsList(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
