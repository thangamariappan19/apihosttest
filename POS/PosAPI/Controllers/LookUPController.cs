using EasyBizBLL.FCPasses;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.FCPasses;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.FCPasses;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.CurrencyResponse;
using EasyBizResponse.Masters.CustomerGroupResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.FranchiseResponse;
using EasyBizResponse.Masters.LanguageResponse;
using EasyBizResponse.Masters.PriceListResponse;
using EasyBizResponse.Masters.ReasonMasterResponse;
using EasyBizResponse.Masters.RoleResponse;
using EasyBizResponse.Masters.TaxMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class LookUPController : ApiController
    {
        
    }

    public class LanguagelookUpController : ApiController
    {
        public IHttpActionResult GetLanguagelookUp()
        {
            try
            {
                SelectAllLanguageRequest request = new SelectAllLanguageRequest();
                SelectAllLanguageResponse response = null;
                request.ShowInActiveRecords = false;
                var bll = new LanguageBLL();
                response = bll.API_SelectLanguageLookUp(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class APICurrencylookUpController : ApiController
    {
        public IHttpActionResult GetCurrencylookUp()
        {
            try
            {
                var RequestData = new SelectCurrencyLookUpRequest();
                SelectCurrencyLookUpResponse response = null;
                var ResponseData = new CurrencyBLL();
                response = ResponseData.API_SelectCurrencyLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class TaxlookUpController : ApiController
    {
        public IHttpActionResult GetTaxlookUp()
        {
            try
            {
                var RequestData = new SelectAllTaxRequest();
                SelectAllTaxResponse response = null;
                var ResponseData = new TaxBLL();
                response = ResponseData.API_SelectTaxLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class CountryMasterLookUPController : ApiController
    {

        public IHttpActionResult GetCountryMasterLookUP()
        {
            try
            {
                SelectCountryLookUpRequest request = new SelectCountryLookUpRequest();
                SelectCountryLookUpResponse response = null;
                request.ShowInActiveRecords = false;
                var bll = new CountryBLL();
                response = bll.SelectCountryLookUp(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class CompanyMasterLookUPController : ApiController
    {
        public IHttpActionResult GetCompanyMasterLookUP(string CountryID)
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
    }
    public class WarehouseTypeMasterLookUPController : ApiController
    {
        public IHttpActionResult GetWarehouseTypeMasterLookUP()
        {
            try
            {
                var RequestData = new SelectAllWarehouseTypeMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.SelectAllWarehouseTypeMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class RoleMasterLookUPController : ApiController
    {
        public IHttpActionResult GetRoleMasterLookUP()
        {
            try
            {
                var RequestData = new SelectAllRoleRequest();
                SelectAllRoleResponse response = null;
                var ResponseData = new RoleBLL();
                response = ResponseData.SelectAllRoleMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public class ReasonLookupController : ApiController
        {

            public IHttpActionResult GetReasonLookUp()
            {
                try
                {
                    var RequestData = new SelectReasonMasterLookUpRequest();
                    SelectReasonMasterLookUpResponse response = null;
                    var ResponseData = new ReasonMasterBLL();
                    response = ResponseData.API_SelectReasonMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class FranchiseLookupController : ApiController
        {
            public IHttpActionResult GetFranchiseLookUp()
            {
                try
                {
                    var RequestData = new SelectFranchiseLookUpRequest();
                    SelectFranchiseLookupResponse response = null;
                    var ResponseData = new FranchiseBLL();
                    response = ResponseData.API_SelectFranchiseMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class BrandLookUPController : ApiController
        {
            public IHttpActionResult GetBrandLookup()
            {
                try
                {
                    var RequestData = new SelectBrandLookUpRequest();
                    SelectBrandLookUpResponse response = null;
                    var ResponseData = new BrandBLL();
                    response = ResponseData.API_SelectBrandMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class PriceListMasterLookUpController : ApiController
        {
            public IHttpActionResult GetPriceListLookUp()
            {
                try
                {
                    var RequestData = new SelectPriceListLookUPRequest();
                    RequestData.ShowInActiveRecords = false;
                    RequestData.Type = "Type";
                    SelectPriceListLookUPResponse response = null;
                    var ResponseData = new PriceListBLL();
                    response = ResponseData.API_SelectPriceListMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class PassMasterLookUp : ApiController
        {
            public IHttpActionResult GetPassMasterLookUp()
            {
                try
                {
                    var RequestData = new SelectPassMasterLookUpRequest();
                    RequestData.ShowInActiveRecords = false;
                    RequestData.Type = "Type";
                    SelectPassMasterLookUpResponse response = null;
                    var ResponseData = new PassMasterBLL();
                    response = ResponseData.API_SelectPassMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class CustomerGroupLookUp : ApiController
        {
            public IHttpActionResult GetCustomerGroupLookUp()
            {
                try
                {
                    var RequestData = new SelectCustomerGroupLookUpRequest();
                    RequestData.ShowInActiveRecords = false;                    
                    SelectCustomerGroupLookUpResponse response = null;
                    var ResponseData = new CustomerGroupBLL();
                    response = ResponseData.SelectCustomerGroupLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        public class GetCustomerMasterLookUpCCController : ApiController
        {
            public IHttpActionResult GetCustomerMasterLookUp(int ID)
            {
                try
                {
                    var RequestData = new SelectCustomerMasterLookUpRequest();
                    RequestData.ShowInActiveRecords = false;
                    RequestData.ID = ID;
                    SelectCustomerMasterLookUpResponse response = null;
                    var ResponseData = new CustomerMasterBLL();
                    response = ResponseData.SelectCustomerMasterLookUp(RequestData);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }
    }
}
