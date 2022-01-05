using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SalesExchangeController : ApiController
    {

        public IHttpActionResult PostExchangeInvoice(SalesExchangeHeader SalesExchangeHeader)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                SalesExchangeHeader.CreateBy = UserID;


                var RequestData = new SaveSalesExchangeRequest();
                var response = new SaveSalesExchangeResponse();
                RequestData.SalesExchangeHeaderRecord = new SalesExchangeHeader();
                RequestData.SalesExchangeHeaderRecord = SalesExchangeHeader;
                RequestData.DocumentNumberingID = 68;
                RequestData.RequestFrom = Enums.RequestFrom.SyncService;
           

                //var ReturnList = new List<SalesExchangeDetail>();
                //ReturnList = SalesExchangeHeader.ReturnExchangeDetailList.Where(x => x.ExchangeQty > 0 && x.IsExchange == true).ToList();
                //int TotalExchangeQty = SalesExchangeHeader.ReturnExchangeDetailList.Sum(x => x.Qty);

                //RequestData.SalesExchangeDetailList = SalesExchangeHeader.ReturnExchangeDetailList; // Newly exchanged items
                //RequestData.SalesExchangeDetailList = SalesExchangeHeader.ReturnExchangeDetailList; // Newly exchanged items
                //RequestData.ReturnList = ReturnList;//For Updatiing Invoice Details
  

                SalesExchangeBLL _ResponseData = new SalesExchangeBLL();
                response = _ResponseData.SaveSalesExchange(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public IHttpActionResult PostExchangeInvoice(SalesExchangeHeader SalesExchangeHeader)
        //{
        //    try
        //    {
        //        var RequestData = new SaveSalesExchangeRequest();
        //        var response = new SaveSalesExchangeResponse();
        //        //SelectDocumentNumberingRecord();
        //        RequestData.SalesExchangeHeaderRecord = new SalesExchangeHeader();

        //        var ReturnList = new List<SalesExchangeDetail>();
        //        ReturnList = SalesExchangeHeader.ReturnExchangeDetailList.Where(x => x.ExchangeQty > 0 && x.IsExchange == true).ToList();
        //        int TotalExchangeQty = SalesExchangeHeader.ReturnExchangeDetailList.Sum(x => x.Qty);

        //        RequestData.SalesExchangeDetailList = SalesExchangeHeader.ReturnExchangeDetailList; // Newly exchanged items
        //        RequestData.ReturnList = ReturnList;//For Updatiing Invoice Details


        //        RequestData.SalesExchangeHeaderRecord.DocumentNo = SalesExchangeHeader.DocumentNo;
        //        RequestData.SalesExchangeHeaderRecord.DocumentDate = SalesExchangeHeader.DocumentDate;
        //        RequestData.SalesExchangeHeaderRecord.SalesInvoiceNumber = SalesExchangeHeader.SalesInvoiceNumber;
        //        RequestData.SalesExchangeHeaderRecord.InvoiceHeaderID = SalesExchangeHeader.InvoiceHeaderID;

        //        RequestData.SalesExchangeHeaderRecord.CountryID = SalesExchangeHeader.CountryID;
        //        RequestData.SalesExchangeHeaderRecord.StoreID = SalesExchangeHeader.StoreID;
        //        RequestData.SalesExchangeHeaderRecord.PosID = SalesExchangeHeader.PosID;
        //        RequestData.SalesExchangeHeaderRecord.ExchangeWithOutInvoiceNo = SalesExchangeHeader.ExchangeWithOutInvoiceNo;

        //        RequestData.SalesExchangeHeaderRecord.CreateBy = SalesExchangeHeader.CreateBy;
        //        RequestData.SalesExchangeHeaderRecord.CashierID = SalesExchangeHeader.CashierID;
        //        RequestData.SalesExchangeHeaderRecord.CreditSales = SalesExchangeHeader.CreditSales;

        //        RequestData.SalesExchangeHeaderRecord.TotalExchangeQty = TotalExchangeQty;

        //        RequestData.SalesExchangeHeaderRecord.ExchangeMode = SalesExchangeHeader.ExchangeMode;
        //        RequestData.BaseIntegrateStoreID = SalesExchangeHeader.StoreID;
        //        RequestData.TransactionLogList = SalesExchangeHeader.TransactionLogList;
        //        RequestData.SalesExchangeHeaderRecord.CountryCode = SalesExchangeHeader.CountryCode;
        //        RequestData.SalesExchangeHeaderRecord.StoreCode = SalesExchangeHeader.StoreCode;
        //        RequestData.SalesExchangeHeaderRecord.POSCode = SalesExchangeHeader.POSCode;

        //        RequestData.RunningNo = SalesExchangeHeader.RunningNo;
        //        RequestData.DocumentNumberingID = SalesExchangeHeader.DetailID;

        //        SalesExchangeBLL _ResponseData = new SalesExchangeBLL();
        //        response = _ResponseData.SaveSalesExchange(RequestData);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        public IHttpActionResult GetExchangeItemDetails(string InvoiceNo, int StoreID, bool ForceSKUSearch)
        {
            try
            {
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.SearchString = InvoiceNo;
                RequestData.StoreID = StoreID;
                RequestData.ForceSKUSearch = ForceSKUSearch;               
                GetSearchInvoiceHeaderDetailsResponse response = null;
                var ResponseDate = new InvoiceBLL();
                response = ResponseDate.GetExchangeItemDetails(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        public IHttpActionResult GetSalesOrExchangeList(string SearchString)
        {
            try
            {
                var RequestData = new SelectAllInvoiceRequest();
                RequestData.SearchString = SearchString;
                var ResponseData = new GetExchangeOrSalesResponse();
                GetExchangeOrSalesResponse response = null;
                var ResponseDate = new SalesExchangeBLL();
                response = ResponseDate.GetSalesOrExchangeList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //string GetSKUMode = string.Empty;
        //public IHttpActionResult GetAllInvoiceList(string SearchCriteria, string SearchString)
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllInvoiceRequest();
        //        RequestData.SearchCriteria = SearchCriteria; // SearchCriteria = "InvoiceNo"
        //        RequestData.SearchString = SearchString;
        //        var ResponseData = new SelectAllInvoiceResponse();
        //        SelectAllInvoiceResponse response = null;
        //        var ResponseDate = new InvoiceBLL();
        //        response = ResponseDate.SelectAllInvoice(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //public IHttpActionResult GetSKUList( string SearchString)
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllSKUMasterRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        RequestData.RequestFrom = Enums.RequestFrom.Search;
        //        RequestData.Mode = "SALES";
        //        RequestData.SearchString = SearchString;
        //        string ExchangeMode = "WithOutInvoice";
        //        GetSKUMode = "ReturnSearch";
        //        var ResponseData = new SelectAllSKUMasterResponse();
        //        SelectAllSKUMasterResponse response = null;
        //        var ResponseDate = new SKUMasterBLL();
        //        response = ResponseDate.SelectAllSKUMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}




        //public IHttpActionResult GetExchangeInvoiceDetails(string InvoiceNo, string ExchangeMode)
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllSalesExchangeDetailRequest();
        //        RequestData.Mode = ExchangeMode;
        //        RequestData.InvoiceNo = InvoiceNo;
        //       var ResponseData = new SelectAllSalesExchangeDetailResponse();
        //        SelectAllSalesExchangeDetailResponse response = null;
        //        var ResponseDate = new SalesExchangeBLL();
        //      response = ResponseDate.SelectAllSalesExchangeDetailList(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
