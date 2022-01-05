using EasyBizAbsDAL.Transactions;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.CardDetails;
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
    public class InvoiceCardDetailsDAL : BaseCardDetailsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCardDetailsRequest)RequestObj;
            var ResponseData = new SaveCardDetailsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertCardDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.InvoiceCardDetailsrData.ID);
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.InvoiceCardDetailsrData.ApplicationDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.InvoiceCardDetailsrData.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@InvoiceNumber", RequestData.InvoiceCardDetailsrData.InvoiceNumber);
                _CommandObj.Parameters.AddWithValue("@CardType", RequestData.InvoiceCardDetailsrData.CardType);
                _CommandObj.Parameters.AddWithValue("@CardNumber", RequestData.InvoiceCardDetailsrData.CardNumber);
                _CommandObj.Parameters.AddWithValue("@CardHolderName", RequestData.InvoiceCardDetailsrData.CardHolderName);
                _CommandObj.Parameters.AddWithValue("@CardType2", RequestData.InvoiceCardDetailsrData.CardType2);


                _CommandObj.Parameters.AddWithValue("@AmountToBePay", RequestData.InvoiceCardDetailsrData.AmountToBePay);
                _CommandObj.Parameters.AddWithValue("@ReceivedCardAmount", RequestData.InvoiceCardDetailsrData.ReceivedAmount);
                _CommandObj.Parameters.AddWithValue("@BalanceAmountToBePay", RequestData.InvoiceCardDetailsrData.BalanceAmountToBePay);

                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceCardDetailsrData.ReturnAmount);
                _CommandObj.Parameters.AddWithValue("@PaymentCurrency", RequestData.InvoiceCardDetailsrData.PaymentCurrency);
                _CommandObj.Parameters.AddWithValue("@PaymentCurrencyID", RequestData.InvoiceCardDetailsrData.PaymentCurrencyID);
                _CommandObj.Parameters.AddWithValue("@ApprovalNumber", RequestData.InvoiceCardDetailsrData.ApprovalNumber);

                _CommandObj.Parameters.AddWithValue("@FromCountryID", RequestData.FromOrToCountryID);
                _CommandObj.Parameters.AddWithValue("@FromStoreID", RequestData.FromOrToStoreID);

                _CommandObj.Parameters.AddWithValue("@CreatedBy", RequestData.InvoiceCardDetailsrData.CreateBy);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Value = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice Card Details");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = Convert.ToInt32(ID1.Value).ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
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
                PaymentProcesserSave(RequestObj);
            }
            return ResponseData;
        }
        public void PaymentProcesserSave(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCardDetailsRequest)RequestObj;
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
        private string PaymentProcessorXML(List<PaymentProcessor> PaymentProcessorList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (PaymentProcessor PaymentProcess in PaymentProcessorList)
            {
                sSql.Append("<PaymentProcessorData>");
                sSql.Append("<CardNumber>" + PaymentProcess.CardNumber + "</CardNumber>");
                sSql.Append("<RRN>" + PaymentProcess.RRN + "</RRN>");
                sSql.Append("<StatusCode>" + PaymentProcess.StatusCode + "</StatusCode>");
                sSql.Append("<HostErrorCode>" + (PaymentProcess.HostErrorCode) + "</HostErrorCode>");
                sSql.Append("<AuthCode>" + (PaymentProcess.AuthCode) + "</AuthCode>");
                sSql.Append("<CardSchemes>" + PaymentProcess.CardSchemes + "</CardSchemes>");
                sSql.Append("<Stan>" + PaymentProcess.Stan + "</Stan>");
                sSql.Append("<TerminalID>" + PaymentProcess.TerminalID + "</TerminalID>");
                sSql.Append("<DateAndTime>" + (PaymentProcess.DateAndTime) + "</DateAndTime>");
                sSql.Append("<Amount>" + (PaymentProcess.Amount) + "</Amount>");
                sSql.Append("<PosCode>" + (PaymentProcess.PosCode) + "</PosCode>");
                sSql.Append("<User>" + (PaymentProcess.User) + "</User>");
                sSql.Append("<CustomerMobileNumber>" + (PaymentProcess.CustomerMobileNumber) + "</CustomerMobileNumber>");
                sSql.Append("<Currency>" + (PaymentProcess.Currency) + "</Currency>");
                sSql.Append("</PaymentProcessorData>");
            }
            return sSql.ToString();
        }
        /*public override EasyBizResponse.BaseResponseType InsertPaymentProcessorRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCardDetailsRequest)RequestObj;
            var ResponseData = new SaveCardDetailsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertCardDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.InvoiceCardDetailsrData.ID);
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.InvoiceCardDetailsrData.ApplicationDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.InvoiceCardDetailsrData.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@InvoiceNumber", RequestData.InvoiceCardDetailsrData.InvoiceNumber);
                _CommandObj.Parameters.AddWithValue("@CardType", RequestData.InvoiceCardDetailsrData.CardType);
                _CommandObj.Parameters.AddWithValue("@CardNumber", RequestData.InvoiceCardDetailsrData.CardNumber);
                _CommandObj.Parameters.AddWithValue("@CardHolderName", RequestData.InvoiceCardDetailsrData.CardHolderName);
                _CommandObj.Parameters.AddWithValue("@CardType2", RequestData.InvoiceCardDetailsrData.CardType2);


                _CommandObj.Parameters.AddWithValue("@AmountToBePay", RequestData.InvoiceCardDetailsrData.AmountToBePay);
                _CommandObj.Parameters.AddWithValue("@ReceivedCardAmount", RequestData.InvoiceCardDetailsrData.ReceivedAmount);
                _CommandObj.Parameters.AddWithValue("@BalanceAmountToBePay", RequestData.InvoiceCardDetailsrData.BalanceAmountToBePay);

                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InvoiceCardDetailsrData.ReturnAmount);
                _CommandObj.Parameters.AddWithValue("@PaymentCurrency", RequestData.InvoiceCardDetailsrData.PaymentCurrency);
                _CommandObj.Parameters.AddWithValue("@PaymentCurrencyID", RequestData.InvoiceCardDetailsrData.PaymentCurrencyID);
                _CommandObj.Parameters.AddWithValue("@ApprovalNumber", RequestData.InvoiceCardDetailsrData.ApprovalNumber);

                _CommandObj.Parameters.AddWithValue("@FromCountryID", RequestData.FromOrToCountryID);
                _CommandObj.Parameters.AddWithValue("@FromStoreID", RequestData.FromOrToStoreID);

                _CommandObj.Parameters.AddWithValue("@CreatedBy", RequestData.InvoiceCardDetailsrData.CreateBy);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Value = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice Card Details");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = Convert.ToInt32(ID1.Value).ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
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
        }*/

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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectByIDCardDetailsResponse SelectByIDInvoiceDetails(SelectByIDCardDetailsRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override SelectCreditCardDetailsByInvoiceNoResponse SelectCreditCardDetailsByInvoiceNo(SelectCreditCardDetailsByInvoiceNoRequest ObjRequest)
        {
            var InvoiceNoCreditCardDatas = new InvoiceCardDetails();
            var RequestData = (SelectCreditCardDetailsByInvoiceNoRequest)ObjRequest;
            var ResponseData = new SelectCreditCardDetailsByInvoiceNoResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                var PaymentList = new List<PaymentDetail>();
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sSql.Append("select * from InVoiceCardDetails where InvoiceHeaderID=" + RequestData.InvoiceHeaderID);
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                }
                else
                {
                    sSql.Append("select * from InVoiceCardDetails where InvoiceNumber = '" + RequestData.InvoiceNumber + "' and InvoiceHeaderID=" + RequestData.InvoiceHeaderID);
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {                        
                        var objInVoiceCashDetails = new PaymentDetail();
                        objInVoiceCashDetails.ID = Convert.ToInt32(objReader["ID"]);
                        //objInVoiceCashDetails.Mode = "Card";
                        objInVoiceCashDetails.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objInVoiceCashDetails.BusinessDate = Convert.ToDateTime(objReader["ApplicationDate"]);
                        objInVoiceCashDetails.InvoiceNumber = objReader["InVoiceNumber"].ToString();
                        objInVoiceCashDetails.Mode = Convert.ToString(objReader["CardType"]);
                        objInVoiceCashDetails.CardNo = Convert.ToString(objReader["CardNumber"]);
                        objInVoiceCashDetails.CardHolder = Convert.ToString(objReader["CardHolderName"]);
                        objInVoiceCashDetails.FromCountryID = Convert.ToInt32(objReader["FromCountryID"]);
                        objInVoiceCashDetails.FromStoreID = Convert.ToInt32(objReader["FromStoreID"]);
                        objInVoiceCashDetails.Receivedamount = Convert.ToDecimal(objReader["ReceivedCardAmount"]);
                        //objInVoiceCashDetails.ChangeCurrency = Convert.ToString(objReader["ChangeCurrency"]);
                        //objInVoiceCashDetails.ChangeCurrencyID = Convert.ToInt32(objReader["ChangeCurrencyID"]);
                        objInVoiceCashDetails.PayCurrency = Convert.ToString(objReader["PaymentCurrency"]);
                        objInVoiceCashDetails.PayCurrencyID = Convert.ToInt32(objReader["PaymentCurrencyID"]);
                        objInVoiceCashDetails.ApproveNo = Convert.ToString(objReader["ApprovalNumber"]);
                        objInVoiceCashDetails.CreateBy = Convert.ToInt32(objReader["CreateBy"]);

                        objInVoiceCashDetails.BalanceAmountToBePay = objReader["BalanceAmountToBePay"] != DBNull.Value ? Convert.ToDecimal(objReader["BalanceAmountToBePay"]) : 0;                       
                        objInVoiceCashDetails.OnAccountReceiveAmount = objReader["OnAccountReceiveAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["OnAccountReceiveAmount"]) : 0;
                        objInVoiceCashDetails.PendingAmount = objInVoiceCashDetails.BalanceAmountToBePay - objInVoiceCashDetails.OnAccountReceiveAmount;
                        objInVoiceCashDetails.OnAcPaymentCompleted = objReader["OnAcPaymentCompleted"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAcPaymentCompleted"]) : false;

                        PaymentList.Add(objInVoiceCashDetails);
                    }
                    ResponseData.InvoiceNoCreditCardDetails = PaymentList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice");
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

        //public override SaveCardDetailsResponse SavePaymentProcesor(SaveCardDetailsRequest ObjRequest)
        //{
        //    throw new NotImplementedException();
        //}

        /*public override SaveCardDetailsResponse SavePaymentProcesor(SaveCardDetailsRequest ObjRequest)
        {
            //string a = "success";
            var RequestData = (SaveCardDetailsRequest)ObjRequest;
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
            return ResponseData;
            //PaymentProcesserSave(ObjRequest);
            //throw new NotImplementedException();

        }*/
    }
}
