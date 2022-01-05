using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
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
    public class InvoiceCashDetailsDAL : BaseInvoiceCashDetailsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveInVoiceCashDetailsRequest)RequestObj;
            var ResponseData = new SaveInvoiceCashDetailsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertInvoiceCashDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.InVoiceCashDetailsData.ID);
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.InVoiceCashDetailsData.BusinessDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.InVoiceCashDetailsData.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@InVoiceNumber", RequestData.InVoiceCashDetailsData.InvoiceNumber);              
               _CommandObj.Parameters.AddWithValue("@PaymentCurrency", RequestData.InVoiceCashDetailsData.PaymentCurrency);
               _CommandObj.Parameters.AddWithValue("@PaymentCurrencyID", RequestData.InVoiceCashDetailsData.PaymentCurrencyID);
               _CommandObj.Parameters.AddWithValue("@ChangeCurrency", RequestData.InVoiceCashDetailsData.ChangeCurrency);             
                _CommandObj.Parameters.AddWithValue("@ChangeCurrencyID", RequestData.InVoiceCashDetailsData.ChangeCurrencyID);

                _CommandObj.Parameters.AddWithValue("@AmountToBePay", RequestData.InVoiceCashDetailsData.AmountToBePay);
                _CommandObj.Parameters.AddWithValue("@ReceivedAmount", RequestData.InVoiceCashDetailsData.ReceivedAmount);
                _CommandObj.Parameters.AddWithValue("@BalanceAmountToBePay", RequestData.InVoiceCashDetailsData.BalanceAmountToBePay);
                _CommandObj.Parameters.AddWithValue("@ReturnAmount", RequestData.InVoiceCashDetailsData.ReturnAmount);

                _CommandObj.Parameters.AddWithValue("@FromCountryID", RequestData.InVoiceCashDetailsData.FromCountryID);
                _CommandObj.Parameters.AddWithValue("@FromStoreID", RequestData.InVoiceCashDetailsData.FromStoreID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.InVoiceCashDetailsData.CreateBy);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice Cash Details");
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

        public override SelectByInvoiceNoCashDetailsResponse SelectByInvoiceNoCashDetails(SelectByInvoiceNoCashDetailsRequest ReqObj)
        {
            var InvoiceNoCashDatas = new InVoiceCashDetails();
            var RequestData = (SelectByInvoiceNoCashDetailsRequest)ReqObj;
            var ResponseData = new SelectByInvoiceNoCashDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sSql.Append("select * from InVoiceCashDetails where InvoiceHeaderID=" + RequestData.InvoiceHeaderID);
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                }
                else
                {
                    sSql.Append("select * from InVoiceCashDetails where InvoiceNumber = '" + RequestData.InvoiceNo + "' and InvoiceHeaderID=" + RequestData.InvoiceHeaderID);
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    List<PaymentDetail> PaymentList = new List<PaymentDetail>();

                    while (objReader.Read())
                    {
                        var objInVoiceCashDetails = new PaymentDetail();
                        objInVoiceCashDetails.ID = Convert.ToInt32(objReader["ID"]);
                        objInVoiceCashDetails.Mode = "Cash";
                        objInVoiceCashDetails.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objInVoiceCashDetails.BusinessDate = Convert.ToDateTime(objReader["ApplicationDate"]);
                        objInVoiceCashDetails.InvoiceNumber = objReader["InVoiceNumber"].ToString();   
                        objInVoiceCashDetails.FromCountryID = Convert.ToInt32(objReader["FromCountryID"]);
                        objInVoiceCashDetails.FromStoreID = Convert.ToInt32(objReader["FromStoreID"]);
                        objInVoiceCashDetails.Receivedamount = Convert.ToDecimal(objReader["ReceivedAmount"]);                        
                        objInVoiceCashDetails.ChangeCurrency = Convert.ToString(objReader["ChangeCurrency"]);
                        objInVoiceCashDetails.ChangeCurrencyID = Convert.ToInt32(objReader["ChangeCurrencyID"]);
                        objInVoiceCashDetails.PayCurrency = Convert.ToString(objReader["PaymentCurrency"]);
                        objInVoiceCashDetails.PayCurrencyID = Convert.ToInt32(objReader["PaymentCurrencyID"]);
                        objInVoiceCashDetails.CreateBy = Convert.ToInt32(objReader["CreatedBy"]);

                        PaymentList.Add(objInVoiceCashDetails);
                    }
                    ResponseData.InvoiceNoCashDetails = PaymentList;
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
    }
}
