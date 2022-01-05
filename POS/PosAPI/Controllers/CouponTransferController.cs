using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizBLL.Transactions.CouponTransfer;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CouponTransferController : ApiController
    {
        public IHttpActionResult GetCouponTransferData()
        {
            try
            {
                var RequestData = new SelectAllCouponTransferRequest();
                SelectAllCouponTransferResponse response = null;
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new CouponTransferBLL();
                response = ResponseData.SelectAllCouponTransfer(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCouponTransferData1(string ID)
        {
            try
            {
                var RequestData = new SelectByIDCouponTransferRequest();
                RequestData.ID =Convert.ToInt32(ID);
                SelectByIDCouponTransferResponse response = null;
                var ResponseData = new CouponTransferBLL();
                response = ResponseData.SelectCouponTransferRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCouponTransferData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCouponTransferRequest();

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

                SelectAllCouponTransferResponse response = null;
                RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                var ResponseData = new CouponTransferBLL();
                response = ResponseData.API_SelectAllCouponTransfer(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetSerialNumber(string  CouponCode,string FromSerialNum,string ToSerialNum)
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
                response = ResponseData.SelectCouponSerialNum(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostCouponTransferDetails(SaveCouponTransferRequest _ICouponTransferView)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCouponTransferRequest();
                RequestData.CouponTransferRecord = new CouponTransferMaster();
          
                RequestData.CouponReceiptDetailsList = _ICouponTransferView.CouponReceiptDetailsList;
                RequestData.CouponTransferDetailsList = _ICouponTransferView.CouponTransferDetailsList;
                RequestData.CouponTransferRecord.ID = _ICouponTransferView.CouponTransferRecord.ID;
                RequestData.CouponTransferRecord.CouponID = _ICouponTransferView.CouponTransferRecord.CouponID;
                RequestData.CouponTransferRecord.CouponCode = _ICouponTransferView.CouponTransferRecord.CouponCode;
                RequestData.CouponTransferRecord.FromCountryID = _ICouponTransferView.CouponTransferRecord.FromCountryID;
                RequestData.CouponTransferRecord.FromCountryCode = _ICouponTransferView.CouponTransferRecord.FromCountryCode;
                RequestData.CouponTransferRecord.ToStoreID = _ICouponTransferView.CouponTransferRecord.ToStoreID;
                RequestData.CouponTransferRecord.ToStoreCode = _ICouponTransferView.CouponTransferRecord.ToStoreCode;
                RequestData.CouponTransferRecord.FromSerialNum = _ICouponTransferView.CouponTransferRecord.FromSerialNum;
                RequestData.CouponTransferRecord.ToSerialNum = _ICouponTransferView.CouponTransferRecord.ToSerialNum;
                RequestData.CouponTransferRecord.Active = _ICouponTransferView.CouponTransferRecord.Active;
                RequestData.CouponTransferRecord.Fromloaction = _ICouponTransferView.CouponTransferRecord.Fromloaction;
                //RequestData.CouponTransferRecord.CouponTransferDetailsList = _ICouponTransferView.CouponTransferRecord.CouponTransferDetailsList;

                RequestData.CouponTransferRecord.CreateBy = UserID;
                RequestData.CouponTransferRecord.CreateOn = DateTime.Now;
                RequestData.CouponTransferRecord.SCN = 0;

                SaveCouponTransferResponse response = null;
                var ResponseData = new CouponTransferBLL();
                response = ResponseData.SaveCouponTransfer(RequestData);
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
