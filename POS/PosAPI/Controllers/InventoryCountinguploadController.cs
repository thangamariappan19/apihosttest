using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using EasyBizBLL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class InventoryCountinguploadController : ApiController
    {
        public IHttpActionResult Getskucode(string skucode, int storeid)
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = skucode;
                RequestData.StoreIDs = Convert.ToString(storeid);
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectAllSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetDocumentByDate(string documentNo, string documentDate)
        {
            try
            {
                var RequestData = new GetInventoryCountingInitRecordRequest();
                RequestData.SelectionMode = "Date";
                RequestData.DocumentNo = documentNo;
                RequestData.DocumentDate =Convert.ToDateTime(documentDate);
                GetInventoryCountingInitRecordResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.GetInventoryCountingInitRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostManualStock(SaveManualStockRequest _objSystemStockRequest)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveManualStockRequest();
                RequestData.InventoryManualCountRecord = _objSystemStockRequest.InventoryManualCountRecord;
                RequestData.Status = _objSystemStockRequest.Status;
                RequestData.InventoryManualCountRecord.CreateBy = UserID;
                 SaveManualStockResponse response = null;
                var ResponseData = new InventoryCountingBLL();
                response = ResponseData.SaveManualStock(RequestData);
                return Ok(response);
            }
            catch (Exception ex)

            {
                return InternalServerError(ex);
            }
        }
    }
   
      
}
