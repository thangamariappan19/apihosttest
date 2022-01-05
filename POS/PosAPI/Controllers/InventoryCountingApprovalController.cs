using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class InventoryCountingApprovalController : ApiController
    {
        public IHttpActionResult InventoryFinalize(InventoryFinalizeRequest _IInventoryCountingApproveView)
        {
            try
            {
                var _InventoryCountingBLL = new InventoryCountingBLL();
                var RequestData = new InventoryFinalizeRequest();
                InventoryFinalizeResponse response = null;
                RequestData.DocumentNo = _IInventoryCountingApproveView.DocumentNo;
                RequestData.Status = _IInventoryCountingApproveView.Status;
                RequestData.RARemarks = _IInventoryCountingApproveView.RARemarks;
                RequestData.RequestedByUserID = _IInventoryCountingApproveView.RequestedByUserID;
                RequestData.TransactionLogList = _IInventoryCountingApproveView.TransactionLogList;
                response = _InventoryCountingBLL.InventoryFinalize(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
