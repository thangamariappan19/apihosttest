using CommonRoutines;
using EasyBizAbsDAL.SyncSettings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizRequest;
using EasyBizRequest.Common;
using EasyBizRequest.SyncSettings;
using EasyBizResponse;
using EasyBizResponse.Common;
using EasyBizResponse.SyncSettings;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.SyncSettings
{

    public class SyncSettingsDAL : BaseSyncSettingsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override GetClientSyncFailedRecordsResponse GetClientSyncFailedRecords(GetClientSyncFailedRecordsRequest RequestObj)
        {
            var SyncList = new List<ClientSyncFailed>();
            var RequestData = (GetClientSyncFailedRecordsRequest)RequestObj;
            var ResponseData = new GetClientSyncFailedRecordsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select csf.*,st.SyncType as SyncTypeName,dt.DocumentName as DocumentTypeName,pm.ID as ProcessMode,pm.ProcessType as ProcessModeName from ClientSyncFailed csf ");
                sSql.Append("join SyncTypes st on csf.SyncTypeID=st.ID join DocumentType dt on csf.DocumentTypeID=dt.ID join ProcessMode pm on csf.ProcessModeID=pm.ID ");
                sSql.Append("where csf.SyncStatus='" + RequestData.SyncStatus + "'");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objFailedStoreSyncData = new ClientSyncFailed();
                        objFailedStoreSyncData.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objFailedStoreSyncData.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objFailedStoreSyncData.SyncTypeID = objReader["SyncTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["SyncTypeID"]) : 0;
                        objFailedStoreSyncData.SyncTypeName = objReader["SyncTypeName"] != DBNull.Value ? Convert.ToString(objReader["SyncTypeName"]) : string.Empty;

                        objFailedStoreSyncData.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        objFailedStoreSyncData.DocumentTypeName = objReader["DocumentTypeName"] != DBNull.Value ? Convert.ToString(objReader["DocumentTypeName"]) : string.Empty;
                        objFailedStoreSyncData.DocumentIDs = Convert.ToString(objReader["DocumentIDs"]);
                        objFailedStoreSyncData.DocumentNos = Convert.ToString(objReader["DocumentNos"]);

                        objFailedStoreSyncData.ProcessModeID = objReader["ProcessMode"] != DBNull.Value ? Convert.ToInt32(objReader["ProcessMode"]) : 0;
                        objFailedStoreSyncData.ProcessModeName = objReader["ProcessModeName"] != DBNull.Value ? Convert.ToString(objReader["ProcessModeName"]) : string.Empty;

                        objFailedStoreSyncData.BLLName = objReader["BLLName"] != DBNull.Value ? Convert.ToString(objReader["BLLName"]) : string.Empty;
                        objFailedStoreSyncData.MethodName = objReader["MethodName"] != DBNull.Value ? Convert.ToString(objReader["MethodName"]) : string.Empty; ;
                        objFailedStoreSyncData.ExceptionMessage = objReader["ExceptionMsg"] != DBNull.Value ? Convert.ToString(objReader["ExceptionMsg"]) : string.Empty;
                        objFailedStoreSyncData.SyncStatus = objReader["SyncStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["SyncStatus"]) : true;
                        objFailedStoreSyncData.ProcessTime = objReader["ProcessTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ProcessTime"]) : DateTime.Now;
                        SyncList.Add(objFailedStoreSyncData);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SyncList = SyncList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sync List");
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
        public List<DBConnection> DBConnectionList(int BaseStoreID, string BaseStoreIDs, Enums.SyncMode SyncMode)
        {
            var ConnectionList = new List<DBConnection>();

            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;

                if (SyncMode == Enums.SyncMode.EnterpriseToAllStores)
                {
                    sSql = "Select * from DBConnections ";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (SyncMode == Enums.SyncMode.EnterpriseToSpecificStores) // Sales Data Integration
                {
                    sSql = "GetCountryWiseConnection";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@StoreID", BaseStoreID);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                }
                else if (SyncMode == Enums.SyncMode.EnterpriseToBrandWiseStores) // Brand wise Integration // Design/Style/Sku/Images
                {
                    sSql = "GetBrandWiseConnection";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@BrandID", BaseStoreID);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                }
                else if (SyncMode == Enums.SyncMode.StoreToEnterprise)
                {
                    sSql = "Select * from DBConnections Where ConnectionType = 'Main Server' and isnull(StoreID,0)=0";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (SyncMode == Enums.SyncMode.EnterpriseToStore)
                {
                    if (BaseStoreIDs != null && BaseStoreIDs != string.Empty)
                    {
                        sSql = "Select * from DBConnections where StoreID in(" + BaseStoreIDs + ")";
                    }
                    else
                    {
                        sSql = "Select * from DBConnections where StoreID=" + BaseStoreID;
                    }
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }

                if (sSql.Trim() != string.Empty)
                {
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objDBConnections = new DBConnection();
                            objDBConnections.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objDBConnections.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                            objDBConnections.ConnectionType = Convert.ToString(objReader["ConnectionType"]);
                            objDBConnections.ConnectionString = Convert.ToString(objReader["ConnectionString"]);
                            objDBConnections.IsBaseServer = objReader["IsBaseServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseServer"]) : false;
                            if (SyncMode == Enums.SyncMode.EnterpriseToSpecificStores || SyncMode == Enums.SyncMode.EnterpriseToBrandWiseStores)
                            {
                                objDBConnections.FranchiseCode = Convert.ToString(objReader["FranchiseCode"]);
                            }
                            else
                            {
                                objDBConnections.FranchiseCode = string.Empty;
                            }
                            ConnectionList.Add(objDBConnections);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ConnectionList;
        }
        public void InsertDBConnection(string ConnectionString)
        {
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "truncate table DBConnections; Insert into DBConnections(ConnectionType,ConnectionString)values('Main Server','" + ConnectionString + "')";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                _CommandObj.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }

        public void SaveClientSyncFailedData(ClientSyncFailed objFailedStoreSyncData, string ConnectionString)
        {
            var sqlCommon = new MsSqlCommon();
            try
            {
                var _FailedServer = GetFailedServer(ConnectionString);               

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("INSERTSYNCFAILEDDATA", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;                

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = objFailedStoreSyncData.StoreID;

                SqlParameter SyncTypeID = _CommandObj.Parameters.Add("@SyncTypeID", SqlDbType.Int);
                SyncTypeID.Direction = ParameterDirection.Input;
                SyncTypeID.Value = objFailedStoreSyncData.SyncTypeID;

                SqlParameter DocumentTypeID = _CommandObj.Parameters.Add("@DocumentTypeID", SqlDbType.Int);
                DocumentTypeID.Direction = ParameterDirection.Input;
                DocumentTypeID.Value = objFailedStoreSyncData.DocumentTypeID;

                SqlParameter DocumentIDs = _CommandObj.Parameters.Add("@DocumentIDs", SqlDbType.NVarChar);
                DocumentIDs.Direction = ParameterDirection.Input;
                DocumentIDs.Value = objFailedStoreSyncData.DocumentIDs;

                SqlParameter ProcessModeID = _CommandObj.Parameters.Add("@ProcessModeID", SqlDbType.Int);
                ProcessModeID.Direction = ParameterDirection.Input;
                ProcessModeID.Value = objFailedStoreSyncData.ProcessModeID;

                SqlParameter BLLName = _CommandObj.Parameters.Add("@BLLName", SqlDbType.VarChar);
                BLLName.Direction = ParameterDirection.Input;
                BLLName.Value = objFailedStoreSyncData.BLLName;

                SqlParameter MethodName = _CommandObj.Parameters.Add("@MethodName", SqlDbType.VarChar);
                MethodName.Direction = ParameterDirection.Input;
                MethodName.Value = objFailedStoreSyncData.MethodName;

                SqlParameter ExceptionMsg = _CommandObj.Parameters.Add("@ExceptionMsg", SqlDbType.VarChar);
                ExceptionMsg.Direction = ParameterDirection.Input;
                ExceptionMsg.Value = objFailedStoreSyncData.ExceptionMessage;

                SqlParameter SyncStatus = _CommandObj.Parameters.Add("@SyncStatus", SqlDbType.Bit);
                SyncStatus.Direction = ParameterDirection.Input;
                SyncStatus.Value = objFailedStoreSyncData.SyncStatus;                

                SqlParameter FailedServer = _CommandObj.Parameters.Add("@FailedServer", SqlDbType.VarChar);
                FailedServer.Direction = ParameterDirection.Input;
                FailedServer.Value = _FailedServer;

                SqlParameter DocumentNos = _CommandObj.Parameters.Add("@DocumentNos", SqlDbType.VarChar);
                DocumentNos.Direction = ParameterDirection.Input;
                DocumentNos.Value = objFailedStoreSyncData.DocumentNos;
                
                _CommandObj.CommandType = CommandType.StoredProcedure;

                int Status = _CommandObj.ExecuteNonQuery();
                
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
        private string GetFailedServer(string ConnectionString)
        {
            string _FailedServer = string.Empty;
            try
            {
                string DecryptConnectionString = EncrypterDecrypter.Decrypt(ConnectionString);
                SqlConnectionStringBuilder _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(DecryptConnectionString);
                if (_SqlConnectionStringBuilder != null)
                {
                    _FailedServer = _SqlConnectionStringBuilder.DataSource;
                }
            }
            catch(Exception ex)
            {

            }
            return _FailedServer;
        }        
        public void UpdateSyncStatus(int ID)
        {
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Update ClientSyncFailed set SyncStatus='True' where ID={0}";
                sSql = string.Format(sSql, ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                int Status = _CommandObj.ExecuteNonQuery();

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
        public void UpdateStoreTransactionSyncStatus(string TableName,long ID , Enums.RequestFrom RequestFrom)
        {
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;
                if (RequestFrom == Enums.RequestFrom.StoreServer || RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql = "Update {0} set IsDataSyncToMainServer='True',MainServerSyncTime=SYSDATETIME() where ID={1}";
                }
                else
                {

                }
                sSql = string.Format(sSql,TableName, ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                int Status = _CommandObj.ExecuteNonQuery();

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
        public override CommonUpdateResponse UpdateBaseID(CommonUpdateRequest RequestObj)
        {
            var sqlCommon = new MsSqlCommon();
            var RequestData = (CommonUpdateRequest)RequestObj;

            CommonUpdateResponse ResponseData = new CommonUpdateResponse();
            try
            {               

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Update {0} set BaseID={1} where ID={2}";
                sSql = string.Format(sSql, RequestObj.TableName,RequestObj.BaseID,RequestObj.DocumentID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                int Status = _CommandObj.ExecuteNonQuery();

                if(Status > 0)
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.UpdateRecordFailed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override GetDBConnectionsResponse GetDBConnectionList(GetDBConnectionsRequest RequestObj)
        {
            var DBConnectionList = new List<DBConnection>();
            var RequestData = (GetDBConnectionsRequest)RequestObj;
            var ResponseData = new GetDBConnectionsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;

                if (RequestData.SyncMode == Enums.SyncMode.EnterpriseToAllStores)
                {
                    sSql = "Select * from DBConnections ";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.SyncMode == Enums.SyncMode.EnterpriseToSpecificStores) // Sales Data Integration
                {
                    sSql = "GetCountryWiseConnection";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.BaseStoreID);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                }
                else if (RequestData.SyncMode == Enums.SyncMode.EnterpriseToBrandWiseStores) // Brand wise Integration // Design/Style/Sku/Images
                {
                    sSql = "GetBrandWiseConnection";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@BrandID", RequestData.BaseStoreID);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                }
                else if (RequestData.SyncMode == Enums.SyncMode.StoreToEnterprise)
                {
                    sSql = "Select * from DBConnections Where ConnectionType = 'Main Server' and isnull(StoreID,0)=0";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.SyncMode == Enums.SyncMode.EnterpriseToStore)
                {
                    sSql = "Select * from DBConnections where StoreID=" + RequestData.BaseStoreID;
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }

                if (sSql.Trim() != string.Empty)
                {
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objDBConnections = new DBConnection();
                            objDBConnections.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objDBConnections.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                            objDBConnections.ConnectionType = Convert.ToString(objReader["ConnectionType"]);
                            objDBConnections.ConnectionString = Convert.ToString(objReader["ConnectionString"]);
                            objDBConnections.IsBaseServer = objReader["IsBaseServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseServer"]) : false;
                            if (RequestData.SyncMode == Enums.SyncMode.EnterpriseToSpecificStores || RequestData.SyncMode == Enums.SyncMode.EnterpriseToBrandWiseStores)
                            {
                                objDBConnections.FranchiseCode = Convert.ToString(objReader["FranchiseCode"]);
                            }
                            else
                            {
                                objDBConnections.FranchiseCode = string.Empty;
                            }
                            DBConnectionList.Add(objDBConnections);
                        }
                        ResponseData.DBConnectionList = DBConnectionList;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                    else
                    {
                        ResponseData.DBConnectionList = new List<DBConnection>();
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    }
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
        public void SaveDataSyncLog(DataSyncLog objDataSyncLog)
        {
            var sqlCommon = new MsSqlCommon();
            try
            {
                //var _FailedServer = GetFailedServer(ConnectionString);

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SaveDataSyncLog", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = objDataSyncLog.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = objDataSyncLog.DocumentDate;

                SqlParameter DocumentType = _CommandObj.Parameters.Add("@DocumentType", SqlDbType.VarChar);
                DocumentType.Direction = ParameterDirection.Input;
                DocumentType.Value = objDataSyncLog.DocumentType;

                SqlParameter ProcessMode = _CommandObj.Parameters.Add("@ProcessMode", SqlDbType.VarChar);
                ProcessMode.Direction = ParameterDirection.Input;
                ProcessMode.Value = objDataSyncLog.ProcessMode;

                SqlParameter BaseStoreID = _CommandObj.Parameters.Add("@BaseStoreID", SqlDbType.Int);
                BaseStoreID.Direction = ParameterDirection.Input;
                BaseStoreID.Value = objDataSyncLog.BaseStoreID;

                SqlParameter BaseStoreCode = _CommandObj.Parameters.Add("@BaseStoreCode", SqlDbType.VarChar);
                BaseStoreCode.Direction = ParameterDirection.Input;
                BaseStoreCode.Value = objDataSyncLog.BaseStoreCode;

                SqlParameter ToStoreID = _CommandObj.Parameters.Add("@ToStoreID", SqlDbType.Int);
                ToStoreID.Direction = ParameterDirection.Input;
                ToStoreID.Value = objDataSyncLog.ToStoreID;

                SqlParameter ToStoreCode = _CommandObj.Parameters.Add("@ToStoreCode", SqlDbType.VarChar);
                ToStoreCode.Direction = ParameterDirection.Input;
                ToStoreCode.Value = objDataSyncLog.ToStoreCode;

                SqlParameter IsSynced = _CommandObj.Parameters.Add("@IsSynced", SqlDbType.Bit);
                IsSynced.Direction = ParameterDirection.Input;
                IsSynced.Value = objDataSyncLog.IsSynced;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                int Status = _CommandObj.ExecuteNonQuery();

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
    }
}
