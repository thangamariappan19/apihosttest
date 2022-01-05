using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SKUSearchForSalesController : ApiController
    {
        public IHttpActionResult GetSKUSearchForSales(string SKUCode, string storeid, int PriceListID)
        {
            try
            {
                SelectAllSKUMasterRequest request = new SelectAllSKUMasterRequest();
                //request.ShowInActiveRecords = true;
                //request.Count = 1;
                request.SearchString = SKUCode;
                request.PriceListID = PriceListID;
                request.StoreIDs = storeid;
                //request.Mode = "SALES";
                //request.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.GetSKUSearchForSales(request);
                if (response.StatusCode == Enums.OpStatusCode.Success ||
                    response.StatusCode == Enums.OpStatusCode.RecordNotFound)
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
