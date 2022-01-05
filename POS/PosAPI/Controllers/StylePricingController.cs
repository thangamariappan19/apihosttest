using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Modules
{
    public class StylePricingController : ApiController
    {
        public IHttpActionResult GetSKU(string SKUCode)
        {
            try
            {
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.SKUCode = SKUCode;
                GetStylePricingBySKUCodeResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectGetStylePricingBySKUCode(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}