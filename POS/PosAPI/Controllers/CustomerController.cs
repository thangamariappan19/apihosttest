using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CustomerController : ApiController
    {
        //[Authorize]
        public IHttpActionResult GetCustomerData(string CustSearchString)
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.SearchString = CustSearchString;
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.GetCommonCustomerData(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetAllCustomerData()
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.SearchString = "";
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SelectAllCustomerMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetAllCustomerData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
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
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.API_SelectAllCustomerMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCustomerByID(int ID)
        {
            try
            { 
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Source = "Sales";
                RequestData.ID = ID;
                RequestData.CustomerInfo = "";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SelectAllCustomerMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCustomerDetails(CustomerMaster _objCustomer)
        {
            try
            {
                /*if (_ICustomerMasterView.ID == 0)
                    SelectDocumentNumberingRecord();*/
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCustomerMasterRequest();
                RequestData.CustomerMasterData = new CustomerMaster();
                RequestData.CustomerMasterData = _objCustomer;
                /*RequestData.CustomerMasterData.ID = _objCustomer.ID;
                RequestData.CustomerMasterData.CustomerCode = _objCustomer.CustomerCode;
                RequestData.CustomerMasterData.CustomerName = _objCustomer.CustomerName;
                RequestData.CustomerMasterData.PhoneNumber = _objCustomer.PhoneNumber;
                RequestData.CustomerMasterData.AlterPhoneNumber = _objCustomer.AlterPhoneNumber;
                RequestData.CustomerMasterData.CustomerGroupID = _objCustomer.CustomerGroupID;
                RequestData.CustomerMasterData.BuildingAndBlockNo = _objCustomer.BuildingAndBlockNo;
                RequestData.CustomerMasterData.StreetName = _objCustomer.StreetName;
                RequestData.CustomerMasterData.AreaName1 = _objCustomer.AreaName1;
                RequestData.CustomerMasterData.AreaName2 = _objCustomer.AreaName2;
                RequestData.CustomerMasterData.City = _objCustomer.City;
                RequestData.CustomerMasterData.StateID = _objCustomer.StateID;
                RequestData.CustomerMasterData.CountryID = _objCustomer.CountryID;
                RequestData.CustomerMasterData.Email = _objCustomer.Email;
                RequestData.CustomerMasterData.DOB = _objCustomer.DOB;
                RequestData.CustomerMasterData.Gender = _objCustomer.Gender;*/
                RequestData.CustomerMasterData.CreateBy = UserID;
                RequestData.CustomerMasterData.CreateOn = DateTime.Now;
                /*RequestData.CustomerMasterData.SCN = _objCustomer.SCN;
                RequestData.CustomerMasterData.CreditAmount = _objCustomer.CreditAmount;
                RequestData.CustomerMasterData.Remarks = _objCustomer.Remarks;
                RequestData.CustomerMasterData.Active = _objCustomer.Active;
                RequestData.CustomerMasterData.OnAccountApplicable = _objCustomer.OnAccountApplicable;*/
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                /*RequestData.CustomerMasterData.StoreID = _objCustomer.StoreID;
                RequestData.CustomerMasterData.CustomerGroupCode = _objCustomer.CustomerGroupCode;
                RequestData.CustomerMasterData.StateCode = _objCustomer.StateCode;
                RequestData.CustomerMasterData.CountryCode = _objCustomer.CountryCode;
                RequestData.RunningNo = _objCustomer.DocumentTypeID;
                RequestData.DocumentNumberingID = _objCustomer.DocumentNumberingID;
                RequestData.CustomerMasterData.CustomerImage = _objCustomer.CustomerImage;
                RequestData.CustomerMasterData.StateName = _objCustomer.StateName;
                RequestData.CustomerMasterData.Pincode = _objCustomer.Pincode;
                RequestData.CustomerMasterData.ShippingAddress1 = _objCustomer.ShippingAddress1;
                RequestData.CustomerMasterData.ShippingAddress2 = _objCustomer.ShippingAddress2;
                RequestData.CustomerMasterData.ShippingPhoneNumber = _objCustomer.ShippingPhoneNumber;
                RequestData.CustomerMasterData.ShippingStateID = _objCustomer.ShippingStateID;
                RequestData.CustomerMasterData.ShippingStateCode = _objCustomer.ShippingStateCode;
                RequestData.CustomerMasterData.ShippingStateName = _objCustomer.ShippingStateName;
                RequestData.CustomerMasterData.ShippingCity = _objCustomer.ShippingCity;
                RequestData.CustomerMasterData.ShippingPincode = _objCustomer.ShippingPincode;*/
                RequestData.CustomerMasterData.IsoCode = _objCustomer.IsoCode;
                SaveCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.SaveCustomerMaster(RequestData);
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

        public IHttpActionResult PutUpdateCustomer(CustomerMaster _objCustomer)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCustomerMasterRequest();
                RequestData.CustomerMasterData = new CustomerMaster();

                RequestData.CustomerMasterData = _objCustomer;
                /*RequestData.CustomerMasterData.ID = _objCustomer.ID;
                RequestData.CustomerMasterData.CustomerCode = _objCustomer.CustomerCode;
                RequestData.CustomerMasterData.CustomerName = _objCustomer.CustomerName;
                RequestData.CustomerMasterData.PhoneNumber = _objCustomer.PhoneNumber;
                RequestData.CustomerMasterData.AlterPhoneNumber = _objCustomer.AlterPhoneNumber;
                RequestData.CustomerMasterData.CustomerGroupID = _objCustomer.CustomerGroupID;
                RequestData.CustomerMasterData.BuildingAndBlockNo = _objCustomer.BuildingAndBlockNo;
                RequestData.CustomerMasterData.StreetName = _objCustomer.StreetName;
                RequestData.CustomerMasterData.AreaName1 = _objCustomer.AreaName1;
                RequestData.CustomerMasterData.AreaName2 = _objCustomer.AreaName2;
                RequestData.CustomerMasterData.City = _objCustomer.City;
                RequestData.CustomerMasterData.StateID = _objCustomer.StateID;
                RequestData.CustomerMasterData.CountryID = _objCustomer.CountryID;
                RequestData.CustomerMasterData.Email = _objCustomer.Email;
                RequestData.CustomerMasterData.DOB = _objCustomer.DOB;
                RequestData.CustomerMasterData.Gender = _objCustomer.Gender;*/
                RequestData.CustomerMasterData.CreateBy = UserID;
                RequestData.CustomerMasterData.CreateOn = DateTime.Now;
                /*RequestData.CustomerMasterData.SCN = _objCustomer.SCN;
                RequestData.CustomerMasterData.CreditAmount = _objCustomer.CreditAmount;
                RequestData.CustomerMasterData.Remarks = _objCustomer.Remarks;
                RequestData.CustomerMasterData.Active = _objCustomer.Active;
                RequestData.CustomerMasterData.OnAccountApplicable = _objCustomer.OnAccountApplicable;*/
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                /*RequestData.CustomerMasterData.StoreID = _objCustomer.StoreID;
                RequestData.CustomerMasterData.CustomerGroupCode = _objCustomer.CustomerGroupCode;
                RequestData.CustomerMasterData.StateCode = _objCustomer.StateCode;
                RequestData.CustomerMasterData.CountryCode = _objCustomer.CountryCode;
                RequestData.RunningNo = _objCustomer.DocumentTypeID;
                RequestData.DocumentNumberingID = _objCustomer.DocumentNumberingID;
                RequestData.CustomerMasterData.CustomerImage = _objCustomer.CustomerImage;
                RequestData.CustomerMasterData.StateName = _objCustomer.StateName;
                RequestData.CustomerMasterData.Pincode = _objCustomer.Pincode;
                RequestData.CustomerMasterData.ShippingAddress1 = _objCustomer.ShippingAddress1;
                RequestData.CustomerMasterData.ShippingAddress2 = _objCustomer.ShippingAddress2;
                RequestData.CustomerMasterData.ShippingPhoneNumber = _objCustomer.ShippingPhoneNumber;
                RequestData.CustomerMasterData.ShippingStateID = _objCustomer.ShippingStateID;
                RequestData.CustomerMasterData.ShippingStateCode = _objCustomer.ShippingStateCode;
                RequestData.CustomerMasterData.ShippingStateName = _objCustomer.ShippingStateName;
                RequestData.CustomerMasterData.ShippingCity = _objCustomer.ShippingCity;
                RequestData.CustomerMasterData.ShippingPincode = _objCustomer.ShippingPincode;*/

                UpdateCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.UpdateCustomerMaster(RequestData);
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
        /*public IHttpActionResult PostUpdateCustomerData(CustomerMaster _CustomerData)
        {
            try
            {
                var RequestData = new SaveShiftLOGRequest();
                SaveShiftLOGResponse response = new SaveShiftLOGResponse();
                var ResponseDate = new DayShiftLOGBLL();
                response = ResponseDate.SaveDayClosing(RequestData);
                return null;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }*/


        public IHttpActionResult GetCustomerDetailsData(string CustomerSearchString)
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.SearchString = CustomerSearchString;
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.GetCommonCustomerDetailsData(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCustomerDetailsByLastID(string CustomerSearchString,string last)
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.SearchString = CustomerSearchString;
                SelectAllCustomerMasterResponse response = null;
                var ResponseData = new CustomerMasterBLL();
                response = ResponseData.GetCommonCustomerDetailsLastBYID(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
