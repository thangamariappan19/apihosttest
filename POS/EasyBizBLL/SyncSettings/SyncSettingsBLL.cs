using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizFactory;
using EasyBizRequest;
using EasyBizRequest.SyncSettings;
using EasyBizResponse;
using EasyBizResponse.SyncSettings;
using MsSqlDAL.SyncSettings;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.SyncSettings
{
    public class SyncSettingsBLL
    {
        public GetDBConnectionsResponse GetDBConnectionList(GetDBConnectionsRequest objRequest)
        {
            GetDBConnectionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSyncSettingsDAL = objFactory.GetDALRepository().GetSyncSettingsDAL();
                objResponse = (GetDBConnectionsResponse)objSyncSettingsDAL.GetDBConnectionList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetDBConnectionsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "DB Connection List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetClientSyncFailedRecordsResponse GetServerToStoreSyncRecords(GetClientSyncFailedRecordsRequest objRequest)
        {
            GetClientSyncFailedRecordsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSyncSettingsDAL = objFactory.GetDALRepository().GetSyncSettingsDAL();
                objResponse = (GetClientSyncFailedRecordsResponse)objSyncSettingsDAL.GetClientSyncFailedRecords(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetClientSyncFailedRecordsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Sync List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public InsertClientSyncResponse SyncData(InsertClientSyncRequest objRequest, Enums.RequestFrom SyncRequest)
        {
            InsertClientSyncResponse objResponse = new InsertClientSyncResponse();
            var objClientSyncFailed = objRequest.ClientSyncData;

            try
            {

                SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
                var ConnectionList = new List<DBConnection>();

                ConnectionList = _SyncSettingsDAL.DBConnectionList(objClientSyncFailed.StoreID, objRequest.StoreIDs,objClientSyncFailed.SyncMode);

                BllAndFunction objBllAndFunction = new BllAndFunction();
                objBllAndFunction = SyncLinkedClass.ClassAndFunction(objClientSyncFailed.BLLName, objClientSyncFailed.MethodName);

                BaseRequestType SelecRequestData = objBllAndFunction.SelectRequestType;
                BaseResponseType SelectResponseData = objBllAndFunction.SelectBaseResponse;
                dynamic SelectedDynamicData = objBllAndFunction.DynamicData;

                SelecRequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                SelecRequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                SelecRequestData.RequestFrom = Enums.RequestFrom.SyncService;

                Type thisType = Type.GetType(objBllAndFunction.ClassName, true, true);
                Object o = (Activator.CreateInstance(thisType));
                SelectResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.SelectFunctionName).Invoke(o, new[] { SelecRequestData });

                if (SelectResponseData.StatusCode == Enums.OpStatusCode.Success)
                {

                    SelectedDynamicData = SelectResponseData.ResponseDynamicData;

                    if (SelectedDynamicData != null)
                    {
                        int TotalConnectionCount = ConnectionList.Count - 1;
                        int SyncCount = 0;

                        foreach (DBConnection objDBConnection in ConnectionList)
                        {
                            BaseRequestType RequestData = null;
                            BaseResponseType ResponseData = null;

                            if (objClientSyncFailed.StoreID != objDBConnection.StoreID)
                            {
                                string ConnectionString = string.Empty;
                                ConnectionString = objDBConnection.ConnectionString;

                                if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.New || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkNew)
                                {
                                    RequestData = objBllAndFunction.InsertRequestData;
                                    RequestData.RequestDynamicData = SelectedDynamicData;
                                    RequestData.DataSync = true;
                                    RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                                    RequestData.ConnectionString = ConnectionString;

                                    RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                                    RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                                    RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                                    RequestData.DocumentType = objClientSyncFailed.DocumentType;

                                    ResponseData = objBllAndFunction.InsertResponseData;

                                    Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                                    Object OperationObject = (Activator.CreateInstance(OperationType));
                                    ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.InsertFunctionName).Invoke(o, new[] { RequestData });

                                }
                                else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Edit || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkEdit)
                                {
                                    RequestData = objBllAndFunction.UpdateRequestData;
                                    RequestData.RequestDynamicData = SelectedDynamicData;
                                    RequestData.DataSync = true;
                                    RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                                    RequestData.ConnectionString = ConnectionString;

                                    RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                                    RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                                    RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                                    RequestData.DocumentType = objClientSyncFailed.DocumentType;

                                    ResponseData = objBllAndFunction.UpdateResponseData;

                                    Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                                    Object OperationObject = (Activator.CreateInstance(OperationType));
                                    ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.UpdateFunctionName).Invoke(o, new[] { RequestData });
                                }
                                else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Delete || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkDelete)
                                {
                                    //RequestData = objBllAndFunction.d;
                                    //RequestData.RequestDynamicData = SelectedDynamicData;
                                    //RequestData.DataSync = true;
                                    //RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                                    //RequestData.ConnectionString = ConnectionString;

                                    //ResponseData = objBllAndFunction.UpdateResponseData;

                                    //Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                                    //Object OperationObject = (Activator.CreateInstance(OperationType));
                                    //ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.UpdateFunctionName).Invoke(o, new[] { RequestData });
                                }

                                if (ResponseData != null)
                                {
                                    objResponse.DisplayMessage = ResponseData.DisplayMessage;
                                    objResponse.StatusCode = ResponseData.StatusCode;

                                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                                    {
                                        //_SyncSettingsDAL.UpdateSyncStatus(objClientSyncFailed.ID);
                                    }
                                    else
                                    {
                                        SaveFailedData(objDBConnection.StoreID, objDBConnection.ConnectionType, objDBConnection.ConnectionString, RequestData, objClientSyncFailed.BLLName, objClientSyncFailed.MethodName, objResponse.DisplayMessage);
                                    }
                                    SyncCount++;
                                }
                            }
                        }
                        //if(TotalConnectionCount == SyncCount)
                        //{
                        //    _SyncSettingsDAL.UpdateStoreTransactionSyncStatus("", objClientSyncFailed.ID , Enums.RequestFrom.MainServer);
                        //}
                    }
                    else
                    {
                        objResponse.DisplayMessage = "Record not exists !.";
                    }
                }
                else
                {
                    objResponse.DisplayMessage = SelectResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                objResponse = new InsertClientSyncResponse();
                string ErrMsg = string.Format(CommonStrings.ClientSyncFailed, objClientSyncFailed.DocumentNos, objClientSyncFailed.StoreID);
                objResponse.DisplayMessage = ErrMsg; // CommonStrings.ClientSyncFailed.Replace("{}", "Sync List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public string SyncData(ClientSyncFailed objClientSyncFailed, Enums.RequestFrom SyncRequest)
        {
            string Message = string.Empty;
            try
            {

                SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
                var ConnectionList = new List<DBConnection>();

                if (SyncRequest == Enums.RequestFrom.MainServer)
                {
                    ConnectionList = _SyncSettingsDAL.DBConnectionList(objClientSyncFailed.StoreID,null,Enums.SyncMode.EnterpriseToStore);
                }
                else
                {				
                    ConnectionList = _SyncSettingsDAL.DBConnectionList(0, null, Enums.SyncMode.StoreToEnterprise);
                }

                BllAndFunction objBllAndFunction = new BllAndFunction();
                objBllAndFunction = SyncLinkedClass.ClassAndFunction(objClientSyncFailed.BLLName, objClientSyncFailed.MethodName);

                BaseRequestType SelecRequestData = objBllAndFunction.SelectRequestType;
                BaseResponseType SelectResponseData = objBllAndFunction.SelectBaseResponse;
                dynamic SelectedDynamicData = objBllAndFunction.DynamicData;

                SelecRequestData.DocumentNos = string.IsNullOrEmpty(objClientSyncFailed.DocumentNos) ? "" : objClientSyncFailed.DocumentNos;
                SelecRequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                SelecRequestData.RequestFrom = Enums.RequestFrom.SyncService;
                SelecRequestData.ConnectionString = objClientSyncFailed.BaseConnectionString;

                Type thisType = Type.GetType(objBllAndFunction.ClassName, true, true);
                Object o = (Activator.CreateInstance(thisType));
                SelectResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.SelectFunctionName).Invoke(o, new[] { SelecRequestData });

                if (SelectResponseData.StatusCode == Enums.OpStatusCode.Success)
                {

                    SelectedDynamicData = SelectResponseData.ResponseDynamicData;

                    if (SelectedDynamicData != null)
                    {
                        foreach (DBConnection objDBConnection in ConnectionList)
                        {
                            BaseRequestType RequestData = null;
                            BaseResponseType ResponseData = null;

                            //if (objClientSyncFailed.StoreID != objDBConnection.StoreID)
                            //{
                            string ConnectionString = string.Empty;
                            ConnectionString = objDBConnection.ConnectionString;

                            if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.New || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkNew)
                            {
                                RequestData = objBllAndFunction.InsertRequestData;
                                RequestData.RequestDynamicData = SelectedDynamicData;
                                RequestData.DataSync = true;
                                RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                                RequestData.ConnectionString = ConnectionString;

                                RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                                RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                                RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                                RequestData.DocumentType = objClientSyncFailed.DocumentType;

								RequestData.BaseIntegrateStoreID = objDBConnection.StoreID;

                                ResponseData = objBllAndFunction.InsertResponseData;

                                Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                                Object OperationObject = (Activator.CreateInstance(OperationType));
                                ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.InsertFunctionName).Invoke(o, new[] { RequestData });

                            }
                            else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Edit || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkEdit)
                            {
                                RequestData = objBllAndFunction.UpdateRequestData;
                                RequestData.RequestDynamicData = SelectedDynamicData;
                                RequestData.DataSync = true;
                                RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                                RequestData.ConnectionString = ConnectionString;

                                RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                                RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                                RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                                RequestData.DocumentType = objClientSyncFailed.DocumentType;

                                ResponseData = objBllAndFunction.UpdateResponseData;

                                Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                                Object OperationObject = (Activator.CreateInstance(OperationType));
                                ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.UpdateFunctionName).Invoke(o, new[] { RequestData });
                            }
                            else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Delete || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkDelete)
                            {

                            }

                            if (ResponseData != null)
                            {
                                Message = ResponseData.DisplayMessage;
                                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                                {
                                    Message = "Success";
                                    _SyncSettingsDAL.UpdateSyncStatus(objClientSyncFailed.ID);
                                }
                                else
                                {
                                    SaveFailedData(objDBConnection.StoreID, objDBConnection.ConnectionType, objDBConnection.ConnectionString, RequestData, objClientSyncFailed.BLLName, objClientSyncFailed.MethodName, Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        Message = "Record not exists !.";
                    }
                }
                else
                {
                    Message = SelectResponseData.DisplayMessage;
                }
                return Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ManualSyncData(ClientSyncFailed objClientSyncFailed)
        {
            string Message = string.Empty;
            try
            {
                BllAndFunction objBllAndFunction = new BllAndFunction();
                objBllAndFunction = SyncLinkedClass.ClassAndFunction(objClientSyncFailed.BLLName, objClientSyncFailed.MethodName);

                BaseRequestType SelecRequestData = objBllAndFunction.SelectRequestType;
                BaseResponseType SelectResponseData = objBllAndFunction.SelectBaseResponse;
                dynamic SelectedDynamicData = objBllAndFunction.DynamicData;

                SelecRequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                SelecRequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                SelecRequestData.RequestFrom = Enums.RequestFrom.SyncService;
                SelecRequestData.ConnectionString = objClientSyncFailed.BaseConnectionString;
                SelecRequestData.FromOrToStoreID = objClientSyncFailed.StoreID;

                Type thisType = Type.GetType(objBllAndFunction.ClassName, true, true);
                Object o = (Activator.CreateInstance(thisType));
                SelectResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.SelectFunctionName).Invoke(o, new[] { SelecRequestData });

                if (SelectResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    SelectedDynamicData = SelectResponseData.ResponseDynamicData;

                    if (SelectedDynamicData != null)
                    {
                        BaseRequestType RequestData = null;
                        BaseResponseType ResponseData = null;                       
                       

                        if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.New || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkNew)
                        {
                            RequestData = objBllAndFunction.InsertRequestData;
                            RequestData.RequestDynamicData = SelectedDynamicData;
                            RequestData.DataSync = true;
                            RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                            RequestData.ConnectionString = objClientSyncFailed.TargetConnectionString;

                            RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                            RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                            RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                            RequestData.DocumentType = objClientSyncFailed.DocumentType;

                            ResponseData = objBllAndFunction.InsertResponseData;

                            Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                            Object OperationObject = (Activator.CreateInstance(OperationType));
                            ResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.InsertFunctionName).Invoke(o, new[] { RequestData });

                        }
                        else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Edit || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkEdit)
                        {

                        }
                        else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Delete || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkDelete)
                        {

                        }
                        if (ResponseData != null)
                        {
                            Message = ResponseData.DisplayMessage;
                            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                            {
                                Message = "Success";                               
                            }                                                        
                        }                        
                    }
                    else
                    {
                        Message = "Record not exists !.";
                    }
                }
                else
                {
                    Message = SelectResponseData.DisplayMessage;
                }
                return Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveFailedData(int StoreID, string ConnectionType, string ConnectionString, BaseRequestType ObjBaseRequestType, string ClassString, string MethodName, string ExceptionMessage)
        {
            try
            {
                SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
                var objFailedStoreSyncData = new ClientSyncFailed();

                objFailedStoreSyncData.StoreID = StoreID;
                if (ConnectionType == "Main Server")
                {
                    objFailedStoreSyncData.SyncTypeID = (int)Enums.SyncTypes.MainServer;
                }
                else if (ConnectionType == "Country Server")
                {
                    objFailedStoreSyncData.SyncTypeID = (int)Enums.SyncTypes.CountryServer;
                }
                else if (ConnectionType == "Store Server")
                {
                    objFailedStoreSyncData.SyncTypeID = (int)Enums.SyncTypes.StoreServer;
                }
                else
                {
                    objFailedStoreSyncData.SyncTypeID = (int)Enums.SyncTypes.StorePOS;
                }
                objFailedStoreSyncData.DocumentTypeID = (int)ObjBaseRequestType.DocumentType;
                objFailedStoreSyncData.DocumentIDs = Convert.ToString(ObjBaseRequestType.DocumentIDs);
                objFailedStoreSyncData.DocumentNos = Convert.ToString(ObjBaseRequestType.DocumentNos);
                objFailedStoreSyncData.ProcessModeID = (int)ObjBaseRequestType.ProcessMode;
                objFailedStoreSyncData.BLLName = ClassString;
                objFailedStoreSyncData.MethodName = MethodName;
                objFailedStoreSyncData.ExceptionMessage = ExceptionMessage;
                objFailedStoreSyncData.SyncStatus = false;
                objFailedStoreSyncData.ProcessTime = DateTime.Now;

                _SyncSettingsDAL.SaveClientSyncFailedData(objFailedStoreSyncData, ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorHandler objErrorHandler = new ErrorHandler();
                //objErrorHandler.SetLogger(objErrorHandler.GetLogger());
                //objErrorHandler.WriteToLog("SaveFailedData", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        public dynamic GetResponseData(ClientSyncFailed objClientSyncFailed)
        {
            try
            {
                BllAndFunction objBllAndFunction = new BllAndFunction();
                objBllAndFunction = SyncLinkedClass.ClassAndFunction(objClientSyncFailed.BLLName, objClientSyncFailed.MethodName);

                BaseRequestType SelecRequestData = objBllAndFunction.SelectRequestType;
                BaseResponseType SelectResponseData = objBllAndFunction.SelectBaseResponse;
                dynamic SelectedDynamicData = objBllAndFunction.DynamicData;

                SelecRequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                SelecRequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                SelecRequestData.RequestFrom = Enums.RequestFrom.SyncService;
                SelecRequestData.ConnectionString = objClientSyncFailed.BaseConnectionString;
                SelecRequestData.FromOrToStoreID = objClientSyncFailed.StoreID;

                Type thisType = Type.GetType(objBllAndFunction.ClassName, true, true);
                Object o = (Activator.CreateInstance(thisType));
                SelectResponseData = (BaseResponseType)o.GetType().GetMethod(objBllAndFunction.SelectFunctionName).Invoke(o, new[] { SelecRequestData });

                if (SelectResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    SelectedDynamicData = SelectResponseData.ResponseDynamicData;
                }
                else
                {
                    SelectedDynamicData = null;
                }
                return SelectedDynamicData;
            }
            catch
            {
                return null;
            }
        }
        public string SyncRecords(ClientSyncFailed objClientSyncFailed, dynamic SelectedDynamicData)
        {
            string Message = string.Empty;
            try
            {
                if (SelectedDynamicData != null)
                {
                    BllAndFunction objBllAndFunction = new BllAndFunction();
                    BaseRequestType RequestData = null;
                    BaseResponseType ResponseData = null;

                    if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.New || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkNew)
                    {
                        RequestData = objBllAndFunction.InsertRequestData;
                        RequestData.RequestDynamicData = SelectedDynamicData;
                        RequestData.DataSync = true;
                        RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                        RequestData.ConnectionString = objClientSyncFailed.TargetConnectionString;

                        RequestData.DocumentIDs = objClientSyncFailed.DocumentIDs;
                        RequestData.DocumentNos = objClientSyncFailed.DocumentNos;
                        RequestData.ProcessMode = (Enums.ProcessMode)objClientSyncFailed.ProcessModeID;
                        RequestData.DocumentType = objClientSyncFailed.DocumentType;

                        ResponseData = objBllAndFunction.InsertResponseData;

                        Type OperationType = Type.GetType(objBllAndFunction.ClassName, true, true);
                        Object OperationObject = (Activator.CreateInstance(OperationType));
                        ResponseData = (BaseResponseType)OperationObject.GetType().GetMethod(objBllAndFunction.InsertFunctionName).Invoke(OperationObject, new[] { RequestData });

                    }
                    else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Edit || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkEdit)
                    {

                    }
                    else if (objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.Delete || objClientSyncFailed.ProcessModeID == (int)Enums.ProcessMode.BulkDelete)
                    {

                    }
                    if (ResponseData != null)
                    {
                        Message = ResponseData.DisplayMessage;
                        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                        {
                            Message = "Success";
                        }
                    }
                }
                else
                {
                    Message = "Record not exists !.";
                }
            }
            catch
            {
                Message = "Process failed !.";
            }
            return Message;
        }
    }
}
