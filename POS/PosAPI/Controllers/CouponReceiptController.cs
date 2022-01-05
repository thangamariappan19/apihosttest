using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using EasyBizDBTypes.Transactions.Coupons;

namespace PosAPI.Controllers
{
    public class CouponReceiptController : ApiController
    {
        public IHttpActionResult GetCouponReceiptData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCouponReceiptRequest();

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

                SelectAllCouponReceiptResponse response = null;
                RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.API_SelectAllCouponReceipt(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCouponReceiptSerialNumbers(string FromSerialNum, string ToSerialNum, string CouponCode)
        {
            try
            {
                var RequestData = new GetSerialNumberRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CouponCode = CouponCode;
                RequestData.FromSerialNum = FromSerialNum;
                RequestData.ToSerialNum = ToSerialNum;
                GetSerialNumberResponse response = null;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.API_SelectCouponSerialNum(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostCouponReceiptDetails(SaveCouponReceiptRequest _IcouponReceiptHeader)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCouponReceiptRequest();
                RequestData.CouponReceiptHeaderRecord = new CouponReceiptHeader();

                RequestData.CouponReceiptDetailsList = _IcouponReceiptHeader.CouponReceiptDetailsList;
               
                RequestData.CouponReceiptHeaderRecord.CouponID = _IcouponReceiptHeader.CouponReceiptHeaderRecord.CouponID;
                RequestData.CouponReceiptHeaderRecord.CouponCode = _IcouponReceiptHeader.CouponReceiptHeaderRecord.CouponCode;
                RequestData.CouponReceiptHeaderRecord.Active = _IcouponReceiptHeader.CouponReceiptHeaderRecord.Active;
                RequestData.CouponReceiptHeaderRecord.CurrentLocation = _IcouponReceiptHeader.CouponReceiptHeaderRecord.CurrentLocation;
                RequestData.CouponReceiptHeaderRecord.ID = _IcouponReceiptHeader.CouponReceiptHeaderRecord.ID;
                RequestData.CouponReceiptHeaderRecord.CreateBy = UserID;
                RequestData.CouponReceiptHeaderRecord.CreateOn = DateTime.Now;
                RequestData.CouponReceiptHeaderRecord.SCN = 0;

                SaveCouponReceiptResponse response = null;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.SaveCouponReceipt(RequestData);
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
        public IHttpActionResult GetCouponReceiptHeaderData1(string ID)
        {
            try
            {
                var RequestData = new SelectByIDCouponReceiptRequest();
                RequestData.ID = Convert.ToInt32(ID);
                SelectByIDCouponReceiptResponse response = null;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.SelectCouponReceiptRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult SelectCouponReceiptDetails(string ID,string details)
        {
            try
            {
                var RequestData = new SelectByCouponReceiptDetailsRequest();
                RequestData.ID = Convert.ToInt32(ID);
                SelectByCouponReceiptDetailsResponse response = null;
                var ResponseData = new CouponreceiptBLL();
                response = ResponseData.SelectCouponReceiptDetails(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
   
}
