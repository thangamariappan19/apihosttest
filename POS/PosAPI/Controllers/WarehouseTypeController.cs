using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class WarehouseTypeController : ApiController
    {
        public IHttpActionResult GetWarehouseTypeList()
        {
            try
            {
                var RequestData = new SelectAllWarehouseTypeMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.SelectAllWarehouseTypeMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetWarehouseType(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllWarehouseTypeMasterRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.API_SelectAllWarehouseTypeMaster(RequestData);
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

        public IHttpActionResult GetWarehouseTypeByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDWarehouseTypeMasterRequest();
                RequestData.ID = ID;
                SelectByIDWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.SelectWarehouseTypeMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostWarehouseType(WarehouseTypeMaster _objWarehouseType)
        {
            try
            {

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveWarehouseTypeMasterRequest();
                RequestData.WarehouseTypMasterData = new WarehouseTypeMaster();
                RequestData.WarehouseTypMasterData = _objWarehouseType;
                RequestData.WarehouseTypMasterData.CreateOn = DateTime.Now;
                RequestData.WarehouseTypMasterData.CreateBy = UserID;
                RequestData.WarehouseTypMasterData.SCN = 0;
                SaveWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.SaveWarehouseTypeMaster(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutWarehouseType(WarehouseTypeMaster _objWarehouseType)
        {
            try
            {


                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;


                var RequestData = new UpdateWarehouseTypeMasterRequest();
                RequestData.WarehouseTypeMasterData = new WarehouseTypeMaster();
                RequestData.WarehouseTypeMasterData = _objWarehouseType;
                RequestData.WarehouseTypeMasterData.UpdateOn = DateTime.Now;
                RequestData.WarehouseTypeMasterData.SCN = 0;
                RequestData.WarehouseTypeMasterData.UpdateBy = UserID;
                RequestData.WarehouseTypeMasterData.SCN = 0;
                UpdateWarehouseTypeMasterResponse response = null;
                var ResponseData = new WarehouseTypeMasterBLL();
                response = ResponseData.UpdateWarehouseTypeMaster(RequestData);
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
