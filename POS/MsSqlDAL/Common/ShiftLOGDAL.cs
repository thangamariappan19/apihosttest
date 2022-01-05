using EasyBizAbsDAL.Common;
using EasyBizDBTypes.Common;
using EasyBizRequest.Common;
using EasyBizResponse.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Common
{
    public class ShiftLOGDAL : BaseShiftLOGDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveShiftLOGRequest)RequestObj;
            var ResponseData = new SaveShiftLOGResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertShiftLOG", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DayClosingID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                DayClosingID.Direction = ParameterDirection.Input;
                DayClosingID.Value = RequestData.ShiftRecord.ID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.ShiftRecord.CountryID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.ShiftRecord.CountryCode;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.ShiftRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.ShiftRecord.StoreCode;

                SqlParameter POSID = _CommandObj.Parameters.Add("@POSID", SqlDbType.Int);
                POSID.Direction = ParameterDirection.Input;
                POSID.Value = RequestData.ShiftRecord.POSID;

                SqlParameter PosCode = _CommandObj.Parameters.Add("@PosCode", SqlDbType.NVarChar);
                PosCode.Direction = ParameterDirection.Input;
                PosCode.Value = RequestData.ShiftRecord.PosCode;

                SqlParameter ShiftInUserID = _CommandObj.Parameters.Add("@ShiftInUserID", SqlDbType.Int);
                ShiftInUserID.Direction = ParameterDirection.Input;
                ShiftInUserID.Value = RequestData.ShiftRecord.ShiftInUserID;

                SqlParameter ShiftInUserCode = _CommandObj.Parameters.Add("@ShiftInUserCode", SqlDbType.NVarChar);
                ShiftInUserCode.Direction = ParameterDirection.Input;
                ShiftInUserCode.Value = RequestData.ShiftRecord.ShiftInUserCode;

                SqlParameter ShiftOutUserID = _CommandObj.Parameters.Add("@ShiftOutUserID", SqlDbType.Int);
                ShiftOutUserID.Direction = ParameterDirection.Input;
                ShiftOutUserID.Value = RequestData.ShiftRecord.ShiftOutUserID;

                SqlParameter ShiftInOutUserCode = _CommandObj.Parameters.Add("@ShiftInOutUserCode", SqlDbType.NVarChar);
                ShiftInOutUserCode.Direction = ParameterDirection.Input;
                ShiftInOutUserCode.Value = RequestData.ShiftRecord.ShiftInOutUserCode;

                SqlParameter ShiftID = _CommandObj.Parameters.Add("@ShiftID", SqlDbType.Int);
                ShiftID.Direction = ParameterDirection.Input;
                ShiftID.Value = RequestData.ShiftRecord.ShiftID;

                SqlParameter ShiftCode = _CommandObj.Parameters.Add("@ShiftCode", SqlDbType.NVarChar);
                ShiftCode.Direction = ParameterDirection.Input;
                ShiftCode.Value = RequestData.ShiftRecord.ShiftCode;

                SqlParameter BusinessDate = _CommandObj.Parameters.Add("@BusinessDate", SqlDbType.DateTime);
                BusinessDate.Direction = ParameterDirection.Input;
                BusinessDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ShiftRecord.BusinessDate);
                

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.ShiftRecord.Status;


                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ShiftLOG");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyLogedIN.Replace("{}", "ShiftLOG");
                    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ShiftLOG");
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
            var RequestData = (SaveShiftLOGRequest)RequestObj;
            var ResponseData = new SaveShiftLOGResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateShiftLOG", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DayClosingID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                DayClosingID.Direction = ParameterDirection.Input;
                DayClosingID.Value = RequestData.ShiftRecord.ID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.ShiftRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.ShiftRecord.StoreID;

                SqlParameter POSID = _CommandObj.Parameters.Add("@POSID", SqlDbType.Int);
                POSID.Direction = ParameterDirection.Input;
                POSID.Value = RequestData.ShiftRecord.POSID;

                SqlParameter Amount = _CommandObj.Parameters.Add("@ShiftOutAmount", SqlDbType.Money);
                Amount.Direction = ParameterDirection.Input;
                Amount.Value = RequestData.ShiftRecord.Amount;

                SqlParameter BusinessDate = _CommandObj.Parameters.Add("@BusinessDate", SqlDbType.Date);
                BusinessDate.Direction = ParameterDirection.Input;
                BusinessDate.Value = RequestData.ShiftRecord.BusinessDate;  

                SqlParameter ShiftInUserID = _CommandObj.Parameters.Add("@ShiftInUserID", SqlDbType.Int);
                ShiftInUserID.Direction = ParameterDirection.Input;
                ShiftInUserID.Value = RequestData.ShiftRecord.ShiftInUserID;

                SqlParameter ShiftInOutUserCode = _CommandObj.Parameters.Add("@ShiftInOutUserCode", SqlDbType.NVarChar);
                ShiftInOutUserCode.Direction = ParameterDirection.Input;
                ShiftInOutUserCode.Value = RequestData.ShiftRecord.ShiftInOutUserCode;

                SqlParameter ShiftOutUserID = _CommandObj.Parameters.Add("@ShiftOutUserID", SqlDbType.Int);
                ShiftOutUserID.Direction = ParameterDirection.Input;
                ShiftOutUserID.Value = RequestData.ShiftRecord.ShiftOutUserID;

                SqlParameter ShiftID = _CommandObj.Parameters.Add("@ShiftID", SqlDbType.Int);
                ShiftID.Direction = ParameterDirection.Input;
                ShiftID.Value = RequestData.ShiftRecord.ShiftID;               

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = "Open";

                




                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ShiftLOG");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ShiftLOG");
                    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ShiftLOG");
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
    }
}
