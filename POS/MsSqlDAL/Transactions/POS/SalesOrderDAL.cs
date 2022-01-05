using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizResponse;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using EasyBizResponse.Transactions.POS.SalesOrder;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.POS
{
    public class SalesOrderDAL : BaseSalesOrderDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveSalesOrderRequest)RequestObj;
            var ResponseData = new SaveSalesOrderReponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertorUpdateSalesOrder", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.SalesOrderHeaderRecord.ID;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.SalesOrderHeaderRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.SalesOrderHeaderRecord.DocumentDate);

                SqlParameter DeliveryDate = _CommandObj.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                DeliveryDate.Direction = ParameterDirection.Input;
                DeliveryDate.Value = sqlCommon.GetSQLServerDateString(RequestData.SalesOrderHeaderRecord.DeliveryDate);

                SqlParameter TotalQty = _CommandObj.Parameters.Add("@TotalQty", SqlDbType.Int);
                TotalQty.Direction = ParameterDirection.Input;
                TotalQty.Value = RequestData.SalesOrderHeaderRecord.TotalQty;

                SqlParameter TotalAmount = _CommandObj.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
                TotalAmount.Direction = ParameterDirection.Input;
                TotalAmount.Value = RequestData.SalesOrderHeaderRecord.TotalAmount;

                SqlParameter DiscountType = _CommandObj.Parameters.Add("@DiscountType", SqlDbType.NVarChar);
                DiscountType.Direction = ParameterDirection.Input;
                DiscountType.Value = RequestData.SalesOrderHeaderRecord.DiscountType;

                SqlParameter DiscountValue = _CommandObj.Parameters.Add("@DiscountValue", SqlDbType.Decimal);
                DiscountValue.Direction = ParameterDirection.Input;
                DiscountValue.Value = RequestData.SalesOrderHeaderRecord.DiscountValue;

                SqlParameter NetAmount = _CommandObj.Parameters.Add("@NetAmount", SqlDbType.Decimal);
                NetAmount.Direction = ParameterDirection.Input;
                NetAmount.Value = RequestData.SalesOrderHeaderRecord.NetAmount;

                SqlParameter OrderStatus = _CommandObj.Parameters.Add("@OrderStatus", SqlDbType.NVarChar);
                OrderStatus.Direction = ParameterDirection.Input;
                OrderStatus.Value = RequestData.SalesOrderHeaderRecord.OrderStatus;

                SqlParameter PaymentStatus = _CommandObj.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar);
                PaymentStatus.Direction = ParameterDirection.Input;
                PaymentStatus.Value = RequestData.SalesOrderHeaderRecord.PaymentStatus;

                SqlParameter CustomerCode = _CommandObj.Parameters.Add("@CustomerCode", SqlDbType.NVarChar);
                CustomerCode.Direction = ParameterDirection.Input;
                CustomerCode.Value = RequestData.SalesOrderHeaderRecord.CustomerCode;


                SqlParameter SalesOrderDetails = _CommandObj.Parameters.Add("@SalesOrderDetails", SqlDbType.Xml);
                SalesOrderDetails.Direction = ParameterDirection.Input;
                SalesOrderDetails.Value = SalesOrderDetailMasterXML(RequestData.SalesOrderDetailsList);

                string sCashDetails = CashDetailXML(RequestData.PaymentList);
                var CashDetails = _CommandObj.Parameters.Add("@CashDetails", SqlDbType.Xml);
                CashDetails.Direction = ParameterDirection.Input;

                if (sCashDetails != string.Empty)
                {
                    CashDetails.Value = sCashDetails;
                }
                else
                {
                    CashDetails.Value = DBNull.Value;
                }

                string sCardDetails = CardDetailXML(RequestData.PaymentList);
                var CardDetails = _CommandObj.Parameters.Add("@CardDetails", SqlDbType.Xml);
                CardDetails.Direction = ParameterDirection.Input;
                if (sCardDetails != string.Empty)
                {
                    CardDetails.Value = sCardDetails;
                }
                else
                {
                    CardDetails.Value = DBNull.Value;
                }

                SqlParameter RunningNo = _CommandObj.Parameters.Add("@RunningNo", SqlDbType.BigInt);
                RunningNo.Direction = ParameterDirection.Input;
                RunningNo.Value = RequestData.RunningNo;

                SqlParameter DocumentNumberingID = _CommandObj.Parameters.Add("@DocumentNumberingID", SqlDbType.BigInt);
                DocumentNumberingID.Direction = ParameterDirection.Input;
                DocumentNumberingID.Value = RequestData.DocumentNumberingID;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.SalesOrderHeaderRecord.CreateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sales Order");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Order");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Order");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }



        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var SalesOrderRecord = new SalesOrderHeader();
            var RequestData = (DeleteSalesOrderRequest)RequestObj;
            var ResponseData = new DeleteSalesOrderResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from  SalesOrderDetails where SalesOrderID={0} ; Delete from SalesOrderHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "SalesOrder");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SalesOrder");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var SalesOrderRecord = new SalesOrderHeader();
            var RequestData = (SelectBySalesOrderIDRequest)RequestObj;
            var ResponseData = new SelectBySalesOrderIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from SalesOrderHeader with(NoLock) ";

                if (RequestData.ID > 0)
                {
                    sSql = sSql + "where ID={0}";
                    sSql = string.Format(sSql, RequestData.ID);
                }
                else if(RequestData.DocumentNo != null && RequestData.DocumentNo != string.Empty)
                {
                    sSql = sSql + "where DocumentNo='{0}'";
                    sSql = string.Format(sSql, RequestData.DocumentNo);
                }                

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesOrder = new SalesOrderHeader();
                        objSalesOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesOrder.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objSalesOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesOrder.DeliveryDate = objReader["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DeliveryDate"]) : DateTime.Now;
                        objSalesOrder.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objSalesOrder.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objSalesOrder.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objSalesOrder.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objSalesOrder.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objSalesOrder.OrderStatus = Convert.ToString(objReader["OrderStatus"]);
                        objSalesOrder.PaymentStatus = Convert.ToString(objReader["PaymentStatus"]);
                        objSalesOrder.CustomerCode = Convert.ToString(objReader["CustomerCode"]);

                        objSalesOrder.SalesOrderDetailsList = new List<SalesOrderDetail>();

                        var objSelectSalesOrderDetailRequest = new SelectSalesOrderDetailRequest();
                        objSelectSalesOrderDetailRequest.SalesOrderID = objSalesOrder.ID;
                        var objSelectSalesOrderDetailResponse = SelectSalesOrderDetails(objSelectSalesOrderDetailRequest);
                        if(objSelectSalesOrderDetailResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesOrder.SalesOrderDetailsList = objSelectSalesOrderDetailResponse.SalesOrderDetailList;
                        }

                        objSalesOrder.PaymentList = new List<PaymentDetail>();

                        var objInvoiceCashDetailsDAL = new InvoiceCashDetailsDAL();
                        var objSelectByInvoiceNoCashDetailsRequest = new SelectByInvoiceNoCashDetailsRequest();
                        var objSelectByInvoiceNoCashDetailsResponse = new SelectByInvoiceNoCashDetailsResponse();
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceNo = objSalesOrder.DocumentNo;
                        objSelectByInvoiceNoCashDetailsRequest.InvoiceHeaderID = objSalesOrder.ID;
                        objSelectByInvoiceNoCashDetailsRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectByInvoiceNoCashDetailsResponse = objInvoiceCashDetailsDAL.SelectByInvoiceNoCashDetails(objSelectByInvoiceNoCashDetailsRequest);

                        if (objSelectByInvoiceNoCashDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesOrder.PaymentList.AddRange(objSelectByInvoiceNoCashDetailsResponse.InvoiceNoCashDetails);
                        }


                        var objInvoiceCardDetailsDAL = new InvoiceCardDetailsDAL();
                        var objSelectCreditCardDetailsByInvoiceNoRequest = new SelectCreditCardDetailsByInvoiceNoRequest();
                        var objSelectCreditCardDetailsByInvoiceNoResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceNumber = objSalesOrder.DocumentNo;
                        objSelectCreditCardDetailsByInvoiceNoRequest.InvoiceHeaderID = objSalesOrder.ID;
                        objSelectCreditCardDetailsByInvoiceNoRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectCreditCardDetailsByInvoiceNoResponse = objInvoiceCardDetailsDAL.SelectCreditCardDetailsByInvoiceNo(objSelectCreditCardDetailsByInvoiceNoRequest);
                        if (objSelectCreditCardDetailsByInvoiceNoResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesOrder.PaymentList.AddRange(objSelectCreditCardDetailsByInvoiceNoResponse.InvoiceNoCreditCardDetails);
                        }

                        objSalesOrder.PaymentList.ForEach(x => x.FromSalesOrder = true);

                        ResponseData.SalesOrderMasterRecord = objSalesOrder;
                        ResponseData.ResponseDynamicData = objSalesOrder;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SalesOrder");
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



        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var SalesOrderList = new List<SalesOrderHeader>();
            var RequestData = (SelectAllSalesOrderRequest)RequestObj;
            var ResponseData = new SelectAllSalesOrderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select * from SalesOrderHeader ";

                if (RequestData.DataMode != "All")
                {
                    sQuery = sQuery + "where OrderStatus='" + RequestData.DataMode + "'";
                }
                
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesOrder = new SalesOrderHeader();
                        objSalesOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesOrder.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objSalesOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesOrder.DeliveryDate = objReader["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DeliveryDate"]) : DateTime.Now;
                        objSalesOrder.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objSalesOrder.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objSalesOrder.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objSalesOrder.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objSalesOrder.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objSalesOrder.OrderStatus = Convert.ToString(objReader["OrderStatus"]);
                        objSalesOrder.PaymentStatus = Convert.ToString(objReader["PaymentStatus"]);
                        objSalesOrder.CustomerCode = Convert.ToString(objReader["CustomerCode"]);

                        objSalesOrder.SalesOrderDetailsList = new List<SalesOrderDetail>();

                        var objSelectSalesOrderDetailRequest = new SelectSalesOrderDetailRequest();
                        objSelectSalesOrderDetailRequest.SalesOrderID = objSalesOrder.ID;
                        var objSelectSalesOrderDetailResponse = SelectSalesOrderDetails(objSelectSalesOrderDetailRequest);
                        if (objSelectSalesOrderDetailResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesOrder.SalesOrderDetailsList = objSelectSalesOrderDetailResponse.SalesOrderDetailList;
                        }

                        SalesOrderList.Add(objSalesOrder);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesOrderHeaderList = SalesOrderList;
                    ResponseData.ResponseDynamicData = SalesOrderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Order");
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }



        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        private string SalesOrderDetailMasterXML(List<SalesOrderDetail> salesOrderDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (SalesOrderDetail objSalesOrderDetailsDetails in salesOrderDetailsList)
            {
                sSql.Append("<SalesOrderDetailsData>");
                sSql.Append("<ID>" + objSalesOrderDetailsDetails.ID + "</ID>");
                sSql.Append("<SalesOrderID>" + objSalesOrderDetailsDetails.SalesOrderID + "</SalesOrderID>");
                sSql.Append("<SalesOrderDocumentNo>" + objSalesOrderDetailsDetails.SalesOrderDocumentNo + "</SalesOrderDocumentNo>");
                sSql.Append("<StyleCode>" + (objSalesOrderDetailsDetails.StyleCode) + "</StyleCode>");
                sSql.Append("<SKUCode>" + (objSalesOrderDetailsDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<Qty>" + objSalesOrderDetailsDetails.Qty + "</Qty>");
                sSql.Append("<Price>" + objSalesOrderDetailsDetails.Price + "</Price>");
                sSql.Append("<SellingLineTotal>" + objSalesOrderDetailsDetails.SellingLineTotal + "</SellingLineTotal>");
                sSql.Append("<Status>" + (objSalesOrderDetailsDetails.Status) + "</Status>");
                sSql.Append("<Remarks>" + (objSalesOrderDetailsDetails.Remarks) + "</Remarks>");
                sSql.Append("</SalesOrderDetailsData>");
            }
            return sSql.ToString();
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
                        sSql.Append("<InVoiceNumber>" + objInvoiceCash.InvoiceNumber + "</InVoiceNumber>");
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
                        sSql.Append("<InVoiceNumber>" + objInvoiceCard.InvoiceNumber + "</InVoiceNumber>");
                        sSql.Append("<PaymentCurrency>" + (objInvoiceCard.PayCurrency) + "</PaymentCurrency>");
                        sSql.Append("<PaymentCurrencyID>" + (objInvoiceCard.PayCurrencyID) + "</PaymentCurrencyID>");

                        if (objInvoiceCard.Mode == "On-Account")
                        {
                            sSql.Append("<ReceivedAmount>0</ReceivedAmount>");
                            sSql.Append("<BalanceAmount>" + objInvoiceCard.Receivedamount + "</BalanceAmount>");
                        }
                        else
                        {
                            sSql.Append("<ReceivedAmount>" + objInvoiceCard.Receivedamount + "</ReceivedAmount>");
                            sSql.Append("<BalanceAmount>0</BalanceAmount>");
                        }

                        sSql.Append("<CardType>" + (objInvoiceCard.Mode) + "</CardType>");
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

        public override SelectSalesOrderDetailResponse SelectSalesOrderDetails(SelectSalesOrderDetailRequest RequestObj)
        {
            var SalesOrderDetailList = new List<SalesOrderDetail>();
            var RequestData = (SelectSalesOrderDetailRequest)RequestObj;
            var ResponseData = new SelectSalesOrderDetailResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select * from SalesOrderDetails ";

                if (RequestData.SalesOrderID > 0)
                {
                    sQuery = sQuery + "where SalesOrderID=" + RequestData.SalesOrderID;
                }
                else if (RequestData.SalesOrderNo != null && RequestData.SalesOrderNo != string.Empty)
                {
                    sQuery = sQuery + "where SalesOrderDocumentNo='" + RequestData.SalesOrderNo + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesOrder = new SalesOrderDetail();
                        objSalesOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesOrder.SalesOrderID = objReader["SalesOrderID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesOrderID"]) : 0;
                        objSalesOrder.SalesOrderDocumentNo = objReader["SalesOrderDocumentNo"] != DBNull.Value ? Convert.ToString(objReader["SalesOrderDocumentNo"]) : string.Empty;
                        objSalesOrder.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objSalesOrder.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objSalesOrder.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objSalesOrder.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objSalesOrder.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;
                        objSalesOrder.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : string.Empty;
                        objSalesOrder.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : string.Empty;
                        objSalesOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSalesOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSalesOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        if(objSalesOrder.Status != string.Empty && objSalesOrder.Status == "Cancel")
                        {
                            objSalesOrder.IsDeleted = true;
                        }


                        SalesOrderDetailList.Add(objSalesOrder);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesOrderDetailList = SalesOrderDetailList;
                    ResponseData.ResponseDynamicData = SalesOrderDetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Order Detail");
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
    }
}
