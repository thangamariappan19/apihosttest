using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse;
using EasyBizBLL.Transactions.Promotions;

namespace PosAPI.Controllers
{
    public class PromotionCriteriaController : ApiController
    {
        public IHttpActionResult GetPromotionCriteriaDetails(string PromotionCode)
        {
            try
            {
                var RequestData = new SelectPromotionCriteriaRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.PromotionCode = PromotionCode;
                //RequestData.StoreIDs = Convert.ToString(StoreID);
                SelectPromotionCriteriaResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectPromotionCriteria(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}