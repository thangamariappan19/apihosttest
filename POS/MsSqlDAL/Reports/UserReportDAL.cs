using EasyBizAbsDAL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizRequest;
using EasyBizRequest.Reports.UserReports;
using EasyBizResponse;
using EasyBizResponse.Reports.UserReports;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Reports
{
    public class UserReportDAL : BaseUserReportDAL
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
            var RequestData = (InsertUserReportRequest)RequestObj;
            var ResponseData = new InsertUserReportResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateUserReport", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.UserReportRecord.ID;

                SqlParameter ReportName = _CommandObj.Parameters.Add("@ReportName", SqlDbType.NVarChar);
                ReportName.Direction = ParameterDirection.Input;
                ReportName.Value = RequestData.UserReportRecord.ReportName;

                SqlParameter ReportFile = _CommandObj.Parameters.Add("@ReportFile", SqlDbType.VarBinary);
                ReportFile.Direction = ParameterDirection.Input;
                ReportFile.Value = RequestData.UserReportRecord.ReportFile;

                SqlParameter ViewRoles = _CommandObj.Parameters.Add("@ViewRoles", SqlDbType.NVarChar);
                ViewRoles.Direction = ParameterDirection.Input;
                ViewRoles.Value = RequestData.UserReportRecord.ViewRoles;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.UserReportRecord.Remarks;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.UserReportRecord.Active;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.UserReportRecord.CreateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, int.MaxValue);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ReportID = _CommandObj.Parameters.Add("@ReportID", SqlDbType.Int);
                ReportID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Register User Report");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ReportID.Value.ToString();
                }                
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.IDs = string.Empty;
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Register User Report");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var UserReportList = new List<UserReport>();
            var RequestData = (SelectAllUserReportRequest)RequestObj;
            var ResponseData = new SelectAllUserReportResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                
               
                string sSql = "Select * from UserReport ";

                if (RequestData.ShowInActiveRecords == false)
                {
                    sSql = sSql + " where Active='True'";
                }

                if (RequestData.ShowInActiveRecords == true &&  RequestData.ID != 0)
                {
                    sSql = sSql + " where ID=" + RequestData.ID;
                }
                else if (RequestData.ShowInActiveRecords == false)
                {
                    sSql = sSql + " and ID=" + RequestData.ID;
                }

                if (RequestData.ShowInActiveRecords == true && RequestData.ReportName != null && RequestData.ReportName != string.Empty)
                {
                    sSql = sSql + " where ReportName='" + RequestData.ReportName + "'";
                }
                else if (RequestData.ReportName != null && RequestData.ReportName != string.Empty)
                {
                    sSql = sSql + " and ReportName='" + RequestData.ReportName + "'";
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objAgent = new UserReport();
                        objAgent.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAgent.ReportName = objReader["ReportName"] != DBNull.Value ? Convert.ToString(objReader["ReportName"]) : string.Empty;
                        objAgent.ReportFile = objReader["ReportFile"] != DBNull.Value ? (byte[])objReader["ReportFile"] : null;                        
                        objAgent.ViewRoles = objReader["ViewRoles"] != DBNull.Value ? Convert.ToString(objReader["ViewRoles"]) : string.Empty;
                        objAgent.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : string.Empty;

                        objAgent.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objAgent.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objAgent.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objAgent.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;                        
                        objAgent.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        UserReportList.Add(objAgent);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UserReportList = UserReportList;
                    ResponseData.ResponseDynamicData = UserReportList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Report");
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

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
