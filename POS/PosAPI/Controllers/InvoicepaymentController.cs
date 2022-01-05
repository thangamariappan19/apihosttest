using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;
using PosAPI.Modules;
using PosAPI.DTOs;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Transactions.POS;
using System.Security.Claims;
using EasyBizRequest.Masters.SKUMasterRequest;

namespace PosAPI.Controllers
{
    public class InvoicepaymentController : ApiController
    {




        #region PostSavecashandcardDtls

        public IHttpActionResult PostSavecashandcardDtls(InvoiceHeader _InvoiceHeader)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

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

                
                    //SelectDocumentNumberingRecord();
                    //_Doc.GetDocumentNumber(_InvoiceHeader.CountryID, 0, _InvoiceHeader.StoreID, _InvoiceHeader.StoreCode, Enums.RequestFrom.StoreSales, Enums.DocumentType.SALES);
                    objInvoiceHeaderTypes.InvoiceNo = _InvoiceHeader.InvoiceNo;
                    objInvoiceHeaderTypes.PaymentList = _InvoiceHeader.PaymentList;
                


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

                objInvoiceHeaderTypes.CountryCode = _InvoiceHeader.CountryCode;
                objInvoiceHeaderTypes.StoreCode = _InvoiceHeader.StoreCode;
                objInvoiceHeaderTypes.PosCode = _InvoiceHeader.PosCode;
                objInvoiceHeaderTypes.CustomerCode = _InvoiceHeader.CustomerCode;
                objInvoiceHeaderTypes.SalesEmployeeCode = _InvoiceHeader.SalesEmployeeCode;
                objInvoiceHeaderTypes.DiscountRemarks = _InvoiceHeader.DiscountRemarks;
                objInvoiceHeaderTypes.BeforeRoundOffAmount = _InvoiceHeader.BeforeRoundOffAmount;
                objInvoiceHeaderTypes.RoundOffAmount = _InvoiceHeader.RoundOffAmount;



                //int Status = (int)InvoiceStatus;
                int Status = 1;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                objInvoiceHeaderTypes.SalesStatus = "Completed";

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
                SaveInvoiceResponse response = new SaveInvoiceResponse();
                var ResponseData = new InvoiceBLL();
                response = ResponseData.SavePaymentcardcashInvoiceDtls(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        #endregion
    }
}
