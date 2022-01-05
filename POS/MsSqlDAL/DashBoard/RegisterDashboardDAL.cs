using EasyBizAbsDAL.DashBoard;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Dashboard;
using EasyBizRequest.DashBoardRequest;
using EasyBizRequest.Masters.DashboardRequest;
using EasyBizResponse.DashBoard;
using EasyBizResponse.Masters.DashboardReponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.DashBoard
{
    public class RegisterDashboardDAL : BaseRegisterDashBoardDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveDashBoardRequest)RequestObj;
            var ResponseData = new SaveDashBoardResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertDashBoardReports", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ReportName = _CommandObj.Parameters.Add("@ReportName", SqlDbType.NVarChar);
                ReportName.Direction = ParameterDirection.Input;
                ReportName.Value = RequestData.DashBoardReportsRecord.ReportName;

                
                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.DashBoardReportsRecord.Remarks;

                SqlParameter ReportFile = _CommandObj.Parameters.Add("@ReportFile", SqlDbType.Binary);
                ReportFile.Direction = ParameterDirection.Input;
                ReportFile.Value = RequestData.DashBoardReportsRecord.ReportFile;

                SqlParameter IsActive = _CommandObj.Parameters.Add("@IsActive", SqlDbType.Bit);
                IsActive.Direction = ParameterDirection.Input;
                IsActive.Value = RequestData.DashBoardReportsRecord.IsActive;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.DashBoardReportsRecord.CreateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "DashBoard Reports");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }

                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "DashBoard Reports");

                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateDashBoardRequest)RequestObj;
            var ResponseData = new UpdateRegisterDashBoardResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateDashBoardReports", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.DashBoardReportsRecord.ID;

                SqlParameter ReportName = _CommandObj.Parameters.Add("@ReportName", SqlDbType.NVarChar);
                ReportName.Direction = ParameterDirection.Input;
                ReportName.Value = RequestData.DashBoardReportsRecord.ReportName;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.DashBoardReportsRecord.Remarks;

                SqlParameter ReportFile = _CommandObj.Parameters.Add("@ReportFile", SqlDbType.Binary);
                ReportFile.Direction = ParameterDirection.Input;
                ReportFile.Value = RequestData.DashBoardReportsRecord.ReportFile;

                SqlParameter IsActive = _CommandObj.Parameters.Add("@IsActive", SqlDbType.Bit);
                IsActive.Direction = ParameterDirection.Input;
                IsActive.Value = RequestData.DashBoardReportsRecord.IsActive;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@UpdatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.DashBoardReportsRecord.CreateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "DashBoard Reports");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "DashBoard Reports");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                }


            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }


            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (DeleteRegisterDashboardRequest)RequestObj;
            var ResponseData = new DeleteRegisterDashBoardResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                _CommandObj = new SqlCommand("Delete DashboardReport where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "DashBoardReports");
      
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "DashBoard Reports");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ReportGeneratorRecord = new RegisterDashboard();
            var RequestData = (SelectDashBoardRequest)RequestObj;
            var ResponseData = new SelectRegisterDashBoardResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from DashboardReport with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReportGenerator = new RegisterDashboard();
                        objReportGenerator.ID = Convert.ToInt32(objReader["ID"]);
                        objReportGenerator.ReportName = Convert.ToString(objReader["ReportName"]);                       
                        objReportGenerator.Remarks = Convert.ToString(objReader["Remarks"]);
                        objReportGenerator.ReportFile = (Byte[])objReader["ReportFile"];
                        objReportGenerator.IsActive = Convert.ToBoolean(objReader["IsActive"]);
                        objReportGenerator.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objReportGenerator.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;
                        objReportGenerator.UpdateOn = objReader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdatedOn"]) : DateTime.Now;
                        objReportGenerator.UpdateBy = objReader["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdatedBy"]) : 0; ;
                        objReportGenerator.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;                       
                        ReportGeneratorRecord = objReportGenerator;
                    }
                    ResponseData.DashBoardReportsRecord = ReportGeneratorRecord;
                    ResponseData.ResponseDynamicData = ReportGeneratorRecord;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DashBoard Reports");
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
           
            var DashBoardReportsList = new List<RegisterDashboard>();
            var RequestData = (SelectAllRegisterDashboardRequest)RequestObj;
            var ResponseData = new SelectAllRegisterDashBoardResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from DashboardReport with(NoLock) ", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReportGenerator = new RegisterDashboard();
                        objReportGenerator.ID = Convert.ToInt32(objReader["ID"]);
                        objReportGenerator.ReportName = Convert.ToString(objReader["ReportName"]);
                       // objReportGenerator.AccessRoles = Convert.ToString(objReader["AccessRoles"]);
                        objReportGenerator.Remarks = Convert.ToString(objReader["Remarks"]);
                        objReportGenerator.ReportFile = (Byte[])objReader["ReportFile"];
                        objReportGenerator.IsActive = Convert.ToBoolean(objReader["IsActive"]);
                       // objReportGenerator.Purpose = Convert.ToString(objReader["Purpose"]);
                        objReportGenerator.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objReportGenerator.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;
                        objReportGenerator.UpdateOn = objReader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdatedOn"]) : DateTime.Now;
                        objReportGenerator.UpdateBy = objReader["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdatedBy"]) : 0; ;
                        objReportGenerator.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                      //  objReportGenerator.IsActive = objReader["IsActive"] != DBNull.Value ? Convert.ToBoolean(objReader["IsActive"]) : true;

                        DashBoardReportsList.Add(objReportGenerator);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DashBoardReportsList = DashBoardReportsList;
                    ResponseData.ResponseDynamicData = DashBoardReportsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Report Generator");
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
        public override SelectDashboardResponse API_SelectBetweenDayDetails(SelectDashboardRequest requestData)
        {
            throw new NotImplementedException();
        }
    }
}
