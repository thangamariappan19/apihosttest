using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using EasyBizResponse.Masters.CurrencyResponse;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using EasyBizResponse.Transactions.POS.DenominationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DenominationController : ApiController
    {
        public IHttpActionResult GetPaymentTypeList(int CountyID)
        {
            try
            {
                var RequestData = new SelectPaymentTypeByCountryRequest();
                RequestData.ShowInActiveRecords = false;
                SelectPaymentTypeByCountryResponse response = null;
                RequestData.CountryID = CountyID;
                var bll = new PaymentTypeMasterBLL();
                response = bll.SelectPaymentTypeByCountry(RequestData);
                return Ok(response);
               
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCashList(string PayCurrencyCode)
        {
            try
            {
                var RequestData = new SelectCurrencyDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                SelectCurreucyDetailsResponse response = null;
                RequestData.CurrencyCode = PayCurrencyCode;
                var bll = new CurrencyBLL();
                response=bll.SelectCurrencyDetails(RequestData);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        public IHttpActionResult SaveDenomination(SaveDenominationRequest _IDenomination)
        {
            try
            {
               
                var RequestData = new SaveDenominationRequest();
                RequestData.DenominationForShiftoutTypeHeader = new DenominationForShiftoutTypeHeader();
                RequestData.DenominationForShiftoutTypeHeader.ID = _IDenomination.DenominationForShiftoutTypeHeader.ID;
           
                RequestData.DenominationForShiftoutTypeHeader.ShifLogId = _IDenomination.DenominationForShiftoutTypeHeader.ShifLogId;
                RequestData.DenominationForShiftoutTypeHeader.StoreCode = _IDenomination.DenominationForShiftoutTypeHeader.StoreCode;
                RequestData.DenominationForShiftoutTypeHeader.ShiftCode = _IDenomination.DenominationForShiftoutTypeHeader.ShiftCode;
                RequestData.DenominationForShiftoutTypeHeader.POSCode = _IDenomination.DenominationForShiftoutTypeHeader.POSCode;
                RequestData.DenominationForShiftoutTypeHeader.ShiftInAmount = _IDenomination.DenominationForShiftoutTypeHeader.ShiftInAmount;
                RequestData.DenominationForShiftoutTypeHeader.ShiftOutAmount = _IDenomination.DenominationForShiftoutTypeHeader.ShiftOutAmount;
                RequestData.DenominationForShiftoutTypeHeader.GrandTotalValue = _IDenomination.DenominationForShiftoutTypeHeader.GrandTotalValue;
                RequestData.DenominationForShiftoutTypeHeader.TotalValueCount = _IDenomination.DenominationForShiftoutTypeHeader.TotalValueCount;
                RequestData.DenominationForShiftoutTypeHeader.TotalCardValue = _IDenomination.DenominationForShiftoutTypeHeader.TotalCardValue;
                RequestData.DenominationForShiftoutTypeHeader.remarks = _IDenomination.DenominationForShiftoutTypeHeader.remarks;
                RequestData.DenominationForShiftOutTypeList = _IDenomination.DenominationForShiftOutTypeList;
                RequestData.ReceivedDenominationData = _IDenomination.ReceivedDenominationData;
                RequestData.PaymentTypeMasterTypeList = _IDenomination.PaymentTypeMasterTypeList;
                SaveDenominationResponse response=null;
                var ResponseData =  new DenominationBLL();
                response = ResponseData.SaveDenomination(RequestData);
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
                throw ex;
            }
        }
    }
}
