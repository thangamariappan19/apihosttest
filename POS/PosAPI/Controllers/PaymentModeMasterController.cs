using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PaymentModeMaterRequest;
using EasyBizResponse.Masters.PaymentModeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
namespace PosAPI.Controllers
{
    public class PaymentModeMasterController : ApiController
    {
        public IHttpActionResult GetAllPaymentModeData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPaymentModeMasterRequest();
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
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                SelectAllPaymentModeMasterResponse response = null;
                var ResponseData = new PaymentModeMasterBLL();
                response = ResponseData.SelectAllPaymentModeMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetPaymentModeID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDPaymentModeMasterRequest();
                RequestData.ID = ID;
                SelectByIDPaymentModeMasterResponse response = null;
                var ResponseData = new PaymentModeMasterBLL();
                response = ResponseData.SelectPaymentModeRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetPaymentModeLookUP()
        {
            try
            {
                SelectPaymentModeLooKUpRequest request = new SelectPaymentModeLooKUpRequest();
                SelectPaymentModeLooKUpResponse response = null;
                request.ShowInActiveRecords = false;
                var bll = new PaymentModeMasterBLL();
                response = bll.SelectPaymentModeLookUpRecord(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostPaymentModeDataDetails(PaymentModeTypes _objCustomer)
        {
            try
            {
                /*if (_ICustomerMasterView.ID == 0)
                    SelectDocumentNumberingRecord();*/
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SavePaymentModeMasterRequest();
                RequestData.PaymentModeTypesData = new PaymentModeTypes();
                RequestData.PaymentModeTypesData = _objCustomer;
                RequestData.PaymentModeTypesData.CreateBy = UserID;
                RequestData.PaymentModeTypesData.CreateOn = DateTime.Now;
               
                SavePaymentModeMasterResponse response = null;
                var ResponseData = new PaymentModeMasterBLL();
                response = ResponseData.SavePaymentModeMaster(RequestData);
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

        public IHttpActionResult PutUpdatePaymentModeData(PaymentModeTypes _objCustomer)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdatePaymentModeMasterRequest();
                RequestData.PaymentModeMasterData = new PaymentModeTypes();
                RequestData.PaymentModeMasterData = _objCustomer;
                RequestData.PaymentModeMasterData.CreateBy = UserID;
                RequestData.PaymentModeMasterData.CreateOn = DateTime.Now;
               

                UpdatePaymentModeMasterResponse response = null;
                var ResponseData = new PaymentModeMasterBLL();
                response = ResponseData.UpdatePaymentMode(RequestData);
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
