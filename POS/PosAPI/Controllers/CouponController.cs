using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Coupens;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizResponse.Masters.CouponMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;


namespace PosAPI.Controllers
{
    public class CouponController : ApiController
    {

        public IHttpActionResult GetCouponList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCouponMasterRequest();
                RequestData.ShowInActiveRecords = true;

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

                SelectAllCouponMasterResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.API_SelectAllCouponRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        public IHttpActionResult PostCountryData(CouponMaster _CouponMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCouponMasterRequest();
              //  var response = new SaveCouponMasterResponse();

                RequestData.CouponMasterData = new CouponMaster();
                RequestData.CouponMasterData.ID = _CouponMaster.ID;
                RequestData.CouponMasterData.CouponCode = _CouponMaster.CouponCode;
                RequestData.CouponMasterData.Coupondescription = _CouponMaster.Coupondescription;
                RequestData.CouponMasterData.BarCode = _CouponMaster.BarCode;
                //RequestData.CouponMasterData.CountryName = _CouponMaster.CountryName;
                RequestData.CouponMasterData.Country = _CouponMaster.Country;
                RequestData.CouponMasterData.CouponType = _CouponMaster.CouponType;
                RequestData.CouponMasterData.StartDate = _CouponMaster.StartDate;
                RequestData.CouponMasterData.EndDate = _CouponMaster.EndDate;
                RequestData.CouponMasterData.DiscountType = _CouponMaster.DiscountType;
                RequestData.CouponMasterData.DiscountValue = _CouponMaster.DiscountValue;
                RequestData.CouponMasterData.IssuableAtPOS = _CouponMaster.IssuableAtPOS;
                RequestData.CouponMasterData.Serial = _CouponMaster.Serial;
                RequestData.CouponMasterData.Remarks = _CouponMaster.Remarks;
                RequestData.CouponMasterData.Active = _CouponMaster.Active;

                RequestData.CouponMasterData.CouponSerialCode = _CouponMaster.CouponSerialCode;
                RequestData.CouponMasterData.PhysicalStore = _CouponMaster.PhysicalStore;
                RequestData.CouponMasterData.Remainingamount = _CouponMaster.Remainingamount;
                RequestData.CouponMasterData.Redeemedstatus = _CouponMaster.Redeemedstatus;

                RequestData.CouponMasterData.Issuedstatus = _CouponMaster.Issuedstatus;
                RequestData.CouponMasterData.CreateBy = UserID;
                RequestData.CouponMasterData.CreateOn = DateTime.Now;
                RequestData.CouponMasterData.SCN = _CouponMaster.SCN;


                //RequestData.StoreTypeList = _CouponMaster.StoreCommonUtilData;
                //RequestData.CustomerTypeList = _CouponMaster.CustomerList;
                //RequestData.ProductTypeList = _CouponMaster.ProductTypeList;
                RequestData.StoreCommonUtilData = _CouponMaster.StoreCommonUtilData;
                RequestData.CustomerCommonUtilData = _CouponMaster.CustomerCommonUtilData;
                RequestData.TotalMasterCommonUtilData = _CouponMaster.TotalMasterCommonUtilData;
                RequestData.CouponDetailsList = _CouponMaster.ObjCouponListDetails;

                //RequestData.StoreTypeList = _CouponMaster.StoreList;
                //RequestData.CustomerTypeList = _CouponMaster.CustomerList;
                //RequestData.ProductTypeList = _CouponMaster.ProductTypeList;
                //RequestData.StoreCommonUtilData = _CouponMaster.StoreList;
                //RequestData.CustomerCommonUtilData = _CouponMaster.CustomerList;
                //RequestData.TotalMasterCommonUtilData = _CouponMaster.ProductTypeList;

                RequestData.CouponMasterData.IsCouponExpirable = _CouponMaster.IsCouponExpirable;
                RequestData.CouponMasterData.MinAmount = _CouponMaster.MinAmount;
                RequestData.CouponMasterData.MaxCouponIssuePerDay = _CouponMaster.MaxCouponIssuePerDay;
                RequestData.CouponMasterData.MaxLimitOfCoupon = _CouponMaster.MaxLimitOfCoupon;
                RequestData.CouponMasterData.RedeemType = _CouponMaster.RedeemType;
                RequestData.CouponMasterData.CouponExpiresInNoOfDays = _CouponMaster.CouponExpiresInNoOfDays;


                SaveCouponMasterResponse response = null;
                var ResponseData = new CouponMasterBLL();
                CouponMasterBLL _ResponseData = new CouponMasterBLL();
                response = _ResponseData.SaveCouponMaster(RequestData);

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
                //return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutCouponMasterData(CouponMaster _CouponMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCouponMasterRequest();
                var response = new UpdateCouponMasterResponse();

                RequestData.CouponMasterData = new CouponMaster();
                RequestData.CouponMasterData.ID = _CouponMaster.ID;
                RequestData.CouponMasterData.CouponCode = _CouponMaster.CouponCode;
                RequestData.CouponMasterData.Coupondescription = _CouponMaster.Coupondescription;
                RequestData.CouponMasterData.BarCode = _CouponMaster.BarCode;
                //RequestData.CouponMasterData.CountryName = _CouponMaster.CountryName;
                RequestData.CouponMasterData.Country = _CouponMaster.Country;
                RequestData.CouponMasterData.CouponType = _CouponMaster.CouponType;
                RequestData.CouponMasterData.StartDate = _CouponMaster.StartDate;
                RequestData.CouponMasterData.EndDate = _CouponMaster.EndDate;
                RequestData.CouponMasterData.DiscountType = _CouponMaster.DiscountType;
                RequestData.CouponMasterData.DiscountValue = _CouponMaster.DiscountValue;
                RequestData.CouponMasterData.IssuableAtPOS = _CouponMaster.IssuableAtPOS;
                RequestData.CouponMasterData.Serial = _CouponMaster.Serial;
                RequestData.CouponMasterData.Remarks = _CouponMaster.Remarks;
                RequestData.CouponMasterData.Active = _CouponMaster.Active;

                RequestData.CouponMasterData.CouponSerialCode = _CouponMaster.CouponSerialCode;
                RequestData.CouponMasterData.PhysicalStore = _CouponMaster.PhysicalStore;
                RequestData.CouponMasterData.Remainingamount = _CouponMaster.Remainingamount;
                RequestData.CouponMasterData.Redeemedstatus = _CouponMaster.Redeemedstatus;

                RequestData.CouponMasterData.Issuedstatus = _CouponMaster.Issuedstatus;
                RequestData.CouponMasterData.CreateBy = UserID;
                RequestData.CouponMasterData.CreateOn = DateTime.Now;
                RequestData.CouponMasterData.SCN = _CouponMaster.SCN;


                //RequestData.StoreTypeList = _CouponMaster.StoreCommonUtilData;
                //RequestData.CustomerTypeList = _CouponMaster.CustomerList;
                //RequestData.ProductTypeList = _CouponMaster.ProductTypeList;
                RequestData.StoreCommonUtilData = _CouponMaster.StoreCommonUtilData;
                RequestData.CustomerCommonUtilData = _CouponMaster.CustomerCommonUtilData;
                RequestData.TotalMasterCommonUtilData = _CouponMaster.TotalMasterCommonUtilData;
                RequestData.CouponDetailsList = _CouponMaster.ObjCouponListDetails;

                //RequestData.StoreTypeList = _CouponMaster.StoreList;
                //RequestData.CustomerTypeList = _CouponMaster.CustomerList;
                //RequestData.ProductTypeList = _CouponMaster.ProductTypeList;
                //RequestData.StoreCommonUtilData = _CouponMaster.StoreList;
                //RequestData.CustomerCommonUtilData = _CouponMaster.CustomerList;
                //RequestData.TotalMasterCommonUtilData = _CouponMaster.ProductTypeList;

                //RequestData.CouponMasterData.IsExpirable = _CouponMaster.IsExpirable;
                //RequestData.CouponMasterData.MinBillAmount = _CouponMaster.MinBillAmount;
                //RequestData.CouponMasterData.MaxNoIssue = _CouponMaster.MaxNoIssue;
                //RequestData.CouponMasterData.MaxLimit = _CouponMaster.MaxLimit;
                RequestData.CouponMasterData.RedeemType = _CouponMaster.RedeemType;
                CouponMasterBLL _ResponseData = new CouponMasterBLL();
                response = _ResponseData.UpdateCouponMaster(RequestData);

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

        public IHttpActionResult GetCouponList1(int ID)
        {
            try
            {
                var RequestData = new SelectByIDCouponMasterRequest();
                //RequestData.ShowInActiveRecords = true;
                RequestData.ID = ID;
                SelectByIDCouponMasterResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.SelectCouponMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult GetCouponList()
        {
            try
            {
                var RequestData = new SelectAllCouponMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllCouponMasterResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.SelectAllCouponMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
