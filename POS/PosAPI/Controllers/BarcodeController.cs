using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BarcodeController : ApiController
    {

        //select all
        public IHttpActionResult GetBarcodeData()
        {
            try
            {
                var RequestData = new SelectAllBarcodeSettingsRequest();
                SelectAllBarcodeSettingsResponse response = null;
                var ResponseData = new BarcodeSettingsBLL();
                response = ResponseData.SelectAllBarcodeSettings(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetBarcodeData(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllBarcodeSettingsRequest();

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

                SelectAllBarcodeSettingsResponse response = null;
                var ResponseData = new BarcodeSettingsBLL();
                response = ResponseData.API_SelectAllBarcodeSettings(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetBarcodebyID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDBarcodeSettingsRequest();
                RequestData.ID = ID;
                SelectByIDBarcodeSettingsResponse response = null;
                var ResponseData = new BarcodeSettingsBLL();
                response = ResponseData.SelectBarcodeSettingsRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostBarcodeMaster(List<BarcodeSettings> _objBarcodeMaster)
        {
            try
            {
                var RequestData = new SaveBarcodeSettingsRequest();
                RequestData.BarcodeSettingsList = new List<BarcodeSettings>();
                RequestData.BarcodeSettingsList = _objBarcodeMaster;
                SaveBarcodeSettingsResponse response = null;
                var ResponseData = new BarcodeSettingsBLL();
                response = ResponseData.SaveBarcodeSettings(RequestData);
                //return Ok(response);
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
