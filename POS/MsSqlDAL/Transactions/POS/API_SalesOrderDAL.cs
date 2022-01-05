using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizRequest.Transactions.POS.API_SalesOrderRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using EasyBizResponse.Transactions.POS.API_SalesOrderResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizResponse.Transactions.POS.SalesOrder;

namespace MsSqlDAL.Transactions.POS
{
    public class API_SalesOrderDAL : BaseAPI_SalesOrderDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (API_SaveSalesOrderRequest)RequestObj;
            var ResponseData = new API_SaveSalesOrderResponse();
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

                SqlParameter PickedQty = _CommandObj.Parameters.Add("@PickedQty", SqlDbType.Int);
                PickedQty.Direction = ParameterDirection.Input;
                PickedQty.Value = RequestData.SalesOrderHeaderRecord.PickedQty;

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
                SalesOrderDetails.Value = SalesOrderDetailMasterXML(RequestData.SalesOrderHeaderRecord.SalesOrderDetailsList);

                string sCashDetails = CashDetailXML(RequestData.SalesOrderHeaderRecord.PaymentList);
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

        private string SalesOrderDetailMasterXML(List<API_SalesOrderDetails> salesOrderDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (API_SalesOrderDetails objSalesOrderDetailsDetails in salesOrderDetailsList)
            {
                sSql.Append("<SalesOrderDetailsData>");
                sSql.Append("<StyleCode>" + (objSalesOrderDetailsDetails.StyleCode) + "</StyleCode>");
                sSql.Append("<SKUCode>" + (objSalesOrderDetailsDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<Qty>" + objSalesOrderDetailsDetails.Qty + "</Qty>");
                sSql.Append("<PickedQty>" + objSalesOrderDetailsDetails.PickedQty + "</PickedQty>");
                sSql.Append("<Price>" + objSalesOrderDetailsDetails.Price + "</Price>");
                sSql.Append("<SellingLineTotal>" + objSalesOrderDetailsDetails.SellingLineTotal + "</SellingLineTotal>");
                sSql.Append("<Status>" + (objSalesOrderDetailsDetails.Status) + "</Status>");
                sSql.Append("<Remarks>" + (objSalesOrderDetailsDetails.Remarks) + "</Remarks>");
                sSql.Append("<StoreID>" + (objSalesOrderDetailsDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objSalesOrderDetailsDetails.StoreCode) + "</StoreCode>");
                sSql.Append("</SalesOrderDetailsData>");
            }
            return sSql.ToString();
        }

        public string CashDetailXML(List<API_SalesOrderPayments> PaymentList)
        {
            StringBuilder sSql = new StringBuilder();
            var CashList = new List<API_SalesOrderPayments>();

            if (PaymentList != null && PaymentList.Count > 0)
            {
                if (CashList != null && CashList.Count > 0)
                {
                    foreach (API_SalesOrderPayments objInvoiceCash in CashList)
                    {
                        sSql.Append("<PaymentDetails>");
                        sSql.Append("<DocumentDate>" + objInvoiceCash.DocumentDate + "</DocumentDate>");
                        sSql.Append("<PaymentCurrency>" + objInvoiceCash.PaymentCurrency + "</PaymentCurrency>");
                        sSql.Append("<PaymentMode>" + (objInvoiceCash.PaymentMode) + "</PaymentMode>");
                        sSql.Append("<TotalAmount>" + (objInvoiceCash.TotalAmount) + "</TotalAmount>");
                        sSql.Append("<PaidAmount>" + objInvoiceCash.PaidAmount + "</PaidAmount>");
                        sSql.Append("<BalanceAmountToPay>" + objInvoiceCash.BalanceAmountToPay + "</BalanceAmountToPay>");
                        sSql.Append("<CardType>" + objInvoiceCash.CardType + "</CardType>");
                        sSql.Append("<CardHolderName>" + objInvoiceCash.CardHolderName + "</CardHolderName>");
                        sSql.Append("<ApprovalNo>" + objInvoiceCash.ApprovalNo + "</ApprovalNo>");
                        sSql.Append("<CardNumber>" + objInvoiceCash.CardNumber + "</CardNumber>");
                        sSql.Append("</PaymentDetails>");
                    }
                }
            }
            return sSql.ToString().Replace("&", "&#38;");
        }
        
        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override API_SelectALLSalesOrderResponse SelectAll(API_SelectAllSalesOrderRequest objRequest)
        {
            var SalesOrderHeaderList = new List<API_SalesOrderHeader>();
            var RequestData = (API_SelectAllSalesOrderRequest)objRequest;
            var ResponseData = new API_SelectALLSalesOrderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_SelectALLSalesOrderDocuments", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesOrderHeader = new API_SalesOrderHeader();
                        objSalesOrderHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesOrderHeader.DocumentNo = objReader["DocumentNo"].ToString();
                        objSalesOrderHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesOrderHeader.DeliveryDate = objReader["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DeliveryDate"]) : DateTime.Now;
                        objSalesOrderHeader.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objSalesOrderHeader.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objSalesOrderHeader.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objSalesOrderHeader.OrderStatus = objReader["OrderStatus"].ToString();
                        objSalesOrderHeader.PaymentStatus = objReader["PaymentStatus"].ToString();
                        
                        SalesOrderHeaderList.Add(objSalesOrderHeader);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesOrderHeaderList = SalesOrderHeaderList;
                    ResponseData.ResponseDynamicData = SalesOrderHeaderList;
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override API_SelectBySalesOrderIDResponse SelectRecord(API_SelectBySalesOrderIDRequest objRequest)
        {
            var SalesOrderDetailsList = new List<API_SalesOrderDetails>();
            var RequestData = (API_SelectBySalesOrderIDRequest)objRequest;
            var ResponseData = new API_SelectBySalesOrderIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_SelectSalesOrderByID", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@ID", RequestData.ID);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    var objSalesOrderHeader = new API_SalesOrderHeader();
                    while (objReader.Read())
                    {
                        var objSalesOrderDetails = new API_SalesOrderDetails();
                        objSalesOrderHeader.ID = objReader["SalesOrderID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesOrderID"]) : 0;
                        objSalesOrderHeader.DocumentNo = objReader["DocumentNo"].ToString();
                        objSalesOrderHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesOrderHeader.DeliveryDate = objReader["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DeliveryDate"]) : DateTime.Now;
                        objSalesOrderHeader.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objSalesOrderHeader.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objSalesOrderHeader.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objSalesOrderHeader.OrderStatus = objReader["OrderStatus"].ToString();
                        objSalesOrderHeader.PaymentStatus = objReader["PaymentStatus"].ToString();
                        objSalesOrderHeader.CustomerCode = objReader["CustomerCode"].ToString();
                        objSalesOrderDetails.ID = objReader["DetailsID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailsID"]) : 0;
                        objSalesOrderDetails.SalesOrderID = objReader["SalesOrderID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesOrderID"]) : 0;
                        objSalesOrderDetails.StyleCode = objReader["StyleCode"].ToString();
                        objSalesOrderDetails.SKUCode = objReader["SKUCode"].ToString();
                        objSalesOrderDetails.SalesOrderDocumentNo = objReader["DocumentNo"].ToString();
                        objSalesOrderDetails.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objSalesOrderDetails.PickedQty = objReader["PickedQty"] != DBNull.Value ? Convert.ToInt32(objReader["PickedQty"]) : 0;
                        objSalesOrderDetails.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objSalesOrderDetails.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;
                        objSalesOrderDetails.Status = objReader["DetailsStatus"].ToString();
                        objSalesOrderDetails.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesOrderDetails.StoreCode = objReader["StoreCode"].ToString();

                        SalesOrderDetailsList.Add(objSalesOrderDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesOrderMasterRecord = objSalesOrderHeader;
                    ResponseData.SalesOrderMasterRecord.SalesOrderDetailsList = SalesOrderDetailsList;
                    ResponseData.ResponseDynamicData = objSalesOrderHeader;
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

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
