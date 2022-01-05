using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest;
using EasyBizResponse.Transactions.POS.GiftvoucherDetailsResponse;
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
    public class GiftvoucherDetailsDAL : BaseGiftvoucherDetailsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveGiftvoucherDetailsRequest)RequestObj;
            var ResponseData = new SaveGiftvoucherDetailsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateGiftvoucherDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.GiftvoucherPaymentDetails.ID);
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.GiftvoucherPaymentDetails.ApplicationDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.GiftvoucherPaymentDetails.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@InvoiceNumber", RequestData.GiftvoucherPaymentDetails.InvoiceNumber);
                _CommandObj.Parameters.AddWithValue("@PayMentMode", RequestData.GiftvoucherPaymentDetails.PayMentMode);
                _CommandObj.Parameters.AddWithValue("@GiftvoucherCode", RequestData.GiftvoucherPaymentDetails.GiftvoucherCode);
                _CommandObj.Parameters.AddWithValue("@Amount", RequestData.GiftvoucherPaymentDetails.Amount);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.GiftvoucherPaymentDetails.CreateBy);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice Gift Voucher Details");
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
        public override SelectGiftvoucherDetailByInvoiceNoResponse SelectGiftvoucherDetailByInvoiceNo(SelectGiftvoucherDetailByInvoiceNoRequest ObjReq)
        {
            var InvoiceNoCashDatas = new GiftvoucherDetail();
            var RequestData = (SelectGiftvoucherDetailByInvoiceNoRequest)ObjReq;
            var ResponseData = new SelectGiftvoucherDetailByInvoiceNoResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from GifvoucherPayment where InvoiceNumber = '" + RequestData.InvoiceNumber + "'");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objGiftvoucherDetail = new GiftvoucherDetail();
                        objGiftvoucherDetail.ID = Convert.ToInt32(objReader["ID"]);
                        objGiftvoucherDetail.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objGiftvoucherDetail.InvoiceNumber = objReader["InvoiceNumber"].ToString();
                        objGiftvoucherDetail.ApplicationDate = Convert.ToDateTime(objReader["ApplicationDate"]);
                        objGiftvoucherDetail.GiftvoucherCode = Convert.ToString(objReader["GiftvoucherCode"]);
                        objGiftvoucherDetail.Amount = Convert.ToInt32(objReader["Amount"]);

                        ResponseData.GiftvoucherDetailData = objGiftvoucherDetail;
                    }
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
