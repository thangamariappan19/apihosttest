using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    //[Authorize]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEmployeeList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllEmployeeMasterRequest();

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



                SelectAllEmployeeMasterResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
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
        public IHttpActionResult GetEmployeeFilterList(string limit, string offset, string isActive, int countryid,int storeid,string designation)
        {
           try
            {
                var RequestData = new SelectCountryStoreFilterEmployeeMaster();
                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                //RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";
               
                RequestData.CountryID = countryid;
                RequestData.StoreID = storeid;
                RequestData.Designation = designation;
                SelectAllEmployeeMasterResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.GetSelectFilterRecord(RequestData);
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


        public IHttpActionResult GetEmployeeBYID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDEmployeeMasterRequest();
                RequestData.ID = ID;
                GetEmployeeByStoreResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.SelectEmployeeMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostEmployeeMaster(EmployeeMaster _objEmployee)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveEmployeeMasterRequest();
                RequestData.EmployeeMasterRecord = new EmployeeMaster();
                RequestData.EmployeeMasterRecord = _objEmployee;
                RequestData.EmployeeMasterRecord.CreateBy = UserID;
                SaveEmployeeMasterResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.SaveEmployeeMaster(RequestData);
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

        public IHttpActionResult PutEmployeeMaster(EmployeeMaster _objEmployee)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateEmployeeMasterRequest();
                RequestData.EmployeeMasterRecord = new EmployeeMaster();
                RequestData.EmployeeMasterRecord = _objEmployee;
                RequestData.EmployeeMasterRecord.UpdateBy = UserID;
                UpdateEmployeeMasterResponse response = null;
                var ResponseData = new EmployeeMasterBLL();
                response = ResponseData.UpdateEmployeeMaster(RequestData);
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
