using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.StateMasterResponse;
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
    public class StateMasterDAL : BaseStateMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
       
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStateRequest)RequestObj;
            var ResponseData = new SaveStateResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertStateMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var StateID = _CommandObj.Parameters.Add("@StateID", SqlDbType.Int);
                StateID.Direction = ParameterDirection.Input;
                StateID.Value = RequestData.StateRecord.ID;
              
                var StateCode = _CommandObj.Parameters.Add("@StateCode", SqlDbType.NVarChar);
                StateCode.Direction = ParameterDirection.Input;
                StateCode.Value = RequestData.StateRecord.StateCode;

                var StateName = _CommandObj.Parameters.Add("@StateName", SqlDbType.NVarChar);
                StateName.Direction = ParameterDirection.Input;
                StateName.Value = RequestData.StateRecord.StateName;

                var CountryName = _CommandObj.Parameters.Add("@CountryName", SqlDbType.NVarChar);
                CountryName.Direction = ParameterDirection.Input;
                CountryName.Value = RequestData.StateRecord.CountryName;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.StateRecord.CountryID;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StateRecord.CreateBy;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StateRecord.Remarks;

                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StateRecord.Active);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "State Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "State Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "State Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "State Master");
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
            var RequestData = (UpdateStateRequest)RequestObj;
            var ResponseData = new UpdateStateResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateStateMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StateRecord.ID;

                var StateCode = _CommandObj.Parameters.Add("@StateCode", SqlDbType.NVarChar);
                StateCode.Direction = ParameterDirection.Input;
                StateCode.Value = RequestData.StateRecord.StateCode;

                var StateName = _CommandObj.Parameters.Add("@StateName", SqlDbType.NVarChar);
                StateName.Direction = ParameterDirection.Input;
                StateName.Value = RequestData.StateRecord.StateName;

                var CountryName = _CommandObj.Parameters.Add("@CountryName", SqlDbType.NVarChar);
                CountryName.Direction = ParameterDirection.Input;
                CountryName.Value = RequestData.StateRecord.CountryName;
                
                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.StateRecord.CountryID;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.StateRecord.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.StateRecord.SCN;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StateRecord.Remarks;


                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StateRecord.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "State Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "State Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "State Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "State Master");
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
            var StateMasterRecord = new StateMaster();
            var RequestData = (DeleteStateRequest)RequestObj;
            var ResponseData = new DeleteStateResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from StateMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "State Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "State Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StateMasterRecord = new StateMaster();
            var RequestData = (SelectByStateIDRequest)RequestObj;
            var ResponseData = new SelectByStateIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from StateMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStateMaster = new StateMaster();
                        objStateMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        objStateMaster.StateCode = Convert.ToString(objReader["StateCode"]);
                        objStateMaster.StateName = Convert.ToString(objReader["StateName"]);
                        objStateMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objStateMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objStateMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStateMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStateMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStateMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStateMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStateMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStateMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        ResponseData.StateMasterRecord = objStateMaster;
                        ResponseData.ResponseDynamicData = objStateMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "State Master");
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
           var StateList = new List<StateMaster>();
            var RequestData = (SelectAllStateRequest)RequestObj;
            var ResponseData = new SelectAllStateResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select * from StateMaster with(NoLock)";

               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStateMaster = new StateMaster();
                        objStateMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStateMaster.StateCode = Convert.ToString(objReader["StateCode"]);
                        objStateMaster.StateName = Convert.ToString(objReader["StateName"]);
                        objStateMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objStateMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objStateMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStateMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStateMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStateMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStateMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStateMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStateMaster.Remarks = Convert.ToString(objReader["Remarks"]);

                        StateList.Add(objStateMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.stateMasterList = StateList;
                    ResponseData.ResponseDynamicData = StateList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StateMaster");
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
        public override SelectAllStateResponse API_SelectAll(SelectAllStateRequest objRequest)
        {
            var StateList = new List<StateMaster>();
            var RequestData = (SelectAllStateRequest)objRequest;
            var ResponseData = new SelectAllStateResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

               // sQuery = "Select * from StateMaster with(NoLock)";
                string sSql = "select ID,StateCode,StateName,CountryName,Active,Remarks, RC.TOTAL_CNT [RecordCount] " +
                   "from StateMaster " +
                    "LEFT JOIN(Select count(SM2.ID) As TOTAL_CNT From StateMaster SM2 " +
                    "where SM2.Active= " + RequestData.IsActive + "   and (isnull('" + RequestData.SearchString + "', '') = ''"+
                    "or SM2.StateCode like isnull('%" + RequestData.SearchString + "%', '') " +
                    "or SM2.StateName like isnull('%" + RequestData.SearchString + "%', '') " +
                    "or SM2.CountryName like isnull('%" + RequestData.SearchString + "%', '') " +
                    "or SM2.Remarks like isnull('%" + RequestData.SearchString + "%', ''))) As RC ON 1 = 1 " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or StateCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or StateName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or CountryName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
                   "order by ID asc " +
                   "offset " + RequestData.Offset + " rows " +
                   "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStateMaster = new StateMaster();
                        objStateMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStateMaster.StateCode = Convert.ToString(objReader["StateCode"]);
                        objStateMaster.StateName = Convert.ToString(objReader["StateName"]);
                        objStateMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        //objStateMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objStateMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStateMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStateMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStateMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStateMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStateMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStateMaster.Remarks = Convert.ToString(objReader["Remarks"]);

                        StateList.Add(objStateMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.stateMasterList = StateList;
                    ResponseData.ResponseDynamicData = StateList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StateMaster");
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

        public override SelectStateLookUpResponse SelectStateLookUp(SelectStateLookUpRequest ObjRequest)
        {
            var StateLookUpList = new List<StateMaster>();
            var RequestData = (SelectStateLookUpRequest)ObjRequest;
            var ResponseData = new SelectStateLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[StateName],StateCode from StateMaster with(NoLock) where Active='True' and CountryId ='" + RequestData.CountryID + "'";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStateMaster = new StateMaster();
                        objStateMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStateMaster.StateName = Convert.ToString(objReader["StateName"]);
                        objStateMaster.StateCode = Convert.ToString(objReader["StateCode"]);
                        StateLookUpList.Add(objStateMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StateMasterList = StateLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "State Master");
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

        public override SelectStateAloneLookUPResponse SelectStateAloneLookup(SelectStateAloneLookUPRequest objRequest)
        {
            var StateAloneLookUpList = new List<StateMaster>();
            var RequestData = (SelectStateAloneLookUPRequest)objRequest;
            var ResponseData = new SelectStateAloneLookUPResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,[StateName],StateCode from StateMaster with(NoLock) where Active='True'";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStateMaster = new StateMaster();
                        objStateMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStateMaster.StateName = Convert.ToString(objReader["StateName"]);
                        objStateMaster.StateCode = Convert.ToString(objReader["StateCode"]);
                        StateAloneLookUpList.Add(objStateMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StateMasterList = StateAloneLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "State Master");
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
