using EasyBizAbsDAL.Common;
using EasyBizDBTypes.Common;
using EasyBizRequest.Common;
using EasyBizResponse.Common;
using ResourceStrings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlDAL.Common
{
    public class DayClosingDAL : BaseDayClosingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveDayClosingRequest)RequestObj;
            var ResponseData = new SaveDayClosingResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertDayClosing", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DayClosingID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                DayClosingID.Direction = ParameterDirection.Input;
                DayClosingID.Value = RequestData.DayClosingRecord.ID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.DayClosingRecord.CountryID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.DayClosingRecord.CountryCode;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.DayClosingRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.DayClosingRecord.StoreCode;

                SqlParameter POSID = _CommandObj.Parameters.Add("@POSID", SqlDbType.Int);
                POSID.Direction = ParameterDirection.Input;
                POSID.Value = RequestData.DayClosingRecord.POSID;

                SqlParameter PosCode = _CommandObj.Parameters.Add("@PosCode", SqlDbType.NVarChar);
                PosCode.Direction = ParameterDirection.Input;
                PosCode.Value = RequestData.DayClosingRecord.PosCode;

                SqlParameter ShiftInUserID = _CommandObj.Parameters.Add("@ShiftInUserID", SqlDbType.Int);
                ShiftInUserID.Direction = ParameterDirection.Input;
                ShiftInUserID.Value = RequestData.DayClosingRecord.ShiftInUserID;

                SqlParameter ShiftInUserCode = _CommandObj.Parameters.Add("@ShiftInUserCode", SqlDbType.NVarChar);
                ShiftInUserCode.Direction = ParameterDirection.Input;
                ShiftInUserCode.Value = RequestData.DayClosingRecord.ShiftInUserCode;

                SqlParameter ShiftOutUserID = _CommandObj.Parameters.Add("@ShiftOutUserID", SqlDbType.Int);
                ShiftOutUserID.Direction = ParameterDirection.Input;
                ShiftOutUserID.Value = RequestData.DayClosingRecord.ShiftOutUserID;

                SqlParameter ShiftID = _CommandObj.Parameters.Add("@ShiftID", SqlDbType.Int);
                ShiftID.Direction = ParameterDirection.Input;
                ShiftID.Value = RequestData.DayClosingRecord.ShiftID;

                SqlParameter ShiftCode = _CommandObj.Parameters.Add("@ShiftCode", SqlDbType.NVarChar);
                ShiftCode.Direction = ParameterDirection.Input;
                ShiftCode.Value = RequestData.DayClosingRecord.ShiftCode;

                SqlParameter Amount = _CommandObj.Parameters.Add("@Amount", SqlDbType.Money);
                Amount.Direction = ParameterDirection.Input;
                Amount.Value = RequestData.DayClosingRecord.Amount;

                SqlParameter BusinessDate = _CommandObj.Parameters.Add("@BusinessDate", SqlDbType.DateTime);
                BusinessDate.Direction = ParameterDirection.Input;
                BusinessDate.Value = sqlCommon.GetSQLServerDateString(RequestData.DayClosingRecord.StartingTime);

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.DayClosingRecord.Status;


                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();


                string strStatusCode = StatusCode.Value.ToString();
                string strDisplay = StatusMsg.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "DayClosing Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else if (strStatusCode == "2")
                {
                    //ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "DayClosing Master");
                    ResponseData.DisplayMessage = strDisplay;
                    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
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
            var RequestData = (SaveDayClosingRequest)RequestObj;
            var ResponseData = new SaveDayClosingResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_UpdateDayClosing", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DayClosingID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                DayClosingID.Direction = ParameterDirection.Input;
                DayClosingID.Value = RequestData.DayClosingRecord.ID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.DayClosingRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.DayClosingRecord.StoreID;

                SqlParameter POSID = _CommandObj.Parameters.Add("@POSID", SqlDbType.Int);
                POSID.Direction = ParameterDirection.Input;
                POSID.Value = RequestData.DayClosingRecord.POSID;

                SqlParameter ShiftInUserID = _CommandObj.Parameters.Add("@ShiftInUserID", SqlDbType.Int);
                ShiftInUserID.Direction = ParameterDirection.Input;
                ShiftInUserID.Value = RequestData.DayClosingRecord.ShiftInUserID;

                SqlParameter ShiftOutUserID = _CommandObj.Parameters.Add("@ShiftOutUserID", SqlDbType.Int);
                ShiftOutUserID.Direction = ParameterDirection.Input;
                ShiftOutUserID.Value = RequestData.DayClosingRecord.ShiftOutUserID;

                SqlParameter ShiftID = _CommandObj.Parameters.Add("@ShiftID", SqlDbType.Int);
                ShiftID.Direction = ParameterDirection.Input;
                ShiftID.Value = RequestData.DayClosingRecord.ShiftID;

                SqlParameter Amount = _CommandObj.Parameters.Add("@ShiftOutAmount", SqlDbType.Money);
                Amount.Direction = ParameterDirection.Input;
                Amount.Value = RequestData.DayClosingRecord.Amount;

                SqlParameter BusinessDate = _CommandObj.Parameters.Add("@BusinessDate", SqlDbType.Date);
                BusinessDate.Direction = ParameterDirection.Input;
                BusinessDate.Value = RequestData.DayClosingRecord.BuisnessDateStr;

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.DayClosingRecord.Status;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();


                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "DayClosing Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "DayClosing Master");
                }
                else
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
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

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
