using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StorePromotionController : ApiController
    {
        public IHttpActionResult GetStorePromotionList(int StoreID)
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.RequestedProcess = "SalesInvoice";
                //RequestData.StoreIDs = Convert.ToString(StoreID);
                RequestData.StoreID = StoreID;
                SelectAllPromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectAllStorePromotions(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
