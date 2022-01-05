using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class WarehouseController : ApiController
    {
       [Authorize]
        public IHttpActionResult GetWarehouseList()
        {
            try
            {
                var RequestData = new SelectAllWarehouseMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllWarehouseMasterResponse response = null;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.SelectAllWarehouseMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetWarehouse(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllWarehouseMasterRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllWarehouseMasterResponse response = null;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.API_SelectAllWarehouseMaster(RequestData);
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

        public IHttpActionResult GetWarehouseByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDWarehouseMasterRequest();
                RequestData.ID = ID;
                SelectByIDWarehouseMasterResponse response = null;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.SelectWarehouseMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostWarehouse(WarehouseMaster _objWarehouse)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveWarehouseMasterRequest();
                RequestData.WarehouseMasterData = new WarehouseMaster();
                RequestData.WarehouseMasterData = _objWarehouse;
                RequestData.WarehouseMasterData.CreateBy = UserID;
                RequestData.WarehouseMasterData.SCN = 0;
                SaveWarehouseMasterResponse response = null;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.SaveWarehouseMaster(RequestData);
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

        public IHttpActionResult PutWarehouse(WarehouseMaster _objWarehouse)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateWarehouseMasterRequest();
                RequestData.WarehouseMasterData = new WarehouseMaster();
                RequestData.WarehouseMasterData = _objWarehouse;
                RequestData.WarehouseMasterData.UpdateBy = UserID;
                RequestData.WarehouseMasterData.SCN = 0;
                UpdateWarehouseMasterResponse response = null;
                var ResponseData = new WarehouseMasterBLL();
                response = ResponseData.UpdateWarehouseMaster(RequestData);
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
