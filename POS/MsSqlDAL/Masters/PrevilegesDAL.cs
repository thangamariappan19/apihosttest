using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
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
    public class PrevilegesDAL : BasePrevilegesDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.Masters.PrevilegesResponse.SelectRoleLookupResponse SelectRoleLookUp(EasyBizRequest.Masters.PrevilegesRequest.SelectRoleLookupRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePrevilegesRequestt)RequestObj;
            var ResponseData = new SavePrevilegesResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertUserPrivilages", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter RoleID = _CommandObj.Parameters.Add("@RoleID", SqlDbType.Int);
                RoleID.Direction = ParameterDirection.Input;
                RoleID.Value = RequestData.UserPrivilagesData.RoleID;

                SqlParameter ScreenName = _CommandObj.Parameters.Add("@ScreenName", SqlDbType.NVarChar);
                ScreenName.Direction = ParameterDirection.Input;
                ScreenName.Value = RequestData.UserPrivilagesData.ScreenName;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.UserPrivilagesData.CreateBy;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.UserPrivilagesData.UpdateBy;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;


                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "User Privilages");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "User Privilages");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "User Privilages");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "User Privilages");
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
            UserPrivilagesTypes MASUserID = new UserPrivilagesTypes();
            SelectByUserIDPrivilagesRequest RequestData = (SelectByUserIDPrivilagesRequest)RequestObj;
            SelectByUserIDPrivilagesResponse ResponseData = new SelectByUserIDPrivilagesResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from UserPrivilages with(NoLock) where RoleID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        UserPrivilagesTypes objMASUserIDPrivilages = new UserPrivilagesTypes();
                        objMASUserIDPrivilages.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objMASUserIDPrivilages.RoleID = Convert.ToInt32(objReader["RoleID"] != DBNull.Value ? Convert.ToDecimal(objReader["RoleID"]) : 0);
                        objMASUserIDPrivilages.ScreenName = Convert.ToString(objReader["ScreenName"] != DBNull.Value ? Convert.ToString(objReader["ScreenName"]) : string.Empty);
                        objMASUserIDPrivilages.IsActive = objReader["IsActive"] != DBNull.Value ? Convert.ToBoolean(objReader["IsActive"]) : true;
                        ResponseData.MASUserPrivilagesRecord = objMASUserIDPrivilages;

                        ResponseData.ResponseDynamicData = objMASUserIDPrivilages;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Privilages");
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
            var UserPrivilagesTypesList = new List<UserPrivilagesTypes>();

            var RequestData = new SelectAllPrevilegesRequest();
            var ResponseData = new SelectAllPrevilegesResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from UserPrivilages with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var UserPrivilagesTypesMaster = new UserPrivilagesTypes();

                        UserPrivilagesTypesMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        UserPrivilagesTypesMaster.RoleID = Convert.ToInt32(objReader["RoleID"] != DBNull.Value ? Convert.ToDecimal(objReader["RoleID"]) : 0);
                        UserPrivilagesTypesMaster.ScreenName = Convert.ToString(objReader["ScreenName"] != DBNull.Value ? Convert.ToString(objReader["ScreenName"]) : string.Empty);
                        UserPrivilagesTypesMaster.IsActive = objReader["IsActive"] != DBNull.Value ? Convert.ToBoolean(objReader["IsActive"]) : true;
                        UserPrivilagesTypesList.Add(UserPrivilagesTypesMaster);                      

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UserPrivilagesTypesList = UserPrivilagesTypesList;
                    ResponseData.ResponseDynamicData = UserPrivilagesTypesList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        

        public override GetScreenNamesResponse SelectPOSScreenNameLookUp(GetScreenNamesRequest ObjRequest)
        {
            List<POSScreenTypes> POSScreenTypesList = new List<POSScreenTypes>();
            GetScreenNamesRequest RequestData = (GetScreenNamesRequest)ObjRequest;
            GetScreenNamesResponse ResponseData = new GetScreenNamesResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from PosScreens with(NoLock) where Active=1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        POSScreenTypes objPOSScreenTypes = new POSScreenTypes();
                        objPOSScreenTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPOSScreenTypes.Name = Convert.ToString(objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty);
                        objPOSScreenTypes.UId = Convert.ToString(objReader["UId"] != DBNull.Value ? Convert.ToString(objReader["UId"]) : string.Empty);
                        objPOSScreenTypes.IsBackOffice = Convert.ToBoolean(objReader["IsBackOffice"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBackOffice"]) : true);
                        objPOSScreenTypes.IsStore = Convert.ToBoolean(objReader["IsStore"] != DBNull.Value ? Convert.ToBoolean(objReader["IsStore"]) : true);
                        POSScreenTypesList.Add(objPOSScreenTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.POSScreenTypesList = POSScreenTypesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "User Privilages");
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
        public override SelectPrevilegesLookUpResponse SelectUserPrivilagesLookUp(SelectPrevilegesLookUpRequest ObjRequest)
        {
            var UserPrivilagesTypesList = new List<UserPrivilagesTypes>();
            var RequestData = (SelectPrevilegesLookUpRequest)ObjRequest;
            var ResponseData = new SelectPrevilegesLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "select * from UserPrivilages where RoleID='" + RequestData.RoleID + "'";
           
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objUserPrivilagesTypes = new UserPrivilagesTypes();
                        objUserPrivilagesTypes.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        objUserPrivilagesTypes.IsActive = objReader["RoleID"] != DBNull.Value ? Convert.ToBoolean(objReader["IsActive"]) : true;
                        objUserPrivilagesTypes.ScreenName = Convert.ToString(objReader["ScreenName"]);
                        UserPrivilagesTypesList.Add(objUserPrivilagesTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.UserPrivilagesTypesList = UserPrivilagesTypesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "UserPrivilagesTypes");
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
