using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class OnAccountPaymentController : ApiController
    {
        private string _StoreIDs;

        public IHttpActionResult GetOnAccountDetails(string Mode,string SearchString,int i)
        {
            try
            {
                var RequestData = new GetOnAccountPaymentDetailsRequest();
                GetOnAccountPaymentDetailsResponse response = null;
                RequestData.Mode = Mode;
                RequestData.SearchString = SearchString;
                var ResponseDate = new OnAccountPaymentBLL();
                response = ResponseDate.GetOnAccountDetails(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetAllOnAccountPayment(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllOnAccountPaymentRequest();
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
                SelectAllOnAccountPaymentResponse response = null;
                if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                {
                    RequestData.RequestFrom = 0;
                }
                else
                {
                    RequestData.RequestFrom = Enums.RequestFrom.DefaultLoad;

                }
                //RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                var ResponseDate = new OnAccountPaymentBLL();
                response = ResponseDate.API_GetALLOnAccountPaymentRecort(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetPendingPayments(string type, string SearchString)
        {
            try
            {
                var RequestData = new GetOnAccountPaymentPendingRequest();
                GetOnAccountPaymentPendingResponse response = null;
                RequestData.Type = type;
                RequestData.SearchString = SearchString;
                var ResponseDate = new OnAccountPaymentBLL();
                response = ResponseDate.GetPendingPayments(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetByIDOnAccountPayment(int ID)
        {
            try
            {
                var RequestData = new SelectOnAccountPaymentRequest();
                SelectOnAccountPaymentResponse response = null;
                RequestData.ID = ID;
                var ResponseDate = new OnAccountPaymentBLL();
                response = ResponseDate.GetOnAccountPaymentRecord(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostOnAccountDetails(SaveOnAccountPaymentRequest _IOnAccountPayment)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveOnAccountPaymentRequest();

                if (_IOnAccountPayment.OnAccountPaymentRecord != null && _IOnAccountPayment.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList != null)
                {
                    var StoreWiseGroupList = _IOnAccountPayment.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList.GroupBy(x => x.StoreID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    if (StoreWiseGroupList != null && StoreWiseGroupList.Count > 0)
                    {
                        foreach (List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList in StoreWiseGroupList)
                        {
                            _StoreIDs = _StoreIDs + "," + OnAcInvoiceWisePaymentList.FirstOrDefault().StoreID;
                        }
                    }
                    RequestData.StoreIDs = _StoreIDs.TrimStart(',');
                }
                RequestData.RequestFrom = _IOnAccountPayment.RequestFrom;
                RequestData.OnAccountPaymentRecord = _IOnAccountPayment.OnAccountPaymentRecord;
                RequestData.OnAccountPaymentRecord.CreateBy = UserID;
                SaveOnAccountPaymentResponse response = null;
                var ResponseData = new OnAccountPaymentBLL();
                response = ResponseData.SaveOnAccountPayment(RequestData);
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
