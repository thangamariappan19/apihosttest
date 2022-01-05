using EasyBizBLL.Masters;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.VendorMasterRequest;
using EasyBizResponse.Masters.VendorMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class VendorController : ApiController
    {
        public IHttpActionResult GetVendorList()
        {
            try
            {
                var RequestData = new SelectAllVendorRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllVendorResponse response = null;
                var ResponseData = new VendorMasterBLL();
                response = ResponseData.SelectAllVendorRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetVendorByID(int ID)
        {
            try
            {
                var RequestData = new SelectByVendorIDRequest();
                RequestData.ID = ID;
                SelectByVendorIDResponse response = null;
                var ResponseData = new VendorMasterBLL();
                response = ResponseData.SelectVendorRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostVendorGroup(VendorMaster _objVendor)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveVendorRequest();
                RequestData.VendorRecord = new VendorMaster();
                RequestData.VendorRecord = _objVendor;
                RequestData.VendorRecord.CreateOn = DateTime.Now;
                RequestData.VendorRecord.CreateBy = UserID;
                RequestData.VendorRecord.SCN = 0;
                SaveVendorResponse response = null;
                var ResponseData = new VendorMasterBLL();
                response = ResponseData.SaveVendor(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutVendorGroup(VendorMaster _objVendor)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateVendorRequest();
                RequestData.VendorRecord = new VendorMaster();
                RequestData.VendorRecord = _objVendor;
                RequestData.VendorRecord.UpdateOn = DateTime.Now;
                RequestData.VendorRecord.UpdateBy = UserID;
                RequestData.VendorRecord.SCN = 0;
                UpdateVendorResponse response = null;
                var ResponseData = new VendorMasterBLL();
                response = ResponseData.UpdateVendor(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
