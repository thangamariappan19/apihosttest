using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.TillSettingRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.TillSettingsResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
    public class TillSettingsDAL: BaseTillSettingsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveTillSettingsRequest)RequestObj;
            var ResponseData = new SaveTillSettingsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertTillSettings", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.TillSettingsData.CountryID;

                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.TillSettingsData.StoreID;

                var PosID = _CommandObj.Parameters.Add("@PosID", SqlDbType.Int);
                PosID.Direction = ParameterDirection.Input;
                PosID.Value = RequestData.TillSettingsData.PosID;

                var UserID = _CommandObj.Parameters.Add("@UserID", SqlDbType.Int);
                UserID.Direction = ParameterDirection.Input;
                UserID.Value = RequestData.TillSettingsData.UserTeamID;

                var FloatingAmount = _CommandObj.Parameters.Add("@FloatingAmount ", SqlDbType.Decimal);
                FloatingAmount.Direction = ParameterDirection.Input;
                FloatingAmount.Value = RequestData.TillSettingsData.FloatingAmount;

                var CountType = _CommandObj.Parameters.Add("@CountType ", SqlDbType.Int);
                CountType.Direction = ParameterDirection.Input;
                CountType.Value = RequestData.TillSettingsData.CountType;

                var CountRequired = _CommandObj.Parameters.Add("@CountRequired ", SqlDbType.Bit);
                CountRequired.Direction = ParameterDirection.Input;
                CountRequired.Value = RequestData.TillSettingsData.StoreID;

                var TillCountOnAssign = _CommandObj.Parameters.Add("@TillCountOnAssign ", SqlDbType.Bit);
                TillCountOnAssign.Direction = ParameterDirection.Input;
                TillCountOnAssign.Value = RequestData.TillSettingsData.TillCountOnAssign;

                var TillCountOnClose = _CommandObj.Parameters.Add("@TillCountOnClose  ", SqlDbType.Bit);
                TillCountOnClose.Direction = ParameterDirection.Input;
                TillCountOnClose.Value = RequestData.TillSettingsData.TillCountOnClose;

                var TillCountOnFinalize = _CommandObj.Parameters.Add("@TillCountOnFinalize  ", SqlDbType.Bit);
                TillCountOnFinalize.Direction = ParameterDirection.Input;
                TillCountOnFinalize.Value = RequestData.TillSettingsData.TillCountOnFinalize;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.TillSettingsData.Remarks;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.TillSettingsData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.TillSettingsData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TillSettings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "TillSettings");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TillSettings");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TillSettings");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateTillSettingsRequest)RequestObj;
            var ResponseData = new UpdateTillSettingsResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateTillSettings", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.TillSettingsData.ID;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.TillSettingsData.CountryID;

                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.TillSettingsData.StoreID;

                var PosID = _CommandObj.Parameters.Add("@PosID", SqlDbType.Int);
                PosID.Direction = ParameterDirection.Input;
                PosID.Value = RequestData.TillSettingsData.PosID;

                var UserID = _CommandObj.Parameters.Add("@UserID", SqlDbType.Int);
                UserID.Direction = ParameterDirection.Input;
                UserID.Value = RequestData.TillSettingsData.UserTeamID;

                var FloatingAmount = _CommandObj.Parameters.Add("@FloatingAmount ", SqlDbType.Decimal);
                FloatingAmount.Direction = ParameterDirection.Input;
                FloatingAmount.Value = RequestData.TillSettingsData.FloatingAmount;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.TillSettingsData.Remarks;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.TillSettingsData.Active;

                var CountType = _CommandObj.Parameters.Add("@CountType ", SqlDbType.Int);
                CountType.Direction = ParameterDirection.Input;
                CountType.Value = RequestData.TillSettingsData.CountType;

                var CountRequired = _CommandObj.Parameters.Add("@CountRequired ", SqlDbType.Bit);
                CountRequired.Direction = ParameterDirection.Input;
                CountRequired.Value = RequestData.TillSettingsData.StoreID;

                var TillCountOnAssign = _CommandObj.Parameters.Add("@TillCountOnAssign ", SqlDbType.Bit);
                TillCountOnAssign.Direction = ParameterDirection.Input;
                TillCountOnAssign.Value = RequestData.TillSettingsData.TillCountOnAssign;

                var TillCountOnClose = _CommandObj.Parameters.Add("@TillCountOnClose  ", SqlDbType.Bit);
                TillCountOnClose.Direction = ParameterDirection.Input;
                TillCountOnClose.Value = RequestData.TillSettingsData.TillCountOnClose;

                var TillCountOnFinalize = _CommandObj.Parameters.Add("@TillCountOnFinalize  ", SqlDbType.Bit);
                TillCountOnFinalize.Direction = ParameterDirection.Input;
                TillCountOnFinalize.Value = RequestData.TillSettingsData.TillCountOnFinalize;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.TillSettingsData.SCN;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.TillSettingsData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "TillSettings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "TillSettings");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "TillSettings");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var TillSettingsRecord = new TillSettings();

            var RequestData = (DeleteTillSettingsRequest)RequestObj;
            var ResponseData = new DeleteTillSettingsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Delete from TillSettings where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "TillSettings");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "TillSettings");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var TillSettingsRecord = new TillSettings();
            var RequestData = (SelectByIDTillSettingsRequest)RequestObj;
            var ResponseData = new SelectByIDTillSettingsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from TillSettings with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTillSettings = new TillSettings();
                        objTillSettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTillSettings.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) :0;
                        objTillSettings.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objTillSettings.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) :0;
                        objTillSettings.UserTeamID = objReader["UserID"] != DBNull.Value ? Convert.ToInt32(objReader["UserID"]) :0;
                        objTillSettings.FloatingAmount = objReader["FloatingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatingAmount"]) :0;
                        objTillSettings.CountRequired = objReader["CountRequired"] != DBNull.Value ? Convert.ToBoolean(objReader["CountRequired"]) : true;
                        objTillSettings.CountType = objReader["CountType"] != DBNull.Value ? Convert.ToInt32(objReader["CountType"]) :0;
                        objTillSettings.Remarks = Convert.ToString(objReader["Remarks"]);
                        objTillSettings.TillCountOnAssign = objReader["TillCountOnAssign"] != DBNull.Value ? Convert.ToBoolean(objReader["TillCountOnAssign"]) :true;
                        objTillSettings.TillCountOnClose = objReader["TillCountOnClose"] != DBNull.Value ? Convert.ToBoolean(objReader["TillCountOnClose"]) : true;
                        objTillSettings.TillCountOnFinalize = objReader["TillCountOnFinalize"] != DBNull.Value ? Convert.ToBoolean(objReader["TillCountOnFinalize"]) : true;
                        objTillSettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTillSettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTillSettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTillSettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objTillSettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTillSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.TillSettingsRecord = objTillSettings;
                        ResponseData.ResponseDynamicData = objTillSettings;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "TillSettings");
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
            var TillSettingsList = new List<TillSettings>();
            var RequestData = (SelectAllTillSettingsRequest)RequestObj;
            var ResponseData = new SelectAllTillSettingsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select *,RM.RoleName,Cm.CountTypeName,PM.PosName,SM.StoreName,CN.CountryName from TillSettings TS  ");
                sSql.Append("left outer Join RoleMaster RM on TS.UserID=RM.ID ");
                sSql.Append("left outer Join CountTypeMaster CM on CM.ID=TS.CountType   ");
                sSql.Append("left outer Join PosMaster PM on PM.ID=TS.PosID   ");
                sSql.Append("left outer Join StoreMaster SM on SM.ID=TS.StoreID  ");
                sSql.Append("left outer Join CountryMaster CN on CN.ID=TS.CountryID  ");
                
                sSql.Append("order by TS.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTillSettings = new TillSettings();
                        objTillSettings.ID =objReader["ID"] != DBNull.Value ?  Convert.ToInt32(objReader["ID"]) :0;
                        objTillSettings.StoreID =objReader["StoreID"] != DBNull.Value ?  Convert.ToInt32(objReader["StoreID"]) :0;
                        objTillSettings.CountryID =objReader["CountryID"] != DBNull.Value ?  Convert.ToInt32(objReader["CountryID"]) :0;
                        objTillSettings.PosID =objReader["PosID"] != DBNull.Value ?  Convert.ToInt32(objReader["PosID"]) :0;
                        objTillSettings.UserTeamID =objReader["UserID"] != DBNull.Value ?  Convert.ToInt32(objReader["UserID"]) :0;
                        objTillSettings.FloatingAmount =objReader["FloatingAmount"] != DBNull.Value ?  Convert.ToDecimal(objReader["FloatingAmount"]) :0;
                        objTillSettings.CountRequired =objReader["CountRequired"] != DBNull.Value ?  Convert.ToBoolean(objReader["CountRequired"]) : true;
                        objTillSettings.CountType =objReader["CountType"] != DBNull.Value ?  Convert.ToInt32(objReader["CountType"]) :0;
                        objTillSettings.TillCountOnAssign =objReader["TillCountOnAssign"] != DBNull.Value ?  Convert.ToBoolean(objReader["TillCountOnAssign"]) : true;
                        objTillSettings.TillCountOnClose =objReader["TillCountOnClose"] != DBNull.Value ?  Convert.ToBoolean(objReader["TillCountOnClose"]) : true;
                        objTillSettings.TillCountOnFinalize = objReader["TillCountOnFinalize"] != DBNull.Value ? Convert.ToBoolean(objReader["TillCountOnFinalize"]) : true;
                        objTillSettings.Remarks = Convert.ToString(objReader["Remarks"]);
                        objTillSettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTillSettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTillSettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTillSettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objTillSettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTillSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objTillSettings.CountTypeName = Convert.ToString(objReader["CountTypeName"]);
                        objTillSettings.StoreName = Convert.ToString(objReader["StoreName"]);
                        objTillSettings.PosName = Convert.ToString(objReader["PosName"]);
                        objTillSettings.CountryName = Convert.ToString(objReader["CountryName"]);
                        objTillSettings.RoleName = Convert.ToString(objReader["RoleName"]);                        
                        TillSettingsList.Add(objTillSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TillSettingsList = TillSettingsList;
                    ResponseData.ResponseDynamicData = TillSettingsList;                   

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "TillSettings");
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

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectTillSettingsLookUpResponse SelectTillSettingsLookUp(SelectTillSettingsLookUpRequest RequestObj)
        {
            var TillSettingsList = new List<TillSettings>();


            SelectTillSettingsLookUpRequest RequestData = new SelectTillSettingsLookUpRequest();

            SelectTillSettingsLookUpResponse ResponseData = new SelectTillSettingsLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select * from TillSettings with(NoLock) where Active='True' ";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTillSettings = new TillSettings();
                        objTillSettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTillSettings.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objTillSettings.StoreID =objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) :0;
                        objTillSettings.PosID =objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) :0;
                        objTillSettings.UserTeamID =objReader["UserID"] != DBNull.Value ? Convert.ToInt32(objReader["UserID"]) :0;
                        TillSettingsList.Add(objTillSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TillSettingsList = TillSettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "TillSettings");
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

    }
}
