using EasyBizBLL.FCPasses;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.FCPasses;
using EasyBizRequest.FCPasses;
using EasyBizResponse.FCPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace PosAPI.Controllers
{
    //[System.Web.Http.Authorize]
    public class PassesTransactionsController : ApiController
    {
        // GET: PassesTransactions
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetPassesTransactionList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new PassesTransactionRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";

                PassesTransactionResponse response = null;
                var ResponseData = new PassesTransactionBLL();
                response = ResponseData.API_SelectALL(RequestData);
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
        public IHttpActionResult GetPassesTransactionByID(int ID)
        {
            try
            {
                var RequestData = new SelectPassesTransactionRequest();
                RequestData.ID = ID;
                SelectPassesTransactionResponse response = null;
                var ResponseData = new PassesTransactionBLL();
                response = ResponseData.SelectPassesTransactionDetails(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
                //return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPassesTransactionsMaster(PassesTransaction _ObjPassesTransaction)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new PassesTransactionRequest();
                RequestData.PassesTransactionHeaderData = new PassesTransaction();
                RequestData.PassesTransactionHeaderData = _ObjPassesTransaction;
                RequestData.PassesTransactionHeaderData.CreateBy = UserID;
                RequestData.PassesTransactionDetailsList = _ObjPassesTransaction.PassesTransactionDetailsList;
                PassesTransactionResponse response = null;
                var ResponseData = new PassesTransactionBLL();
                response = ResponseData.SavePassesTransaction(RequestData);
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