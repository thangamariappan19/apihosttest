using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponTransfer;

namespace PosAPI.Controllers
{
    public class CouponReceiptTransactionLogController : ApiController
    {
        public IHttpActionResult PostCouponTransferDetails(SaveCouponTransactionRequest _ICouponTransferView)
        {
            try
            {

                var RequestData = new SaveCouponTransactionRequest();

                RequestData.CouponTransactionList = _ICouponTransferView.CouponTransactionList;
                SaveCouponTransactionResponse response = null;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.SaveCouponTransactionLog(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
