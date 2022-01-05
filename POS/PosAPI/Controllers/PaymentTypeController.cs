using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PaymentTypeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPaymentList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllPaymentTypeRequest();

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



                SelectAllPaymentTypeResponse response = null;
                var ResponseData = new PaymentTypeMasterBLL();
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
        public IHttpActionResult GetPaymentList()
        {
            try
            {
                var RequestData = new SelectAllPaymentTypeRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllPaymentTypeResponse response = null;
                var ResponseData = new PaymentTypeMasterBLL();
                response = ResponseData.SelectAllPaymentType(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPaymentByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDPaymentTypeRequest();
                RequestData.ID = ID;
                SelectByIDPaymentTypeResponse response = null;
                var ResponseData = new PaymentTypeMasterBLL();
                response = ResponseData.SelectByIDPaymentType(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPayment(PaymentTypeMasterType _objPayment)
        {
            try
            {
                var RequestData = new SavePaymentTypeRequest();
                RequestData.PaymentTypeMasterData = new PaymentTypeMasterType();
                RequestData.PaymentTypeMasterData = _objPayment;
                RequestData.PaymentTypeMasterData.CreateOn = DateTime.Now;
                RequestData.PaymentTypeMasterData.SCN = 0;
                SavePaymentTypeResponse response = null;
                var ResponseData = new PaymentTypeMasterBLL();
                response = ResponseData.SavePaymentType(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutPayment(PaymentTypeMasterType _objPayment)
        {
            try
            {
                var RequestData = new UpdatePaymentTypeRequest();
                RequestData.PaymentTypeMasterData = new PaymentTypeMasterType();
                RequestData.PaymentTypeMasterData = _objPayment;
                RequestData.PaymentTypeMasterData.UpdateOn = DateTime.Now;
                RequestData.PaymentTypeMasterData.SCN = 0;
                UpdatePaymentTypeResponse response = null;
                var ResponseData = new PaymentTypeMasterBLL();
                response = ResponseData.UpdatePaymentType(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
