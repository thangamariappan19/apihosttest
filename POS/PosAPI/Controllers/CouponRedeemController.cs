using EasyBizBLL.Transactions.Coupens;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest.Transactions.Coupons;
using EasyBizResponse.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CouponRedeemController : ApiController
    {
        public IHttpActionResult GetCouponData(string CouponCode)
        {
            try
            {
                var RequestData = new SelectCouponDataOnCouponCodeRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CouponCode = CouponCode;
                SelectCouponDataOnCouponCodeResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.SelectCouponDataOnCouponCode(RequestData);

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
                throw ex;
            }
        }

        public IHttpActionResult GetDeActiveCouponOnReturn(string CouponCode, bool ReturnUpdate)
        {
            try
            {
                var RequestData = new SelectCouponDataOnCouponCodeRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CouponCode = CouponCode;
                RequestData.ReturnUpdate = ReturnUpdate;
                UpdateCouponDetailsListResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.GetDeActiveCouponOnReturn(RequestData);

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
                throw ex;
            }
        }

        public IHttpActionResult GetUpdateCoupon(int CouponID, String CouponCode, string RedeemAmount, int LineNo, string CouponHeaderCode)
        {
            try
            {
                var RequestData = new UpdateCouponDetailsListRequest();
                RequestData.CouponListDetailsReq = new CouponListDetails();
                RequestData.CouponListDetailsReq.CouponHeaderCode = CouponHeaderCode;
                RequestData.CouponListDetailsReq.CouponListHeaderID = CouponID;
                RequestData.CouponListDetailsReq.CouponSerialCode = CouponCode;
                RequestData.CouponListDetailsReq.RemainingAmount = RedeemAmount;
                RequestData.CouponListDetailsReq.RedeemedStatus = "Fully Redeemed";
                RequestData.CouponListDetailsReq.LineNo = LineNo;
                UpdateCouponDetailsListResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.UpdateCouponListDetails(RequestData);

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
                throw ex;
            }
        }

        public IHttpActionResult GetInsertCouponToEnt(int CouponID, String CouponCode, string PhysicalStore, DateTime ExpiredDate, string CouponHeaderCode)
        {
            try
            {
                var RequestData = new UpdateCouponDetailsListRequest();
                RequestData.CouponListDetailsReq = new CouponListDetails();
                RequestData.CouponListDetailsReq.CouponHeaderCode = CouponHeaderCode;
                RequestData.CouponListDetailsReq.CouponListHeaderID = CouponID;
                RequestData.CouponListDetailsReq.CouponSerialCode = CouponCode;
                RequestData.CouponListDetailsReq.IssuedStatus = "YES";
                RequestData.CouponListDetailsReq.PhysicalStore = PhysicalStore;
                RequestData.CouponListDetailsReq.RemainingAmount = "0";
                RequestData.CouponListDetailsReq.RedeemedStatus = "Not";
                RequestData.CouponListDetailsReq.LineNo = 1;
                RequestData.CouponListDetailsReq.RedeemCount = 1;
                RequestData.CouponListDetailsReq.ExpiredDate = ExpiredDate;
                //RequestData.CouponListDetailsReq.CouponSerialCode = CouponHeaderCode;
                UpdateCouponDetailsListResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.InsertCouponListDetails(RequestData);

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
                throw ex;
            }
        }

        public IHttpActionResult PutCouponListData(CouponListDetails _CouponListDetails)
        {
            try
            {

                var RequestData = new UpdateCouponDetailsListRequest();
                RequestData.CouponListDetailsReq = new CouponListDetails();
                RequestData.CouponListDetailsReq = _CouponListDetails;
                UpdateCouponDetailsListResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.UpdateCouponListDetails(RequestData);
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
                throw ex;
            }
        }
    }
}
