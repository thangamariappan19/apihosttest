using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizBLL.Transactions.CouponTransfer;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CouponTransactionlogController : ApiController
    {
        public IHttpActionResult PostCouponTransferDetails(SaveCouponTransactionRequest _ICouponTransferView)
        {
            try
            {

                var RequestData = new SaveCouponTransactionRequest();

                RequestData.CouponTransactionList = _ICouponTransferView.CouponTransactionList;
                SaveCouponTransactionResponse response = null;
                var ResponseData = new CouponTransferBLL();
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
