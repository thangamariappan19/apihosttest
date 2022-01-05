using EasyBizBLL.Masters;
using EasyBizDBTypes.Masters;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SKUController : ApiController
    {

        //select all
        public IHttpActionResult GetSKUData()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectAllSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetSKUData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();

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

                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.API_SelectAllSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Select by ID - Edit
        public IHttpActionResult GetSKUDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDSKUMasterRequest();
                RequestData.ID = ID;
                SelectByIDSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectByIdSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Insert and Update
        public IHttpActionResult PostSKUMaster(List<SKUMasterTypes> _objSKUMaster)
        {
            try
            {
                var RequestData = new SaveSKUMasterRequest();
                RequestData.SKUMasterTypesRecord = new List<SKUMasterTypes>();
                RequestData.SKUMasterTypesRecord = _objSKUMaster;
                SaveSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SaveSKUMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
