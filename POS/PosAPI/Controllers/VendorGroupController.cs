using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class VendorGroupController : ApiController
    {
        public IHttpActionResult GetVendorGroupList()
        {
            try
            {
                var RequestData = new SelectAllVendorGroupRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllVendorGroupResponse response = null;
                var ResponseData = new VendorGroupMasterBLL();
                response = ResponseData.SelectAllRecordVendorGroup(RequestData);
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
                var RequestData = new SelectByVendorGroupIDRequest();
                RequestData.ID = ID;
                SelectByVendorGroupIDResponse response = null;
                var ResponseData = new VendorGroupMasterBLL();
                response = ResponseData.SelectVendorGroupRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostVendorGroup(VendorGroupMaster _objVendor)
        {
            try
            {
                var RequestData = new SaveVendorGroupRequest();
                RequestData.VendorGroupRecord = new VendorGroupMaster();
                RequestData.VendorGroupRecord = _objVendor;
                RequestData.VendorGroupRecord.CreateOn = DateTime.Now;
                RequestData.VendorGroupRecord.SCN = 0;
                SaveVendorGroupResponse response = null;
                var ResponseData = new VendorGroupMasterBLL();
                response = ResponseData.SaveVendorGroup(RequestData);
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

        public IHttpActionResult PutVendorGroup(VendorGroupMaster _objVendor)
        {
            try
            {
                var RequestData = new UpdateVendorGroupRequest();
                RequestData.VendorGroupRecord = new VendorGroupMaster();
                RequestData.VendorGroupRecord = _objVendor;
                RequestData.VendorGroupRecord.UpdateOn = DateTime.Now;
                RequestData.VendorGroupRecord.SCN = 0;
                UpdateVendorGroupResponse response = null;
                var ResponseData = new VendorGroupMasterBLL();
                response = ResponseData.UpdateVendorGroup(RequestData);
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
