using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Common;
using EasyBizResponse.Common;
using EasyBizBLL.Common;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizDBTypes.Masters;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class CompanyController : ApiController
    {
        [Authorize]
        //public IHttpActionResult GetCompany()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllCompanySettingRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        var response = new SelectAllCompanySettingResponse();
        //        var ResponseData = new CompanySettingBLL();
        //        response = ResponseData.SelectAllCompanySettingResponse(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
       // [HttpGet]
        public IHttpActionResult GetCompany(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCompanySettingRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllCompanySettingResponse response = null;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.API_SelectAllCountryMaster(RequestData);
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

        public IHttpActionResult GetCompany(int ID)
        {
            try
            {
                var RequestData = new SelectByIDCompanySettingRequest();
                RequestData.ID = ID;
                SelectByIDCompanySettingResponse response = null;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.SelectByIDCompanySetting(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Company based on CountryID fro warehouse form
        public IHttpActionResult GetCompany(string CountryID)
        {
            try
            {
                var RequestData = new SelectCompanySettingsLookUpRequest();
                RequestData.CountryID = Convert.ToInt32(CountryID);
                SelectCompanySettingsLookUpResponse response = null;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.SelectCompanySettingsLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        public IHttpActionResult PostInsertCompanyMaster(CompanySettings _CompanySettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCompanySettingRequest();
                RequestData.CompanySettingData = new CompanySettings();

                RequestData.CompanySettingData.ID = _CompanySettings.ID;
                RequestData.CompanySettingData.CompanyCode = _CompanySettings.CompanyCode;
                RequestData.CompanySettingData.CompanyName = _CompanySettings.CompanyName;
                RequestData.CompanySettingData.Address = _CompanySettings.Address;
                RequestData.CompanySettingData.CountrySettingID = _CompanySettings.CountrySettingID;
                RequestData.CompanySettingData.CountrySettingCode = _CompanySettings.CountrySettingCode;
                //RequestData.CompanySettingData.RetailSettingID = _ICompanySettingMasterView.RetailSettingID;
                RequestData.CompanySettingData.Remarks = _CompanySettings.Remarks;
                RequestData.CompanySettingData.Active = _CompanySettings.Active;
                RequestData.CompanySettingData.CreateBy = UserID;
                RequestData.CompanySettingData.CreateOn = DateTime.Now;
                SaveCompanySettingResponse response = null;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.SaveCompanySetting(RequestData);
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

        public IHttpActionResult PutUpdateCompanyMaster(CompanySettings _CompanySettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCompanySettingRequest();
                RequestData.CompanySettingData = new CompanySettings();

                RequestData.CompanySettingData.ID = _CompanySettings.ID;
                RequestData.CompanySettingData.CompanyCode = _CompanySettings.CompanyCode;
                RequestData.CompanySettingData.CompanyName = _CompanySettings.CompanyName;
                RequestData.CompanySettingData.Address = _CompanySettings.Address;
                RequestData.CompanySettingData.CountrySettingID = _CompanySettings.CountrySettingID;
                RequestData.CompanySettingData.CountrySettingCode = _CompanySettings.CountrySettingCode;
                //RequestData.CompanySettingData.RetailSettingID = _ICompanySettingMasterView.RetailSettingID;
                RequestData.CompanySettingData.Remarks = _CompanySettings.Remarks;
                RequestData.CompanySettingData.Active = _CompanySettings.Active;
                RequestData.CompanySettingData.CreateBy = UserID;
                RequestData.CompanySettingData.UpdateOn = DateTime.Now;
                RequestData.CompanySettingData.SCN = 1;
                UpdateCompanySettingResponse response = null;
                var ResponseData = new CompanySettingBLL();
                response = ResponseData.UpdateCompanySetting(RequestData);
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