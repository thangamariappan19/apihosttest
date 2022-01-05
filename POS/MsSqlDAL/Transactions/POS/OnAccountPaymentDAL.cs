using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
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
    public class OnAccountPaymentDAL : BaseOnAccountPaymentDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;  
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveOnAccountPaymentRequest)RequestObj;
            var ResponseData = new SaveOnAccountPaymentResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateOnAccountPayment", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.OnAccountPaymentRecord.ID;

                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.OnAccountPaymentRecord.StoreID;

                var StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
                StoreCode.Direction = ParameterDirection.Input;
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer && RequestData.OnAccountPaymentRecord.StoreCode != null)
                {
                    StoreCode.Value = RequestData.OnAccountPaymentRecord.StoreCode;
                }
                else
                {
                    StoreCode.Value = "Enterprise";
                }

                var CustomerCode = _CommandObj.Parameters.Add("@CustomerCode", SqlDbType.VarChar);
                CustomerCode.Direction = ParameterDirection.Input;
                CustomerCode.Value = RequestData.OnAccountPaymentRecord.CustomerCode;

                var PaymentDate = _CommandObj.Parameters.Add("@PaymentDate", SqlDbType.Date);
                PaymentDate.Direction = ParameterDirection.Input;
                PaymentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.OnAccountPaymentRecord.PaymentDate);

                var BillingAmount = _CommandObj.Parameters.Add("@BillingAmount", SqlDbType.Decimal);
                BillingAmount.Direction = ParameterDirection.Input;
                BillingAmount.Value = RequestData.OnAccountPaymentRecord.BillingAmount;

                var ReceivedAmount = _CommandObj.Parameters.Add("@ReceivedAmount", SqlDbType.Decimal);
                ReceivedAmount.Direction = ParameterDirection.Input;
                ReceivedAmount.Value = RequestData.OnAccountPaymentRecord.ReceivedAmount;

                var ReturnAmount = _CommandObj.Parameters.Add("@ReturnAmount", SqlDbType.Decimal);
                ReturnAmount.Direction = ParameterDirection.Input;
                ReturnAmount.Value = RequestData.OnAccountPaymentRecord.ReturnAmount;

                var OnAccountPaymentDetails = _CommandObj.Parameters.Add("@OnAccountPaymentDetails", SqlDbType.Xml);
                OnAccountPaymentDetails.Direction = ParameterDirection.Input;
                OnAccountPaymentDetails.Value = OnAccountPaymentDetailsXml(RequestData.OnAccountPaymentRecord.OnAccountPaymentDetailsList);

                var OnAcInvoiceWisePayment = _CommandObj.Parameters.Add("@OnAcInvoiceWisePayment", SqlDbType.Xml);
                OnAcInvoiceWisePayment.Direction = ParameterDirection.Input;
                OnAcInvoiceWisePayment.Value = OnAcInvoiceWisePaymentXml(RequestData.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList);

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.OnAccountPaymentRecord.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, int.MaxValue);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "On Account Payment");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();
                }
                else
                {
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "On Account Payment");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
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
            var RequestData = (SelectOnAccountPaymentRequest)RequestObj;
            var ResponseData = new SelectOnAccountPaymentResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                
                string sSql = "Select * from OnAccountPayment with(nolock) ";

                if (RequestData.DocumentIDs != null && RequestData.DocumentIDs != string.Empty)
                {
                    sSql = sSql + " where ID={0}";
                    sSql = string.Format(sSql, Convert.ToInt64(RequestData.DocumentIDs));
                }
                else if (RequestData.DocumentNos != null && RequestData.DocumentNos != string.Empty)
                {
                    sSql = sSql + " where CustomerCode='{0}'";
                    sSql = string.Format(sSql, RequestData.DocumentNos.Trim());
                }
                else 
                {                    
                    sSql = sSql + " where ID={0}";                    
                    sSql = string.Format(sSql, RequestData.ID);
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOnAccountPayment = new OnAccountPayment();
                        objOnAccountPayment.BillingAmount = objReader["BillingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillingAmount"]) : 0;
                        objOnAccountPayment.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objOnAccountPayment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOnAccountPayment.PaymentDate = objReader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PaymentDate"]) : DateTime.MinValue;
                        objOnAccountPayment.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objOnAccountPayment.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objOnAccountPayment.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPayment.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAccountPayment.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;

                        SelectOnAccountPaymentRequest ObjSelectOnAccountPaymentRequest = new SelectOnAccountPaymentRequest();

                        ObjSelectOnAccountPaymentRequest.ID = objOnAccountPayment.ID;
                        ObjSelectOnAccountPaymentRequest.ConnectionString = RequestData.ConnectionString;
                        ObjSelectOnAccountPaymentRequest.RequestFrom = RequestData.RequestFrom;

                        objOnAccountPayment.OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
                        objOnAccountPayment.OnAccountPaymentDetailsList = GetOnAccountPaymentDetailsList(ObjSelectOnAccountPaymentRequest);

                        objOnAccountPayment.OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
                        objOnAccountPayment.OnAcInvoiceWisePaymentList = GetOnAcInvoiceWisePaymentList(ObjSelectOnAccountPaymentRequest);

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.OnAccountPaymentRecord = objOnAccountPayment;

                        ResponseData.ResponseDynamicData = objOnAccountPayment;
                    }

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "On-Account Details");
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var OnAccountPaymentList = new List<OnAccountPayment>();
            var RequestData = (SelectAllOnAccountPaymentRequest)RequestObj;
            var ResponseData = new SelectAllOnAccountPaymentResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;

                if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                {
                    sSql = "Select top 100 * from OnAccountPayment";
                }
                else if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                {
                    sSql = "Select * from OnAccountPayment where CustomerCode='{0}' or PaymentDate='{0}'";
                    sSql = string.Format(sSql, RequestData.SearchString);
                }                

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOnAccountPayment = new OnAccountPayment();
                        objOnAccountPayment.BillingAmount = objReader["BillingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillingAmount"]) : 0;
                        objOnAccountPayment.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objOnAccountPayment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOnAccountPayment.PaymentDate = objReader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PaymentDate"]) : DateTime.MinValue;
                        objOnAccountPayment.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objOnAccountPayment.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objOnAccountPayment.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPayment.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAccountPayment.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;

                        SelectOnAccountPaymentRequest ObjSelectOnAccountPaymentRequest = new SelectOnAccountPaymentRequest();

                        ObjSelectOnAccountPaymentRequest.ID = objOnAccountPayment.ID;
                        ObjSelectOnAccountPaymentRequest.ConnectionString = RequestData.ConnectionString;
                        ObjSelectOnAccountPaymentRequest.RequestFrom = RequestData.RequestFrom;

                        objOnAccountPayment.OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
                        objOnAccountPayment.OnAccountPaymentDetailsList = GetOnAccountPaymentDetailsList(ObjSelectOnAccountPaymentRequest);

                        objOnAccountPayment.OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
                        objOnAccountPayment.OnAcInvoiceWisePaymentList = GetOnAcInvoiceWisePaymentList(ObjSelectOnAccountPaymentRequest);

                        OnAccountPaymentList.Add(objOnAccountPayment);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OnAccountPaymentList = OnAccountPaymentList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "On-Account Details");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }        
        public override GetOnAccountPaymentPendingResponse GetOnAccountPaymentPendingList(GetOnAccountPaymentPendingRequest ObjRequest)
        {
            var OnAccountPendingList = new List<OnAcInvoiceWisePayment>();
            var RequestData = (GetOnAccountPaymentPendingRequest)ObjRequest;
            var ResponseData = new GetOnAccountPaymentPendingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("GetOnAccountDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@Type", RequestData.Type);
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    int SlNo = 0;
                    while (objReader.Read())
                    {
                        var objOnAccountPending = new OnAcInvoiceWisePayment();
                        objOnAccountPending.SlNo = SlNo++;
                        objOnAccountPending.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.MinValue;
                        objOnAccountPending.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAccountPending.PurchaseStoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPending.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objOnAccountPending.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objOnAccountPending.BillAmount = objReader["BillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillAmount"]) : 0;
                        objOnAccountPending.CashPaid = objReader["CashPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CashPaid"]) : 0;
                        objOnAccountPending.CardPaid = objReader["CardPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CardPaid"]) : 0;
                        objOnAccountPending.TotalPaid = objReader["TotalPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalPaid"]) : 0;
                        objOnAccountPending.PendingAmount = objReader["BalanceAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BalanceAmount"]) : 0;
                        objOnAccountPending.CloseBill = false;

                        OnAccountPendingList.Add(objOnAccountPending);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OnAcInvoiceWisePaymentList = OnAccountPendingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "On-Account Details");
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
        public List<OnAccountPaymentDetails> GetOnAccountPaymentDetailsList(BaseRequestType ObjRequest)
        {
            List<OnAccountPaymentDetails> OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
            var RequestData = (SelectOnAccountPaymentRequest)ObjRequest;
           
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from OnAccountPaymentDetails where OnAccountPaymentID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOnAccountPaymentDetails = new OnAccountPaymentDetails();
                        objOnAccountPaymentDetails.ApprovalNumber = objReader["ApprovalNumber"] != DBNull.Value ? Convert.ToString(objReader["ApprovalNumber"]) : string.Empty;
                        objOnAccountPaymentDetails.CardHolderName = objReader["CardHolderName"] != DBNull.Value ? Convert.ToString(objReader["CardHolderName"]) : string.Empty;
                        objOnAccountPaymentDetails.CardNumber = objReader["CardNumber"] != DBNull.Value ? Convert.ToString(objReader["CardNumber"]) : string.Empty;
                        objOnAccountPaymentDetails.CardType = objReader["CardType"] != DBNull.Value ? Convert.ToString(objReader["CardType"]) : string.Empty;
                        objOnAccountPaymentDetails.ChangeCurrency = objReader["ChangeCurrency"] != DBNull.Value ? Convert.ToString(objReader["ChangeCurrency"]) : string.Empty;
                        objOnAccountPaymentDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOnAccountPaymentDetails.OnAccountPaymentID = objReader["OnAccountPaymentID"] != DBNull.Value ? Convert.ToInt32(objReader["OnAccountPaymentID"]) : 0;
                        objOnAccountPaymentDetails.PaymentCurrency = objReader["PaymentCurrency"] != DBNull.Value ? Convert.ToString(objReader["PaymentCurrency"]) : string.Empty;
                        objOnAccountPaymentDetails.PaymentType = objReader["PaymentType"] != DBNull.Value ? Convert.ToString(objReader["PaymentType"]) : string.Empty;
                        objOnAccountPaymentDetails.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objOnAccountPaymentDetails.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPaymentDetails.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;

                        OnAccountPaymentDetailsList.Add(objOnAccountPaymentDetails);
                    }                 
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return OnAccountPaymentDetailsList;
        }
        public List<OnAcInvoiceWisePayment> GetOnAcInvoiceWisePaymentList(BaseRequestType ObjRequest)
        {
            List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
            var RequestData = (SelectOnAccountPaymentRequest)ObjRequest;
            
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from OnAcInvoiceWisePayment where OnAccountPaymentID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOnAcInvoiceWisePayment = new OnAcInvoiceWisePayment();
                        objOnAcInvoiceWisePayment.BillAmount = objReader["BillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillAmount"]) : 0;
                        objOnAcInvoiceWisePayment.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.MinValue;
                        objOnAcInvoiceWisePayment.CardPaid = objReader["CardPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CardPaid"]) : 0;
                        objOnAcInvoiceWisePayment.CashPaid = objReader["CashPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CashPaid"]) : 0;
                        objOnAcInvoiceWisePayment.CloseBill = objReader["CloseBill"] != DBNull.Value ? Convert.ToBoolean(objReader["CloseBill"]) : false;
                        objOnAcInvoiceWisePayment.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objOnAcInvoiceWisePayment.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        objOnAcInvoiceWisePayment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOnAcInvoiceWisePayment.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objOnAcInvoiceWisePayment.OnAccountPaymentID = objReader["OnAccountPaymentID"] != DBNull.Value ? Convert.ToInt32(objReader["OnAccountPaymentID"]) : 0;
                        objOnAcInvoiceWisePayment.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        objOnAcInvoiceWisePayment.PendingAmount = objReader["PendingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PendingAmount"]) : 0;
                        objOnAcInvoiceWisePayment.PurchaseStoreCode = objReader["PurchaseStoreCode"] != DBNull.Value ? Convert.ToString(objReader["PurchaseStoreCode"]) : string.Empty;
                        objOnAcInvoiceWisePayment.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAcInvoiceWisePayment.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAcInvoiceWisePayment.TotalPaid = objReader["TotalPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalPaid"]) : 0;

                        OnAcInvoiceWisePaymentList.Add(objOnAcInvoiceWisePayment);
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }           
            return OnAcInvoiceWisePaymentList;
        }
        private string OnAccountPaymentDetailsXml(List<OnAccountPaymentDetails> OnAccountPaymentDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (OnAccountPaymentDetails objOnAccountPaymentDetails in OnAccountPaymentDetailsList)
            {
                sSql.Append("<OnAccountPaymentDetails>");
                sSql.Append("<ID>" + (objOnAccountPaymentDetails.ID) + "</ID>");
                sSql.Append("<StoreID>" + (objOnAccountPaymentDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objOnAccountPaymentDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<PaymentCurrency>" + objOnAccountPaymentDetails.PaymentCurrency + "</PaymentCurrency>");
                sSql.Append("<ChangeCurrency>" + objOnAccountPaymentDetails.ChangeCurrency + "</ChangeCurrency>");
                sSql.Append("<PaymentType>" + (objOnAccountPaymentDetails.PaymentType) + "</PaymentType>");
                sSql.Append("<CardType>" + objOnAccountPaymentDetails.CardType + "</CardType>");
                sSql.Append("<CardNumber>" + (objOnAccountPaymentDetails.CardNumber) + "</CardNumber>");
                sSql.Append("<CardHolderName>" + objOnAccountPaymentDetails.CardHolderName + "</CardHolderName>");
                sSql.Append("<ApprovalNumber>" + objOnAccountPaymentDetails.ApprovalNumber + "</ApprovalNumber>");
                sSql.Append("<ReceivedAmount>" + (objOnAccountPaymentDetails.ReceivedAmount) + "</ReceivedAmount>");
                sSql.Append("</OnAccountPaymentDetails>");
            }
            return sSql.ToString();
        }
        private string OnAcInvoiceWisePaymentXml(List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (OnAcInvoiceWisePayment objOnAcInvoiceWisePayment in OnAcInvoiceWisePaymentList)
            {
                sSql.Append("<OnAcInvoiceWisePayment>");
                sSql.Append("<ID>" + (objOnAcInvoiceWisePayment.ID) + "</ID>");
                sSql.Append("<OnAccountPaymentID>" + (objOnAcInvoiceWisePayment.OnAccountPaymentID) + "</OnAccountPaymentID>");
                sSql.Append("<PurchaseStoreCode>" + (objOnAcInvoiceWisePayment.PurchaseStoreCode) + "</PurchaseStoreCode>");
                sSql.Append("<InvoiceNo>" + objOnAcInvoiceWisePayment.InvoiceNo + "</InvoiceNo>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objOnAcInvoiceWisePayment.BusinessDate) + "</BusinessDate>");
                sSql.Append("<CustomerCode>" + objOnAcInvoiceWisePayment.CustomerCode + "</CustomerCode>");
                sSql.Append("<BillAmount>" + objOnAcInvoiceWisePayment.BillAmount + "</BillAmount>");
                sSql.Append("<CashPaid>" + objOnAcInvoiceWisePayment.CashPaid + "</CashPaid>");
                sSql.Append("<CardPaid>" + objOnAcInvoiceWisePayment.CardPaid + "</CardPaid>");
                sSql.Append("<TotalPaid>" + objOnAcInvoiceWisePayment.TotalPaid + "</TotalPaid>");
                sSql.Append("<PendingAmount>" + objOnAcInvoiceWisePayment.PendingAmount + "</PendingAmount>");
                sSql.Append("<DiscountAmount>" + objOnAcInvoiceWisePayment.DiscountAmount + "</DiscountAmount>");
                sSql.Append("<PaidAmount>" + (objOnAcInvoiceWisePayment.PaidAmount) + "</PaidAmount>");
                sSql.Append("<CloseBill>" + (objOnAcInvoiceWisePayment.CloseBill) + "</CloseBill>");
                sSql.Append("</OnAcInvoiceWisePayment>");
            }
            return sSql.ToString();
        }

        //Rajaram - For Getting On Account details- API
        public override GetOnAccountPaymentDetailsResponse GetOnAccountDetails(GetOnAccountPaymentDetailsRequest ObjRequest)
        {
            var OnAccountPendingRecord = new OnAcInvoiceWisePayment();
            var RequestData = (GetOnAccountPaymentDetailsRequest)ObjRequest;
            var ResponseData = new GetOnAccountPaymentDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_GetOnAccountDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@Mode", RequestData.Mode);
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);


                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    int SlNo = 0;
                    while (objReader.Read())
                    {
                        var objOnAccountPending = new OnAcInvoiceWisePayment();
                        //objOnAccountPending.SlNo = SlNo++;
                        objOnAccountPending.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.MinValue;
                        objOnAccountPending.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAccountPending.PurchaseStoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPending.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;                       
                        objOnAccountPending.BillAmount = objReader["BillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillAmount"]) : 0;
                        objOnAccountPending.CashPaid = objReader["CashPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CashPaid"]) : 0;
                        objOnAccountPending.CardPaid = objReader["CardPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CardPaid"]) : 0;
                        objOnAccountPending.TotalPaid = objReader["TotalPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalPaid"]) : 0;
                        objOnAccountPending.PendingAmount = objReader["BalanceAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BalanceAmount"]) : 0;
                        objOnAccountPending.CloseBill = false;

                        OnAccountPendingRecord = objOnAccountPending;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OnAccountPaymentDetails = OnAccountPendingRecord;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "On-Account Details");
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

        public override SelectAllOnAccountPaymentResponse API_GetALLOnAccountDetails(SelectAllOnAccountPaymentRequest objRequest)
        {
            var OnAccountPaymentList = new List<OnAccountPayment>();
            var RequestData = (SelectAllOnAccountPaymentRequest)objRequest;
            var ResponseData = new SelectAllOnAccountPaymentResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sSql = string.Empty;
               
                        sbSql.Append("Select ID,StoreID,StoreCode,CustomerCode,PaymentDate,BillingAmount,ReceivedAmount,ReturnAmount,CreateBy,CreateOn,UpdateBy,UpdateOn,Remarks,SalesReturnDocumentNo,RC.TOTAL_CNT [RecordCount]   from OnAccountPayment with(NoLock) ");
                        sbSql.Append(" left JOIN (Select count(AP.ID) As TOTAL_CNT from OnAccountPayment AP with(NoLock) ");
                        sbSql.Append(" where  AP.CustomerCode  like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append(" or  AP.BillingAmount  like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append(" or  AP.ReceivedAmount  like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append(" or  AP.ReturnAmount  like isnull('%" + RequestData.SearchString + "%','')) AS  RC ON 1 = 1  ");
                        sbSql.Append(" where  CustomerCode  like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append("or  BillingAmount like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append("or  ReceivedAmount like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append("or  ReturnAmount like isnull('%" + RequestData.SearchString + "%','')");
                        sbSql.Append("order by ID  asc ");
                        sbSql.Append("offset " + RequestData.Offset + " rows ");
                        sbSql.Append("fetch first " + RequestData.Limit + " rows only");
                   

                    
                    

                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOnAccountPayment = new OnAccountPayment();
                        objOnAccountPayment.BillingAmount = objReader["BillingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillingAmount"]) : 0;
                        objOnAccountPayment.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        objOnAccountPayment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOnAccountPayment.PaymentDate = objReader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PaymentDate"]) : DateTime.MinValue;
                        objOnAccountPayment.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objOnAccountPayment.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objOnAccountPayment.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objOnAccountPayment.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOnAccountPayment.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;

                        SelectOnAccountPaymentRequest ObjSelectOnAccountPaymentRequest = new SelectOnAccountPaymentRequest();

                        ObjSelectOnAccountPaymentRequest.ID = objOnAccountPayment.ID;
                        ObjSelectOnAccountPaymentRequest.ConnectionString = RequestData.ConnectionString;
                        ObjSelectOnAccountPaymentRequest.RequestFrom = RequestData.RequestFrom;

                        objOnAccountPayment.OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
                        objOnAccountPayment.OnAccountPaymentDetailsList = GetOnAccountPaymentDetailsList(ObjSelectOnAccountPaymentRequest);

                        objOnAccountPayment.OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
                        objOnAccountPayment.OnAcInvoiceWisePaymentList = GetOnAcInvoiceWisePaymentList(ObjSelectOnAccountPaymentRequest);

                        OnAccountPaymentList.Add(objOnAccountPayment);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OnAccountPaymentList = OnAccountPaymentList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "On-Account Details");
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
