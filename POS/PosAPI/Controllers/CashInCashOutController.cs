using EasyBizBLL.Transactions.POSOperations;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.POSOperations;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using EasyBizResponse.Transactions.POSOperations.CashInCashOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CashInCashOutController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetCashInCashOut()
        {
            try
            {
                var RequestData = new SelectAllCashInCashOutRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllCashInCashOutResponse response = null;
                var ResponseData = new CashInCashOutBLL();
                response = ResponseData.SelectAllCashInCashOut(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        public IHttpActionResult GetCashInCashOutReport(string FromDate,string Todate,string CategoryType,int Storeid)
        {
            try
            {
                var RequestData = new SelectAllCashInCashoutReportRequest();
                RequestData.FromDate = Convert.ToDateTime(FromDate);
                RequestData.ToDate = Convert.ToDateTime(Todate);
                RequestData.CategoryType = CategoryType;
                RequestData.StoreID = Storeid;
                SelectAllCashInCashoutReportResponse response = null;
                var ResponseData = new CashInCashOutBLL();
                response = ResponseData.SelectAllCashInCashOutReport(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        public IHttpActionResult GetCashInCashOutRecord(string Date, string CategoryType, int Storeid)
        {
            try
            {
                var RequestData = new SelectAllCashInCashOutDateWiseRequest();
                RequestData.Date = Convert.ToDateTime(Date);
                RequestData.CategoryType = CategoryType;
                RequestData.StoreID = Storeid;
                SelectAllCashInCashOutDateWiseReponse response = null;
                var Bll = new CashInCashOutBLL();
                response = Bll.SelectAllCashInCashOutRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        public IHttpActionResult GetCasinCashout(int id)
        {
            try
            {
                var RequestData = new SelectByCashInCashOutIDRequest();
                RequestData.ID = id;
                SelectByCashInCashOutIDResponse response = null;
                var ResponseData = new CashInCashOutBLL();
                response = ResponseData.SelectCashInCashOutRecord(RequestData);
                if (response.CashInCashOutMasterRecord != null)
                {
                    SelectCashInCashOutDetailsRequest RequestData1 = new SelectCashInCashOutDetailsRequest();
                    RequestData1.ID = id;
                    SelectCashInCashOutDetailsResponse Response1 = null;
                    Response1 = ResponseData.SelectAllStoreGroupDetails(RequestData1);
                    if (Response1.CashInCashOutDetailsRecord != null)
                    {
                        response.CashInCashOutDetailsRecord = Response1.CashInCashOutDetailsRecord;
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostInsertCashInCashOut(CashInCashOutMaster _objcashMaster)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCashInCashOutRequest();
                SaveCashInCashOutResponse response = null;
                RequestData.CashInCashOutMasterRecord = new CashInCashOutMaster();
                if (_objcashMaster.CashInCashOutDetailsList.Count != 0)
                {
                    RequestData.CashInCashOutDetailsList = _objcashMaster.CashInCashOutDetailsList;
                    RequestData.CashInCashOutMasterRecord.ID = _objcashMaster.ID;
                    RequestData.CashInCashOutMasterRecord.Total = _objcashMaster.Total;
                    RequestData.CashInCashOutMasterRecord.DocumentDate = _objcashMaster.DocumentDate;
                    RequestData.CashInCashOutMasterRecord.CreateBy = UserID;
                    RequestData.CashInCashOutMasterRecord.CreateOn = DateTime.Now;
                    RequestData.CashInCashOutMasterRecord.Active = true;
                    RequestData.CashInCashOutMasterRecord.StoreID = _objcashMaster.StoreID;
                    RequestData.CashInCashOutMasterRecord.CountryID = _objcashMaster.CountryID;
                    RequestData.CashInCashOutMasterRecord.StoreCode = _objcashMaster.StoreCode;
                    RequestData.CashInCashOutMasterRecord.POSID = _objcashMaster.POSID;
                    RequestData.CashInCashOutMasterRecord.POSCode = _objcashMaster.POSCode;
                    RequestData.CashInCashOutMasterRecord.ShiftID = _objcashMaster.ShiftID;
                    RequestData.CashInCashOutMasterRecord.ShiftCode = _objcashMaster.ShiftCode;
                    RequestData.CashInCashOutMasterRecord.SCN = _objcashMaster.SCN;


                    var ResponseData = new CashInCashOutBLL();
                    response = ResponseData.SaveCashInCashOut(RequestData);
                    if (response.StatusCode == Enums.OpStatusCode.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response.DisplayMessage);
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

