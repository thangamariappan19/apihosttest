using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Transactions.POS;
using System.Security.Claims;
using EasyBizRequest.Masters.SKUMasterRequest;

namespace PosAPI.Controllers
{
    public class InvoiceController : ApiController
    {
        public IHttpActionResult GetSKU(string SKUCode, string storeid)
        {
            try
            {
                SelectAllSKUMasterRequest request = new SelectAllSKUMasterRequest();
                request.ShowInActiveRecords = true;
                request.Count = 1;
                request.SearchString = SKUCode;
                request.Mode = "SALES";
                request.StoreIDs = storeid;
                request.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.GetSKUDetails(request);
                if (response.StatusCode == Enums.OpStatusCode.Success ||
                    response.StatusCode == Enums.OpStatusCode.RecordNotFound)
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
        public IHttpActionResult GetBINSKU(string SKUCode, string storeid, string BinCode)
        {
            try
            {
                SelectAllSKUMasterRequest request = new SelectAllSKUMasterRequest();
                request.ShowInActiveRecords = true;
                request.Count = 1;
                request.SearchString = SKUCode;
                request.Mode = "SALES";
                request.StoreIDs = storeid;
                request.BINCode = BinCode;
                request.RequestFrom = Enums.RequestFrom.StoreSales;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.GetSKUDetailsByBin(request);
                if (response.StatusCode == Enums.OpStatusCode.Success ||
                    response.StatusCode == Enums.OpStatusCode.RecordNotFound)
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


        //sku based on country id
        public IHttpActionResult GetSKU(string SKUCode, int CountryID)
        {
            try
            {
                SelectSKUOnCountryRequest request = new SelectSKUOnCountryRequest();

                request.Searchstring = SKUCode;
                request.CountryID = CountryID;
                SelectSKUOnCountryResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.SelectSKUOnCountry(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        //Get SKU Details in any of the stores(without StoreId as Parameter)
        public IHttpActionResult GetSKUWithoutStoreID(string SKUCode)
        {
            try
            {
                SelectAllSKUMasterRequest request = new SelectAllSKUMasterRequest();
                request.SearchString = SKUCode;
                SelectAllSKUMasterResponse response = null;
                var ResponseData = new SKUMasterBLL();
                response = ResponseData.GetSKUWithoutStoreID(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public IHttpActionResult GetInvoiceDetails(string InvoiceNo)
        //{
        //    try
        //    {
        //        var RequestData = new SelectInvoiceDetailsListRequest();
        //        RequestData.InvoiceHeaderID = 0;
        //        RequestData.RequestFrom = Enums.RequestFrom.Search;
        //        RequestData.SearchString = InvoiceNo;

        //        //var ResponseData = InvoiceBLL.SelectInvoiceDetailsList(RequestData);
        //        SelectInvoiceDetailsListResponse response = null;
        //        var ResponseDate = new InvoiceBLL();
        //        response = ResponseDate.SelectInvoiceDetailsList(RequestData);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}



        public IHttpActionResult PostSaveInvoice(InvoiceHeader _InvoiceHeader)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                _InvoiceHeader.CreateBy = UserID;

                //Enums.InvoiceStatus myEnum = Enums.InvoiceStatus.Completed;
                Enums.InvoiceStatus myEnum;
                Enum.TryParse(_InvoiceHeader.SalesStatus, out myEnum);



                var RequestData = new SaveInvoiceRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.InvoiceHeaderData = new InvoiceHeader();

                var objInvoiceHeaderTypes = new InvoiceHeader();

                objInvoiceHeaderTypes.SalesStatus = _InvoiceHeader.SalesStatus;
                objInvoiceHeaderTypes.ID = _InvoiceHeader.ID;
                objInvoiceHeaderTypes.CountryID = _InvoiceHeader.CountryID;
                objInvoiceHeaderTypes.StoreID = _InvoiceHeader.StoreID;
                objInvoiceHeaderTypes.PosID = _InvoiceHeader.PosID;
                objInvoiceHeaderTypes.DocumentDate = _InvoiceHeader.DocumentDate;
                objInvoiceHeaderTypes.BusinessDate = _InvoiceHeader.BusinessDate;

                if (myEnum == Enums.InvoiceStatus.ParkSale)
                {
                    //GetSalesHoldDocumentNumberingRecord();
                    //_Doc.GetDocumentNumber(_InvoiceHeader.CountryID, 0, _InvoiceHeader.StoreID, _InvoiceHeader.StoreCode, Enums.RequestFrom.StoreSales, Enums.DocumentType.SALESHOLD);
                    objInvoiceHeaderTypes.InvoiceNo = _InvoiceHeader.InvoiceNo;
                    RequestData.PaymentList = new List<PaymentDetail>();
                }
                else
                {
                    //SelectDocumentNumberingRecord();
                    //_Doc.GetDocumentNumber(_InvoiceHeader.CountryID, 0, _InvoiceHeader.StoreID, _InvoiceHeader.StoreCode, Enums.RequestFrom.StoreSales, Enums.DocumentType.SALES);
                    objInvoiceHeaderTypes.InvoiceNo = _InvoiceHeader.InvoiceNo;
                    objInvoiceHeaderTypes.PaymentList = _InvoiceHeader.PaymentList;
                }


                objInvoiceHeaderTypes.CustomerGroupID = 0;
                objInvoiceHeaderTypes.CustomerID = _InvoiceHeader.CustomerID;
                objInvoiceHeaderTypes.ShiftID = _InvoiceHeader.ShiftID;
                objInvoiceHeaderTypes.TotalQty = _InvoiceHeader.TotalQty;
                objInvoiceHeaderTypes.SubTotalAmount = _InvoiceHeader.SubTotalAmount;
                objInvoiceHeaderTypes.TotalDiscountType = _InvoiceHeader.TotalDiscountType;
                objInvoiceHeaderTypes.TotalDiscountAmount = _InvoiceHeader.TotalDiscountAmount;
                objInvoiceHeaderTypes.TotalDiscountPercentage = _InvoiceHeader.TotalDiscountPercentage;
                objInvoiceHeaderTypes.TaxID = _InvoiceHeader.TaxID;
                objInvoiceHeaderTypes.TaxAmount = _InvoiceHeader.TaxAmount;
                objInvoiceHeaderTypes.SubTotalWithTaxAmount = _InvoiceHeader.SubTotalWithTaxAmount;
                objInvoiceHeaderTypes.NetAmount = _InvoiceHeader.NetAmount;
                objInvoiceHeaderTypes.ReceivedAmount = _InvoiceHeader.ReceivedAmount;
                objInvoiceHeaderTypes.ReturnAmount = _InvoiceHeader.ReturnAmount;
                objInvoiceHeaderTypes.AppliedPriceListID = 0;
                objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = 0;
                objInvoiceHeaderTypes.AppliedPromotionID = _InvoiceHeader.AppliedPromotionID;
                objInvoiceHeaderTypes.SalesEmployeeID = _InvoiceHeader.SalesEmployeeID;
                objInvoiceHeaderTypes.SalesManagerID = _InvoiceHeader.SalesManagerID;
                objInvoiceHeaderTypes.CashierID = _InvoiceHeader.CashierID;
                objInvoiceHeaderTypes.RefNumber = _InvoiceHeader.RefNumber;
                objInvoiceHeaderTypes.CreateBy = _InvoiceHeader.CreateBy;
                objInvoiceHeaderTypes.SCN = _InvoiceHeader.SCN;
                objInvoiceHeaderTypes.IsCreditSale = _InvoiceHeader.IsCreditSale;

                objInvoiceHeaderTypes.CountryCode = _InvoiceHeader.CountryCode;
                objInvoiceHeaderTypes.StoreCode = _InvoiceHeader.StoreCode;
                objInvoiceHeaderTypes.PosCode = _InvoiceHeader.PosCode;
                objInvoiceHeaderTypes.CustomerCode = _InvoiceHeader.CustomerCode;
                objInvoiceHeaderTypes.SalesEmployeeCode = _InvoiceHeader.SalesEmployeeCode;
                objInvoiceHeaderTypes.DiscountRemarks = _InvoiceHeader.DiscountRemarks;
                objInvoiceHeaderTypes.BeforeRoundOffAmount = _InvoiceHeader.BeforeRoundOffAmount;
                objInvoiceHeaderTypes.RoundOffAmount = _InvoiceHeader.RoundOffAmount;

                objInvoiceHeaderTypes.CouponID = _InvoiceHeader.CouponID;
                objInvoiceHeaderTypes.RedeemCouponCode = _InvoiceHeader.RedeemCouponCode;
                objInvoiceHeaderTypes.RedeemCouponLineNo = _InvoiceHeader.RedeemCouponLineNo;
                objInvoiceHeaderTypes.RedeemCouponSerialCode = _InvoiceHeader.RedeemCouponSerialCode;
                objInvoiceHeaderTypes.RedeemCouponDiscountType = _InvoiceHeader.RedeemCouponDiscountType;
                objInvoiceHeaderTypes.RedeemCouponDiscountValue = _InvoiceHeader.RedeemCouponDiscountValue;
                objInvoiceHeaderTypes.RedeemValue = _InvoiceHeader.RedeemValue;
                objInvoiceHeaderTypes.IssuedCouponCode = _InvoiceHeader.IssuedCouponCode;
                objInvoiceHeaderTypes.IssuedCouponLineNo = _InvoiceHeader.IssuedCouponLineNo;
                objInvoiceHeaderTypes.IssuedCouponSerialCode = _InvoiceHeader.IssuedCouponSerialCode;
                objInvoiceHeaderTypes.CustomerName = _InvoiceHeader.CustomerName;
                objInvoiceHeaderTypes.CustomerMobileNo = _InvoiceHeader.CustomerMobileNo;


                //int Status = (int)InvoiceStatus;
                int Status = (int)myEnum;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                objInvoiceHeaderTypes.SalesStatus = TypeName;

                if (_InvoiceHeader.InvoiceDetailList != null)
                {
                    RequestData.InvoiceDetailList = _InvoiceHeader.InvoiceDetailList;
                }
                if (_InvoiceHeader.PaymentList != null)
                {
                    RequestData.PaymentList = _InvoiceHeader.PaymentList;
                }
                if (_InvoiceHeader.PaymentProcessorList != null)
                {
                    RequestData.PaymentProcessorList = _InvoiceHeader.PaymentProcessorList;
                }
                RequestData.InvoiceHeaderData = objInvoiceHeaderTypes;
                RequestData.BaseIntegrateStoreID = _InvoiceHeader.StoreID;
                RequestData.TransactionLogList = _InvoiceHeader.TransactionLogList;
                RequestData.RunningNo = _InvoiceHeader.RunningNo;
                RequestData.DocumentNumberingID = _InvoiceHeader.DocumentTypeID;

                //Commented for Sale Order 
                //if (_InvoiceHeader.SalesOrderRecord != null)
                //{
                //    RequestData.SalesOrderDocumentNo = _InvoiceHeader.SalesOrderRecord.DocumentNo;
                //}
                //else
                //{
                //RequestData.SalesOrderDocumentNo = string.Empty;
                //}
                /*RequestData.UserName = _IInvoiceView.UserInformation.UserName;
                RequestData.Password = _IInvoiceView.UserInformation.Password;*/
                SaveInvoiceResponse response = new SaveInvoiceResponse();
                var ResponseDate = new InvoiceBLL();
                response = ResponseDate.SaveInvoice(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }

                //return CreatedAtRoute("DefaultApi", new { id = response.IDs }, response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /*public IHttpActionResult GetStockBySKU(string SKUCode,int StoreID)
        {
            try
            {
                GetStockBySkuRequest request = new GetStockBySkuRequest();
                request.SKUCode = SKUCode;
                request.StoreID = StoreID;
                GetStockBySkuResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetStockBySku(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }*/
    }
}