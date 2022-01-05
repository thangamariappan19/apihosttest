using EasyBizBLL.Transactions.Cardex.CardexLocation;
using EasyBizRequest.Transactions.Cardex.CardexLocationRequest;
using EasyBizResponse.Transactions.Cardex.CardexLocationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StyleLedgerController : ApiController
    {

        //select all
        public IHttpActionResult GetStyleLedgerData(string Searchstring,int StoreID,string FromDate, string ToDate)
        {
            try
            {
                var RequestData = new SelectAllCardexLocationRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = EasyBizDBTypes.Common.Enums.RequestFrom.MainServer;
                RequestData.SearchString = Searchstring;
                RequestData.StoreID = StoreID;
                RequestData.FromDate = Convert.ToDateTime(FromDate);
                RequestData.ToDate = Convert.ToDateTime(ToDate);
                SelectAllCardexLocationResponse response = null;
                var ResponseData = new CardexLocationBLL();
                response = ResponseData.SelectAllCardexLocation(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
