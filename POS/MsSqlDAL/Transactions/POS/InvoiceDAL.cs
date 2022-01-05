using EasyBizAbsDAL.Transactions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MsSqlDAL.Transactions.POS
{
    public class InvoiceDAL : BaseInvoiceDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        #region SaveRecord
        public override SaveInvoiceResponse SaveSalesRecord(SaveInvoiceRequest RequestObj)
        {
            var RequestData = (SaveInvoiceRequest)RequestObj;
            var ResponseData = new SaveInvoiceResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_SaveInsertInvoiceInfo", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.InvoiceHeaderData.CountryID);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.InvoiceHeaderData.StoreID);
                _CommandObj.Parameters.AddWithValue("@PosID", RequestData.InvoiceHeaderData.PosID);
                _CommandObj.Parameters.AddWithValue("@BusinessDate", RequestData.InvoiceHeaderData.BusinessDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceHeaderData.InvoiceNo);
                _CommandObj.Parameters.AddWithValue("@CustomerGroupID", RequestData.InvoiceHeaderData.CustomerGroupID);
                _CommandObj.Parameters.AddWithValue("@CustomerID", RequestData.InvoiceHeaderData.CustomerID);
                _CommandObj.Parameters.AddWithValue("@TotalQty", RequestData.InvoiceHeaderData.TotalQty);
                _CommandObj.Parameters.AddWithValue("@SubTotalAmount", RequestData.InvoiceHeaderData.SubTotalAmount);
                _CommandObj.Parameters.AddWithValue("@TaxID", RequestData.InvoiceHeaderData.TaxID);
                _CommandObj.Parameters.AddWithValue("@TaxAmount", RequestData.InvoiceHeaderData.TaxAmount);
                _CommandObj.Parameters.AddWithValue("@SubTotalWithTaxAmount", RequestData.InvoiceHeaderData.SubTotalWithTaxAmount);
                _CommandObj.Parameters.AddWithValue("@TotalDiscountType", RequestData.InvoiceHeaderData.TotalDiscountType);
                _CommandObj.Parameters.AddWithValue("@TotalDiscountAmount", RequestData.InvoiceHeaderData.TotalDiscountAmount);
                //_CommandObj.Parameters.AddWithValue("@TotalDiscountPercentage", RequestData.InvoiceHeaderData.TotalDiscountPercentage);
                _CommandObj.Parameters.AddWithValue("@NetAmount", RequestData.InvoiceHeaderData.NetAmount);
                _CommandObj.Parameters.AddWithValue("@ReceivedAmount", RequestData.InvoiceHeaderData.ReceivedAmount);
                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceHeaderData.ReturnAmount);
                _CommandObj.Parameters.AddWithValue("@SalesStatus", RequestData.InvoiceHeaderData.SalesStatus);
                _CommandObj.Parameters.AddWithValue("@AppliedPriceListID", RequestData.InvoiceHeaderData.AppliedPriceListID);
                _CommandObj.Parameters.AddWithValue("@AppliedCustomerSpecialPricesID", RequestData.InvoiceHeaderData.AppliedCustomerSpecialPriceID);
                _CommandObj.Parameters.AddWithValue("@AppliedPromotionID", RequestData.InvoiceHeaderData.AppliedPromotionID);
                _CommandObj.Parameters.AddWithValue("@SalesEmployeeID", RequestData.InvoiceHeaderData.SalesEmployeeID);
                _CommandObj.Parameters.AddWithValue("@SalesManagerID", RequestData.InvoiceHeaderData.SalesManagerID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.InvoiceHeaderData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.InvoiceHeaderData.ShiftID);
                _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.InvoiceHeaderData.CashierID);
                _CommandObj.Parameters.AddWithValue("@IsCreditSale", RequestData.InvoiceHeaderData.IsCreditSale);

                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.InvoiceHeaderData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.InvoiceHeaderData.StoreCode);
                _CommandObj.Parameters.AddWithValue("@PosCode", RequestData.InvoiceHeaderData.PosCode);
                _CommandObj.Parameters.AddWithValue("@SalesEmployeeCode", RequestData.InvoiceHeaderData.SalesEmployeeCode);
                _CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.InvoiceHeaderData.CustomerCode);
                _CommandObj.Parameters.AddWithValue("@DiscountRemarks", RequestData.InvoiceHeaderData.DiscountRemarks);

                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                // "Senthamil_Changes"
                {
                    _CommandObj.Parameters.AddWithValue("@CouponID", RequestData.InvoiceHeaderData.CouponID);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponCode", RequestData.InvoiceHeaderData.RedeemCouponCode);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponLineNo", RequestData.InvoiceHeaderData.RedeemCouponLineNo);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponSerialCode", RequestData.InvoiceHeaderData.RedeemCouponSerialCode);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponDiscountType", RequestData.InvoiceHeaderData.RedeemCouponDiscountType);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponDiscountValue", RequestData.InvoiceHeaderData.RedeemCouponDiscountValue);
                    _CommandObj.Parameters.AddWithValue("@RedeemValue", RequestData.InvoiceHeaderData.RedeemValue);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponCode", RequestData.InvoiceHeaderData.IssuedCouponCode);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponLineNo", RequestData.InvoiceHeaderData.IssuedCouponLineNo);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponSerialCode", RequestData.InvoiceHeaderData.IssuedCouponSerialCode);
                    _CommandObj.Parameters.AddWithValue("@CustomerName", RequestData.InvoiceHeaderData.CustomerName);
                    _CommandObj.Parameters.AddWithValue("@CustomerMobileNo", RequestData.InvoiceHeaderData.CustomerMobileNo);
                }




                // _CommandObj.Parameters.AddWithValue("@DocumentNo", RequestData.InvoiceHeaderData.DocumentNo);

                //_CommandObj.Parameters.AddWithValue("@SubTotalWithOutDiscount", RequestData.InvoiceHeaderData.SubTotalWithOutDiscount);

                var InvoiceDetail = _CommandObj.Parameters.Add("@InvoiceDetails", SqlDbType.Xml);
                InvoiceDetail.Direction = ParameterDirection.Input;
                InvoiceDetail.Value = InvoiceDetailXML(RequestData.InvoiceDetailList, RequestData.InvoiceHeaderData.InvoiceNo, RequestData.InvoiceHeaderData.TotalDiscountPercentage);

                //var CashDetails = _CommandObj.Parameters.Add("@CashDetails", SqlDbType.Xml);
                //CashDetails.Direction = ParameterDirection.Input;
                //CashDetails.Value = CashDetailXML(RequestData.PaymentList);

                //var CardDetails = _CommandObj.Parameters.Add("@CardDetails", SqlDbType.Xml);
                //CardDetails.Direction = ParameterDirection.Input;
                //CardDetails.Value = CardDetailXML(RequestData.PaymentList);

                var TransactionLog = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLog.Direction = ParameterDirection.Input;
                if (RequestData.TransactionLogList != null && RequestData.TransactionLogList.Count > 0)
                {
                    TransactionLog.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);
                }
                else
                {
                    TransactionLog.Value = string.Empty;
                }

                var SalesOrderDocumentNo = _CommandObj.Parameters.Add("@SalesOrderDocumentNo", SqlDbType.VarChar);
                SalesOrderDocumentNo.Direction = ParameterDirection.Input;
                if (RequestData.SalesOrderDocumentNo != null)
                {
                    SalesOrderDocumentNo.Value = RequestData.SalesOrderDocumentNo;
                }
                else
                {
                    SalesOrderDocumentNo.Value = string.Empty;
                }

                _CommandObj.Parameters.AddWithValue("@BeforeRoundOffAmount", RequestData.InvoiceHeaderData.BeforeRoundOffAmount);
                _CommandObj.Parameters.AddWithValue("@RoundOffAmount", RequestData.InvoiceHeaderData.RoundOffAmount);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ReturnID", SqlDbType.BigInt);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.InvoiceDetailList != null && RequestData.InvoiceDetailList.Count > 0)
                {
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();

                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = Convert.ToInt32(ID.Value).ToString();
                        ResponseData.InvoiceHeaderID = Convert.ToInt64(ID.Value);
                    }
                    else if (strStatusCode == "2")
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                        ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.ExceptionMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.StackTrace = Convert.ToString(StatusMsg.Value);
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.ExceptionMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.StackTrace = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = Convert.ToString(ex.Message);
                ResponseData.ExceptionMessage = Convert.ToString(ex.Message);
                ResponseData.StackTrace = Convert.ToString(ex.StackTrace);
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
                PaymentProcesserSave(RequestObj);
            }
            return ResponseData;
        }
        #endregion SaveRecord


        #region SaveCashandCardRecord
        public override SaveInvoiceResponse SaveCashandCardRecord(SaveInvoiceRequest RequestObj)
        {
            var RequestData = (SaveInvoiceRequest)RequestObj;
            var ResponseData = new SaveInvoiceResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_SaveCashandcardInvoiceDtls", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceHeaderData.InvoiceNo);

                var CashDetails = _CommandObj.Parameters.Add("@CashDetails", SqlDbType.Xml);
                CashDetails.Direction = ParameterDirection.Input;
                CashDetails.Value = CashDetailXML(RequestData.PaymentList);

                var CardDetails = _CommandObj.Parameters.Add("@CardDetails", SqlDbType.Xml);
                CardDetails.Direction = ParameterDirection.Input;
                CardDetails.Value = CardDetailXML(RequestData.PaymentList);

                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceHeaderData.ReturnAmount);


                //_CommandObj.Parameters.AddWithValue("@CountryID", RequestData.InvoiceHeaderData.CountryID);
                //_CommandObj.Parameters.AddWithValue("@StoreID", RequestData.InvoiceHeaderData.StoreID);
                //_CommandObj.Parameters.AddWithValue("@PosID", RequestData.InvoiceHeaderData.PosID);
                //_CommandObj.Parameters.AddWithValue("@BusinessDate", RequestData.InvoiceHeaderData.BusinessDate);
                // _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceHeaderData.InvoiceNo);
                // _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.InvoiceHeaderData.in);
                //_CommandObj.Parameters.AddWithValue("@CustomerID", RequestData.InvoiceHeaderData.CustomerID);
                //_CommandObj.Parameters.AddWithValue("@TotalQty", RequestData.InvoiceHeaderData.TotalQty);
                //_CommandObj.Parameters.AddWithValue("@SubTotalAmount", RequestData.InvoiceHeaderData.SubTotalAmount);
                //_CommandObj.Parameters.AddWithValue("@TaxID", RequestData.InvoiceHeaderData.TaxID);
                //_CommandObj.Parameters.AddWithValue("@TaxAmount", RequestData.InvoiceHeaderData.TaxAmount);
                //_CommandObj.Parameters.AddWithValue("@SubTotalWithTaxAmount", RequestData.InvoiceHeaderData.SubTotalWithTaxAmount);
                //_CommandObj.Parameters.AddWithValue("@TotalDiscountType", RequestData.InvoiceHeaderData.TotalDiscountType);
                //_CommandObj.Parameters.AddWithValue("@TotalDiscountAmount", RequestData.InvoiceHeaderData.TotalDiscountAmount);
                ////_CommandObj.Parameters.AddWithValue("@TotalDiscountPercentage", RequestData.InvoiceHeaderData.TotalDiscountPercentage);
                //_CommandObj.Parameters.AddWithValue("@NetAmount", RequestData.InvoiceHeaderData.NetAmount);
                //_CommandObj.Parameters.AddWithValue("@ReceivedAmount", RequestData.InvoiceHeaderData.ReceivedAmount);
                //_CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceHeaderData.ReturnAmount);
                //_CommandObj.Parameters.AddWithValue("@SalesStatus", RequestData.InvoiceHeaderData.SalesStatus);
                //_CommandObj.Parameters.AddWithValue("@AppliedPriceListID", RequestData.InvoiceHeaderData.AppliedPriceListID);
                //_CommandObj.Parameters.AddWithValue("@AppliedCustomerSpecialPricesID", RequestData.InvoiceHeaderData.AppliedCustomerSpecialPriceID);
                //_CommandObj.Parameters.AddWithValue("@AppliedPromotionID", RequestData.InvoiceHeaderData.AppliedPromotionID);
                //_CommandObj.Parameters.AddWithValue("@SalesEmployeeID", RequestData.InvoiceHeaderData.SalesEmployeeID);
                //_CommandObj.Parameters.AddWithValue("@SalesManagerID", RequestData.InvoiceHeaderData.SalesManagerID);
                //_CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.InvoiceHeaderData.CreateBy);
                //_CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.InvoiceHeaderData.ShiftID);
                //_CommandObj.Parameters.AddWithValue("@CashierID", RequestData.InvoiceHeaderData.CashierID);

                //_CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.InvoiceHeaderData.CountryCode);
                //_CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.InvoiceHeaderData.StoreCode);
                //_CommandObj.Parameters.AddWithValue("@PosCode", RequestData.InvoiceHeaderData.PosCode);
                //_CommandObj.Parameters.AddWithValue("@SalesEmployeeCode", RequestData.InvoiceHeaderData.SalesEmployeeCode);
                //_CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.InvoiceHeaderData.CustomerCode);
                //_CommandObj.Parameters.AddWithValue("@DiscountRemarks", RequestData.InvoiceHeaderData.DiscountRemarks);

                //_CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                //_CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                // _CommandObj.Parameters.AddWithValue("@DocumentNo", RequestData.InvoiceHeaderData.DocumentNo);

                //_CommandObj.Parameters.AddWithValue("@SubTotalWithOutDiscount", RequestData.InvoiceHeaderData.SubTotalWithOutDiscount);

                //var InvoiceDetail = _CommandObj.Parameters.Add("@InvoiceDetails", SqlDbType.Xml);
                //InvoiceDetail.Direction = ParameterDirection.Input;
                //InvoiceDetail.Value = InvoiceDetailXML(RequestData.InvoiceDetailList, RequestData.InvoiceHeaderData.InvoiceNo, RequestData.InvoiceHeaderData.TotalDiscountPercentage);



                //var TransactionLog = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                //TransactionLog.Direction = ParameterDirection.Input;
                //if (RequestData.TransactionLogList != null && RequestData.TransactionLogList.Count > 0)
                //{
                //    TransactionLog.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);
                //}
                //else
                //{
                //    TransactionLog.Value = string.Empty;
                //}

                //var SalesOrderDocumentNo = _CommandObj.Parameters.Add("@SalesOrderDocumentNo", SqlDbType.VarChar);
                //SalesOrderDocumentNo.Direction = ParameterDirection.Input;
                //if (RequestData.SalesOrderDocumentNo != null)
                //{
                //    SalesOrderDocumentNo.Value = RequestData.SalesOrderDocumentNo;
                //}
                //else
                //{
                //    SalesOrderDocumentNo.Value = string.Empty;
                //}

                //_CommandObj.Parameters.AddWithValue("@BeforeRoundOffAmount", RequestData.InvoiceHeaderData.BeforeRoundOffAmount);
                //_CommandObj.Parameters.AddWithValue("@RoundOffAmount", RequestData.InvoiceHeaderData.RoundOffAmount);

                //SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                //StatusCode.Direction = ParameterDirection.Output;

                //SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                //StatusMsg.Direction = ParameterDirection.Output;

                //SqlParameter ID = _CommandObj.Parameters.Add("@ReturnID", SqlDbType.BigInt);
                //ID.Direction = ParameterDirection.Output;

                //_CommandObj.CommandType = CommandType.StoredProcedure;

                /*if (RequestData.InvoiceDetailList != null && RequestData.InvoiceDetailList.Count > 0)
                {*/
                _CommandObj.ExecuteNonQuery();
                ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice");

                //string strStatusCode = StatusCode.Value.ToString();

                //if (strStatusCode == "1")
                //{
                //ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice");
                //ResponseData.StatusCode = Enums.OpStatusCode.Success;
                //ResponseData.IDs = Convert.ToInt32(ID.Value).ToString();
                //ResponseData.InvoiceHeaderID = Convert.ToInt64(ID.Value);
                //}
                //else if (strStatusCode == "2")
                //{
                //    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                //    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
                //}
                //else
                //{
                //    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                //    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                //    ResponseData.ExceptionMessage = Convert.ToString(StatusMsg.Value);
                //    ResponseData.StackTrace = Convert.ToString(StatusMsg.Value);
                //}
                /*}
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.ExceptionMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.StackTrace = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                }*/
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = Convert.ToString(ex.Message);
                ResponseData.ExceptionMessage = Convert.ToString(ex.Message);
                ResponseData.StackTrace = Convert.ToString(ex.StackTrace);
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
                PaymentProcesserSave(RequestObj);
            }
            return ResponseData;
        }
        #endregion SaveRecord
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveInvoiceRequest)RequestObj;
            var ResponseData = new SaveInvoiceResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertInvoiceInfo", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.InvoiceHeaderData.CountryID);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.InvoiceHeaderData.StoreID);
                _CommandObj.Parameters.AddWithValue("@PosID", RequestData.InvoiceHeaderData.PosID);
                _CommandObj.Parameters.AddWithValue("@BusinessDate", RequestData.InvoiceHeaderData.BusinessDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceHeaderData.InvoiceNo);
                _CommandObj.Parameters.AddWithValue("@CustomerGroupID", RequestData.InvoiceHeaderData.CustomerGroupID);
                _CommandObj.Parameters.AddWithValue("@CustomerID", RequestData.InvoiceHeaderData.CustomerID);
                _CommandObj.Parameters.AddWithValue("@TotalQty", RequestData.InvoiceHeaderData.TotalQty);
                _CommandObj.Parameters.AddWithValue("@SubTotalAmount", RequestData.InvoiceHeaderData.SubTotalAmount);
                _CommandObj.Parameters.AddWithValue("@TaxID", RequestData.InvoiceHeaderData.TaxID);
                _CommandObj.Parameters.AddWithValue("@TaxAmount", RequestData.InvoiceHeaderData.TaxAmount);
                _CommandObj.Parameters.AddWithValue("@SubTotalWithTaxAmount", RequestData.InvoiceHeaderData.SubTotalWithTaxAmount);
                _CommandObj.Parameters.AddWithValue("@TotalDiscountType", RequestData.InvoiceHeaderData.TotalDiscountType);
                _CommandObj.Parameters.AddWithValue("@TotalDiscountAmount", RequestData.InvoiceHeaderData.TotalDiscountAmount);
                //_CommandObj.Parameters.AddWithValue("@TotalDiscountPercentage", RequestData.InvoiceHeaderData.TotalDiscountPercentage);
                _CommandObj.Parameters.AddWithValue("@NetAmount", RequestData.InvoiceHeaderData.NetAmount);
                _CommandObj.Parameters.AddWithValue("@ReceivedAmount", RequestData.InvoiceHeaderData.ReceivedAmount);
                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceHeaderData.ReturnAmount);
                _CommandObj.Parameters.AddWithValue("@SalesStatus", RequestData.InvoiceHeaderData.SalesStatus);
                _CommandObj.Parameters.AddWithValue("@AppliedPriceListID", RequestData.InvoiceHeaderData.AppliedPriceListID);
                _CommandObj.Parameters.AddWithValue("@AppliedCustomerSpecialPricesID", RequestData.InvoiceHeaderData.AppliedCustomerSpecialPriceID);
                _CommandObj.Parameters.AddWithValue("@AppliedPromotionID", RequestData.InvoiceHeaderData.AppliedPromotionID);
                _CommandObj.Parameters.AddWithValue("@SalesEmployeeID", RequestData.InvoiceHeaderData.SalesEmployeeID);
                _CommandObj.Parameters.AddWithValue("@SalesManagerID", RequestData.InvoiceHeaderData.SalesManagerID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.InvoiceHeaderData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.InvoiceHeaderData.ShiftID);
                _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.InvoiceHeaderData.CashierID);

                _CommandObj.Parameters.AddWithValue("@IsCreditSale", RequestData.InvoiceHeaderData.IsCreditSale);

                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.InvoiceHeaderData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.InvoiceHeaderData.StoreCode);
                _CommandObj.Parameters.AddWithValue("@PosCode", RequestData.InvoiceHeaderData.PosCode);
                _CommandObj.Parameters.AddWithValue("@SalesEmployeeCode", RequestData.InvoiceHeaderData.SalesEmployeeCode);
                _CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.InvoiceHeaderData.CustomerCode);
                _CommandObj.Parameters.AddWithValue("@DiscountRemarks", RequestData.InvoiceHeaderData.DiscountRemarks);

                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                // "Senthamil_Changes"
                {
                    _CommandObj.Parameters.AddWithValue("@CouponID", RequestData.InvoiceHeaderData.CouponID);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponCode", RequestData.InvoiceHeaderData.RedeemCouponCode);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponLineNo", RequestData.InvoiceHeaderData.RedeemCouponLineNo);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponSerialCode", RequestData.InvoiceHeaderData.RedeemCouponSerialCode);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponDiscountType", RequestData.InvoiceHeaderData.RedeemCouponDiscountType);
                    _CommandObj.Parameters.AddWithValue("@RedeemCouponDiscountValue", RequestData.InvoiceHeaderData.RedeemCouponDiscountValue);
                    _CommandObj.Parameters.AddWithValue("@RedeemValue", RequestData.InvoiceHeaderData.RedeemValue);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponCode", RequestData.InvoiceHeaderData.IssuedCouponCode);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponLineNo", RequestData.InvoiceHeaderData.IssuedCouponLineNo);
                    _CommandObj.Parameters.AddWithValue("@IssuedCouponSerialCode", RequestData.InvoiceHeaderData.IssuedCouponSerialCode);
                    _CommandObj.Parameters.AddWithValue("@CustomerName", RequestData.InvoiceHeaderData.CustomerName);
                    _CommandObj.Parameters.AddWithValue("@CustomerMobileNo", RequestData.InvoiceHeaderData.CustomerMobileNo);
                }

                // _CommandObj.Parameters.AddWithValue("@DocumentNo", RequestData.InvoiceHeaderData.DocumentNo);

                //_CommandObj.Parameters.AddWithValue("@SubTotalWithOutDiscount", RequestData.InvoiceHeaderData.SubTotalWithOutDiscount);

                var InvoiceDetail = _CommandObj.Parameters.Add("@InvoiceDetails", SqlDbType.Xml);
                InvoiceDetail.Direction = ParameterDirection.Input;
                InvoiceDetail.Value = InvoiceDetailXML(RequestData.InvoiceDetailList, RequestData.InvoiceHeaderData.InvoiceNo, RequestData.InvoiceHeaderData.TotalDiscountPercentage);

                var CashDetails = _CommandObj.Parameters.Add("@CashDetails", SqlDbType.Xml);
                CashDetails.Direction = ParameterDirection.Input;
                CashDetails.Value = CashDetailXML(RequestData.PaymentList);

                var CardDetails = _CommandObj.Parameters.Add("@CardDetails", SqlDbType.Xml);
                CardDetails.Direction = ParameterDirection.Input;
                CardDetails.Value = CardDetailXML(RequestData.PaymentList);

                var TransactionLog = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLog.Direction = ParameterDirection.Input;
                if (RequestData.TransactionLogList != null && RequestData.TransactionLogList.Count > 0)
                {
                    TransactionLog.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);
                }
                else
                {
                    TransactionLog.Value = string.Empty;
                }

                var SalesOrderDocumentNo = _CommandObj.Parameters.Add("@SalesOrderDocumentNo", SqlDbType.VarChar);
                SalesOrderDocumentNo.Direction = ParameterDirection.Input;
                if (RequestData.SalesOrderDocumentNo != null)
                {
                    SalesOrderDocumentNo.Value = RequestData.SalesOrderDocumentNo;
                }
                else
                {
                    SalesOrderDocumentNo.Value = string.Empty;
                }

                _CommandObj.Parameters.AddWithValue("@BeforeRoundOffAmount", RequestData.InvoiceHeaderData.BeforeRoundOffAmount);
                _CommandObj.Parameters.AddWithValue("@RoundOffAmount", RequestData.InvoiceHeaderData.RoundOffAmount);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ReturnID", SqlDbType.BigInt);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.InvoiceDetailList != null && RequestData.InvoiceDetailList.Count > 0)
                {
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();

                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = Convert.ToInt32(ID.Value).ToString();
                        ResponseData.InvoiceHeaderID = Convert.ToInt64(ID.Value);
                    }
                    else if (strStatusCode == "2")
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                        ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.ExceptionMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.StackTrace = Convert.ToString(StatusMsg.Value);
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.ExceptionMessage = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                    ResponseData.StackTrace = "Detail list not found on " + RequestData.InvoiceHeaderData.InvoiceNo;
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = Convert.ToString(ex.Message);
                ResponseData.ExceptionMessage = Convert.ToString(ex.Message);
                ResponseData.StackTrace = Convert.ToString(ex.StackTrace);
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
                PaymentProcesserSave(RequestObj);
            }
            return ResponseData;
        }

        public void PaymentProcesserSave(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveInvoiceRequest)RequestObj;
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertPaymentProcesserLog", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                SqlParameter PaymentProcessorList1 = _CommandObj.Parameters.Add("@PaymentProcessor", SqlDbType.Xml);
                PaymentProcessorList1.Direction = ParameterDirection.Input;
                PaymentProcessorList1.Value = PaymentProcessorXML(RequestData.PaymentProcessorList);

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }

        public string InvoiceDetailXML(List<InvoiceDetails> InvoiceDetailList, string InvoiceNo, Decimal TotalDiscountPercentage)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (InvoiceDetails objInvoiceDetail in InvoiceDetailList)
            {
                sSql.Append("<InvoiceDetail>");
                sSql.Append("<ID>" + (objInvoiceDetail.ID) + "</ID>");
                sSql.Append("<SerialNo>" + objInvoiceDetail.SerialNo + "</SerialNo>");
                sSql.Append("<InvoiceHeaderID>" + objInvoiceDetail.InvoiceHeaderID + "</InvoiceHeaderID>");
                sSql.Append("<SKUID>" + (objInvoiceDetail.SKUID) + "</SKUID>");
                sSql.Append("<SKUCode>" + (objInvoiceDetail.SKUCode) + "</SKUCode>");
                sSql.Append("<BrandID>" + objInvoiceDetail.BrandID + "</BrandID>");
                sSql.Append("<SubBrandID>" + objInvoiceDetail.SubBrandID + "</SubBrandID>");
                sSql.Append("<Category>" + objInvoiceDetail.Category + "</Category>");
                sSql.Append("<AppliedPriceListID>" + objInvoiceDetail.AppliedPriceListID + "</AppliedPriceListID>");
                sSql.Append("<Qty>" + objInvoiceDetail.Qty + "</Qty>");

                sSql.Append("<Price>" + objInvoiceDetail.Price + "</Price>");
                sSql.Append("<LineTotal>" + objInvoiceDetail.LineTotal + "</LineTotal>");
                sSql.Append("<SellingPrice>" + objInvoiceDetail.SellingPrice + "</SellingPrice>");
                sSql.Append("<SellingLineTotal>" + objInvoiceDetail.SellingLineTotal + "</SellingLineTotal>");

                sSql.Append("<DiscountType>" + objInvoiceDetail.DiscountType + "</DiscountType>");
                sSql.Append("<DiscountAmount>" + objInvoiceDetail.DiscountAmount + "</DiscountAmount>");
                sSql.Append("<AppliedCustomerSpecialPricesID>" + objInvoiceDetail.AppliedCustomerSpecialPricesID + "</AppliedCustomerSpecialPricesID>");
                sSql.Append("<AppliedPromotionID>" + objInvoiceDetail.AppliedPromotionID + "</AppliedPromotionID>");

                sSql.Append("<CountryID>" + objInvoiceDetail.CountryID + "</CountryID>");
                sSql.Append("<StoreID>" + objInvoiceDetail.StoreID + "</StoreID>");
                sSql.Append("<PosID>" + objInvoiceDetail.PosID + "</PosID>");
                sSql.Append("<TaxID>" + objInvoiceDetail.TaxID + "</TaxID>");
                sSql.Append("<TaxAmount>" + objInvoiceDetail.TaxAmount + "</TaxAmount>");
                sSql.Append("<IsExchange>" + objInvoiceDetail.IsExchanged + "</IsExchange>");

                sSql.Append("<CountryCode>" + objInvoiceDetail.CountryCode + "</CountryCode>");
                sSql.Append("<StoreCode>" + objInvoiceDetail.StoreCode + "</StoreCode>");
                sSql.Append("<PosCode>" + objInvoiceDetail.PosCode + "</PosCode>");
                sSql.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>");
                sSql.Append("<InvoiceType>" + objInvoiceDetail.InvoiceType + "</InvoiceType>");

                sSql.Append("<EmployeeDiscountID>" + objInvoiceDetail.EmployeeDiscountID + "</EmployeeDiscountID>");
                sSql.Append("<FamilyDiscountAmount>" + objInvoiceDetail.FamilyDiscountAmount + "</FamilyDiscountAmount>");
                sSql.Append("<EmployeeDiscountAmount>" + objInvoiceDetail.EmployeeDiscountAmount + "</EmployeeDiscountAmount>");
                sSql.Append("<DiscountRemarks>" + objInvoiceDetail.DiscountRemarks + "</DiscountRemarks>");
                sSql.Append("<SpecialDiscountType>" + objInvoiceDetail.SpecialDiscountType + "</SpecialDiscountType>");
                sSql.Append("<SpecialPromoDiscountPercentage>" + objInvoiceDetail.SpecialPromoDiscountPercentage + "</SpecialPromoDiscountPercentage>");
                sSql.Append("<SpecialPromoDiscount>" + objInvoiceDetail.SpecialPromoDiscount + "</SpecialPromoDiscount>");
                sSql.Append("<Tag_Id>" + objInvoiceDetail.Tag_Id + "</Tag_Id>");
                sSql.Append("<PromoGroupID>" + objInvoiceDetail.PromoGroupID + "</PromoGroupID>");
                sSql.Append("<IsCombo>" + objInvoiceDetail.IsCombo + "</IsCombo>");
                sSql.Append("<IsHeader>" + objInvoiceDetail.IsHeader + "</IsHeader>");
                sSql.Append("<ComboGroupID>" + objInvoiceDetail.ComboGroupID + "</ComboGroupID>");
                sSql.Append("<IsGift>" + objInvoiceDetail.IsGift + "</IsGift>");
                sSql.Append("</InvoiceDetail>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString().Replace("&", "&#38;");
        }

        public string CashDetailXML(List<PaymentDetail> PaymentList)
        {
            StringBuilder sSql = new StringBuilder();
            var CashList = new List<PaymentDetail>();

            if (PaymentList != null && PaymentList.Count > 0)
            {
                CashList = PaymentList.Where(x => x.Mode == "Cash").ToList();
                if (CashList != null && CashList.Count > 0)
                {
                    foreach (PaymentDetail objInvoiceCash in CashList)
                    {
                        sSql.Append("<CashDetails>");
                        sSql.Append("<ID>" + (objInvoiceCash.ID) + "</ID>");
                        sSql.Append("<AppliCationDate>" + objInvoiceCash.BusinessDate + "</AppliCationDate>");
                        //sSql.Append("<InVoiceNumber>" + objInvoiceCash.InvoiceNumber + "</InVoiceNumber>");
                        sSql.Append("<PaymentCurrency>" + (objInvoiceCash.PayCurrency) + "</PaymentCurrency>");
                        sSql.Append("<PaymentCurrencyID>" + (objInvoiceCash.PayCurrencyID) + "</PaymentCurrencyID>");
                        sSql.Append("<ChangeCurrencyID>" + objInvoiceCash.ChangeCurrencyID + "</ChangeCurrencyID>");
                        sSql.Append("<ChangeCurrency>" + objInvoiceCash.ChangeCurrency + "</ChangeCurrency>");
                        sSql.Append("<ReceivedAmount>" + objInvoiceCash.Receivedamount + "</ReceivedAmount>");
                        sSql.Append("<FromCountryID>" + objInvoiceCash.FromCountryID + "</FromCountryID>");
                        sSql.Append("<FromStoreID>" + objInvoiceCash.FromStoreID + "</FromStoreID>");
                        sSql.Append("<CreatedBy>" + objInvoiceCash.CreateBy + "</CreatedBy>");
                        sSql.Append("</CashDetails>");
                    }
                }
            }
            return sSql.ToString().Replace("&", "&#38;");
        }

        public string PaymentProcessorXML(List<PaymentProcessor> PaymentProcessorList)
        {
            StringBuilder sSql = new StringBuilder();
            var PaymentProcessList = new List<PaymentProcessor>();
            if (PaymentProcessorList != null && PaymentProcessorList.Count > 0)
            {
                PaymentProcessList = PaymentProcessorList.ToList();
                if (PaymentProcessList != null && PaymentProcessList.Count > 0)
                {
                    foreach (PaymentProcessor objPaymentProcess in PaymentProcessList)
                    {
                        sSql.Append("<PaymentProcessorData>");
                        sSql.Append("<CardNumber>" + objPaymentProcess.CardNumber + "</CardNumber>");
                        sSql.Append("<RRN>" + objPaymentProcess.RRN + "</RRN>");
                        sSql.Append("<StatusCode>" + objPaymentProcess.StatusCode + "</StatusCode>");
                        sSql.Append("<HostErrorCode>" + (objPaymentProcess.HostErrorCode) + "</HostErrorCode>");
                        sSql.Append("<AuthCode>" + (objPaymentProcess.AuthCode) + "</AuthCode>");
                        sSql.Append("<CardSchemes>" + objPaymentProcess.CardSchemes + "</CardSchemes>");
                        sSql.Append("<Stan>" + objPaymentProcess.Stan + "</Stan>");
                        sSql.Append("<TerminalID>" + objPaymentProcess.TerminalID + "</TerminalID>");
                        sSql.Append("<DateAndTime>" + (objPaymentProcess.DateAndTime) + "</DateAndTime>");
                        sSql.Append("<Amount>" + (objPaymentProcess.Amount) + "</Amount>");
                        sSql.Append("<PosCode>" + (objPaymentProcess.PosCode) + "</PosCode>");
                        sSql.Append("<User>" + (objPaymentProcess.User) + "</User>");
                        sSql.Append("<CustomerMobileNumber>" + (objPaymentProcess.CustomerMobileNumber) + "</CustomerMobileNumber>");
                        sSql.Append("<Currency>" + (objPaymentProcess.Currency) + "</Currency>");
                        sSql.Append("</PaymentProcessorData>");
                    }
                }
            }
            return sSql.ToString().Replace("&", "&#38;");
        }

        public string CardDetailXML(List<PaymentDetail> PaymentList)
        {
            StringBuilder sSql = new StringBuilder();
            var CardList = new List<PaymentDetail>();

            if (PaymentList != null && PaymentList.Count > 0)
            {
                CardList = PaymentList.Where(x => x.Mode != "Cash").ToList();
                if (CardList != null && CardList.Count > 0)
                {
                    foreach (PaymentDetail objInvoiceCard in CardList)
                    {
                        sSql.Append("<CardDetails>");
                        sSql.Append("<ID>" + (objInvoiceCard.ID) + "</ID>");
                        sSql.Append("<AppliCationDate>" + objInvoiceCard.BusinessDate + "</AppliCationDate>");
                        //sSql.Append("<InVoiceNumber>" + objInvoiceCard.InvoiceNumber + "</InVoiceNumber>");
                        sSql.Append("<PaymentCurrency>" + (objInvoiceCard.PayCurrency) + "</PaymentCurrency>");
                        sSql.Append("<PaymentCurrencyID>" + (objInvoiceCard.PayCurrencyID) + "</PaymentCurrencyID>");



                        var PaymentProcessor = objInvoiceCard.IsPaymentProcesser == true ? "1" : "0";

                        sSql.Append("<CardType>" + (objInvoiceCard.CardType2) + "</CardType>");
                        sSql.Append("<CardType2>" + (objInvoiceCard.Mode) + "</CardType2>");
                        sSql.Append("<PaymentProcessor>" + PaymentProcessor + "</PaymentProcessor>");
                        if (objInvoiceCard.CardType2 == "On-Account")
                        {
                            sSql.Append("<ReceivedAmount>" + objInvoiceCard.Receivedamount + "</ReceivedAmount>");
                            sSql.Append("<BalanceAmount>" + objInvoiceCard.BalanceAmountToBePay + "</BalanceAmount>");
                        }
                        else
                        {
                            sSql.Append("<ReceivedAmount>" + objInvoiceCard.Receivedamount + "</ReceivedAmount>");
                            sSql.Append("<BalanceAmount>0</BalanceAmount>");
                        }
                        //if ((objInvoiceCard.Mode != null) && (objInvoiceCard.CardType2 != null))
                        //{
                        //    sSql.Append("<CardType>" + (objInvoiceCard.CardType2) + "</CardType>");
                        //    sSql.Append("<CardType2>" + (objInvoiceCard.Mode) + "</CardType2>");
                        //    sSql.Append("<PaymentProcessor>1</PaymentProcessor>");
                        //}
                        //else
                        //{
                        //    sSql.Append("<CardType>" + (objInvoiceCard.Mode) + "</CardType>");
                        //    sSql.Append("<CardType2>" + (objInvoiceCard.Mode) + "</CardType2>");
                        //    sSql.Append("<PaymentProcessor>0</PaymentProcessor>");
                        //}

                        sSql.Append("<CardNumber>" + objInvoiceCard.CardNo + "</CardNumber>");
                        sSql.Append("<CardHolderName>" + objInvoiceCard.CardHolder + "</CardHolderName>");
                        sSql.Append("<ApprovalNumber>" + objInvoiceCard.ApproveNo + "</ApprovalNumber>");
                        sSql.Append("<FromCountryID>" + objInvoiceCard.FromCountryID + "</FromCountryID>");
                        sSql.Append("<FromStoreID>" + objInvoiceCard.FromCountryID + "</FromStoreID>");
                        sSql.Append("<CreatedBy>" + objInvoiceCard.CreateBy + "</CreatedBy>");

                        sSql.Append("</CardDetails>");
                    }
                }
            }
            return sSql.ToString().Replace("&", "&#38;");
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectByIDInvoiceRequest)RequestObj;
            var ResponseData = new SelectByIDInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = "Select * from InvoiceHeader with(NoLock) ";

                long ID = 0;
                if (RequestData.ID > 0)
                {
                    sSql = sSql + " where ID='{0}'";
                    ID = RequestData.ID;
                    sSql = string.Format(sSql, ID);
                }
                else if (RequestData.DocumentNos != null && RequestData.DocumentNos != string.Empty)
                {
                    sSql = sSql + " where InvoiceNo='{0}'";
                    sSql = string.Format(sSql, RequestData.DocumentNos.Trim());
                }
                else
                {
                    sSql = sSql + " where ID='{0}'";
                    ID = Convert.ToInt64(RequestData.DocumentIDs);
                    sSql = string.Format(sSql, ID);
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;

                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;
                        objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objInvoiceHeaderTypes.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;

                        objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objInvoiceHeaderTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objInvoiceHeaderTypes.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objInvoiceHeaderTypes.SalesEmployeeCode = objReader["SalesEmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["SalesEmployeeCode"]) : string.Empty;
                        objInvoiceHeaderTypes.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;
                        objInvoiceHeaderTypes.BeforeRoundOffAmount = objReader["BeforeRoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BeforeRoundOffAmount"]) : 0;
                        objInvoiceHeaderTypes.RoundOffAmount = objReader["RoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["RoundOffAmount"]) : 0;

                        // "Senthamil_Changes"
                        {
                            objInvoiceHeaderTypes.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponCode = objReader["RedeemCouponCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponLineNo = objReader["RedeemCouponLineNo"] != DBNull.Value ? Convert.ToInt32(objReader["RedeemCouponLineNo"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponSerialCode = objReader["RedeemCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountType = objReader["RedeemCouponDiscountType"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponDiscountType"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountValue = objReader["RedeemCouponDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemCouponDiscountValue"]) : 0;
                            objInvoiceHeaderTypes.RedeemValue = objReader["RedeemValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemValue"]) : 0;
                            objInvoiceHeaderTypes.IssuedCouponCode = objReader["IssuedCouponCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponLineNo = objReader["IssuedCouponLineNo"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponLineNo"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponSerialCode = objReader["IssuedCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerMobileNo = objReader["CustomerMobileNo"] != DBNull.Value ? Convert.ToString(objReader["CustomerMobileNo"]) : string.Empty;
                        }
                        

                        objInvoiceHeaderTypes.InvoiceDetailList = new List<InvoiceDetails>();
                        var objSelectInvoiceDetailsByIDRequest = new SelectInvoiceDetailsListRequest();
                        var objSelectInvoiceDetailsByIDResponse = new SelectInvoiceDetailsListResponse();


                        //SelectInvoiceDetailsListResponse SelectInvoiceDetailsList(SelectInvoiceDetailsListRequest RequestObj)
                        objSelectInvoiceDetailsByIDRequest.SearchString = objInvoiceHeaderTypes.InvoiceNo;
                        objSelectInvoiceDetailsByIDRequest.InvoiceHeaderID = objInvoiceHeaderTypes.ID;
                        objSelectInvoiceDetailsByIDRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectInvoiceDetailsByIDResponse = SelectInvoiceDetailsList(objSelectInvoiceDetailsByIDRequest);
                        if (objSelectInvoiceDetailsByIDResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInvoiceHeaderTypes.InvoiceDetailList = objSelectInvoiceDetailsByIDResponse.InvoiceDetailsList;
                        }

                        objInvoiceHeaderTypes.PaymentList = new List<PaymentDetail>();

                        var objInvoiceCashDetailsDAL = new InvoiceCashDetailsDAL();
                        var objSelectByInvoiceNoCashDetailsRequest = new SelectByInvoiceNoCashDetailsRequest();
                        var objSelectByInvoiceNoCashDetailsResponse = new SelectByInvoiceNoCashDetailsResponse();
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceNo = objInvoiceHeaderTypes.InvoiceNo;
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceHeaderID = objInvoiceHeaderTypes.ID;
                        objSelectByInvoiceNoCashDetailsRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectByInvoiceNoCashDetailsResponse = objInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objSelectByInvoiceNoCashDetailsRequest);

                        if (objSelectByInvoiceNoCashDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInvoiceHeaderTypes.PaymentList.AddRange(objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails);
                        }


                        var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
                        var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
                        var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = objInvoiceHeaderTypes.InvoiceNo;
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = objInvoiceHeaderTypes.ID;
                        objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
                        if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInvoiceHeaderTypes.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
                        }

                        if (RequestData.RequestFrom == Enums.RequestFrom.SyncService && RequestData.FromOrToStoreID == 0)
                        {
                            objInvoiceHeaderTypes.TransactionLogList = TransactionLogList(objInvoiceHeaderTypes.StoreID, objInvoiceHeaderTypes.ID, RequestData);
                        }
                        else
                        {
                            objInvoiceHeaderTypes.TransactionLogList = new List<TransactionLog>();
                        }

                        ResponseData.InvoiceHeaderData = objInvoiceHeaderTypes;
                        ResponseData.ResponseDynamicData = objInvoiceHeaderTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;

        }
        public List<TransactionLog> TransactionLogList(int StoreID, long DocumentID, SelectByIDInvoiceRequest RequestData)
        {
            var _TransactionLogList = new List<TransactionLog>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from TransactionLog with(NoLock) where StoreID={0} and DocumentID={1} and TransactionType='Invoice'";
                sSql = string.Format(sSql, StoreID, DocumentID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTransactionLog.TransactionType = objReader["TransactionType"] != DBNull.Value ? Convert.ToString(objReader["TransactionType"]) : string.Empty;
                        objTransactionLog.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objTransactionLog.ActualDateTime = objReader["ActualDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ActualDateTime"]) : DateTime.Now;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.InQty = objReader["InQty"] != DBNull.Value ? Convert.ToInt32(objReader["InQty"]) : 0;
                        objTransactionLog.OutQty = objReader["OutQty"] != DBNull.Value ? Convert.ToInt32(objReader["OutQty"]) : 0;
                        objTransactionLog.TransactionPrice = objReader["TransactionPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["TransactionPrice"]) : 0;
                        objTransactionLog.Currency = objReader["Currency"] != DBNull.Value ? Convert.ToDecimal(objReader["Currency"]) : 0;
                        objTransactionLog.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;
                        objTransactionLog.DocumentPrice = objReader["DocumentPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["DocumentPrice"]) : 0;
                        objTransactionLog.UserID = objReader["UserID"] != DBNull.Value ? Convert.ToInt32(objReader["UserID"]) : 0;
                        objTransactionLog.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objTransactionLog.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objTransactionLog.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objTransactionLog.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        _TransactionLogList.Add(objTransactionLog);
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<TransactionLog>();
            }
            return _TransactionLogList;
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {

            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectAllInvoiceRequest)RequestObj;
            var ResponseData = new SelectAllInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;

                if (RequestData.BusinessDate != DateTime.MinValue && RequestData.SalesStatus != null && RequestData.SalesStatus != string.Empty)
                {
                    sSql = "SELECT TOP 100 *,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                    sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode ";

                    sSql = sSql + " where ih.BusinessDate='{0}' and ih.SalesStatus='{1}' order by ih.id";

                    string BusinessDate = sqlCommon.GetSQLServerDateString(RequestData.BusinessDate);
                    sSql = string.Format(sSql, BusinessDate, RequestData.SalesStatus);

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                {
                    sSql = "SELECT TOP 20 *,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                    sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode ";

                    if (RequestData.SalesStatus != string.Empty)
                    {
                        sSql = sSql + " and ih.SalesStatus='{0}' and IH.StoreID = '" + RequestData.StoreID + "'";
                    }
                    sSql = sSql + " ORDER BY ih.ID DESC";

                    if (RequestData.SalesStatus != string.Empty)
                    {
                        sSql = string.Format(sSql, RequestData.SalesStatus);
                    }

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.Search)
                {
                    string BusinessDate = sqlCommon.SearchByDate(RequestData.SearchString);
                    _CommandObj = new SqlCommand("[API_SearchInvoice]", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                }
                else if (RequestData.SearchCriteria == "InvoiceNo" && RequestData.SearchString != string.Empty)
                {
                    sSql = "SELECT *,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                    sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode Where InvoiceNo='" + RequestData.SearchString + "'";

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sSql = "SELECT *,um.CustomerName,um.PhoneNumber,cm.countryname,sm.storename,pm.posname FROM InvoiceHeader ih with(NoLock) join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode left join countrymaster cm with(NoLock) on ih.countryid=cm.id left join storemaster sm with(NoLock) on ih.storeid=sm.id ";
                    sSql = sSql + "left join posmaster pm with(NoLock) on ih.posid=pm.id Where ih.Businessdate='" + RequestData.BusinessDate + "' and ih.storeid ='" + RequestData.StoreID + "'";

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else
                {
                    sSql = "SELECT *,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                    sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode";

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;

                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        // "Senthamil_Changes"
                        {
                            objInvoiceHeaderTypes.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponCode = objReader["RedeemCouponCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponLineNo = objReader["RedeemCouponLineNo"] != DBNull.Value ? Convert.ToInt32(objReader["RedeemCouponLineNo"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponSerialCode = objReader["RedeemCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountType = objReader["RedeemCouponDiscountType"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponDiscountType"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountValue = objReader["RedeemCouponDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemCouponDiscountValue"]) : 0;
                            objInvoiceHeaderTypes.RedeemValue = objReader["RedeemValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemValue"]) : 0;
                            objInvoiceHeaderTypes.IssuedCouponCode = objReader["IssuedCouponCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponLineNo = objReader["IssuedCouponLineNo"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponLineNo"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponSerialCode = objReader["IssuedCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerMobileNo = objReader["CustomerMobileNo"] != DBNull.Value ? Convert.ToString(objReader["CustomerMobileNo"]) : string.Empty;
                        }

                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objInvoiceHeaderTypes.PosName = objReader["posname"] != DBNull.Value ? Convert.ToString(objReader["posname"]) : string.Empty;
                            objInvoiceHeaderTypes.CountryName = objReader["countryname"] != DBNull.Value ? Convert.ToString(objReader["countryname"]) : string.Empty;
                            objInvoiceHeaderTypes.StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                            objInvoiceHeaderTypes.SalesEmployeeCode = objReader["SalesEmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["SalesEmployeeCode"]) : string.Empty;
                        }
                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }
                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override SelectInvoiceDetailsListResponse SelectInvoiceDetailsList(SelectInvoiceDetailsListRequest RequestObj)
        {
            var InvoiceDetailMasterList = new List<InvoiceDetails>();
            var RequestData = (SelectInvoiceDetailsListRequest)RequestObj;
            var ResponseData = new SelectInvoiceDetailsListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                //Commented and changed for Exhange with Return.No need the Exchange Invoice Number

                //sSql.Append("select a.*,c.ID as CustomerID,c.CustomerCode,c.CustomerName,b.InvoiceNo,b.DocumentDate,b.NetAmount,sku.StyleCode from InvoiceDetail a with(NoLock) ");
                //sSql.Append("join InvoiceHeader b with(NoLock) on b.ID = a.InvoiceHeaderID ");
                //sSql.Append("left join SKUMaster sku with(NoLock) on a.SKUCode = sku.SKUCode ");
                //sSql.Append("join CustomerMaster c with(NoLock) on c.CustomerCode = b.CustomerCode ");


                sSql.Append("select Distinct a.*,c.ID as CustomerID,c.CustomerCode,c.CustomerName,b.InvoiceNo,b.DocumentDate,b.NetAmount,sku.StyleCode,red.ExchangeSKU ");
                sSql.Append("from InvoiceDetail a with(NoLock) join InvoiceHeader b with(NoLock) on b.ID = a.InvoiceHeaderID ");
                sSql.Append("join CustomerMaster c with(NoLock) on c.CustomerCode = b.CustomerCode ");
                sSql.Append("left join SKUMaster sku with(NoLock) on a.SKUCode = sku.SKUCode ");
                sSql.Append("left join SalesExchangeHeader seh with(NoLock) on seh.SalesInvoiceNumber = a.InvoiceNo ");
                sSql.Append("left join ReturnExchangeDetail red with(NoLock) on a.ID = red.InvoiceDetailID ");


                if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                {
                    sSql.Append("where b.InvoiceNo='" + RequestData.SearchString + "'");
                }
                else
                {
                    sSql.Append("where a.InvoiceHeaderID = " + RequestData.InvoiceHeaderID + "");
                }
                if (RequestData.SalesStatus == null || RequestData.SalesStatus.Trim() == string.Empty)
                {
                    sSql.Append(" and b.SalesStatus='Completed'");
                }
                else
                {
                    sSql.Append(" and b.SalesStatus='" + RequestData.SalesStatus.Trim() + "'");
                }
                sSql.Append(" order by a.SerialNo");

                //sSql.Append("select * from  InvoiceDetail ");
                //sSql.Append("where InvoiceHeaderID = '" + RequestData.InvoiceHeaderID + "' ");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceDetailMaster = new InvoiceDetails();
                        //objInvoiceDetailMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceDetailMaster.InvoiceDetailID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceDetailMaster.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objInvoiceDetailMaster.InvoiceDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        objInvoiceDetailMaster.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceDetailMaster.InvoiceType = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                        objInvoiceDetailMaster.SerialNo = Convert.ToInt32(objReader["SerialNo"]);
                        objInvoiceDetailMaster.SKUID = Convert.ToInt32(objReader["SKUID"]);
                        objInvoiceDetailMaster.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objInvoiceDetailMaster.SKUCode = objReader["SKUCode"].ToString();
                        objInvoiceDetailMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceDetailMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceDetailMaster.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        objInvoiceDetailMaster.Category = Convert.ToInt32(objReader["Category"]);
                        objInvoiceDetailMaster.Qty = Convert.ToInt32(objReader["Qty"]);
                        objInvoiceDetailMaster.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objInvoiceDetailMaster.DiscountType = objReader["DiscountType"].ToString();
                        objInvoiceDetailMaster.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        objInvoiceDetailMaster.LineTotal = objReader["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["LineTotal"]) : 0;
                        objInvoiceDetailMaster.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceDetailMaster.AppliedCustomerSpecialPricesID = objReader["AppliedCustomerSpecialPricesID"].ToString();
                        objInvoiceDetailMaster.AppliedPromotionID = objReader["AppliedPromotionID"].ToString();
                        objInvoiceDetailMaster.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["SalesStatus"]) : true;
                        objInvoiceDetailMaster.ModifiedSalesEmployee = objReader["ModifiedSalesEmployee"].ToString();
                        objInvoiceDetailMaster.ModifiedSalesManager = objReader["ModifiedSalesManager"].ToString();
                        objInvoiceDetailMaster.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceDetailMaster.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceDetailMaster.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
                        objInvoiceDetailMaster.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objInvoiceDetailMaster.SyncFailedReason = objReader["SyncFailedReason"].ToString();
                        objInvoiceDetailMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceDetailMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceDetailMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceDetailMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceDetailMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInvoiceDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objInvoiceDetailMaster.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceDetailMaster.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceDetailMaster.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToString(objReader["AppliedPromotionID"]) : string.Empty;

                        objInvoiceDetailMaster.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;
                        objInvoiceDetailMaster.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;

                        objInvoiceDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        if (RequestData.RequestFrom != Enums.RequestFrom.MainServer)
                        {
                            objInvoiceDetailMaster.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        }
                        objInvoiceDetailMaster.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0; // need to change for Sales exchange
                        objInvoiceDetailMaster.OldExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;

                        objInvoiceDetailMaster.ReturnQty = 0;
                        objInvoiceDetailMaster.OldReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        objInvoiceDetailMaster.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ExchangeRefID"]) : 0;
                        objInvoiceDetailMaster.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ReturnRefID"]) : 0;

                        objInvoiceDetailMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objInvoiceDetailMaster.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objInvoiceDetailMaster.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objInvoiceDetailMaster.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;


                        objInvoiceDetailMaster.EmployeeDiscountID = objReader["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeDiscountID"]) : 0;
                        objInvoiceDetailMaster.FamilyDiscountAmount = objReader["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FamilyDiscountAmount"]) : 0;
                        objInvoiceDetailMaster.EmployeeDiscountAmount = objReader["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["EmployeeDiscountAmount"]) : 0;
                        objInvoiceDetailMaster.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        objInvoiceDetailMaster.SpecialPromoDiscountType = objReader["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader["SpecialPromoDiscountType"]) : string.Empty;
                        objInvoiceDetailMaster.SpecialPromoDiscountPercentage = objReader["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscountPercentage"]) : 0;
                        objInvoiceDetailMaster.SpecialPromoDiscount = objReader["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscount"]) : 0;
                        objInvoiceDetailMaster.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : string.Empty;
                        objInvoiceDetailMaster.PromoGroupID = objReader["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["PromoGroupID"]) : 0;
                        if (objInvoiceDetailMaster.OldExchangeQty > 0)
                        {
                            objInvoiceDetailMaster.ExchangedSKU = objReader["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader["ExchangeSKU"]) : string.Empty;
                        }
                        else
                        {
                            objInvoiceDetailMaster.ExchangedSKU = string.Empty;
                        }
                        //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        //{

                        objInvoiceDetailMaster.PaymentList = new List<PaymentDetail>();

                        var objInvoiceCashDetailsDAL = new InvoiceCashDetailsDAL();
                        var objSelectByInvoiceNoCashDetailsRequest = new SelectByInvoiceNoCashDetailsRequest();
                        var objSelectByInvoiceNoCashDetailsResponse = new SelectByInvoiceNoCashDetailsResponse();
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceHeaderID = objInvoiceDetailMaster.InvoiceHeaderID;
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceNo = objInvoiceDetailMaster.InvoiceNo;
                        objSelectByInvoiceNoCashDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectByInvoiceNoCashDetailsResponse = objInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objSelectByInvoiceNoCashDetailsRequest);

                        if (objSelectByInvoiceNoCashDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInvoiceDetailMaster.PaymentList.AddRange(objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails);
                        }


                        var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
                        var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
                        var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = objInvoiceDetailMaster.InvoiceHeaderID;
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = objInvoiceDetailMaster.InvoiceNo;
                        objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
                        if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInvoiceDetailMaster.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
                        }
                        //}

                        string ReturnRemarks = string.Empty;
                        if (objInvoiceDetailMaster.ExchangeQty > 0)
                        {
                            ReturnRemarks = objInvoiceDetailMaster.ExchangeQty + " items are exchanged this sales.";
                        }
                        if (objInvoiceDetailMaster.OldReturnQty > 0)
                        {
                            ReturnRemarks = ReturnRemarks + objInvoiceDetailMaster.OldReturnQty + " items are already returned this sales.";
                        }
                        objInvoiceDetailMaster.ReturnRemarks = ReturnRemarks;
                        objInvoiceDetailMaster.ExchangeRemarks = ReturnRemarks;

                        InvoiceDetailMasterList.Add(objInvoiceDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceDetailsList = InvoiceDetailMasterList;

                    if (RequestData.SalesStatus == "ParkSale")
                    {
                        SqlConnection con = new SqlConnection();
                        sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                        string sql = "Delete from InvoiceHeader where ID={0}; Delete from InvoiceDetail where InvoiceHeaderID={0}";

                        sql = string.Format(sql, RequestData.InvoiceHeaderID);
                        SqlCommand cmd;

                        cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleWithScaleMaster");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override SelectInvoiceDetailsByIDResponse SelectInvoiceDetailsByID(SelectInvoiceDetailsByIDRequest RequestObj)
        {
            var InvoiceDetailList = new List<InvoiceDetails>();
            var RequestData = (SelectInvoiceDetailsByIDRequest)RequestObj;
            var ResponseData = new SelectInvoiceDetailsByIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                sSql.Append("Select id.*,sed.SKUCode as ExchangedSKU,srd.ItemCode as  ReurnedSKU from InvoiceDetail id with(NoLock) ");
                sSql.Append("left join SalesExchangeDetail sed with(NoLock) on id.ID=sed.InvoiceDetailID ");
                sSql.Append("left join SalesReturnDetail srd with(NoLock) on id.ID=srd.InvoiceDetailID ");

                if (RequestData.ID > 0)
                {
                    sSql.Append(" where id.InvoiceHeaderID=" + RequestData.ID);
                }


                sSql.Append(" order by SerialNo");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceDetailMaster = new InvoiceDetails();
                        objInvoiceDetailMaster.InvoiceDetailID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceDetailMaster.SerialNo = Convert.ToInt32(objReader["SerialNo"]);
                        objInvoiceDetailMaster.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objInvoiceDetailMaster.SKUCode = objReader["ItemCode"].ToString();
                        objInvoiceDetailMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceDetailMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceDetailMaster.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        objInvoiceDetailMaster.Category = Convert.ToInt32(objReader["Category"]);
                        objInvoiceDetailMaster.Qty = Convert.ToInt32(objReader["Qty"]);
                        objInvoiceDetailMaster.Price = Convert.ToDecimal(objReader["Price"]);
                        objInvoiceDetailMaster.DiscountType = objReader["DiscountType"].ToString();
                        objInvoiceDetailMaster.DiscountAmount = Convert.ToDecimal(objReader["DiscountAmount"]);
                        objInvoiceDetailMaster.LineTotal = Convert.ToDecimal(objReader["LineTotal"]);
                        objInvoiceDetailMaster.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceDetailMaster.AppliedCustomerSpecialPricesID = objReader["AppliedCustomerSpecialPricesCode"].ToString();
                        objInvoiceDetailMaster.AppliedPromotionID = objReader["AppliedPromotionCode"].ToString();
                        objInvoiceDetailMaster.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["SalesStatus"]) : true;
                        objInvoiceDetailMaster.ModifiedSalesEmployee = objReader["ModifiedSalesEmployee"].ToString();
                        objInvoiceDetailMaster.ModifiedSalesManager = objReader["ModifiedSalesManager"].ToString();
                        objInvoiceDetailMaster.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceDetailMaster.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceDetailMaster.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
                        objInvoiceDetailMaster.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objInvoiceDetailMaster.SyncFailedReason = objReader["SyncFailedReason"].ToString();
                        objInvoiceDetailMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceDetailMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceDetailMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceDetailMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceDetailMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInvoiceDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceDetailMaster.IsExchanged = objReader["IsExchanged"] != DBNull.Value ? Convert.ToBoolean(objReader["IsExchanged"]) : false;
                        objInvoiceDetailMaster.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
                        objInvoiceDetailMaster.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ExchangeRefID"]) : 0;
                        objInvoiceDetailMaster.ExchangedSKU = objReader["ExchangedSKU"] != DBNull.Value ? Convert.ToString(objReader["ExchangedSKU"]) : string.Empty;

                        objInvoiceDetailMaster.IsReturned = objReader["IsReturned"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturned"]) : false;
                        objInvoiceDetailMaster.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        objInvoiceDetailMaster.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ReturnRefID"]) : 0;
                        objInvoiceDetailMaster.ReturnedSKU = objReader["ReturnedSKU"] != DBNull.Value ? Convert.ToString(objReader["ReturnedSKU"]) : string.Empty;

                        objInvoiceDetailMaster.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;

                        objInvoiceDetailMaster.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceDetailMaster.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;

                        objInvoiceDetailMaster.ReturnQty = 0;
                        objInvoiceDetailMaster.IsExchanged = false;

                        objInvoiceDetailMaster.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;
                        objInvoiceDetailMaster.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;

                        objInvoiceDetailMaster.SpecialPromoDiscountType = objReader["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader["SpecialPromoDiscountType"]) : string.Empty;
                        objInvoiceDetailMaster.SpecialPromoDiscountPercentage = objReader["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscountPercentage"]) : 0;
                        objInvoiceDetailMaster.SpecialPromoDiscount = objReader["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscount"]) : 0;

                        if (objInvoiceDetailMaster.IsExchanged)
                        {
                            objInvoiceDetailMaster.ExchangeRemarks = "Exchange is already done this item.";
                        }
                        if (objInvoiceDetailMaster.IsReturned)
                        {
                            objInvoiceDetailMaster.ExchangeRemarks = "Return is already done this item.";
                        }

                        InvoiceDetailList.Add(objInvoiceDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceDetailByIDList = InvoiceDetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleWithScaleMaster");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest RequestObj)
        {
            var RequestData = (UpdateInvoiceStatusRequest)RequestObj;
            var ResponseData = new UpdateInvoiceStatusResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Delete from InvoiceHeader where ID={0} and StoreID={1}; Delete from InvoiceDetail where InvoiceHeaderID={0} and StoreID={1}";

                sSql = string.Format(sSql, RequestData.InvoiceID, RequestData.StoreID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();

                int strStatusCode = _CommandObj.ExecuteNonQuery();
                if (strStatusCode == 1)
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Hold Invoice");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override DeleteHoldSaleRecordsResponse DeleteHoldSaleRecords(DeleteHoldSaleRecordsRequest RequestObj)
        {
            var RequestData = (DeleteHoldSaleRecordsRequest)RequestObj;
            var ResponseData = new DeleteHoldSaleRecordsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("DELETE FROM InvoiceDetail WHERE InvoiceHeaderID in(select ID from InvoiceHeader where BusinessDate='{0}' and StoreID={1} and SalesStatus in('ParkSale','Resale'));");
                sbSql.Append("DELETE FROM InvoiceHeader where BusinessDate='{0}' and StoreID={1} and SalesStatus in('ParkSale','Resale');");

                String BusinessDate = sqlCommon.GetSQLServerDateString(RequestData.BusinessDate);
                sSql = string.Format(sbSql.ToString(), BusinessDate, RequestData.StoreID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);

                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();

                _CommandObj.ExecuteNonQuery();

                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Hold Invoice");
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Hold Invoice");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectHoldReceiptByInvoiceNumResponse GetHoldReceipt(SelectHoldReceiptByInvoiceNumRequest RequestObj)
        {
            var HoldReceiptList = new List<HoldReceipt>();
            var RequestData = (SelectHoldReceiptByInvoiceNumRequest)RequestObj;
            var ResponseData = new SelectHoldReceiptByInvoiceNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetHoldDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objHoldReceipt = new HoldReceipt();


                        objHoldReceipt.ShopName = objReader["ShopName"].ToString();
                        objHoldReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objHoldReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objHoldReceipt.ItemCode = objReader["ItemCode"].ToString();
                        objHoldReceipt.Cashier = objReader["Cashier"].ToString();
                        objHoldReceipt.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objHoldReceipt.POSName = objReader["POSName"].ToString();
                        objHoldReceipt.Footer = objReader["Footer"].ToString();
                        objHoldReceipt.ArabicDetails = objReader["ArabicDetails"].ToString();
                        objHoldReceipt.Date = objReader["Date"] != DBNull.Value ? Convert.ToDateTime(objReader["Date"]) : DateTime.Now;
                        objHoldReceipt.Time = objReader["Time"] != DBNull.Value ? Convert.ToDateTime(objReader["Time"]) : DateTime.Now;


                        HoldReceiptList.Add(objHoldReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.HoldReceiptList = HoldReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Hold Print");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        #region Invoice_Receipt_Print
        public override SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt(SelectInvoiceReceiptByInvoiceNumRequest RequestObj)
        {
            var InvoiceReceiptList = new List<InvoiceReceiptTypes>();
            var RequestData = (SelectInvoiceReceiptByInvoiceNumRequest)RequestObj;
            var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetInvoiceDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                if (RequestData.ReturnInvoiceNo != null && RequestData.ReturnInvoiceNo != string.Empty)
                {
                    _CommandObj.Parameters.AddWithValue("@ReturnInvoiceNo", RequestData.ReturnInvoiceNo);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@ReturnInvoiceNo", "");
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new InvoiceReceiptTypes();

                        objInvoiceReceipt.Currency = objReader["Currency"].ToString();
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                        objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objInvoiceReceipt.LicenseImage = objReader["LicenseImage"] != DBNull.Value ? (byte[])objReader["LicenseImage"] : null;
                        objInvoiceReceipt.SiNO = objReader["SiNO"] != DBNull.Value ? Convert.ToInt32(objReader["SiNO"]) : 0;
                        objInvoiceReceipt.PosName = objReader["POSName"] != DBNull.Value ? Convert.ToString(objReader["POSName"]) : string.Empty;
                        objInvoiceReceipt.item_tax = objReader["item_tax"] != DBNull.Value ? Convert.ToDecimal(objReader["item_tax"]) : 0;
                        objInvoiceReceipt.POSTitle = objReader["POSTitle"].ToString();
                        objInvoiceReceipt.item_total = objReader["item_total"] != DBNull.Value ? Convert.ToDecimal(objReader["item_total"]) : 0;
                        objInvoiceReceipt.remark1 = objReader["remark1"].ToString();
                        objInvoiceReceipt.remark2 = objReader["remark2"].ToString();
                        objInvoiceReceipt.remark3 = objReader["remark3"].ToString();
                        //objInvoiceReceipt.totalprice = objReader["totalprice"] != DBNull.Value ? Convert.ToDecimal(objReader["totalprice"]) : 0;
                        objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                        objInvoiceReceipt.TaxNo = objReader["TaxNo"].ToString();
                        objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objInvoiceReceipt.SalesMan = objReader["SalesMan"].ToString();
                        objInvoiceReceipt.CustomerName = objReader["CustomerName"].ToString();
                        objInvoiceReceipt.ItemCode = objReader["ItemCode"].ToString();
                        objInvoiceReceipt.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objInvoiceReceipt.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objInvoiceReceipt.Discount = objReader["Discount"] != DBNull.Value ? Convert.ToDecimal(objReader["Discount"]) : 0;
                        objInvoiceReceipt.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceReceipt.Footer = objReader["Footer"].ToString();
                        objInvoiceReceipt.ArabicDetails = objReader["ArabicDetails"].ToString();
                        objInvoiceReceipt.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                        objInvoiceReceipt.Date = objReader["Date"] != DBNull.Value ? Convert.ToDateTime(objReader["Date"]) : DateTime.Now;
                        objInvoiceReceipt.Time = objReader["Time"] != DBNull.Value ? Convert.ToDateTime(objReader["Time"]) : DateTime.Now;
                        objInvoiceReceipt.CustomerBalance = objReader["CustomerBalance"] != DBNull.Value ? Convert.ToDecimal(objReader["CustomerBalance"]) : 0;
                        objInvoiceReceipt.GrossAmt = objReader["GrossAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["GrossAmt"]) : 0;
                        objInvoiceReceipt.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        objInvoiceReceipt.ReturnedItem = objReader["ReturnedItem"] != DBNull.Value ? Convert.ToString(objReader["ReturnedItem"]) : string.Empty;

                        InvoiceReceiptList.Add(objInvoiceReceipt);
                    }
                    var ObjSelectInvoiceReceiptByInvoiceNumRequest = new SelectInvoiceReceiptByInvoiceNumRequest();
                    var ObjSelectInvoiceReceiptByInvoiceNumResponse = new SelectInvoiceReceiptByInvoiceNumResponse();
                    ObjSelectInvoiceReceiptByInvoiceNumRequest.InvoiceNum = RequestData.InvoiceNum;

                    ObjSelectInvoiceReceiptByInvoiceNumResponse = GetInvoiceReceipt1(ObjSelectInvoiceReceiptByInvoiceNumRequest);
                    ResponseData.InvoiceSubReceiptTList = new List<InvoiceSubReceiptTypes>();
                    if (ObjSelectInvoiceReceiptByInvoiceNumResponse.StatusCode == Enums.OpStatusCode.Success)
                    {
                        ResponseData.InvoiceSubReceiptTList = ObjSelectInvoiceReceiptByInvoiceNumResponse.InvoiceSubReceiptTList;
                    }

                    var ObjSelectInvoiceApprovalNumRequest = new SelectInvoiceApprovalNumRequest();
                    var ObjSelectInvoiceReceiptApprovalNumResponse = new SelectInvoiceReceiptApprovalNumResponse();
                    ObjSelectInvoiceApprovalNumRequest.InvoiceNum = RequestData.InvoiceNum;

                    ObjSelectInvoiceReceiptApprovalNumResponse = GetInvoiceReceipt2(ObjSelectInvoiceApprovalNumRequest);
                    ResponseData.ApprovalNumReceiptList = new List<ApprovalNumReceipt>();
                    if (ObjSelectInvoiceReceiptApprovalNumResponse.StatusCode == Enums.OpStatusCode.Success)
                    {
                        ResponseData.ApprovalNumReceiptList = ObjSelectInvoiceReceiptApprovalNumResponse.ApprovalNumReceiptList;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceList = InvoiceReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt1(SelectInvoiceReceiptByInvoiceNumRequest RequestObj)
        {
            var InvoiceReceiptList1 = new List<InvoiceSubReceiptTypes>();
            var RequestData = (SelectInvoiceReceiptByInvoiceNumRequest)RequestObj;
            var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetInvoiceDetails1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new InvoiceSubReceiptTypes();

                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objInvoiceReceipt.CASH = objReader["Cash"] != DBNull.Value ? Convert.ToDecimal(objReader["Cash"]) : 0;
                        objInvoiceReceipt.KNET = objReader["KNet"] != DBNull.Value ? Convert.ToDecimal(objReader["KNet"]) : 0;
                        objInvoiceReceipt.VISA = objReader["Visa"] != DBNull.Value ? Convert.ToDecimal(objReader["Visa"]) : 0;
                        objInvoiceReceipt.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                        objInvoiceReceipt.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                        objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                        objInvoiceReceipt.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToDecimal(objReader["Amount"]) : 0;

                        InvoiceReceiptList1.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceSubReceiptTList = InvoiceReceiptList1;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectInvoiceReceiptApprovalNumResponse GetInvoiceReceipt2(SelectInvoiceApprovalNumRequest RequestObj)
        {
            var InvoiceReceiptList2 = new List<ApprovalNumReceipt>();
            var RequestData = (SelectInvoiceApprovalNumRequest)RequestObj;
            var ResponseData = new SelectInvoiceReceiptApprovalNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetApprovalNumber", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new ApprovalNumReceipt();


                        objInvoiceReceipt.CardType = objReader["CardType"].ToString();
                        objInvoiceReceipt.ApprovalNumber = objReader["ApprovalNumber"].ToString();


                        InvoiceReceiptList2.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ApprovalNumReceiptList = InvoiceReceiptList2;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        #endregion

        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLogDetailMasterDetails in TransactionLogDetailMasterList)
            {
                sSql.Append("<TransactionLogDetailsData>");
                sSql.Append("<ID>" + objTransactionLogDetailMasterDetails.ID + "</ID>");
                sSql.Append("<TransactionType>" + objTransactionLogDetailMasterDetails.TransactionType + "</TransactionType>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ActualDateTime) + "</ActualDateTime>");
                sSql.Append("<StyleCode>" + objTransactionLogDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<SKUCode>" + objTransactionLogDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<InQty>" + objTransactionLogDetailMasterDetails.InQty + "</InQty>");
                sSql.Append("<OutQty>" + objTransactionLogDetailMasterDetails.OutQty + "</OutQty>");
                sSql.Append("<TransactionPrice>" + objTransactionLogDetailMasterDetails.TransactionPrice + "</TransactionPrice>");
                sSql.Append("<Currency>" + (objTransactionLogDetailMasterDetails.Currency) + "</Currency>");
                sSql.Append("<ExchangeRate>" + (objTransactionLogDetailMasterDetails.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("<DocumentPrice>" + (objTransactionLogDetailMasterDetails.DocumentPrice) + "</DocumentPrice>");
                sSql.Append("<UserID>" + (objTransactionLogDetailMasterDetails.UserID) + "</UserID>");
                sSql.Append("<CountryID>" + (objTransactionLogDetailMasterDetails.CountryID) + "</CountryID>");
                sSql.Append("<CountryCode>" + (objTransactionLogDetailMasterDetails.CountryCode) + "</CountryCode>");
                sSql.Append("<StoreID>" + (objTransactionLogDetailMasterDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objTransactionLogDetailMasterDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<DocumentNo>" + (objTransactionLogDetailMasterDetails.DocumentNo) + "</DocumentNo>");
                sSql.Append("</TransactionLogDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        //public override SelectAllInvoiceResponse SelectAllInvoice(SelectAllInvoiceRequest RequestObj)
        //{
        //    var InvoiceHeaderList = new List<InvoiceHeader>();
        //    var RequestData = (SelectAllInvoiceRequest)RequestObj;
        //    var ResponseData = new SelectAllInvoiceResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;

        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);               
        //        string sSql = "Select * from InvoiceHeader  ";
        //        _CommandObj = new SqlCommand(sSql, _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objInvoiceHeader = new InvoiceHeader();
        //                objInvoiceHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                objInvoiceHeader.CountryID = Convert.ToInt32(objReader["CountryID"]);
        //                objInvoiceHeader.StoreID = Convert.ToInt32(objReader["StoreID"]);
        //                objInvoiceHeader.PosID = Convert.ToInt32(objReader["PosID"]);
        //                objInvoiceHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
        //                objInvoiceHeader.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
        //                objInvoiceHeader.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
        //                objInvoiceHeader.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
        //                objInvoiceHeader.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
        //                objInvoiceHeader.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
        //                objInvoiceHeader.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;
        //                objInvoiceHeader.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
        //                objInvoiceHeader.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
        //                objInvoiceHeader.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
        //                objInvoiceHeader.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
        //                objInvoiceHeader.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
        //                objInvoiceHeader.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
        //                objInvoiceHeader.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
        //                objInvoiceHeader.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
        //                objInvoiceHeader.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
        //                objInvoiceHeader.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
        //                objInvoiceHeader.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
        //                objInvoiceHeader.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

        //                objInvoiceHeader.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
        //                objInvoiceHeader.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
        //                objInvoiceHeader.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
        //                objInvoiceHeader.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
        //                objInvoiceHeader.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

        //                objInvoiceHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
        //                objInvoiceHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
        //                objInvoiceHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
        //                objInvoiceHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
        //                objInvoiceHeader.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
        //                objInvoiceHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

        //                objInvoiceHeader.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
        //                objInvoiceHeader.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;





        //                InvoiceHeaderList.Add(objInvoiceHeader);
        //            }
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            ResponseData.InvoiceHeaderList = InvoiceHeaderList;

        //            ResponseData.ResponseDynamicData = InvoiceHeaderList;
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Agent Master");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlCommon.CloseConnection(_ConnectionObj);
        //    }
        //    return ResponseData;
        //}


        public override SaveInvoiceResponse SavePaymentProcesor(SaveInvoiceRequest objRequest)
        {
            //string a = "success";
            var RequestData = (SaveInvoiceRequest)objRequest;
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertPaymentProcesserLog", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                SqlParameter PaymentProcessorList1 = _CommandObj.Parameters.Add("@PaymentProcessor", SqlDbType.Xml);
                PaymentProcessorList1.Direction = ParameterDirection.Input;
                PaymentProcessorList1.Value = PaymentProcessorXML(RequestData.PaymentProcessorList);

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
                //PaymentProcesserSave(ObjRequest);
            }
            return null;
            //PaymentProcesserSave(ObjRequest);
            //throw new NotImplementedException();
        }


        public override GetSearchInvoiceHeaderDetailsResponse GetSearchInvoiceHeaderDetails(SelectInvoiceDetailsListRequest objRequest)
        {
            //var InvoiceHeaderMasterList = new List<ApprovalNumReceipt>();
            var RequestData = (SelectInvoiceDetailsListRequest)objRequest;
            var ResponseData = new GetSearchInvoiceHeaderDetailsResponse();
            SqlDataReader objReader;
            string Mode = "";
            long ID = 0;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InvoiceHeaderDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@ForceSKUSearch", RequestData.ForceSKUSearch);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderMaster = new InvoiceHeader();
                        ResponseData.InvoiceHeaderDetailsList = objInvoiceHeaderMaster;
                        objInvoiceHeaderMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceHeaderMaster.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objInvoiceHeaderMaster.DocumentDate = Convert.ToDateTime(objReader["BusinessDate"]);
                        objInvoiceHeaderMaster.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderMaster.CustomerID = Convert.ToInt32(objReader["CustomerID"]);
                        objInvoiceHeaderMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        objInvoiceHeaderMaster.CustomerName = Convert.ToString(objReader["CustomerName"]);
                        objInvoiceHeaderMaster.PhoneNumber = Convert.ToString(objReader["PhoneNumber"]);
                        objInvoiceHeaderMaster.SalesStatus = Convert.ToString(objReader["SalesStatus"]);
                        objInvoiceHeaderMaster.IsCreditSale = objReader["IsCreditSale"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditSale"]) : false;
                        objInvoiceHeaderMaster.Mode = Convert.ToString(objReader["Mode"]);
                        Mode = objInvoiceHeaderMaster.Mode;
                        ID = Convert.ToInt32(objReader["ID"]);
                    }

                    ResponseData.InvoiceHeaderDetailsList.InvoiceDetailList = new List<InvoiceDetails>();
                    GetSearchInvoiceDetailsListRequest objInvoiceDetailsRequest = new GetSearchInvoiceDetailsListRequest();
                    GetSearchInvoiceDetailsListResponse objInvoiceDetailsResponse = new GetSearchInvoiceDetailsListResponse();
                    objInvoiceDetailsRequest.ID = ID;
                    objInvoiceDetailsRequest.Mode = Mode;
                    objInvoiceDetailsRequest.SKUCode = RequestData.SearchString;
                    objInvoiceDetailsRequest.StoreID = RequestData.StoreID;
                    objInvoiceDetailsResponse = GetInvocieDetailsListBasedonInvoiceno(objInvoiceDetailsRequest);
                    if (objInvoiceDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                    {
                        ResponseData.InvoiceHeaderDetailsList.InvoiceDetailList = objInvoiceDetailsResponse.InvoiceDetailsList;
                    }
                    ResponseData.PaymentList = new List<PaymentDetail>();

                    var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
                    var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
                    var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
                    objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = ID;
                    objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = RequestData.SearchString;
                    objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;
                    objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
                    if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
                    {
                        ResponseData.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
                        ////objInvoiceHeaderMaster.PaymentList = objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }


        public override GetSearchInvoiceDetailsListResponse GetInvocieDetailsListBasedonInvoiceno(GetSearchInvoiceDetailsListRequest ObjRequest)
        {
            var InvoiceDetailsList = new List<InvoiceDetails>();
            var RequestData = (GetSearchInvoiceDetailsListRequest)ObjRequest;
            var ResponseData = new GetSearchInvoiceDetailsListResponse();
            SqlDataReader objReader1;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sSql = "";

                if (RequestData.Mode == "Invoice")
                {
                    sSql = "select IND.*, SI.SKUImage, SM.StyleCode, PGM.ProductGroupName " +
                            ", SED.SKUCode [ExchangeSKU] " +
                        "from invoicedetail as IND with(nolock) " +
                        "join SKUMaster as SM with(nolock) on IND.SKUCode= SM.SKUCode " +
                        "left join SKUImages as SI with(nolock) on SI.StyleCode = SM.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SM.ProductGroupID= PGM.ID " +
                        "left join SalesExchangeDetail as SED with(nolock) on IND.ExchangeRefID = SED.ID " +
                        "where IND.InvoiceHeaderID = " + RequestData.ID + " " +
                        //"and isnull(IND.IsReturned,0) <> 1 " +
                        "Order by ID";
                }
                else if (RequestData.Mode == "Exchange")
                {
                    sSql = "select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty [ExchangeQty], SED.ReturnQty" +
                        ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode [ExchangeSKU] " +
                        ", IND.Type, IND.InvoiceHeaderID, IND.SKUCode, IND.Price, IND.DiscountType, IND.DiscountAmount" +
                        ", IND.LineTotal, IND.AppliedPriceListID, IND.AppliedCustomerSpecialPricesID, IND.AppliedPromotionID" +
                        ", IND.TaxID, IND.TaxAmount, IND.SellingPrice, IND.SellingLineTotal, IND.ExchangeRefID, IND.ReturnRefID" +
                        ", IND.EmployeeDiscountID, IND.FamilyDiscountAmount, IND.EmployeeDiscountAmount, IND.DiscountRemarks" +
                        ", IND.SpecialPromoDiscountType, IND.SpecialPromoDiscountPercentage, IND.SpecialPromoDiscount" +
                        ", IND.PromoGroupID, IND.BrandID" +
                        ", SI.SKUImage, SM.StyleCode, PGM.ProductGroupName  " +
                        ", SM.BrandID, IND.DiscountType " +
                        ", 0 [ComboGroupID], 0 [IsCombo], 0 [IsHeader], 0 [IsGift] " +

                        "from SalesExchangeDetail as SED with(nolock) " +
                        "left join InvoiceDetail as IND with(nolock) on IND.ID = SED.InvoiceDetailID " +
                        "join SKUMaster as SM with(nolock) on SED.SKUCode= SM.SKUCode " +
                        "left join SKUImages as SI with(nolock) on SI.StyleCode = SM.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SM.ProductGroupID= PGM.ID " +
                        "where SED.SalesExchangeID = " + RequestData.ID + " " +
                        //"and isnull(SED.IsReturned,0) <> 1 " +
                        " Order by ID";
                }
                else if (RequestData.Mode == "SalesExchangeWithExchange")
                {
                    sSql = "Select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty[ExchangeQty], SED.ReturnQty " +
                            ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode[ExchangeSKU] " +
                            ", SED.TaxID, SED.TaxAmount, '' as Type, 0 as InvoiceHeaderID,red.ReturnSKU as SKUCode,SP.Price " +
                            ", ''[DiscountType], 0 as DiscountAmount, SP.Price[LineTotal], SM.PriceListID[AppliedPriceListID] " +
                            ", 0[AppliedCustomerSpecialPricesID], 0[AppliedPromotionID] " +
                            ", (SP.Price - isnull(SED.TaxAmount, 0))[SellingPrice] " +
                            ", (SP.Price - isnull(SED.TaxAmount, 0))[SellingLineTotal], 0[ExchangeRefID] " +
                            ", 0[ReturnRefID], 0[EmployeeDiscountID], 0[FamilyDiscountAmount] " +
                            ", 0[EmployeeDiscountAmount], ''[DiscountRemarks],''[SpecialPromoDiscountType] " +
                            ", 0[SpecialPromoDiscountPercentage], 0[SpecialPromoDiscount], 0[PromoGroupID] " +
                            ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +
                            ", 0 [ComboGroupID], 0 [IsCombo], 0 [IsHeader], 0 [IsGift] " +

                            "from SalesExchangeDetail SED with(nolock) " +
                            "join Stylepricing SP with(nolock) on SP.SKUCode = SED.SKUCode " +
                            "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                            "and SM.ID = SED.StoreID " +
                            "join SKUMaster SK with(nolock) on SK.Skucode = SED.SKUCode " +
                            "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                            "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                            "join ReturnExchangeDetail red on red.ExchangeDetailID = sed.ID " +
                            "where SED.SalesExchangeID = " + RequestData.ID + " and SP.PriceListId = SM.PriceListID Order by ID";
                }
                else if (RequestData.Mode == "ExchangeWithOutInvoice")
                {
                    // Exchange Without Invoice
                    sSql = "Select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty[ExchangeQty], SED.ReturnQty " +
                        ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode[ExchangeSKU] " +
                        ", SED.TaxID, SED.TaxAmount, '' as Type, 0 as InvoiceHeaderID,'' as SKUCode,SP.Price" +
                        ", '' [DiscountType], 0 as DiscountAmount, SP.Price [LineTotal], SM.PriceListID [AppliedPriceListID]" +
                        ", 0 [AppliedCustomerSpecialPricesID], 0 [AppliedPromotionID]" +
                        ", (SP.Price - isnull(SED.TaxAmount, 0)) [SellingPrice]" +
                        ", (SP.Price - isnull(SED.TaxAmount, 0)) [SellingLineTotal], 0 [ExchangeRefID]" +
                        ", 0 [ReturnRefID], 0 [EmployeeDiscountID], 0 [FamilyDiscountAmount]" +
                        ", 0 [EmployeeDiscountAmount], '' [DiscountRemarks],'' [SpecialPromoDiscountType]" +
                        ", 0 [SpecialPromoDiscountPercentage], 0 [SpecialPromoDiscount], 0 [PromoGroupID]" +
                        ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +
                        ", 0 [ComboGroupID], 0 [IsCombo], 0 [IsHeader], 0 [IsGift] " +

                        "from SalesExchangeDetail SED with(nolock) " +
                        "join Stylepricing SP with(nolock) on SP.SKUCode = SED.SKUCode " +
                        "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                        "and SM.ID = SED.StoreID " +
                        "join SKUMaster SK with(nolock) on SK.Skucode = SED.SKUCode " +
                        "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                        "where SED.SalesExchangeID =  " + RequestData.ID + " and SP.PriceListId = SM.PriceListID";
                }
                else if (RequestData.Mode == "WithOutInvoice")
                {
                    // Without Invoice
                    sSql = "Select top 1 0 [ID], 1 [SerialNo], SK.ID [SKUID], 1 [Active], 0 [ExchangeQty], 0 [ReturnQty] " +
                        ", '' [CountryCode], '' [StoreCode], '' [PosCode], '' [Tag_Id], '' [ExchangeSKU] " +
                        ", CM.TaxID" +
                        ", isnull(SP.Price,0) - ((isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0))) [TaxAmount]" +
                        ", '' [Type], 0 [InvoiceHeaderID], SK.SKUCode, SP.Price" +
                        ", '' [DiscountType], 0 [DiscountAmount], SP.Price [LineTotal], SM.PriceListID [AppliedPriceListID]" +
                        ", 0 [AppliedCustomerSpecialPricesID], 0 [AppliedPromotionID]" +
                        ", (isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0)) [SellingPrice]" +
                        ", (isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0)) [SellingLineTotal]" +
                        ", 0 [ExchangeRefID], 0 [ReturnRefID], 0 [EmployeeDiscountID], 0 [FamilyDiscountAmount]" +
                        ", 0 [EmployeeDiscountAmount], '' [DiscountRemarks],'' [SpecialPromoDiscountType]" +
                        ", 0 [SpecialPromoDiscountPercentage], 0 [SpecialPromoDiscount], 0 [PromoGroupID]" +
                        ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +
                        ", 0 [ComboGroupID], 0 [IsCombo], 0 [IsHeader], 0 [IsGift] " +

                        "from SKUMaster SK with(nolock) " +
                        "join Stylepricing SP with(nolock) on SK.SKUCode = SP.SKUCode " +
                        "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                        "and SM.ID = " + RequestData.StoreID.ToString() + " " +
                        "join CountryMaster CM with(nolock) on SM.CountryID = CM.ID " +
                        "join TaxMaster TX with(nolock) on CM.TaxID = TX.ID " +
                        "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                        "where (SK.SKUCode =  '" + RequestData.SKUCode + "' or SK.Barcode =  '" + RequestData.SKUCode + "') and SP.PriceListId = SM.PriceListID";
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader1 = _CommandObj.ExecuteReader();
                if (objReader1.HasRows)
                {
                    while (objReader1.Read())
                    {
                        var objInvoiceDetailsList = new InvoiceDetails();
                        objInvoiceDetailsList.InvoiceDetailID = Convert.ToInt32(objReader1["ID"]);
                        objInvoiceDetailsList.InvoiceType = objReader1["Type"] != DBNull.Value ? Convert.ToString(objReader1["Type"]) : string.Empty;
                        objInvoiceDetailsList.SerialNo = Convert.ToInt32(objReader1["SerialNo"]);
                        objInvoiceDetailsList.SKUID = Convert.ToInt32(objReader1["SKUID"]);
                        //objInvoiceDetailsList.InvoiceHeaderID = Convert.ToInt32(objReader1["InvoiceHeaderID"]);
                        objInvoiceDetailsList.InvoiceHeaderID = objReader1["InvoiceHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader1["InvoiceHeaderID"]) : 0;
                        objInvoiceDetailsList.SKUCode = objReader1["SKUCode"].ToString();
                        objInvoiceDetailsList.BrandID = Convert.ToInt32(objReader1["BrandID"]);
                        objInvoiceDetailsList.SubBrandName = Convert.ToString(objReader1["ProductGroupName"]);
                        objInvoiceDetailsList.SKUImage = objReader1["SKUImage"] != DBNull.Value ? (byte[])objReader1["SKUImage"] : null;

                        objInvoiceDetailsList.Price = objReader1["Price"] != DBNull.Value ? Convert.ToDecimal(objReader1["Price"]) : 0;
                        objInvoiceDetailsList.DiscountType = objReader1["DiscountType"].ToString();
                        objInvoiceDetailsList.DiscountAmount = objReader1["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["DiscountAmount"]) : 0;
                        objInvoiceDetailsList.LineTotal = objReader1["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["LineTotal"]) : 0;
                        objInvoiceDetailsList.AppliedPriceListID = objReader1["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader1["AppliedPriceListID"]) : 0;
                        objInvoiceDetailsList.AppliedCustomerSpecialPricesID = objReader1["AppliedCustomerSpecialPricesID"].ToString();
                        objInvoiceDetailsList.AppliedPromotionID = objReader1["AppliedPromotionID"].ToString();

                        objInvoiceDetailsList.Active = objReader1["Active"] != DBNull.Value ? Convert.ToBoolean(objReader1["Active"]) : true;
                        objInvoiceDetailsList.TaxID = objReader1["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader1["TaxID"]) : 0;
                        objInvoiceDetailsList.TaxAmount = objReader1["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["TaxAmount"]) : 0;

                        objInvoiceDetailsList.SellingPrice = objReader1["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingPrice"]) : 0;
                        objInvoiceDetailsList.SellingLineTotal = objReader1["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingLineTotal"]) : 0;

                        objInvoiceDetailsList.StyleCode = Convert.ToString(objReader1["StyleCode"]);

                        objInvoiceDetailsList.ExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0; // need to change for Sales exchange
                        objInvoiceDetailsList.OldExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0;

                        objInvoiceDetailsList.ReturnQty = 0;
                        objInvoiceDetailsList.OldReturnQty = objReader1["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ReturnQty"]) : 0;
                        objInvoiceDetailsList.ExchangeRefID = objReader1["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ExchangeRefID"]) : 0;
                        objInvoiceDetailsList.ReturnRefID = objReader1["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ReturnRefID"]) : 0;

                        objInvoiceDetailsList.CountryCode = objReader1["CountryCode"] != DBNull.Value ? Convert.ToString(objReader1["CountryCode"]) : string.Empty;
                        objInvoiceDetailsList.StoreCode = objReader1["StoreCode"] != DBNull.Value ? Convert.ToString(objReader1["StoreCode"]) : string.Empty;
                        objInvoiceDetailsList.PosCode = objReader1["PosCode"] != DBNull.Value ? Convert.ToString(objReader1["PosCode"]) : string.Empty;

                        objInvoiceDetailsList.EmployeeDiscountID = objReader1["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader1["EmployeeDiscountID"]) : 0;
                        objInvoiceDetailsList.FamilyDiscountAmount = objReader1["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["FamilyDiscountAmount"]) : 0;
                        objInvoiceDetailsList.EmployeeDiscountAmount = objReader1["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["EmployeeDiscountAmount"]) : 0;
                        objInvoiceDetailsList.DiscountRemarks = objReader1["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader1["DiscountRemarks"]) : string.Empty;

                        objInvoiceDetailsList.SpecialPromoDiscountType = objReader1["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader1["SpecialPromoDiscountType"]) : string.Empty;
                        objInvoiceDetailsList.SpecialPromoDiscountPercentage = objReader1["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscountPercentage"]) : 0;
                        objInvoiceDetailsList.SpecialPromoDiscount = objReader1["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscount"]) : 0;
                        objInvoiceDetailsList.Tag_Id = objReader1["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader1["Tag_Id"]) : string.Empty;
                        objInvoiceDetailsList.PromoGroupID = objReader1["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader1["PromoGroupID"]) : 0;
                        objInvoiceDetailsList.ComboGroupID = objReader1["ComboGroupID"] != DBNull.Value ? Convert.ToInt32(objReader1["ComboGroupID"]) : 0;
                        objInvoiceDetailsList.IsCombo = objReader1["IsCombo"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsCombo"]) : false;
                        objInvoiceDetailsList.IsHeader = objReader1["IsHeader"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsHeader"]) : false;
                        objInvoiceDetailsList.IsGift = objReader1["IsGift"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsGift"]) : false;
                        if (objInvoiceDetailsList.OldExchangeQty > 0)
                        {
                            objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
                            if (objInvoiceDetailsList.OldExchangeQty > 0)
                            {
                                objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
                            }
                            else
                            {
                                objInvoiceDetailsList.ExchangedSKU = string.Empty;
                            }

                        }

                        string ReturnRemarks = string.Empty;
                        if (objInvoiceDetailsList.ExchangeQty > 0)
                        {
                            ReturnRemarks = objInvoiceDetailsList.ExchangeQty.ToString() + " item(s) exchanged.";
                        }
                        if (objInvoiceDetailsList.OldReturnQty > 0)
                        {
                            ReturnRemarks = ReturnRemarks + objInvoiceDetailsList.OldReturnQty.ToString() + " item(s) returned.";
                        }
                        objInvoiceDetailsList.ReturnRemarks = ReturnRemarks;
                        objInvoiceDetailsList.ExchangeRemarks = ReturnRemarks;

                        InvoiceDetailsList.Add(objInvoiceDetailsList);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceDetailsList = InvoiceDetailsList;
                    //ResponseData.InvoiceDetailsList = TailoringOrderDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Details");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        //Exchange Item Search(1) for salesexchange screen
        public override GetSearchInvoiceHeaderDetailsResponse GetExchangeItemDetails(SelectInvoiceDetailsListRequest objRequest)
        {
            var RequestData = (SelectInvoiceDetailsListRequest)objRequest;
            var ResponseData = new GetSearchInvoiceHeaderDetailsResponse();
            SqlDataReader objReader;
            string Mode = "";
            long ID = 0;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InvoiceHeaderDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@ForceSKUSearch", RequestData.ForceSKUSearch);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderMaster = new InvoiceHeader();
                        ResponseData.InvoiceHeaderDetailsList = objInvoiceHeaderMaster;
                        objInvoiceHeaderMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceHeaderMaster.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objInvoiceHeaderMaster.DocumentDate = Convert.ToDateTime(objReader["BusinessDate"]);
                        objInvoiceHeaderMaster.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderMaster.CustomerID = Convert.ToInt32(objReader["CustomerID"]);
                        objInvoiceHeaderMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        objInvoiceHeaderMaster.CustomerName = Convert.ToString(objReader["CustomerName"]);
                        objInvoiceHeaderMaster.PhoneNumber = Convert.ToString(objReader["PhoneNumber"]);
                        objInvoiceHeaderMaster.SalesStatus = Convert.ToString(objReader["SalesStatus"]);
                        objInvoiceHeaderMaster.IsCreditSale = objReader["IsCreditSale"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditSale"]) : false;
                        objInvoiceHeaderMaster.Mode = Convert.ToString(objReader["Mode"]);
                        Mode = objInvoiceHeaderMaster.Mode;
                        ID = Convert.ToInt32(objReader["ID"]);
                    }

                    ResponseData.InvoiceHeaderDetailsList.InvoiceDetailList = new List<InvoiceDetails>();
                    GetSearchInvoiceDetailsListRequest objInvoiceDetailsRequest = new GetSearchInvoiceDetailsListRequest();
                    GetSearchInvoiceDetailsListResponse objInvoiceDetailsResponse = new GetSearchInvoiceDetailsListResponse();
                    objInvoiceDetailsRequest.ID = ID;
                    objInvoiceDetailsRequest.Mode = Mode;
                    objInvoiceDetailsRequest.SKUCode = RequestData.SearchString;
                    objInvoiceDetailsRequest.StoreID = RequestData.StoreID;
                    objInvoiceDetailsResponse = GetExchangeItemDetailsList(objInvoiceDetailsRequest);
                    if (objInvoiceDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                    {
                        ResponseData.InvoiceHeaderDetailsList.InvoiceDetailList = objInvoiceDetailsResponse.InvoiceDetailsList;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        private GetSearchInvoiceDetailsListResponse GetExchangeItemDetailsList(GetSearchInvoiceDetailsListRequest ObjRequest)
        {
            var InvoiceDetailsList = new List<InvoiceDetails>();
            var RequestData = (GetSearchInvoiceDetailsListRequest)ObjRequest;
            var ResponseData = new GetSearchInvoiceDetailsListResponse();
            SqlDataReader objReader1;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sSql = "";

                if (RequestData.Mode == "Invoice")
                {
                    sSql = "select IND.*, SI.SKUImage, SM.StyleCode, PGM.ProductGroupName, isnull(X.SKUCode,'') [ExchangeSKU] " +
                        "from invoicedetail as IND with(nolock) " +
                        "join SKUMaster as SM with(nolock) on IND.SKUCode= SM.SKUCode " +
                        "left join  SKUImages as SI with(nolock) on SI.StyleCode = SM.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SM.ProductGroupID= PGM.ID " +
                        "left join SalesExchangeDetail as X with(nolock) on isnull(IND.ExchangeRefID,0)= X.ID " +
                        "where IND.InvoiceHeaderID = " + RequestData.ID + " " +
                        //"and isnull(IND.IsReturned,0) <> 1 " +
                        "Order by ID";
                }
                else if (RequestData.Mode == "Exchange")
                {
                    sSql = "select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty [ExchangeQty], SED.ReturnQty" +
                        ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode [ExchangeSKU] " +

                        ", IND.Type, IND.InvoiceHeaderID, IND.SKUCode, IND.Price, IND.DiscountType, IND.DiscountAmount" +
                        ", IND.LineTotal, IND.AppliedPriceListID, IND.AppliedCustomerSpecialPricesID, IND.AppliedPromotionID" +
                        ", IND.TaxID, IND.TaxAmount, IND.SellingPrice, IND.SellingLineTotal, IND.ExchangeRefID, IND.ReturnRefID" +
                        ", IND.EmployeeDiscountID, IND.FamilyDiscountAmount, IND.EmployeeDiscountAmount, IND.DiscountRemarks" +
                        ", IND.SpecialPromoDiscountType, IND.SpecialPromoDiscountPercentage, IND.SpecialPromoDiscount" +
                        ", IND.PromoGroupID, IND.BrandID, null as IsReturned, null as IsExchanged" +

                        ", SI.SKUImage, SM.StyleCode, PGM.ProductGroupName,  " +
                        " SM.BrandID, IND.DiscountType " +
                        "from SalesExchangeDetail as SED with(nolock) " +
                        "join InvoiceDetail as IND with(nolock) on IND.ID = SED.InvoiceDetailID " +
                        "join SKUMaster as SM with(nolock) on SED.SKUCode= SM.SKUCode " +
                        "left join SKUImages as SI with(nolock) on SI.StyleCode = SM.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SM.ProductGroupID= PGM.ID " +
                        "where SED.SalesExchangeID = " + RequestData.ID + " " +
                        //"and isnull(SED.IsReturned,0) <> 1 " +
                        " Order by ID";
                }
                else if (RequestData.Mode == "SalesExchangeWithExchange")
                {
                    sSql = "Select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty[ExchangeQty], SED.ReturnQty " +
                            ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode[ExchangeSKU] " +
                            ", SED.TaxID, SED.TaxAmount, '' as Type, 0 as InvoiceHeaderID,red.ReturnSKU as SKUCode,SP.Price , null as IsReturned, null as IsExchanged" +
                            ", ''[DiscountType], 0 as DiscountAmount, SP.Price[LineTotal], SM.PriceListID[AppliedPriceListID] " +
                            ", 0[AppliedCustomerSpecialPricesID], 0[AppliedPromotionID] " +
                            ", (SP.Price - isnull(SED.TaxAmount, 0))[SellingPrice] " +
                            ", (SP.Price - isnull(SED.TaxAmount, 0))[SellingLineTotal], 0[ExchangeRefID] " +
                            ", 0[ReturnRefID], 0[EmployeeDiscountID], 0[FamilyDiscountAmount] " +
                            ", 0[EmployeeDiscountAmount], ''[DiscountRemarks],''[SpecialPromoDiscountType] " +
                            ", 0[SpecialPromoDiscountPercentage], 0[SpecialPromoDiscount], 0[PromoGroupID] " +
                            ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +
                            "from SalesExchangeDetail SED with(nolock) " +
                            "join Stylepricing SP with(nolock) on SP.SKUCode = SED.SKUCode " +
                            "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                            "and SM.ID = SED.StoreID " +
                            "join SKUMaster SK with(nolock) on SK.Skucode = SED.SKUCode " +
                            "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                            "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                            "join ReturnExchangeDetail red on red.ExchangeDetailID = sed.ID " +
                            "where SED.SalesExchangeID = " + RequestData.ID + " and SP.PriceListId = SM.PriceListID Order by ID";
                }
                else if (RequestData.Mode == "ExchangeWithOutInvoice")
                {
                    // Exchange Without Invoice
                    sSql = "Select SED.ID, SED.SerialNo, SED.SKUID, SED.Active, SED.Qty[ExchangeQty], SED.ReturnQty " +
                        ", SED.CountryCode, SED.StoreCode, SED.PosCode, SED.Tag_Id, SED.SKUCode[ExchangeSKU] " +
                        ", SED.TaxID, SED.TaxAmount, '' as Type, 0 as InvoiceHeaderID,red.ReturnSKU as SKUCode,SP.Price, null as IsReturned, null as IsExchanged" +
                        ", '' [DiscountType], 0 as DiscountAmount, SP.Price [LineTotal], SM.PriceListID [AppliedPriceListID]" +
                        ", 0 [AppliedCustomerSpecialPricesID], 0 [AppliedPromotionID]" +
                        ", (SP.Price - isnull(SED.TaxAmount, 0)) [SellingPrice]" +
                        ", (SP.Price - isnull(SED.TaxAmount, 0)) [SellingLineTotal], 0 [ExchangeRefID]" +
                        ", 0 [ReturnRefID], 0 [EmployeeDiscountID], 0 [FamilyDiscountAmount]" +
                        ", 0 [EmployeeDiscountAmount], '' [DiscountRemarks],'' [SpecialPromoDiscountType]" +
                        ", 0 [SpecialPromoDiscountPercentage], 0 [SpecialPromoDiscount], 0 [PromoGroupID]" +
                        ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +
                        "from SalesExchangeDetail SED with(nolock) " +
                        "join Stylepricing SP with(nolock) on SP.SKUCode = SED.SKUCode " +
                        "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                        "and SM.ID = SED.StoreID " +
                        "join SKUMaster SK with(nolock) on SK.Skucode = SED.SKUCode " +
                        "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                        "join ReturnExchangeDetail red on red.ExchangeDetailID = sed.ID " +
                        "where SED.SalesExchangeID =  " + RequestData.ID + " and SP.PriceListId = SM.PriceListID";
                }
                else if (RequestData.Mode == "WithOutInvoice")
                {
                    // Without Invoice
                    sSql = "Select top 1 0 [ID], 1 [SerialNo], SK.ID [SKUID], 1 [Active], 0 [ExchangeQty], 0 [ReturnQty] " +
                        ", '' [CountryCode], '' [StoreCode], '' [PosCode], '' [Tag_Id], '' [ExchangeSKU] " +
                        ", CM.TaxID, null as IsReturned, null as IsExchanged" +
                        ", isnull(SP.Price,0) - ((isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0))) [TaxAmount]" +
                        ", '' [Type], 0 [InvoiceHeaderID], SK.SKUCode, SP.Price" +
                        ", '' [DiscountType], 0 [DiscountAmount], SP.Price [LineTotal], SM.PriceListID [AppliedPriceListID]" +
                        ", 0 [AppliedCustomerSpecialPricesID], 0 [AppliedPromotionID]" +
                        ", (isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0)) [SellingPrice]" +
                        ", (isnull(SP.Price,0) * 100) / (100 + isnull(TX.TaxPercentage,0)) [SellingLineTotal]" +
                        ", 0 [ExchangeRefID], 0 [ReturnRefID], 0 [EmployeeDiscountID], 0 [FamilyDiscountAmount]" +
                        ", 0 [EmployeeDiscountAmount], '' [DiscountRemarks],'' [SpecialPromoDiscountType]" +
                        ", 0 [SpecialPromoDiscountPercentage], 0 [SpecialPromoDiscount], 0 [PromoGroupID]" +
                        ", SI.SKUImage, SK.StyleCode, PGM.ProductGroupName, SK.BrandID " +

                        "from SKUMaster SK with(nolock) " +
                        "join Stylepricing SP with(nolock) on SK.SKUCode = SP.SKUCode " +
                        "join StoreMaster SM with(nolock) on SM.PriceListID = SP.PriceListID " +
                        "and SM.ID = " + RequestData.StoreID.ToString() + " " +
                        "join CountryMaster CM with(nolock) on SM.CountryID = CM.ID " +
                        "join TaxMaster TX with(nolock) on CM.TaxID = TX.ID " +
                        "left join SKUImages SI with(nolock) on SI.StyleCode = SK.StyleCode " +
                        "left join ProductGroupMaster as PGM with(nolock) on SK.ProductGroupID = PGM.ID " +
                        "where (SK.SKUCode =  '" + RequestData.SKUCode + "' or SK.Barcode =  '" + RequestData.SKUCode + "') and SP.PriceListId = SM.PriceListID";
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader1 = _CommandObj.ExecuteReader();
                if (objReader1.HasRows)
                {
                    while (objReader1.Read())
                    {
                        var objInvoiceDetailsList = new InvoiceDetails();
                        objInvoiceDetailsList.ID = Convert.ToInt32(objReader1["ID"]);
                        objInvoiceDetailsList.InvoiceDetailID = Convert.ToInt32(objReader1["ID"]);
                        objInvoiceDetailsList.InvoiceType = objReader1["Type"] != DBNull.Value ? Convert.ToString(objReader1["Type"]) : string.Empty;
                        objInvoiceDetailsList.SerialNo = Convert.ToInt32(objReader1["SerialNo"]);
                        objInvoiceDetailsList.SKUID = Convert.ToInt32(objReader1["SKUID"]);
                        objInvoiceDetailsList.InvoiceHeaderID = Convert.ToInt32(objReader1["InvoiceHeaderID"]);
                        objInvoiceDetailsList.SKUCode = objReader1["SKUCode"].ToString();
                        objInvoiceDetailsList.BrandID = Convert.ToInt32(objReader1["BrandID"]);
                        objInvoiceDetailsList.SubBrandName = Convert.ToString(objReader1["ProductGroupName"]);
                        objInvoiceDetailsList.SKUImage = objReader1["SKUImage"] != DBNull.Value ? (byte[])objReader1["SKUImage"] : null;

                        objInvoiceDetailsList.Price = objReader1["Price"] != DBNull.Value ? Convert.ToDecimal(objReader1["Price"]) : 0;
                        objInvoiceDetailsList.DiscountType = objReader1["DiscountType"].ToString();
                        objInvoiceDetailsList.DiscountAmount = objReader1["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["DiscountAmount"]) : 0;
                        objInvoiceDetailsList.LineTotal = objReader1["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["LineTotal"]) : 0;
                        objInvoiceDetailsList.AppliedPriceListID = objReader1["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader1["AppliedPriceListID"]) : 0;
                        objInvoiceDetailsList.AppliedCustomerSpecialPricesID = objReader1["AppliedCustomerSpecialPricesID"].ToString();
                        objInvoiceDetailsList.AppliedPromotionID = objReader1["AppliedPromotionID"].ToString();

                        objInvoiceDetailsList.Active = objReader1["Active"] != DBNull.Value ? Convert.ToBoolean(objReader1["Active"]) : true;
                        objInvoiceDetailsList.TaxID = objReader1["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader1["TaxID"]) : 0;
                        objInvoiceDetailsList.TaxAmount = objReader1["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["TaxAmount"]) : 0;

                        objInvoiceDetailsList.SellingPrice = objReader1["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingPrice"]) : 0;
                        objInvoiceDetailsList.SellingLineTotal = objReader1["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingLineTotal"]) : 0;

                        objInvoiceDetailsList.StyleCode = Convert.ToString(objReader1["StyleCode"]);

                        objInvoiceDetailsList.ExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0; // need to change for Sales exchange
                        objInvoiceDetailsList.OldExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0;

                        objInvoiceDetailsList.ReturnQty = 0;
                        objInvoiceDetailsList.OldReturnQty = objReader1["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ReturnQty"]) : 0;
                        objInvoiceDetailsList.IsReturned = objReader1["IsReturned"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsReturned"]) : false;
                        objInvoiceDetailsList.IsExchanged = objReader1["IsExchanged"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsExchanged"]) : false;
                        objInvoiceDetailsList.ExchangeRefID = objReader1["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ExchangeRefID"]) : 0;
                        objInvoiceDetailsList.ReturnRefID = objReader1["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ReturnRefID"]) : 0;

                        objInvoiceDetailsList.CountryCode = objReader1["CountryCode"] != DBNull.Value ? Convert.ToString(objReader1["CountryCode"]) : string.Empty;
                        objInvoiceDetailsList.StoreCode = objReader1["StoreCode"] != DBNull.Value ? Convert.ToString(objReader1["StoreCode"]) : string.Empty;
                        objInvoiceDetailsList.PosCode = objReader1["PosCode"] != DBNull.Value ? Convert.ToString(objReader1["PosCode"]) : string.Empty;

                        objInvoiceDetailsList.EmployeeDiscountID = objReader1["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader1["EmployeeDiscountID"]) : 0;
                        objInvoiceDetailsList.FamilyDiscountAmount = objReader1["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["FamilyDiscountAmount"]) : 0;
                        objInvoiceDetailsList.EmployeeDiscountAmount = objReader1["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["EmployeeDiscountAmount"]) : 0;
                        objInvoiceDetailsList.DiscountRemarks = objReader1["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader1["DiscountRemarks"]) : string.Empty;

                        objInvoiceDetailsList.SpecialPromoDiscountType = objReader1["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader1["SpecialPromoDiscountType"]) : string.Empty;
                        objInvoiceDetailsList.SpecialPromoDiscountPercentage = objReader1["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscountPercentage"]) : 0;
                        objInvoiceDetailsList.SpecialPromoDiscount = objReader1["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscount"]) : 0;
                        objInvoiceDetailsList.Tag_Id = objReader1["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader1["Tag_Id"]) : string.Empty;
                        objInvoiceDetailsList.PromoGroupID = objReader1["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader1["PromoGroupID"]) : 0;
                        if (objInvoiceDetailsList.OldExchangeQty > 0)
                        {
                            objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
                            if (objInvoiceDetailsList.OldExchangeQty > 0)
                            {
                                objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
                            }
                            else
                            {
                                objInvoiceDetailsList.ExchangedSKU = string.Empty;
                            }

                        }

                        string ReturnRemarks = string.Empty;
                        if (objInvoiceDetailsList.ExchangeQty > 0)
                        {
                            ReturnRemarks = objInvoiceDetailsList.ExchangeQty.ToString() + " item(s) exchanged.";
                        }
                        if (objInvoiceDetailsList.OldReturnQty > 0)
                        {
                            ReturnRemarks = ReturnRemarks + objInvoiceDetailsList.OldReturnQty.ToString() + " item(s) returned.";
                        }
                        objInvoiceDetailsList.ReturnRemarks = ReturnRemarks;
                        objInvoiceDetailsList.ExchangeRemarks = ReturnRemarks;

                        InvoiceDetailsList.Add(objInvoiceDetailsList);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceDetailsList = InvoiceDetailsList;
                    //ResponseData.InvoiceDetailsList = TailoringOrderDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Details");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllInvoiceResponse SelectPOSSearchAllInvoice(SelectAllInvoiceRequest objRequest)
        {
            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectAllInvoiceRequest)objRequest;
            var ResponseData = new SelectAllInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;


                {
                    sSql = "SELECT TOP 20 IH.ID,IH.InvoiceNo,IH.DocumentDate,IH.BusinessDate,IH.TotalQty,IH.TotalDiscountAmount,IH.NetAmount,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                    sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode ";

                    if (RequestData.SalesStatus != string.Empty)
                    {
                        sSql = sSql + " and ih.SalesStatus='{0}' and IH.StoreID = '" + RequestData.StoreID + "'";
                    }
                    sSql = sSql + " ORDER BY ih.ID DESC";

                    if (RequestData.SalesStatus != string.Empty)
                    {
                        sSql = string.Format(sSql, RequestData.SalesStatus);
                    }

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }


                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        //objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        //objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        //objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        //objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;

                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        //objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        //objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        //objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        //objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        //objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        //objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        //objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        //objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        //objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        //objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        //objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        //objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }
                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllInvoiceResponse SelectedPOSSearchInvoice(SelectAllInvoiceRequest objRequest)
        {
            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectAllInvoiceRequest)objRequest;
            var ResponseData = new SelectAllInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;
                var InvoiceDetailsList = new List<InvoiceDetails>();

                string BusinessDate = sqlCommon.SearchByDate(RequestData.SearchString);
                _CommandObj = new SqlCommand("API_SelectedSearchInvoice", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);



                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();
                        var objInvoiceDetails = new InvoiceDetails();


                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        //objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        //objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        //objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        //objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;
                        //objInvoiceHeaderTypes.SalesEmployeeID = Convert.ToInt32(objReader["SalesEmployeeID"]);
                        //objInvoiceHeaderTypes.CashierID = Convert.ToInt32(objReader["CashierID"]);
                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        //objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        //objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        //objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        //objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        //objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        //objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        //objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        //objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        //objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        //objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        //objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        //objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        //objInvoiceDetails.SKUCode= objReader["SkuCode"] != DBNull.Value ? Convert.ToString(objReader["SkuCode"]) : string.Empty;
                        //objInvoiceDetails.Qty = Convert.ToInt32(objReader["Qty"]);
                        //objInvoiceDetails.Price = Convert.ToInt64(objReader["Price"]);
                        //objInvoiceDetails.SellingPrice= Convert.ToInt64(objReader["SellingPrice"]);

                        //InvoiceDetailsList.Add(objInvoiceDetails);

                        objInvoiceHeaderTypes.InvoiceDetailList = InvoiceDetailsList;
                        //objInvoiceHeaderTypes.Add(InvoiceDetailsList);

                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }

                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    //ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllInvoiceResponse SelectHoldSalesInvoice(SelectAllInvoiceRequest objRequest)
        {
            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectAllInvoiceRequest)objRequest;
            var ResponseData = new SelectAllInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;

                sSql = "SELECT TOP 100 IH.InvoiceNo,IH.ID,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ";
                sSql = sSql + "join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode ";

                sSql = sSql + " where ih.BusinessDate='{0}' and ih.SalesStatus='{1}' order by ih.id desc";

                string BusinessDate = sqlCommon.GetSQLServerDateString(RequestData.BusinessDate);
                sSql = string.Format(sSql, BusinessDate, RequestData.SalesStatus);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        //objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        //objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        //objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        //objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        //objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        //objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        //objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;

                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        //objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        //objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        //objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        //objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        //objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        //objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        //objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        //objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        //objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        //objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        //objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        //objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        //objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        //objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        //objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        //objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        //objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        //{
                        //    objInvoiceHeaderTypes.PosName = objReader["posname"] != DBNull.Value ? Convert.ToString(objReader["posname"]) : string.Empty;
                        //    objInvoiceHeaderTypes.CountryName = objReader["countryname"] != DBNull.Value ? Convert.ToString(objReader["countryname"]) : string.Empty;
                        //    objInvoiceHeaderTypes.StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : string.Empty;
                        //    objInvoiceHeaderTypes.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        //    objInvoiceHeaderTypes.SalesEmployeeCode = objReader["SalesEmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["SalesEmployeeCode"]) : string.Empty;
                        //}
                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }
                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    //ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectInvoiceDetailsListResponse GetSelectedRecallInvoice(SelectInvoiceDetailsListRequest objRequest)
        {
            var InvoiceDetailMasterList = new List<InvoiceDetails>();
            var RequestData = (SelectInvoiceDetailsListRequest)objRequest;
            var ResponseData = new SelectInvoiceDetailsListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                sSql.Append("select Distinct a.ID,a.InvoiceHeaderID,A.SerialNo,a.CountryID,a.StoreID, ");
                sSql.Append("a.SKUID,a.SKUCode,a.BrandID,a.SubBrandID,a.Category,a.Qty,a.Price,a.DiscountType,a.DiscountAmount,a.DiscountRemarks, ");
                sSql.Append("a.AppliedPriceListID,a.AppliedPromotionID,a.CreateBy,a.CreateOn,a.LineTotal,a.TaxAmount,a.TaxID, ");
                sSql.Append("a.Type,a.SellingPrice,a.SellingLineTotal,a.CountryCode,a.StoreCode,a.PosCode,a.InvoiceNo, ");
                sSql.Append("a.DiscountRemarks,a.SpecialPromoDiscount,a.SpecialPromoDiscountPercentage,a.SpecialPromoDiscountType,a.Tag_Id,a.PromoGroupID ");
                sSql.Append(", c.ID as CustomerID,c.CustomerCode,c.CustomerName,b.InvoiceNo,b.DocumentDate,b.NetAmount,SKI.SKUImage,SKI.StyleCode ");
                sSql.Append("from InvoiceDetail a with(NoLock) ");
                sSql.Append("join InvoiceHeader b with(NoLock) on b.ID = a.InvoiceHeaderID ");
                sSql.Append("join CustomerMaster c with(NoLock) on c.CustomerCode = b.CustomerCode ");
                sSql.Append("join SKUMaster SK with(NoLock) on a.SKUCode = SK.SKUCode ");
                sSql.Append("join SKUImages SKI with(NoLock) on Sk.StyleCode = SKI.StyleCode ");
                sSql.Append("where a.InvoiceHeaderID = " + RequestData.InvoiceHeaderID + "");
                sSql.Append(" and b.SalesStatus='" + RequestData.SalesStatus.Trim() + "'");
                sSql.Append(" order by a.SerialNo");



                // Before Optimization - Old One
                //sSql.Append("select Distinct a.*,c.ID as CustomerID,c.CustomerCode,c.CustomerName,b.InvoiceNo,b.DocumentDate,b.NetAmount,sku.StyleCode,red.ExchangeSKU ");
                //sSql.Append("from InvoiceDetail a with(NoLock) join InvoiceHeader b with(NoLock) on b.ID = a.InvoiceHeaderID ");
                //sSql.Append("join CustomerMaster c with(NoLock) on c.CustomerCode = b.CustomerCode ");
                //sSql.Append("left join SKUMaster sku with(NoLock) on a.SKUCode = sku.SKUCode ");
                //sSql.Append("left join SalesExchangeHeader seh with(NoLock) on seh.SalesInvoiceNumber = a.InvoiceNo ");
                //sSql.Append("left join ReturnExchangeDetail red with(NoLock) on a.ID = red.InvoiceDetailID ");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceDetailMaster = new InvoiceDetails();
                        //objInvoiceDetailMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceDetailMaster.InvoiceDetailID = Convert.ToInt32(objReader["ID"]);
                        objInvoiceDetailMaster.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objInvoiceDetailMaster.InvoiceDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        objInvoiceDetailMaster.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceDetailMaster.InvoiceType = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                        objInvoiceDetailMaster.SerialNo = Convert.ToInt32(objReader["SerialNo"]);
                        objInvoiceDetailMaster.SKUID = Convert.ToInt32(objReader["SKUID"]);
                        objInvoiceDetailMaster.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objInvoiceDetailMaster.SKUCode = objReader["SKUCode"].ToString();
                        objInvoiceDetailMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceDetailMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceDetailMaster.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        objInvoiceDetailMaster.Category = Convert.ToInt32(objReader["Category"]);
                        objInvoiceDetailMaster.Qty = Convert.ToInt32(objReader["Qty"]);
                        objInvoiceDetailMaster.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objInvoiceDetailMaster.DiscountType = objReader["DiscountType"].ToString();
                        objInvoiceDetailMaster.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        objInvoiceDetailMaster.LineTotal = objReader["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["LineTotal"]) : 0;
                        objInvoiceDetailMaster.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        //objInvoiceDetailMaster.AppliedCustomerSpecialPricesID = objReader["AppliedCustomerSpecialPricesID"].ToString();
                        objInvoiceDetailMaster.AppliedPromotionID = objReader["AppliedPromotionID"].ToString();
                        //objInvoiceDetailMaster.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["SalesStatus"]) : true;
                        //objInvoiceDetailMaster.ModifiedSalesEmployee = objReader["ModifiedSalesEmployee"].ToString();
                        //objInvoiceDetailMaster.ModifiedSalesManager = objReader["ModifiedSalesManager"].ToString();
                        //objInvoiceDetailMaster.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        //objInvoiceDetailMaster.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        //objInvoiceDetailMaster.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
                        //objInvoiceDetailMaster.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        //objInvoiceDetailMaster.SyncFailedReason = objReader["SyncFailedReason"].ToString();
                        objInvoiceDetailMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceDetailMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objInvoiceDetailMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objInvoiceDetailMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objInvoiceDetailMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objInvoiceDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objInvoiceDetailMaster.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceDetailMaster.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceDetailMaster.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToString(objReader["AppliedPromotionID"]) : string.Empty;

                        objInvoiceDetailMaster.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;
                        objInvoiceDetailMaster.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;

                        objInvoiceDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objInvoiceDetailMaster.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;


                        objInvoiceDetailMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objInvoiceDetailMaster.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objInvoiceDetailMaster.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objInvoiceDetailMaster.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;


                        //objInvoiceDetailMaster.EmployeeDiscountID = objReader["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeDiscountID"]) : 0;
                        //objInvoiceDetailMaster.FamilyDiscountAmount = objReader["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FamilyDiscountAmount"]) : 0;
                        //objInvoiceDetailMaster.EmployeeDiscountAmount = objReader["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["EmployeeDiscountAmount"]) : 0;
                        objInvoiceDetailMaster.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        objInvoiceDetailMaster.SpecialPromoDiscountType = objReader["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader["SpecialPromoDiscountType"]) : string.Empty;
                        objInvoiceDetailMaster.SpecialPromoDiscountPercentage = objReader["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscountPercentage"]) : 0;
                        objInvoiceDetailMaster.SpecialPromoDiscount = objReader["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscount"]) : 0;
                        objInvoiceDetailMaster.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : string.Empty;
                        objInvoiceDetailMaster.PromoGroupID = objReader["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["PromoGroupID"]) : 0;
                        objInvoiceDetailMaster.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        InvoiceDetailMasterList.Add(objInvoiceDetailMaster);
                        ResponseData.InvoiceDetailsList = InvoiceDetailMasterList;

                        if (RequestData.SalesStatus == "ParkSale")
                        {
                            SqlConnection con = new SqlConnection();
                            sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                            string sql = "Delete from InvoiceHeader where ID={0}; Delete from InvoiceDetail where InvoiceHeaderID={0}";

                            sql = string.Format(sql, RequestData.InvoiceHeaderID);
                            SqlCommand cmd;

                            cmd = new SqlCommand(sql, con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Recall Invoice");

                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        #region SelectBillCompletedSalesInvoice

        public override SelectAllInvoiceResponse SelectBillCompletedSalesInvoice(SelectAllInvoiceRequest objRequest)
        {

            var InvoiceHeaderList = new List<InvoiceHeader>();
            var RequestData = (SelectAllInvoiceRequest)objRequest;
            var ResponseData = new SelectAllInvoiceResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;
                var InvoiceDetailsList = new List<InvoiceDetails>();

                //string BusinessDate = sqlCommon.SearchByDate(RequestData.SearchString);
                _CommandObj = new SqlCommand("API_BillCompletedInvoiceInfo", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNo);



                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeader();


                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;
                        objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objInvoiceHeaderTypes.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        objInvoiceHeaderTypes.IsCreditSale = objReader["IsCreditSale"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditSale"]) : true;
                        objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        objInvoiceHeaderTypes.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceHeaderTypes.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceHeaderTypes.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
                        objInvoiceHeaderTypes.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;

                        objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        //objInvoiceHeaderTypes.FromCountryID = objReader["FromCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["FromCountryID"]) : 0;
                        //objInvoiceHeaderTypes.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
                        //objInvoiceHeaderTypes.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnRefID"]) : 0;
                        //objInvoiceHeaderTypes.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;


                        //objInvoiceHeaderTypes.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeRefID"]) : 0;
                        //objInvoiceHeaderTypes.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        //objInvoiceHeaderTypes.FromPosID = objReader["FromPosID"] != DBNull.Value ? Convert.ToInt32(objReader["FromPosID"]) : 0;
                        //objInvoiceHeaderTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;

                        //objInvoiceHeaderTypes.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeRefID"]) : 0;
                        //objInvoiceHeaderTypes.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
                        //objInvoiceHeaderTypes.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnRefID"]) : 0;
                        //objInvoiceHeaderTypes.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;

                        objInvoiceHeaderTypes.TotalDiscountPercentage = objReader["TotalDiscountPercentage"] != DBNull.Value ? Convert.ToInt32(objReader["TotalDiscountPercentage"]) : 0;
                        objInvoiceHeaderTypes.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;

                        //objInvoiceHeaderTypes.OrjwanEntry = objReader["OrjwanEntry"] != DBNull.Value ? Convert.ToInt32(objReader["OrjwanEntry"]) : 0;
                        //objInvoiceHeaderTypes.IsDataSyncToOrjwan = objReader["IsDataSyncToOrjwan"] != DBNull.Value ? Convert.ToInt32(objReader["IsDataSyncToOrjwan"]) : 0;

                        //  objInvoiceHeaderTypes.OrjwanServerSyncTime = objReader["OrjwanServerSyncTime"] != DBNull.Value ? Convert.ToInt32(objReader["OrjwanServerSyncTime"]) : 0;



                        objInvoiceHeaderTypes.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objInvoiceHeaderTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objInvoiceHeaderTypes.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        // objInvoiceHeaderTypes.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;

                        objInvoiceHeaderTypes.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.TaxCode = objReader["TaxCode"] != DBNull.Value ? Convert.ToString(objReader["TaxCode"]) : string.Empty;
                        // objInvoiceHeaderTypes.AppliedPriceListCode = objReader["AppliedPriceListCode"] != DBNull.Value ? Convert.ToString(objReader["AppliedPriceListCode"]) : string.Empty;
                        objInvoiceHeaderTypes.SalesEmployeeCode = objReader["SalesEmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["SalesEmployeeCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.SalesManagerCode = objReader["SalesManagerCode"] != DBNull.Value ? Convert.ToString(objReader["SalesManagerCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.ShiftCode = objReader["ShiftCode"] != DBNull.Value ? Convert.ToString(objReader["ShiftCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.FromCountryCode = objReader["FromCountryCode"] != DBNull.Value ? Convert.ToString(objReader["FromCountryCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.FromStoreCode = objReader["FromStoreCode"] != DBNull.Value ? Convert.ToString(objReader["FromStoreCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.FromPosCode = objReader["FromPosCode"] != DBNull.Value ? Convert.ToString(objReader["FromPosCode"]) : string.Empty;


                        //objInvoiceHeaderTypes.ShiftCode = objReader["ShiftCode"] != DBNull.Value ? Convert.ToString(objReader["ShiftCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.ExchangeRefCode = objReader["ExchangeRefCode"] != DBNull.Value ? Convert.ToString(objReader["ExchangeRefCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.ReturnRefCode = objReader["ReturnRefCode"] != DBNull.Value ? Convert.ToString(objReader["ReturnRefCode"]) : string.Empty;
                        //objInvoiceHeaderTypes.CashierCode = objReader["CashierCode"] != DBNull.Value ? Convert.ToString(objReader["CashierCode"]) : string.Empty;

                        //objInvoiceHeaderTypes.IsDataSyncToOtherStores = objReader["IsDataSyncToOtherStores"] != DBNull.Value ? Convert.ToString(objReader["IsDataSyncToOtherStores"]) : string.Empty;
                        //objInvoiceHeaderTypes.DataSyncToOtherStoresTime = objReader["DataSyncToOtherStoresTime"] != DBNull.Value ? Convert.ToString(objReader["DataSyncToOtherStoresTime"]) : string.Empty;

                        objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;
                        // objInvoiceHeaderTypes.EmailSend = objReader["EmailSend"] != DBNull.Value ? Convert.ToString(objReader["EmailSend"]) : string.Empty;
                        // objInvoiceHeaderTypes.SMSSend = objReader["SMSSend"] != DBNull.Value ? Convert.ToString(objReader["SMSSend"]) : string.Empty;
                        objInvoiceHeaderTypes.BeforeRoundOffAmount = objReader["BeforeRoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BeforeRoundOffAmount"]) : 0;

                        objInvoiceHeaderTypes.RoundOffAmount = objReader["RoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["RoundOffAmount"]) : 0;
                        // objInvoiceHeaderTypes.DocDetailID = objReader["DocDetailID"] != DBNull.Value ? Convert.ToString(objReader["DocDetailID"]) : string.Empty;


                        // "Senthamil_Changes"
                        {
                            objInvoiceHeaderTypes.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponCode = objReader["RedeemCouponCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponLineNo = objReader["RedeemCouponLineNo"] != DBNull.Value ? Convert.ToInt32(objReader["RedeemCouponLineNo"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponSerialCode = objReader["RedeemCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountType = objReader["RedeemCouponDiscountType"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponDiscountType"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountValue = objReader["RedeemCouponDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemCouponDiscountValue"]) : 0;
                            objInvoiceHeaderTypes.RedeemValue = objReader["RedeemValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemValue"]) : 0;
                            objInvoiceHeaderTypes.IssuedCouponCode = objReader["IssuedCouponCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponLineNo = objReader["IssuedCouponLineNo"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponLineNo"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponSerialCode = objReader["IssuedCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerMobileNo = objReader["CustomerMobileNo"] != DBNull.Value ? Convert.ToString(objReader["CustomerMobileNo"]) : string.Empty;
                        }



                        // objInvoiceHeaderTypes.DocRunningNo = objReader["DocRunningNo"] != DBNull.Value ? Convert.ToString(objReader["DocRunningNo"]) : string.Empty;
                        //objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        //objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        objInvoiceHeaderTypes.InvoiceDetailList = InvoiceDetailsList;
                        //objInvoiceHeaderTypes.Add(InvoiceDetailsList);

                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }

                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    //ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
            throw new NotImplementedException();
        }

        public override SearchCommonInvoiceResponse GetSearchInvoice(SearchCommonInvoiceRequest objRequest)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new SearchCommonInvoiceRequest();
            var ResponseData = new SearchCommonInvoiceResponse();

            RequestData = (SearchCommonInvoiceRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                if (RequestData.SearchString != null)
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    var sSql = new StringBuilder();

                    string sQuery = string.Empty;
                    //if (RequestData.SearchString != null)
                    //{
                    sSql.Append("Select Top 10 InvoiceNo [Code],BusinessDate [Date],cm.customername[Name],cm.phonenumber[Number] from invoiceheader ih WITH(NOLOCK) ");
                    sSql.Append("left join customerMaster cm WITH(NOLOCK) ");
                    sSql.Append("on cm.id=ih.customerid WHERE ih.StoreID= " + RequestData.Storeid + " ");
                    sSql.Append("and InvoiceNo LIKE '%" + RequestData.SearchString + "%' Or BusinessDate LIKE '%" + RequestData.SearchString + "%'" +
                        " Or cm.customername LIKE '%" + RequestData.SearchString + "%'  OR cm.PhoneNumber LIKE '%" + RequestData.SearchString + "%'");
                    //sSql.Append(" WHERE TL.StoreID = " + RequestData.Storeid + " and SKU.SKUCode LIKE '%" + RequestData.SearchString + "%' Or SKU.SKUName LIKE '%" + RequestData.SearchString + "%'");
                    //}
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objSearchEngine = new SearchEngine();

                            objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                            objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                            objSearchEngine.Date = objReader["Date"] != DBNull.Value ? Convert.ToString(objReader["Date"]) : string.Empty;
                            objSearchEngine.Number = objReader["Number"] != DBNull.Value ? Convert.ToString(objReader["Number"]) : string.Empty;



                            SearchEngineList.Add(objSearchEngine);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.SearchEngineDataList = SearchEngineList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.InvalidInput;
                    ResponseData.SearchEngineDataList = null;
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        #endregion


        //public List<InvoiceDetails> GetInvocieDetailsListBasedonInvoiceno(long ID, SqlConnection sqlConnection, string Mode)
        //{
        //    var InvoiceDetailsList = new List<InvoiceDetails>();
        //    try
        //    {
        //        //sqlConnection.Open();
        //        //SqlCommand _CommandObj;
        //        var sqlCommon = new MsSqlCommon();
        //        string sSql;
        //        SqlDataReader objReader1;

        //        if (Mode == "Invoice")
        //        {
        //            sSql = "select * from InvoiceDetail where InvoiceHeaderID = " + ID + "Order by ID";
        //        }
        //        else
        //        {
        //            sSql =  "select * from SalesExchangeDetails where SalesExchangeID = " + ID + "Order by ID";
        //        }
        //        _CommandObj = new SqlCommand(sSql, sqlConnection);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader1 = _CommandObj.ExecuteReader();
        //        if (objReader1.HasRows)
        //        {
        //            var objInvoiceDetailsList = new InvoiceDetails();
        //            objInvoiceDetailsList.InvoiceDetailID = Convert.ToInt32(objReader1["ID"]);
        //            objInvoiceDetailsList.InvoiceType = objReader1["Type"] != DBNull.Value ? Convert.ToString(objReader1["Type"]) : string.Empty;
        //            objInvoiceDetailsList.SerialNo = Convert.ToInt32(objReader1["SerialNo"]);
        //            objInvoiceDetailsList.SKUID = Convert.ToInt32(objReader1["SKUID"]);
        //            objInvoiceDetailsList.InvoiceHeaderID = Convert.ToInt32(objReader1["InvoiceHeaderID"]);
        //            objInvoiceDetailsList.SKUCode = objReader1["SKUCode"].ToString();
        //            objInvoiceDetailsList.BrandID = Convert.ToInt32(objReader1["BrandID"]);
        //            objInvoiceDetailsList.Category = Convert.ToInt32(objReader1["Category"]);
        //            objInvoiceDetailsList.Qty = Convert.ToInt32(objReader1["Qty"]);
        //            objInvoiceDetailsList.Price = objReader1["Price"] != DBNull.Value ? Convert.ToDecimal(objReader1["Price"]) : 0;
        //            objInvoiceDetailsList.DiscountType = objReader1["DiscountType"].ToString();
        //            objInvoiceDetailsList.DiscountAmount = objReader1["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["DiscountAmount"]) : 0;
        //            objInvoiceDetailsList.LineTotal = objReader1["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["LineTotal"]) : 0;
        //            objInvoiceDetailsList.AppliedPriceListID = objReader1["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader1["AppliedPriceListID"]) : 0;
        //            objInvoiceDetailsList.AppliedCustomerSpecialPricesID = objReader1["AppliedCustomerSpecialPricesID"].ToString();
        //            objInvoiceDetailsList.AppliedPromotionID = objReader1["AppliedPromotionID"].ToString();
        //            objInvoiceDetailsList.SalesStatus = objReader1["SalesStatus"] != DBNull.Value ? Convert.ToBoolean(objReader1["SalesStatus"]) : true;
        //            objInvoiceDetailsList.ModifiedSalesEmployee = objReader1["ModifiedSalesEmployee"].ToString();
        //            objInvoiceDetailsList.ModifiedSalesManager = objReader1["ModifiedSalesManager"].ToString();
        //            objInvoiceDetailsList.IsDataSyncToCountryServer = objReader1["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsDataSyncToCountryServer"]) : true;
        //            objInvoiceDetailsList.IsDataSyncToMainServer = objReader1["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader1["IsDataSyncToMainServer"]) : true;
        //            objInvoiceDetailsList.CountryServerSyncTime = objReader1["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader1["CountryServerSyncTime"]) : DateTime.Now;
        //            objInvoiceDetailsList.MainServerSyncTime = objReader1["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader1["MainServerSyncTime"]) : DateTime.Now;
        //            objInvoiceDetailsList.SyncFailedReason = objReader1["SyncFailedReason"].ToString();
        //            objInvoiceDetailsList.CreateOn = objReader1["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader1["CreateOn"]) : DateTime.Now;
        //            objInvoiceDetailsList.CreateBy = objReader1["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader1["CreateBy"]) : 0;
        //            objInvoiceDetailsList.UpdateOn = objReader1["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader1["UpdateOn"]) : DateTime.Now;
        //            objInvoiceDetailsList.UpdateBy = objReader1["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader1["UpdateBy"]) : 0; ;
        //            objInvoiceDetailsList.SCN = objReader1["SCN"] != DBNull.Value ? Convert.ToInt32(objReader1["SCN"]) : 0;
        //            objInvoiceDetailsList.Active = objReader1["Active"] != DBNull.Value ? Convert.ToBoolean(objReader1["Active"]) : true;
        //            objInvoiceDetailsList.TaxID = objReader1["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader1["TaxID"]) : 0;
        //            objInvoiceDetailsList.TaxAmount = objReader1["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["TaxAmount"]) : 0;
        //            objInvoiceDetailsList.AppliedPromotionID = objReader1["AppliedPromotionID"] != DBNull.Value ? Convert.ToString(objReader1["AppliedPromotionID"]) : string.Empty;

        //            objInvoiceDetailsList.SellingPrice = objReader1["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingPrice"]) : 0;
        //            objInvoiceDetailsList.SellingLineTotal = objReader1["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader1["SellingLineTotal"]) : 0;

        //            objInvoiceDetailsList.StyleCode = Convert.ToString(objReader1["StyleCode"]);

        //            objInvoiceDetailsList.ExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0; // need to change for Sales exchange
        //            objInvoiceDetailsList.OldExchangeQty = objReader1["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ExchangeQty"]) : 0;

        //            objInvoiceDetailsList.ReturnQty = 0;
        //            objInvoiceDetailsList.OldReturnQty = objReader1["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader1["ReturnQty"]) : 0;
        //            objInvoiceDetailsList.ExchangeRefID = objReader1["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ExchangeRefID"]) : 0;
        //            objInvoiceDetailsList.ReturnRefID = objReader1["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader1["ReturnRefID"]) : 0;

        //            objInvoiceDetailsList.CountryCode = objReader1["CountryCode"] != DBNull.Value ? Convert.ToString(objReader1["CountryCode"]) : string.Empty;
        //            objInvoiceDetailsList.StoreCode = objReader1["StoreCode"] != DBNull.Value ? Convert.ToString(objReader1["StoreCode"]) : string.Empty;
        //            objInvoiceDetailsList.PosCode = objReader1["PosCode"] != DBNull.Value ? Convert.ToString(objReader1["PosCode"]) : string.Empty;
        //            objInvoiceDetailsList.CustomerCode = objReader1["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader1["CustomerCode"]) : string.Empty;


        //            objInvoiceDetailsList.EmployeeDiscountID = objReader1["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader1["EmployeeDiscountID"]) : 0;
        //            objInvoiceDetailsList.FamilyDiscountAmount = objReader1["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["FamilyDiscountAmount"]) : 0;
        //            objInvoiceDetailsList.EmployeeDiscountAmount = objReader1["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader1["EmployeeDiscountAmount"]) : 0;
        //            objInvoiceDetailsList.DiscountRemarks = objReader1["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader1["DiscountRemarks"]) : string.Empty;

        //            objInvoiceDetailsList.SpecialPromoDiscountType = objReader1["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader1["SpecialPromoDiscountType"]) : string.Empty;
        //            objInvoiceDetailsList.SpecialPromoDiscountPercentage = objReader1["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscountPercentage"]) : 0;
        //            objInvoiceDetailsList.SpecialPromoDiscount = objReader1["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader1["SpecialPromoDiscount"]) : 0;
        //            objInvoiceDetailsList.Tag_Id = objReader1["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader1["Tag_Id"]) : string.Empty;
        //            objInvoiceDetailsList.PromoGroupID = objReader1["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader1["PromoGroupID"]) : 0;
        //            if (objInvoiceDetailsList.OldExchangeQty > 0)
        //            {
        //                objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
        //                if (objInvoiceDetailsList.OldExchangeQty > 0)
        //                {
        //                    objInvoiceDetailsList.ExchangedSKU = objReader1["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader1["ExchangeSKU"]) : string.Empty;
        //                }
        //                else
        //                {
        //                    objInvoiceDetailsList.ExchangedSKU = string.Empty;
        //                }

        //                //List<InvoiceDetails> detailslist = new List<InvoiceDetails>();
        //                ////detailslist.Add(objInvoiceDetailsList);

        //                //// objInvoiceHeaderMaster.InvoiceDetailList = detailslist;





        //                //objInvoiceHeaderMaster.PaymentList = new List<PaymentDetail>();

        //                //var objInvoiceCashDetailsDAL = new InvoiceCashDetailsDAL();
        //                //var objSelectByInvoiceNoCashDetailsRequest = new SelectByInvoiceNoCashDetailsRequest();
        //                //var objSelectByInvoiceNoCashDetailsResponse = new SelectByInvoiceNoCashDetailsResponse();
        //                //objSelectByInvoiceNoCashDetailsRequest.InvoiceHeaderID = objInvoiceDetailsList.InvoiceHeaderID;
        //                //objSelectByInvoiceNoCashDetailsRequest.InvoiceNo = objInvoiceHeaderMaster.InvoiceNo;
        //                //objSelectByInvoiceNoCashDetailsRequest.ConnectionString = RequestData.ConnectionString;
        //                //objSelectByInvoiceNoCashDetailsResponse = objInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objSelectByInvoiceNoCashDetailsRequest);

        //                //if (objSelectByInvoiceNoCashDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
        //                //{
        //                //    objInvoiceHeaderMaster.PaymentList.AddRange(objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails);
        //                //    //objInvoiceHeaderMaster.PaymentList = objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails;
        //                //}


        //                //var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
        //                //var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
        //                //var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
        //                //objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = objInvoiceDetailsList.InvoiceHeaderID;
        //                //objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = objInvoiceHeaderMaster.InvoiceNo;
        //                //objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;
        //                //objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
        //                //if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
        //                //{
        //                //    objInvoiceHeaderMaster.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
        //                //    ////objInvoiceHeaderMaster.PaymentList = objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails;
        //                //}
        //                ////}

        //                string ReturnRemarks = string.Empty;
        //                if (objInvoiceDetailsList.ExchangeQty > 0)
        //                {
        //                    ReturnRemarks = objInvoiceDetailsList.ExchangeQty + " items are exchanged this sales.";
        //                }
        //                if (objInvoiceDetailsList.OldReturnQty > 0)
        //                {
        //                    ReturnRemarks = ReturnRemarks + objInvoiceDetailsList.OldReturnQty + " items are already returned this sales.";
        //                }
        //                objInvoiceDetailsList.ReturnRemarks = ReturnRemarks;
        //                objInvoiceDetailsList.ExchangeRemarks = ReturnRemarks;
        //                InvoiceDetailsList.Add(objInvoiceDetailsList);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //    return InvoiceDetailsList;
        //}
        //public override GetSearchInvoiceHeaderDetailsResponse GetSearchInvoiceHeaderDetails(SelectInvoiceDetailsListRequest objRequest)
        //{
        //    var InvoiceHeaderMasterList = new List<InvoiceHeader>();
        //    var RequestData = (SelectInvoiceDetailsListRequest)objRequest;
        //    var ResponseData = new GetSearchInvoiceHeaderDetailsResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;

        //        var sSql = new StringBuilder();

        //        sSql.Append("select Distinct a.*,b.*,c.ID as CustomerID,c.CustomerCode,c.CustomerName,b.InvoiceNo,b.DocumentDate,b.NetAmount,sku.StyleCode,red.ExchangeSKU ");
        //        sSql.Append("from InvoiceDetail a with(NoLock) join InvoiceHeader b with(NoLock) on b.ID = a.InvoiceHeaderID ");
        //        sSql.Append("join CustomerMaster c with(NoLock) on c.CustomerCode = b.CustomerCode ");
        //        sSql.Append("left join SKUMaster sku with(NoLock) on a.SKUCode = sku.SKUCode ");
        //        sSql.Append("left join SalesExchangeHeader seh with(NoLock) on seh.SalesInvoiceNumber = a.InvoiceNo ");
        //        sSql.Append("left join ReturnExchangeDetail red with(NoLock) on a.ID = red.InvoiceDetailID ");


        //        if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
        //        {
        //            sSql.Append("where b.InvoiceNo='" + RequestData.SearchString + "'");
        //        }
        //        else
        //        {
        //            sSql.Append("where a.InvoiceHeaderID = " + RequestData.InvoiceHeaderID + "");
        //        }
        //        if (RequestData.SalesStatus == null || RequestData.SalesStatus.Trim() == string.Empty)
        //        {
        //            sSql.Append(" and b.SalesStatus='Completed'");
        //        }
        //        else
        //        {
        //            sSql.Append(" and b.SalesStatus='" + RequestData.SalesStatus.Trim() + "'");
        //        }
        //        sSql.Append(" order by a.SerialNo");

        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
        //        _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objInvoiceHeaderMaster = new InvoiceHeader();
        //                objInvoiceHeaderMaster.ID = Convert.ToInt32(objReader["ID"]);
        //                objInvoiceHeaderMaster.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
        //                objInvoiceHeaderMaster.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
        //                objInvoiceHeaderMaster.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
        //                objInvoiceHeaderMaster.CountryID = Convert.ToInt32(objReader["CountryID"]);
        //                objInvoiceHeaderMaster.StoreID = Convert.ToInt32(objReader["StoreID"]);
        //                objInvoiceHeaderMaster.CustomerID = Convert.ToInt32(objReader["CustomerID"]);
        //                objInvoiceHeaderMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
        //                objInvoiceHeaderMaster.SalesStatus = Convert.ToString(objReader["SAlesStatus"]);
        //                objInvoiceHeaderMaster.IsCreditSale = objReader["IsCreditSale"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditSale"]) : false;




        //                //objInvoiceDetailMaster.InvoiceDetailList = new List<InvoiceDetails>();

        //                var objInvoiceDetailsList = new InvoiceDetails();
        //                objInvoiceDetailsList.InvoiceDetailID = Convert.ToInt32(objReader["ID"]);
        //                objInvoiceDetailsList.InvoiceType = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
        //                objInvoiceDetailsList.SerialNo = Convert.ToInt32(objReader["SerialNo"]);
        //                objInvoiceDetailsList.SKUID = Convert.ToInt32(objReader["SKUID"]);
        //                objInvoiceDetailsList.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
        //                objInvoiceDetailsList.SKUCode = objReader["SKUCode"].ToString();
        //                objInvoiceDetailsList.BrandID = Convert.ToInt32(objReader["BrandID"]);
        //                objInvoiceDetailsList.Category = Convert.ToInt32(objReader["Category"]);
        //                objInvoiceDetailsList.Qty = Convert.ToInt32(objReader["Qty"]);
        //                objInvoiceDetailsList.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
        //                objInvoiceDetailsList.DiscountType = objReader["DiscountType"].ToString();
        //                objInvoiceDetailsList.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
        //                objInvoiceDetailsList.LineTotal = objReader["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["LineTotal"]) : 0;
        //                objInvoiceDetailsList.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
        //                objInvoiceDetailsList.AppliedCustomerSpecialPricesID = objReader["AppliedCustomerSpecialPricesID"].ToString();
        //                objInvoiceDetailsList.AppliedPromotionID = objReader["AppliedPromotionID"].ToString();
        //                objInvoiceDetailsList.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["SalesStatus"]) : true;
        //                objInvoiceDetailsList.ModifiedSalesEmployee = objReader["ModifiedSalesEmployee"].ToString();
        //                objInvoiceDetailsList.ModifiedSalesManager = objReader["ModifiedSalesManager"].ToString();
        //                objInvoiceDetailsList.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
        //                objInvoiceDetailsList.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
        //                objInvoiceDetailsList.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
        //                objInvoiceDetailsList.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
        //                objInvoiceDetailsList.SyncFailedReason = objReader["SyncFailedReason"].ToString();
        //                objInvoiceDetailsList.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
        //                objInvoiceDetailsList.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
        //                objInvoiceDetailsList.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
        //                objInvoiceDetailsList.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
        //                objInvoiceDetailsList.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
        //                objInvoiceDetailsList.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
        //                objInvoiceDetailsList.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
        //                objInvoiceDetailsList.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
        //                objInvoiceDetailsList.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToString(objReader["AppliedPromotionID"]) : string.Empty;

        //                objInvoiceDetailsList.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;
        //                objInvoiceDetailsList.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;

        //                objInvoiceDetailsList.StyleCode = Convert.ToString(objReader["StyleCode"]);

        //                objInvoiceDetailsList.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0; // need to change for Sales exchange
        //                objInvoiceDetailsList.OldExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;

        //                objInvoiceDetailsList.ReturnQty = 0;
        //                objInvoiceDetailsList.OldReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
        //                objInvoiceDetailsList.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ExchangeRefID"]) : 0;
        //                objInvoiceDetailsList.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt64(objReader["ReturnRefID"]) : 0;

        //                objInvoiceDetailsList.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
        //                objInvoiceDetailsList.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
        //                objInvoiceDetailsList.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
        //                objInvoiceDetailsList.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;


        //                objInvoiceDetailsList.EmployeeDiscountID = objReader["EmployeeDiscountID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeDiscountID"]) : 0;
        //                objInvoiceDetailsList.FamilyDiscountAmount = objReader["FamilyDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FamilyDiscountAmount"]) : 0;
        //                objInvoiceDetailsList.EmployeeDiscountAmount = objReader["EmployeeDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["EmployeeDiscountAmount"]) : 0;
        //                objInvoiceDetailsList.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

        //                objInvoiceDetailsList.SpecialPromoDiscountType = objReader["SpecialPromoDiscountType"] != DBNull.Value ? Convert.ToString(objReader["SpecialPromoDiscountType"]) : string.Empty;
        //                objInvoiceDetailsList.SpecialPromoDiscountPercentage = objReader["SpecialPromoDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscountPercentage"]) : 0;
        //                objInvoiceDetailsList.SpecialPromoDiscount = objReader["SpecialPromoDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SpecialPromoDiscount"]) : 0;
        //                objInvoiceDetailsList.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : string.Empty;
        //                objInvoiceDetailsList.PromoGroupID = objReader["PromoGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["PromoGroupID"]) : 0;
        //                if (objInvoiceDetailsList.OldExchangeQty > 0)
        //                {
        //                    objInvoiceDetailsList.ExchangedSKU = objReader["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader["ExchangeSKU"]) : string.Empty;
        //                }
        //                else
        //                {
        //                    objInvoiceDetailsList.ExchangedSKU = string.Empty;
        //                }

        //                List<InvoiceDetails> detailslist = new List<InvoiceDetails>();
        //                //detailslist.Add(objInvoiceDetailsList);

        //               // objInvoiceHeaderMaster.InvoiceDetailList = detailslist;





        //                objInvoiceHeaderMaster.PaymentList = new List<PaymentDetail>();

        //                var objInvoiceCashDetailsDAL = new InvoiceCashDetailsDAL();
        //                var objSelectByInvoiceNoCashDetailsRequest = new SelectByInvoiceNoCashDetailsRequest();
        //                var objSelectByInvoiceNoCashDetailsResponse = new SelectByInvoiceNoCashDetailsResponse();
        //                objSelectByInvoiceNoCashDetailsRequest.InvoiceHeaderID = objInvoiceDetailsList.InvoiceHeaderID;
        //                objSelectByInvoiceNoCashDetailsRequest.InvoiceNo = objInvoiceHeaderMaster.InvoiceNo;
        //                objSelectByInvoiceNoCashDetailsRequest.ConnectionString = RequestData.ConnectionString;
        //                objSelectByInvoiceNoCashDetailsResponse = objInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objSelectByInvoiceNoCashDetailsRequest);

        //                if (objSelectByInvoiceNoCashDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
        //                {
        //                    objInvoiceHeaderMaster.PaymentList.AddRange(objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails);
        //                    //objInvoiceHeaderMaster.PaymentList = objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails;
        //                }


        //                var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
        //                var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
        //                var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
        //                objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = objInvoiceDetailsList.InvoiceHeaderID;
        //                objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = objInvoiceHeaderMaster.InvoiceNo;
        //                objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;
        //                objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
        //                if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
        //                {
        //                    objInvoiceHeaderMaster.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
        //                    ////objInvoiceHeaderMaster.PaymentList = objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails;
        //                }
        //                //}

        //                string ReturnRemarks = string.Empty;
        //                if (objInvoiceDetailsList.ExchangeQty > 0)
        //                {
        //                    ReturnRemarks = objInvoiceDetailsList.ExchangeQty + " items are exchanged this sales.";
        //                }
        //                if (objInvoiceDetailsList.OldReturnQty > 0)
        //                {
        //                    ReturnRemarks = ReturnRemarks + objInvoiceDetailsList.OldReturnQty + " items are already returned this sales.";
        //                }
        //                objInvoiceDetailsList.ReturnRemarks = ReturnRemarks;
        //                objInvoiceDetailsList.ExchangeRemarks = ReturnRemarks;

        //                detailslist.Add(objInvoiceDetailsList);
        //                objInvoiceHeaderMaster.InvoiceDetailList = detailslist;
        //                InvoiceHeaderMasterList.Add(objInvoiceHeaderMaster);

        //                //PaymentList.Add(objInvoiceCashDetailsDAL)
        //               ResponseData.InvoiceHeaderDetailsList = InvoiceHeaderMasterList;
        //            }
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;

        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice");
        //        }
        //    }     
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlCommon.CloseConnection(_ConnectionObj);
        //    }
        //    return ResponseData;
        //}

    }
}

