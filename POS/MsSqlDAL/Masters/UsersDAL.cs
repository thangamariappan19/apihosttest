using CommonRoutines;
using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.UsersResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DPUruNet;
//using static DPUruNet.Constants;

namespace MsSqlDAL.Masters
{
    public class UsersDAL : BaseUsersDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveUsersRequest)RequestObj;
            var ResponseData = new SaveUsersResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertUsersMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter UserID = _CommandObj.Parameters.Add("@UserID", SqlDbType.Int);
                UserID.Direction = ParameterDirection.Input;
                UserID.Value = RequestData.UsersRecord.ID;

                var UsersCode = _CommandObj.Parameters.Add("@UserCode", SqlDbType.NVarChar);
                UsersCode.Direction = ParameterDirection.Input;
                UsersCode.Value = RequestData.UsersRecord.UserCode;

                var UserName = _CommandObj.Parameters.Add("@UserName", SqlDbType.NVarChar);
                UserName.Direction = ParameterDirection.Input;
                UserName.Value = RequestData.UsersRecord.UserName;

                var Password = _CommandObj.Parameters.Add("@PassWord", SqlDbType.NVarChar);
                Password.Direction = ParameterDirection.Input;
                Password.Value = RequestData.UsersRecord.Password;

                var EmployeeID = _CommandObj.Parameters.Add("@EmployeeID", SqlDbType.Int);
                EmployeeID.Direction = ParameterDirection.Input;
                EmployeeID.Value = RequestData.UsersRecord.EmployeeID;

                var RoleID = _CommandObj.Parameters.Add("@RoleID", SqlDbType.Int);
                RoleID.Direction = ParameterDirection.Input;
                RoleID.Value = RequestData.UsersRecord.RoleID;

                var ManagerOverrideID = _CommandObj.Parameters.Add("@ManagerOverrideID", SqlDbType.Int);
                ManagerOverrideID.Direction = ParameterDirection.Input;
                ManagerOverrideID.Value = RequestData.UsersRecord.ManagerOverrideID;

                var RetailID = _CommandObj.Parameters.Add("@RetailID", SqlDbType.Int);
                RetailID.Direction = ParameterDirection.Input;
                RetailID.Value = RequestData.UsersRecord.RetailID;

                var RetailCode = _CommandObj.Parameters.Add("@RetailSettingCode", SqlDbType.VarChar);
                RetailCode.Direction = ParameterDirection.Input;
                RetailCode.Value = RequestData.UsersRecord.RetailSettingCode;

                var EmployeeCode = _CommandObj.Parameters.Add("@EmployeeCode", SqlDbType.VarChar);
                EmployeeCode.Direction = ParameterDirection.Input;
                EmployeeCode.Value = RequestData.UsersRecord.EmployeeCode;

                var RoleCode = _CommandObj.Parameters.Add("@RoleCode", SqlDbType.VarChar);
                RoleCode.Direction = ParameterDirection.Input;
                RoleCode.Value = RequestData.UsersRecord.RoleCode;

                var ManagerOverrideCode = _CommandObj.Parameters.Add("@ManagerOverrideCode", SqlDbType.VarChar);
                ManagerOverrideCode.Direction = ParameterDirection.Input;
                ManagerOverrideCode.Value = RequestData.UsersRecord.ManagerOverrideCode;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.UsersRecord.Active;

                SqlParameter PasswordReset = _CommandObj.Parameters.Add("@PasswordReset", SqlDbType.NVarChar);
                PasswordReset.Direction = ParameterDirection.Input;
                PasswordReset.Value = RequestData.UsersRecord.PasswordReset;

                var AllowStockEdit = _CommandObj.Parameters.Add("@AllowStockEdit", SqlDbType.Bit);
                AllowStockEdit.Direction = ParameterDirection.Input;
                AllowStockEdit.Value = RequestData.UsersRecord.AllowStockEdit;

                var MobileUser = _CommandObj.Parameters.Add("@MobileUser", SqlDbType.Bit);
                MobileUser.Direction = ParameterDirection.Input;
                MobileUser.Value = RequestData.UsersRecord.MobileUser;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.UsersRecord.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Users");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Users");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
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
            var RequestData = (UpdateUsersRequest)RequestObj;
            var ResponseData = new UpdateUsersResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateUserMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.UsersRecord.ID;

                var UsersCode = _CommandObj.Parameters.Add("@UserCode", SqlDbType.NVarChar);
                UsersCode.Direction = ParameterDirection.Input;
                UsersCode.Value = RequestData.UsersRecord.UserCode;

                var UserName = _CommandObj.Parameters.Add("@UserName", SqlDbType.NVarChar);
                UserName.Direction = ParameterDirection.Input;
                UserName.Value = RequestData.UsersRecord.UserName;

                var Password = _CommandObj.Parameters.Add("@PassWord", SqlDbType.NVarChar);
                Password.Direction = ParameterDirection.Input;
                Password.Value = RequestData.UsersRecord.Password;

                var EmployeeID = _CommandObj.Parameters.Add("@EmployeeID", SqlDbType.Int);
                EmployeeID.Direction = ParameterDirection.Input;
                EmployeeID.Value = RequestData.UsersRecord.EmployeeID;

                var RoleID = _CommandObj.Parameters.Add("@RoleID", SqlDbType.Int);
                RoleID.Direction = ParameterDirection.Input;
                RoleID.Value = RequestData.UsersRecord.RoleID;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.UsersRecord.Active;

                SqlParameter PasswordReset = _CommandObj.Parameters.Add("@PasswordReset", SqlDbType.NVarChar);
                PasswordReset.Direction = ParameterDirection.Input;
                PasswordReset.Value = RequestData.UsersRecord.PasswordReset;

                var ManagerOverrideID = _CommandObj.Parameters.Add("@ManagerOverrideID", SqlDbType.Int);
                ManagerOverrideID.Direction = ParameterDirection.Input;
                ManagerOverrideID.Value = RequestData.UsersRecord.ManagerOverrideID;

                var RetailID = _CommandObj.Parameters.Add("@RetailID", SqlDbType.Int);
                RetailID.Direction = ParameterDirection.Input;
                RetailID.Value = RequestData.UsersRecord.RetailID;

                var RetailCode = _CommandObj.Parameters.Add("@RetailSettingCode", SqlDbType.VarChar);
                RetailCode.Direction = ParameterDirection.Input;
                RetailCode.Value = RequestData.UsersRecord.RetailSettingCode;

                var EmployeeCode = _CommandObj.Parameters.Add("@EmployeeCode", SqlDbType.VarChar);
                EmployeeCode.Direction = ParameterDirection.Input;
                EmployeeCode.Value = RequestData.UsersRecord.EmployeeCode;

                var RoleCode = _CommandObj.Parameters.Add("@RoleCode", SqlDbType.VarChar);
                RoleCode.Direction = ParameterDirection.Input;
                RoleCode.Value = RequestData.UsersRecord.RoleCode;

                var ManagerOverrideCode = _CommandObj.Parameters.Add("@ManagerOverrideCode", SqlDbType.VarChar);
                ManagerOverrideCode.Direction = ParameterDirection.Input;
                ManagerOverrideCode.Value = RequestData.UsersRecord.ManagerOverrideCode;

                var AllowStockEdit = _CommandObj.Parameters.Add("@AllowStockEdit", SqlDbType.Bit);
                AllowStockEdit.Direction = ParameterDirection.Input;
                AllowStockEdit.Value = RequestData.UsersRecord.AllowStockEdit;

                var MobileUser = _CommandObj.Parameters.Add("@MobileUser", SqlDbType.Bit);
                MobileUser.Direction = ParameterDirection.Input;
                MobileUser.Value = RequestData.UsersRecord.MobileUser;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.UsersRecord.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.UsersRecord.SCN;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Users");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Users");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
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
            var UserRecord = new UsersSettings();
            var RequestData = (DeleteUsersRequest)RequestObj;
            var ResponseData = new DeleteUsersResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from UserMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Users");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Users");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var UsersRecord = new UsersSettings();
            var RequestData = (SelectByUsersIDRequest)RequestObj;
            var ResponseData = new SelectByUsersIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from UserMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objUsersMaster = new UsersSettings();
                        objUsersMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objUsersMaster.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsersMaster.UserName = Convert.ToString(objReader["UserName"]);
                        objUsersMaster.Password = Convert.ToString(objReader["Password"]);
                        objUsersMaster.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                        objUsersMaster.RoleID = objReader["RoleId"] != DBNull.Value ? Convert.ToInt32(objReader["RoleId"]) : 0;
                        //  objUsersMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objUsersMaster.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                        objUsersMaster.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                        //objUsersMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //objUsersMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;                     
                        objUsersMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objUsersMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objUsersMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objUsersMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objUsersMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objUsersMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objUsersMaster.PasswordReset = objReader["PasswordReset"] != DBNull.Value ? Convert.ToBoolean(objReader["PasswordReset"]) : true;
                        objUsersMaster.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                        objUsersMaster.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;

                        objUsersMaster.RoleCode = Convert.ToString(objReader["RoleCode"]);
                        objUsersMaster.ManagerOverrideCode = Convert.ToString(objReader["ManagerOverrideCode"]);
                        objUsersMaster.RetailSettingCode = Convert.ToString(objReader["RetailSettingCode"]);
                        objUsersMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);

                        //objUsersMaster.CurrentPassword = Convert.ToString(objReader["CurrentPassword"]);
                        //objUsersMaster.NewPassword = Convert.ToString(objReader["NewPassword"]);
                        //objUsersMaster.ConfirmPassword = Convert.ToString(objReader["ConfirmPassword"]);
                        ResponseData.UsersRecord = objUsersMaster;
                        ResponseData.ResponseDynamicData = objUsersMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Data");
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
            List<UsersSettings> UsersList = new List<UsersSettings>();
            SelectAllUsersRequest RequestData = (SelectAllUsersRequest)RequestObj;
            SelectAllUsersResponse ResponseData = new SelectAllUsersResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                sSql.Append("select UM.*,RM.RoleName,EM.EmployeeName,MO.Name,RS.RetailName from UserMaster UM  with(NoLock)");
                sSql.Append("left outer join RoleMaster RM with(NoLock) on UM.RoleID=RM.ID    ");               
                sSql.Append("left outer join ManagerOverride MO with(NoLock) on UM.ManagerOverrideID=MO.ID  ");
                sSql.Append("left outer join RetailSettings RS with(NoLock) on UM.RetailID=RS.ID  ");
                sSql.Append("left outer join EmployeeMaster EM with(NoLock) on UM.EmployeeID=EM.ID  ");
                if (RequestData.StoreID > 0)
                {
                    sSql.Append("where EM.StoreID=" + RequestData.StoreID);
                    if(RequestData.ShowInActiveRecords == false)
                    {
                        sSql.Append(" and UM.Active='True'");
                    }
                }
                else
                {
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        sSql.Append(" where UM.Active='True'");
                    }
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objUsers = new UsersSettings();
                        objUsers.ID = Convert.ToInt32(objReader["ID"]);


                        objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsers.UserName = Convert.ToString(objReader["UserName"]);
                        objUsers.Password = Convert.ToString(objReader["Password"]);
                        objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                        objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        //objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                        objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                        objUsers.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objUsers.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objUsers.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objUsers.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objUsers.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objUsers.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objUsers.PasswordReset = objReader["PasswordReset"] != DBNull.Value ? Convert.ToBoolean(objReader["PasswordReset"]) : true;
                        objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                        objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;
                        //objUsers.CurrentPassword = Convert.ToString(objReader["CurrentPassword"]);
                        //objUsers.NewPassword = Convert.ToString(objReader["NewPassword"]);
                        //objUsers.ConfirmPassword = Convert.ToString(objReader["ConfirmPassword"]);
                        //objUsers.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        // objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                        objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                        objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objUsers.ManagerOverrideName = Convert.ToString(objReader["Name"]);
                        objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                        UsersList.Add(objUsers);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UsersList = UsersList;
                    ResponseData.ResponseDynamicData = UsersList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Users Data");
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

        public override EasyBizResponse.Masters.LogInResponse.SelectLogInResponse SelectLogIn(EasyBizRequest.Masters.LoginRequest.SelectLogInRequest RequestObj)
        {
            SelectLogInRequest RequestData = (SelectLogInRequest)RequestObj;
            SelectLogInResponse ResponseData = new SelectLogInResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                bool IsLatestVersion = CheckAndUpdateVersionInfo(RequestData);

                if (IsLatestVersion)
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    StringBuilder sbSql = new StringBuilder();

                    string sSql = string.Empty;
                    if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                    {
                        sbSql.Append("select * from UserMaster um ");
                        sbSql.Append("where um.UserName collate Latin1_General_CS_AS='{0}' and um.Password collate Latin1_General_CS_AS ='{1}'");
                        sSql = string.Format(sbSql.ToString(), RequestData.UserName, RequestData.Password);
                    }
                    else
                    {
                        //sbSql.Append("select * from UserMaster um join EmployeeMaster em on um.EmployeeID=em.ID ");
                        //sbSql.Append("where em.StoreID={0} and um.UserName collate Latin1_General_CS_AS='{1}' and um.Password collate Latin1_General_CS_AS ='{2}'");


                        //Join query using id ********************
                        //sbSql.Append("select um.*,em.EmployeeCode,sm.ID as StoreID,sm.StoreCode,sm.ToMailID,sm.CCMailID, cm.ID as CountryID,cm.CountryCode from UserMaster um ");
                        //sbSql.Append("join EmployeeMaster em on um.EmployeeID=em.ID join StoreMaster sm on em.StoreID=sm.ID join CountryMaster cm on cm.ID=sm.CountryID ");
                        //sbSql.Append("where em.StoreID={0} and um.UserName collate Latin1_General_CS_AS='{1}' and um.Password collate Latin1_General_CS_AS ='{2}'");
                        //******************************************
                        sbSql.Append("select um.*,em.EmployeeCode,sm.ID as StoreID,sm.StoreCode,sm.ToMailID,sm.CCMailID,cm.ID as CountryID,cm.CountryCode,cm.CurrencyID,cm.CurrencyCode from UserMaster um ");
                        //sbSql.Append("join EmployeeMaster em on um.EmployeeCode=em.employeecode join StoreMaster sm on em.StoreCode=sm.StoreCode join CountryMaster cm on cm.CountryCode=sm.CountryCode  ");
                        sbSql.Append("join EmployeeMaster em on um.EmployeeCode=em.employeecode join StoreMaster sm on em.StoreID=sm.ID join CountryMaster cm on cm.ID=sm.CountryID  ");
                        sbSql.Append("where em.StoreCode='{0}' and um.UserName collate Latin1_General_CS_AS='{1}' and um.Password collate Latin1_General_CS_AS ='{2}'");


                        sSql = string.Format(sbSql.ToString(), RequestData.FromOrToStoreCode, RequestData.UserName, RequestData.Password);
                    }

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            UsersSettings objUsers = new UsersSettings();
                            objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objUsers.UserName = Convert.ToString(objReader["UserName"]);
                            objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                            objUsers.Password = Convert.ToString(objReader["Password"]);

                            objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                            objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                            objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                            objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                            objUsers.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                            objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                            objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;
                            //objUsers.EnableCashDrawer = objReader["EnableCashDrawer"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableCashDrawer"]) : false;

                            if (RequestData.RequestFrom != Enums.RequestFrom.MainServer)
                            {
                                objUsers.ToMailID = objReader["ToMailID"] != DBNull.Value ? Convert.ToString(objReader["ToMailID"]) : string.Empty;
                                objUsers.CCMailID = objReader["CCMailID"] != DBNull.Value ? Convert.ToString(objReader["CCMailID"]) : string.Empty;

                                objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                                objUsers.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                                objUsers.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                                objUsers.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                                //objUsers.POSTitle = objReader["POSTitle"] != DBNull.Value ? Convert.ToString(objReader["POSTitle"]) : string.Empty;


                                objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                                objUsers.EmployeeCode = objReader["EmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["EmployeeCode"]) : string.Empty;

                                objUsers.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                                objUsers.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : string.Empty;
                                objUsers.IsLoggedStoreCode = objReader["IsLoggedStoreCode"] != DBNull.Value ? Convert.ToString(objReader["IsLoggedStoreCode"]) : string.Empty;
                                objUsers.IsLoggedPosID = objReader["IsLoggedPosID"] != DBNull.Value ? Convert.ToInt32(objReader["IsLoggedPosID"]) : 0;
                            }

                            objUsers.PasswordReset = objReader["PasswordReset"] != DBNull.Value ? Convert.ToBoolean(objReader["PasswordReset"]) : false;
                            objUsers.IsLoggedIn = objReader["IsLoggedIn"] != DBNull.Value ? Convert.ToBoolean(objReader["IsLoggedIn"]) : false;
                           
                            ResponseData.UsersRecord = objUsers;
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;

                            if (RequestData.SourceFrom == "POS")
                            {
                                if (objUsers.IsLoggedIn == false)
                                {
                                    UpdateLoginStatus(objUsers.ID, "True", RequestData.POSID, RequestData.FromOrToStoreCode);
                                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                                }
                                else if (RequestData.CheckLoggedIn == "LogOut" && objUsers.IsLoggedIn == true)
                                {
                                    UpdateLoginStatus(objUsers.ID, "False",0,null);
                                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                                }
                                else
                                {
                                    if (objUsers.IsLoggedStoreCode == RequestData.FromOrToStoreCode && objUsers.IsLoggedPosID == RequestData.POSID)
                                    {
                                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                                    }
                                    else
                                    {
                                        ResponseData.StatusCode = Enums.OpStatusCode.LogInExist;
                                    }
                                }
                            }                            
                        }
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        //ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Data");
                        ResponseData.DisplayMessage = "No User Data Found!..";
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.VersionNotUpdate;
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

        private void UpdateLoginStatus(int UserID, string Status, int POSID, string FromOrToStoreCode)
        {
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Update UserMaster set IsLoggedIn='" + Status + "',IsLoggedPosID='" + POSID + "',IsLoggedStoreCode='" + FromOrToStoreCode + "' where ID=" + UserID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }

        public override SelectLogInResponse SelectUserDetails(SelectLogInRequest RequestObj)
        {
            SelectLogInRequest RequestData = (SelectLogInRequest)RequestObj;
            SelectLogInResponse ResponseData = new SelectLogInResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();

                //sSql.Append("select  distinct a.*, f.Brand_ID,a.StateID,c.CountryName,m.Name,r.RetailName,b.RoleName,d.EmployeeName,d.EmployeeImage,f.StoreName,SGM.StoreGroupName,c.CurrencyID,f.PriceListID,f.StoreGroupID,f.PrintCount,cu.CurrencySymbol,c.DecimalPlaces,c.NearByRoundOff ");
                //sSql.Append("from UserMaster a inner join RoleMaster b on a.RoleID=b.ID inner join EmployeeMaster d on a.EmployeeID=d.ID  inner join CountryMaster c on a.CountryID=c.ID  inner join ManagerOverride m on a.ManagerOverrideID=m.ID inner join RetailSettings r on a.RetailID=r.ID  inner join StoreMaster f on a.StoreID=f.ID and f.CountryID = c.ID ");	
                //sSql.Append("inner join StoreGroupMaster SGM on SGM.ID=f.StoreGroupID ");               
                //sSql.Append("inner join  dbo.CurrencyMaster cu ON cu.ID = c.CurrencyID where UserName collate Latin1_General_CS_AS='" + RequestData.UserName + "' and a.Active='True'");

                //sSql.Append("select  distinct a.*,f.Brand_ID,c.ID as CountryID,c.CountryName, m.Name as ManagerOverrideName, r.RetailName, b.RoleName, d.EmployeeName, d.EmployeeImage,f.ID as StoreID,f.StoreName, SGM.StoreGroupName, ");
                //sSql.Append("c.CurrencyID, f.PriceListID,f.StoreGroupID, f.PrintCount,cu.CurrencySymbol,c.DecimalPlaces,c.NearByRoundOff from UserMaster a inner join RoleMaster b on a.RoleID=b.ID ");
                //sSql.Append("inner join EmployeeMaster d on a.EmployeeID=d.ID inner join CountryMaster c on d.CountryID=c.ID  inner join ManagerOverride m on a.ManagerOverrideID=m.ID ");
                //sSql.Append("inner join RetailSettings r on a.RetailID=r.ID inner join StoreMaster f on d.StoreID=f.ID and d.CountryID = c.ID inner join StoreGroupMaster SGM on SGM.ID=f.StoreGroupID inner join CurrencyMaster cu ON cu.ID = c.CurrencyID ");
                //sSql.Append("where UserName collate Latin1_General_CS_AS='" + RequestData.UserName + "' and a.Active='True'");


                //Using ID join
                //sSql.Append("select  distinct a.*,f.Brand_ID,c.ID as CountryID,c.CountryName,c.CountryCode as CtryCode, m.Name as ManagerOverrideName, r.RetailName, b.RoleName,");
                //sSql.Append("d.EmployeeName,d.EmployeeImage, d.EmployeeCode,f.ID as StoreID,f.StoreName,f.StoreCode, SGM.StoreGroupName, c.CurrencyID, f.PriceListID,");
                //sSql.Append("f.StoreGroupID,f.PrintCount,cu.CurrencySymbol,c.DecimalPlaces, c.NearByRoundOff from UserMaster a inner join RoleMaster b on a.RoleID=b.ID ");
                //sSql.Append("inner join EmployeeMaster d on a.EmployeeID=d.ID inner join CountryMaster c on d.CountryID=c.ID  ");
                //sSql.Append("inner join ManagerOverride m on a.ManagerOverrideID=m.ID inner join RetailSettings r on a.RetailID=r.ID ");
                //sSql.Append("inner join StoreMaster f on d.StoreID=f.ID and d.CountryID = c.ID inner join StoreGroupMaster SGM on SGM.ID=f.StoreGroupID ");


                sSql.Append("select  distinct a.*,f.Brand_ID,c.ID as CountryID,c.CountryName,c.CountryCode as CtryCode,c.POSTitle, m.Name as ManagerOverrideName, r.RetailName, b.RoleName,");
                sSql.Append("d.EmployeeName,d.EmployeeImage, d.EmployeeCode,f.ID as StoreID,f.StoreName,f.StoreCode, SGM.StoreGroupName, c.CurrencyID, f.PriceListID,");
                sSql.Append("f.StoreGroupID,f.PrintCount,f.ExchangePrintCount,f.ReturnPrintCount,cu.CurrencySymbol,cu.CurrencyCode,c.DecimalPlaces, c.NearByRoundOff,c.PromotionRoundOff from UserMaster a inner join RoleMaster b on a.RoleID=b.ID ");
                sSql.Append("inner join EmployeeMaster d on a.employeecode=d.employeecode inner join CountryMaster c on d.countrycode=c.Countrycode   ");
                sSql.Append("inner join ManagerOverride m on a.manageroverridecode=m.code inner join RetailSettings r on a.RetailSettingCode=r.RetailCode ");
                sSql.Append("inner join StoreMaster f on d.StoreCode=f.StoreCode and d.CountryCode = c.CountryCode inner join StoreGroupMaster SGM on SGM.StoreGroupCode=f.StoreGroupCode ");
                sSql.Append("inner join CurrencyMaster cu ON cu.CurrencyCode = c.CurrencyCode where UserName collate Latin1_General_CS_AS='" + RequestData.UserName + "' and a.Active='True'");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        UsersSettings objUsers = new UsersSettings();
                        objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objUsers.BrandID = objReader["Brand_ID"] != DBNull.Value ? Convert.ToString(objReader["Brand_ID"]) : string.Empty;
                        objUsers.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        objUsers.UserName = Convert.ToString(objReader["UserName"]);
                        objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsers.Password = Convert.ToString(objReader["Password"]);
                        objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                        objUsers.ManagerOverrideName = Convert.ToString(objReader["ManagerOverrideName"]);
                        objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                        objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                        objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                        objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                        objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                        objUsers.StoreName = Convert.ToString(objReader["StoreName"]);
                        objUsers.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objUsers.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                        objUsers.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objUsers.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objUsers.EmployeeImage = Convert.ToString(objReader["EmployeeImage"]);

                        objUsers.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                        objUsers.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objUsers.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                        objUsers.PrintCount = objReader["PrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["PrintCount"]) : 1;
                        objUsers.ExchangePrintCount = objReader["ExchangePrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangePrintCount"]) : 1;
                        objUsers.SaleReturnPrintCount = objReader["ReturnPrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnPrintCount"]) : 1;

                        objUsers.CountryCode = objReader["CtryCode"] != DBNull.Value ? Convert.ToString(objReader["CtryCode"]) : string.Empty;
                        objUsers.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objUsers.EmployeeCode = objReader["EmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["EmployeeCode"]) : string.Empty;
                        objUsers.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : string.Empty;
                        objUsers.POSTitle = objReader["POSTitle"] != DBNull.Value ? Convert.ToString(objReader["POSTitle"]) : string.Empty;
                        objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                        objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;

                        objUsers.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;//Convert.ToDecimal("0.50"); //

                        ResponseData.UsersRecord = objUsers;

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Data");
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

        public override UpdateUsersResponse PasswordReset(UpdateUsersRequest RequestObj)
        {
            var RequestData = (UpdateUsersRequest)RequestObj;
            var ResponseData = new UpdateUsersResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("PasswordReset", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                //ID.Direction = ParameterDirection.Input;
                //ID.Value = RequestData.UsersRecord.ID;

                //var UserCode = _CommandObj.Parameters.Add("@UserCode", SqlDbType.NVarChar);
                //UserCode.Direction = ParameterDirection.Input;
                //UserCode.Value = RequestData.UsersRecord.UserCode;


                var UserName = _CommandObj.Parameters.Add("@UserName", SqlDbType.NVarChar);
                UserName.Direction = ParameterDirection.Input;
                UserName.Value = RequestData.UsersRecord.UserName;

                var CurrentPassword = _CommandObj.Parameters.Add("@CurrentPassword", SqlDbType.NVarChar);
                CurrentPassword.Direction = ParameterDirection.Input;
                CurrentPassword.Value = RequestData.UsersRecord.CurrentPassword;

                var ConfirmPassword = _CommandObj.Parameters.Add("@ConfirmPassword", SqlDbType.NVarChar);
                ConfirmPassword.Direction = ParameterDirection.Input;
                ConfirmPassword.Value = RequestData.UsersRecord.ConfirmPassword;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.UsersRecord.SCN;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Users");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Users");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public bool CheckAndUpdateVersionInfo(SelectLogInRequest RequestData)
        {
            bool objBool = false;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string ApplicationType = FSRetailResource.ResourceManager.GetString("ApplicationType");
                string ApplicationVersion = FSRetailResource.ResourceManager.GetString("ApplicationVersion");
                string DBVersion = FSRetailResource.ResourceManager.GetString("DBVersion");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;
                sSql = "Select ID,ApplicationType,ApplicationVersion,DBVersion,VersionUpdateDate from VersionInfo where ApplicationType='{0}'";

                sSql = string.Format(sSql, ApplicationType);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                DataSet objDataSet = new DataSet();
                SqlDataAdapter objAdapter = new SqlDataAdapter(_CommandObj);
                objAdapter.Fill(objDataSet, "VersionInfo");
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    string TempApplicationVersion = objDataSet.Tables[0].Rows[0]["ApplicationVersion"] != DBNull.Value ? Convert.ToString(objDataSet.Tables[0].Rows[0]["ApplicationVersion"]) : string.Empty;
                    string TempDBVersion = objDataSet.Tables[0].Rows[0]["DBVersion"] != DBNull.Value ? Convert.ToString(objDataSet.Tables[0].Rows[0]["DBVersion"]) : string.Empty;
                    DateTime TempVersionUpdateDate = objDataSet.Tables[0].Rows[0]["VersionUpdateDate"] != DBNull.Value ? Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["VersionUpdateDate"]) : DateTime.MinValue;

                    if (TempApplicationVersion.Trim().ToUpper() == ApplicationVersion.Trim().ToUpper() && TempDBVersion.Trim().ToUpper() == DBVersion.Trim().ToUpper())
                    {
                        objBool = true;
                    }
                }
            }
            catch
            {
                objBool = false;
            }
            return objBool;
        }

        public override SelectLogInResponse SelectUserDeatilsfromFingerPrint(SelectLogInByFingerPrintRequest RequestObj)
        {
            SelectLogInByFingerPrintRequest RequestData = (SelectLogInByFingerPrintRequest)RequestObj;
                SelectLogInResponse ResponseData = new SelectLogInResponse();
                SqlDataReader objReader;
                SqlDataAdapter objAdapter = new SqlDataAdapter();

                int UserID = 0;
            
                MsSqlCommon sqlCommon = new MsSqlCommon();
                try
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;


                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                DataTable dt= new DataTable();

                _CommandObj = new SqlCommand("Select A.Employeeid,fingerprint, B.ID UserID from EmployeeFingerPrint A join UserMaster B on A.employeeID = B.EmployeeID where B.Active = 1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objAdapter.SelectCommand = _CommandObj;
                objAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(RequestObj.captureResult.Data, Formats.Fmd.DP_VERIFICATION);

                        //DPUruNet.CompareResult compareResult;
                        //compareResult = Comparison.Compare(resultConversion.Data, 0, Fmd.DeserializeXml(dr["fingerprint"].ToString()), 0);

                        //if (compareResult.Score < (0x7FFFFFFF / (double)100000))
                        //{
                        //    UserID = Convert.ToInt32(dr["UserID"]); // MessageBox.Show("User Valid");
                        //    break;
                        //}
                        //else
                        //{
                        //   // MessageBox.Show("User InValid");
                        //};
                    }
                }



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    StringBuilder sSql = new StringBuilder();



                    sSql.Append("select  distinct a.*,f.Brand_ID,c.ID as CountryID,c.CountryName,c.CountryCode as CtryCode,c.POSTitle, m.Name as ManagerOverrideName, r.RetailName, b.RoleName,");
                    sSql.Append("d.EmployeeName,d.EmployeeImage, d.EmployeeCode,f.ID as StoreID,f.StoreName,f.StoreCode, SGM.StoreGroupName, c.CurrencyID, f.PriceListID,");
                    sSql.Append("f.StoreGroupID,f.PrintCount,f.ExchangePrintCount,f.ReturnPrintCount,cu.CurrencySymbol,cu.CurrencyCode,c.DecimalPlaces, c.NearByRoundOff,c.PromotionRoundOff from UserMaster a inner join RoleMaster b on a.RoleID=b.ID ");
                    sSql.Append("inner join EmployeeMaster d on a.employeecode=d.employeecode inner join CountryMaster c on d.countrycode=c.Countrycode   ");
                    sSql.Append("inner join ManagerOverride m on a.manageroverridecode=m.code inner join RetailSettings r on a.RetailSettingCode=r.RetailCode ");
                    sSql.Append("inner join StoreMaster f on d.StoreCode=f.StoreCode and d.CountryCode = c.CountryCode inner join StoreGroupMaster SGM on SGM.StoreGroupCode=f.StoreGroupCode ");
                    sSql.Append("inner join CurrencyMaster cu ON cu.CurrencyCode = c.CurrencyCode where a.ID = " + UserID + " and a.Active='True'");

                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            UsersSettings objUsers = new UsersSettings();
                            objUsers.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                            objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objUsers.BrandID = objReader["Brand_ID"] != DBNull.Value ? Convert.ToString(objReader["Brand_ID"]) : string.Empty;
                            objUsers.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                            objUsers.UserName = Convert.ToString(objReader["UserName"]);
                            objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                            objUsers.Password = Convert.ToString(objReader["Password"]);
                            objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                            objUsers.ManagerOverrideName = Convert.ToString(objReader["ManagerOverrideName"]);
                            objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                            objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                            objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                            objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                            objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                            objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                            objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                            objUsers.StoreName = Convert.ToString(objReader["StoreName"]);
                            objUsers.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                            objUsers.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                            objUsers.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                            objUsers.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                            objUsers.EmployeeImage = Convert.ToString(objReader["EmployeeImage"]);

                            objUsers.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                            objUsers.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objUsers.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                            objUsers.PrintCount = objReader["PrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["PrintCount"]) : 1;
                            objUsers.ExchangePrintCount = objReader["ExchangePrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangePrintCount"]) : 1;
                            objUsers.SaleReturnPrintCount = objReader["ReturnPrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnPrintCount"]) : 1;

                            objUsers.CountryCode = objReader["CtryCode"] != DBNull.Value ? Convert.ToString(objReader["CtryCode"]) : string.Empty;
                            objUsers.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                            objUsers.EmployeeCode = objReader["EmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["EmployeeCode"]) : string.Empty;
                            objUsers.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : string.Empty;
                            objUsers.POSTitle = objReader["POSTitle"] != DBNull.Value ? Convert.ToString(objReader["POSTitle"]) : string.Empty;
                            objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                            objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;

                            objUsers.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;//Convert.ToDecimal("0.50"); //

                            ResponseData.UsersRecord = objUsers;

                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        }
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Data");
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

        public override SelectLogInResponse SelectCommonLogIn(SelectCommonLoginRequest RequestObj)
        {
            SelectCommonLoginRequest RequestData = (SelectCommonLoginRequest)RequestObj;
            SelectLogInResponse ResponseData = new SelectLogInResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                // bool IsLatestVersion = CheckAndUpdateVersionInfo(RequestData);

                //if (IsLatestVersion)
                //{
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sbSql = new StringBuilder();

                string sSql = string.Empty;

                sbSql.Append("select * from UserMaster um ");
                sbSql.Append("where um.UserName collate Latin1_General_CS_AS='{0}' and um.Password collate Latin1_General_CS_AS ='{1}'");
                sSql = string.Format(sbSql.ToString(), RequestData.UserName, RequestData.Password);


                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        UsersSettings objUsers = new UsersSettings();
                        objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objUsers.UserName = Convert.ToString(objReader["UserName"]);
                        objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsers.Password = Convert.ToString(objReader["Password"]);

             

                        ResponseData.UsersRecord = objUsers;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Data");
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

        public override SelectUserDetailsResponse SelectCommonUserDetailsInfo(SelectCommonLoginRequest RequestData)
        {
            {
                //var RequestData = BaseRequestType;
                SelectUserDetailsResponse ResponseData = new SelectUserDetailsResponse();
                SqlDataReader objReader;
                SqlDataReader objReader1;
                MsSqlCommon sqlCommon = new MsSqlCommon();
                try
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    StringBuilder sSql = new StringBuilder();


                    sSql.Append("select distinct a.*,c.ID as CountryID,c.CountryName,c.CountryCode,c.POSTitle, m.Name as ManagerOverrideName, r.RetailName, b.RoleName, ");
                    sSql.Append("d.EmployeeName,d.EmployeeImage, d.EmployeeCode,f.ID as StoreID,f.StoreName,f.StoreCode, SGM.StoreGroupName,t.ID as TaxID,t.TaxCode,t.TaxPercentage, c.CurrencyID, f.PriceListID,p.PriceListCode,p.PriceListName, ");
                    sSql.Append("f.StoreGroupID,f.PrintCount,f.ExchangePrintCount,f.ReturnPrintCount,cu.CurrencySymbol,cu.CurrencyCode,c.DecimalPlaces, c.NearByRoundOff, ");
                    sSql.Append("c.PromotionRoundOff,r.AllowSalesForNegativeStock,r.AllowSalesForZeroPrice from UserMaster a inner join RoleMaster b on a.RoleID = b.ID inner join EmployeeMaster d on a.employeecode = d.employeecode ");
                    sSql.Append(" inner join CountryMaster c on d.countrycode = c.Countrycode inner join TaxMaster T on t.ID = c.TaxID ");
                    sSql.Append("inner join ManagerOverride m on a.manageroverridecode = m.code inner join RetailSettings r on a.RetailSettingCode = r.RetailCode ");
                    sSql.Append("inner join StoreMaster f on d.StoreCode = f.StoreCode and d.CountryCode = c.CountryCode join pricelistmaster p on p.ID = f.PriceListID ");
                    sSql.Append("inner join StoreGroupMaster SGM on SGM.StoreGroupCode = f.StoreGroupCode inner join CurrencyMaster cu ON cu.CurrencyCode = c.CurrencyCode ");
                    //sSql.Append(" where UserName collate Latin1_General_CS_AS = 'ALADMIN' and a.Active = 'True' ");
                    sSql.Append(" where a.ID = "+ RequestData.UserID +" and a.Active = 'True' ");
                    //2218

                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            UserDetailsInfo objUsers = new UserDetailsInfo();
                            objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            //objUsers.BrandID = objReader["Brand_ID"] != DBNull.Value ? Convert.ToString(objReader["Brand_ID"]) : string.Empty;
                            //objUsers.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                            objUsers.UserName = Convert.ToString(objReader["UserName"]);
                            objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                            objUsers.Password = Convert.ToString(objReader["Password"]);
                            objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                            objUsers.ManagerOverrideName = Convert.ToString(objReader["ManagerOverrideName"]);
                            objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                            objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                            objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                            objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                            objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                            objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                            objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                            objUsers.StoreName = Convert.ToString(objReader["StoreName"]);
                            objUsers.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                            objUsers.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                            objUsers.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                            objUsers.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                            objUsers.PriceListName = Convert.ToString(objReader["PriceListName"]);
                            objUsers.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                            objUsers.TaxCode = Convert.ToString(objReader["TaxCode"]);
                            objUsers.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);
                            objUsers.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                            objUsers.EmployeeImage = Convert.ToString(objReader["EmployeeImage"]);

                            objUsers.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                            objUsers.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objUsers.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                            objUsers.PrintCount = objReader["PrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["PrintCount"]) : 1;
                            objUsers.ExchangePrintCount = objReader["ExchangePrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangePrintCount"]) : 1;
                            objUsers.SaleReturnPrintCount = objReader["ReturnPrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnPrintCount"]) : 1;

                            objUsers.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                            objUsers.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                            objUsers.EmployeeCode = objReader["EmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["EmployeeCode"]) : string.Empty;
                            objUsers.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : string.Empty;
                            objUsers.POSTitle = objReader["POSTitle"] != DBNull.Value ? Convert.ToString(objReader["POSTitle"]) : string.Empty;
                            objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                            objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;

                            objUsers.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;
                            //Api
                            //SqlConnection con = new SqlConnection();
                            ////con = RequestData.ConnectionString;
                            //sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                            ////con.Open();
                            //var sql = "Select pm.PromotionCode,pm.PromotionName from promotionsmaster pm   inner join PromotionsWithStoreDetails psd on pm.ID = psd.PromotionHeaderID where psd.code ='" + objUsers.StoreCode + "' ";
                            //SqlCommand cmd;
                            //cmd = new SqlCommand(sql.ToString(), con);
                            //cmd.CommandType = CommandType.Text;
                            //objReader1 = cmd.ExecuteReader();
                            //if (objReader1.HasRows)
                            //{
                            //    List<LoginPromoDetails> ms = new List<LoginPromoDetails>();
                            //    while (objReader1.Read())
                            //    {

                            //        LoginPromoDetails objpromotion = new LoginPromoDetails();
                            //        objpromotion.PromotionCode = objReader1["PromotionCode"] != DBNull.Value ? Convert.ToString(objReader1["PromotionCode"]) : string.Empty;
                            //        objpromotion.PromotionName = objReader1["PromotionName"] != DBNull.Value ? Convert.ToString(objReader1["PromotionName"]) : string.Empty;
                            //        ms.Add(objpromotion);
                            //        objUsers.PromoDetails = ms;
                            //    }
                            //}
                            //con.Close();

                            ResponseData.UserInfoRecord = objUsers;

                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        }
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Details Info");
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

        public override SelectUserDetailsResponse API_SelectCommonUserDetailsInfo(SelectCommonLoginRequest RequestData)
        {
            {
                //var RequestData = BaseRequestType;
                SelectUserDetailsResponse ResponseData = new SelectUserDetailsResponse();
                SqlDataReader objReader;
                SqlDataReader objReader1;
                MsSqlCommon sqlCommon = new MsSqlCommon();
                try
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    StringBuilder sSql = new StringBuilder();

                    _CommandObj = new SqlCommand("API_GetUserLoginDetails", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                    var UserID = _CommandObj.Parameters.Add("@UserID", SqlDbType.BigInt);
                    UserID.Direction = ParameterDirection.Input;
                    UserID.Value = RequestData.UserID;

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            UserDetailsInfo objUsers = new UserDetailsInfo();
                            objUsers.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            //objUsers.BrandID = objReader["Brand_ID"] != DBNull.Value ? Convert.ToString(objReader["Brand_ID"]) : string.Empty;
                            objUsers.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                            objUsers.UserName = Convert.ToString(objReader["UserName"]);
                            objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                            //objUsers.Password = Convert.ToString(objReader["Password"]);
                            objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                            objUsers.ManagerOverrideName = Convert.ToString(objReader["ManagerOverrideName"]);
                            objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                            objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                            objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                            objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                            objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                            objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                            objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                            objUsers.StoreName = Convert.ToString(objReader["StoreName"]);
                            objUsers.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                            objUsers.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                            objUsers.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                            objUsers.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                            objUsers.PriceListName = Convert.ToString(objReader["PriceListName"]);
                            objUsers.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                            objUsers.TaxCode = Convert.ToString(objReader["TaxCode"]);
                            objUsers.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);
                            objUsers.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                            objUsers.EmployeeImage = Convert.ToString(objReader["EmployeeImage"]);

                            objUsers.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                            objUsers.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objUsers.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                            objUsers.PrintCount = objReader["PrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["PrintCount"]) : 1;
                            objUsers.ExchangePrintCount = objReader["ExchangePrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangePrintCount"]) : 1;
                            objUsers.SaleReturnPrintCount = objReader["ReturnPrintCount"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnPrintCount"]) : 1;

                            objUsers.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                            objUsers.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                            objUsers.EmployeeCode = objReader["EmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["EmployeeCode"]) : string.Empty;
                            objUsers.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : string.Empty;
                            objUsers.POSTitle = objReader["POSTitle"] != DBNull.Value ? Convert.ToString(objReader["POSTitle"]) : string.Empty;
                            objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                            objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;

                            objUsers.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;
                            objUsers.StoreFooter= objReader["StoreFooter"] != DBNull.Value ? Convert.ToString(objReader["StoreFooter"]) : string.Empty;
                            objUsers.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                            ResponseData.UserInfoRecord = objUsers;

                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        }
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Details Info");
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

        public override SelectAllUsersResponse API_SelectALL(SelectAllUsersRequest requestData)
        {
            List<UsersSettings> UsersList = new List<UsersSettings>();
            SelectAllUsersRequest RequestData = (SelectAllUsersRequest)requestData;
            SelectAllUsersResponse ResponseData = new SelectAllUsersResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sSql = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //var sSql = new StringBuilder();
                //sSql.Append("select UM.*,RM.RoleName,EM.EmployeeName,MO.Name,RS.RetailName from UserMaster UM  with(NoLock)");
                //sSql.Append("left outer join RoleMaster RM with(NoLock) on UM.RoleID=RM.ID    ");
                //sSql.Append("left outer join ManagerOverride MO with(NoLock) on UM.ManagerOverrideID=MO.ID  ");
                //sSql.Append("left outer join RetailSettings RS with(NoLock) on UM.RetailID=RS.ID  ");
                //sSql.Append("left outer join EmployeeMaster EM with(NoLock) on UM.EmployeeID=EM.ID  ");

                sSql = "Select UM.ID, UM.UserCode, UM.UserName, EM.EmployeeName, RM.RoleName, UM.Active, RC.TOTAL_CNT [RecordCount] " +
                   "from UserMaster UM  with(NoLock)" +
                   " left outer join RoleMaster RM with(NoLock) on UM.RoleID=RM.ID" +
                   " left outer join EmployeeMaster EM with(NoLock) on UM.EmployeeID=EM.ID " +
                   "LEFT JOIN(Select  count(UM1.ID) As TOTAL_CNT From UserMaster UM1 with(NoLock) " +
                   " left outer join RoleMaster RM1 with(NoLock) on UM1.RoleID=RM1.ID" +
                   " left outer join EmployeeMaster EM1 with(NoLock) on UM1.EmployeeID=EM1.ID " +
                   " where UM1.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or UM1.UserCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or UM1.UserName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.EmployeeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or RM1.RoleName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                  " where UM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or UM.UserCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or UM.UserName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.EmployeeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or RM.RoleName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by UM.ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                //if (RequestData.StoreID > 0)
                //{
                //    sSql.Append("where EM.StoreID=" + RequestData.StoreID);
                //    if (RequestData.ShowInActiveRecords == false)
                //    {
                //        sSql.Append(" and UM.Active='True'");
                //    }
                //}
                //else
                //{
                //    if (RequestData.ShowInActiveRecords == false)
                //    {
                //        sSql.Append(" where UM.Active='True'");
                //    }
                //}
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objUsers = new UsersSettings();
                        objUsers.ID = Convert.ToInt32(objReader["ID"]);


                        objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsers.UserName = Convert.ToString(objReader["UserName"]);
                        //objUsers.Password = Convert.ToString(objReader["Password"]);
                        //objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                        //objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        ////objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                        //objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                        //objUsers.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objUsers.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objUsers.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objUsers.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objUsers.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objUsers.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objUsers.PasswordReset = objReader["PasswordReset"] != DBNull.Value ? Convert.ToBoolean(objReader["PasswordReset"]) : true;
                        //objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                        //objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;
                        //objUsers.CurrentPassword = Convert.ToString(objReader["CurrentPassword"]);
                        //objUsers.NewPassword = Convert.ToString(objReader["NewPassword"]);
                        //objUsers.ConfirmPassword = Convert.ToString(objReader["ConfirmPassword"]);
                        //objUsers.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        // objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                        objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                        objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        //objUsers.ManagerOverrideName = Convert.ToString(objReader["Name"]);
                        //objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                        UsersList.Add(objUsers);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UsersList = UsersList;
                    //ResponseData.ResponseDynamicData = UsersList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Users Data");
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

        public override SelectAllUsersResponse API_SelectRecordInStoreID(SelectAllUsersRequest requestData)
        {
            List<UsersSettings> UsersList = new List<UsersSettings>();
            SelectAllUsersRequest RequestData = (SelectAllUsersRequest)requestData;
            SelectAllUsersResponse ResponseData = new SelectAllUsersResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sSql = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //var sSql = new StringBuilder();
                //sSql.Append("select UM.*,RM.RoleName,EM.EmployeeName,MO.Name,RS.RetailName from UserMaster UM  with(NoLock)");
                //sSql.Append("left outer join RoleMaster RM with(NoLock) on UM.RoleID=RM.ID    ");
                //sSql.Append("left outer join ManagerOverride MO with(NoLock) on UM.ManagerOverrideID=MO.ID  ");
                //sSql.Append("left outer join RetailSettings RS with(NoLock) on UM.RetailID=RS.ID  ");
                //sSql.Append("left outer join EmployeeMaster EM with(NoLock) on UM.EmployeeID=EM.ID  ");

                sSql = "Select UM.ID, UM.UserCode, UM.UserName " +
                   "from UserMaster UM  with(NoLock)" +
                   " join EmployeeMaster EM with(NoLock) on UM.EmployeeID=EM.ID" +
                   " where UM.Active = " + 1 + " " +
                       "and EM.StoreID = "+RequestData.StoreID + "";/*
                       "or UM.UserCode = isnull('" + RequestData.SearchString + "','') " +*/
      ;

                //if (RequestData.StoreID > 0)
                //{
                //    sSql.Append("where EM.StoreID=" + RequestData.StoreID);
                //    if (RequestData.ShowInActiveRecords == false)
                //    {
                //        sSql.Append(" and UM.Active='True'");
                //    }
                //}
                //else
                //{
                //    if (RequestData.ShowInActiveRecords == false)
                //    {
                //        sSql.Append(" where UM.Active='True'");
                //    }
                //}
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objUsers = new UsersSettings();
                        objUsers.ID = Convert.ToInt32(objReader["ID"]);


                        objUsers.UserCode = Convert.ToString(objReader["UserCode"]);
                        objUsers.UserName = Convert.ToString(objReader["UserName"]);
                        //objUsers.Password = Convert.ToString(objReader["Password"]);
                        //objUsers.EmployeeID = objReader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["EmployeeID"]) : 0;
                        //objUsers.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        ////objUsers.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objUsers.ManagerOverrideID = objReader["ManagerOverrideID"] != DBNull.Value ? Convert.ToInt32(objReader["ManagerOverrideID"]) : 0;
                        //objUsers.RetailID = objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) : 0;
                        //objUsers.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objUsers.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objUsers.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objUsers.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objUsers.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objUsers.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objUsers.PasswordReset = objReader["PasswordReset"] != DBNull.Value ? Convert.ToBoolean(objReader["PasswordReset"]) : true;
                        //objUsers.AllowStockEdit = objReader["AllowStockEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowStockEdit"]) : false;
                        //objUsers.MobileUser = objReader["MobileUser"] != DBNull.Value ? Convert.ToBoolean(objReader["MobileUser"]) : false;
                        //objUsers.CurrentPassword = Convert.ToString(objReader["CurrentPassword"]);
                        //objUsers.NewPassword = Convert.ToString(objReader["NewPassword"]);
                        //objUsers.ConfirmPassword = Convert.ToString(objReader["ConfirmPassword"]);
                        //objUsers.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        // objUsers.CountryName = Convert.ToString(objReader["CountryName"]);
                        //objUsers.RoleName = Convert.ToString(objReader["RoleName"]);
                        //objUsers.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        //objUsers.ManagerOverrideName = Convert.ToString(objReader["Name"]);
                        //objUsers.RetailName = Convert.ToString(objReader["RetailName"]);
                        UsersList.Add(objUsers);
                        //ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UsersList = UsersList;
                    //ResponseData.ResponseDynamicData = UsersList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Users Data");
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
