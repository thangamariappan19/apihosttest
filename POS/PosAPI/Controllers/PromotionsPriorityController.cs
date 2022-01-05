using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionPriority;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionPriority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PromotionsPriorityController : ApiController
    {
        public IHttpActionResult GetPromotionsPriorityList()
        {
            try
            {
                //var RequestData = new SelectAllPromotionsRequest();
                //RequestData.ShowInActiveRecords = true;
                //SelectAllPromotionsResponse response = null;
                //var ResponseData = new PromotionsMasterBLL();
                //response = ResponseData.SelectAllPromotionsRecords(RequestData);
                //return Ok(response);

                var RequestData = new SelectAllPromotionsRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllPromotionsResponse response = null;
                var ResponseData = new PromotionsMasterBLL();
                response = ResponseData.SelectPromotionWithPriorityRecords(RequestData);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostPromotionsPriority(List<PromotionPriorityType> _objPromotionsPriority)
        {
            try
            {
                var RequestData = new SavePromotionPriorityRequest();
                RequestData.PromotionPriorityTypeData = _objPromotionsPriority;
                SavePromotionPriorityResponse response = null;
                var ResponseData = new PromotionPriorityBLL();
                response = ResponseData.SavePromotionPriority(RequestData);
                //return Ok(response);
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
