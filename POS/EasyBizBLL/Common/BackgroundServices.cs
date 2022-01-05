using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizRequest;
using EasyBizRequest.Common;
using EasyBizResponse;
using EasyBizResponse.Common;
using MsSqlDAL.SyncSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyBizBLL.Common
{
    public class BackgroundServices
    {
        private IList<BackgroundWorker> WorkerList = new List<BackgroundWorker>();
        BackgroundWorker myWorker = new BackgroundWorker();
        public long BaseID = 0;
        public int DocumentID = 0;
        Enums.RequestFrom RequestFrom;

        object[] arrObjects = null;
        BaseRequestType ObjBaseRequestType;
        BaseResponseType objBaseResponseType;
        string ClassString = string.Empty;
        string MethodName = string.Empty;

        public bool CheckAllThreadsHaveFinishedWorking()
        {
            bool hasAllThreadsFinished = false;
            try
            {
                while (!hasAllThreadsFinished)
                {
                    hasAllThreadsFinished = (from worker in WorkerList
                                             where worker.IsBusy
                                             select worker).ToList().Count == 0;
                    Application.DoEvents(); //This call is very important if you want to have a progress bar and want to update it
                    //from the Progress event of the background worker.
                    Thread.Sleep(1000);     //This call waits if the loop continues making sure that the CPU time gets freed before
                    //re-checking.
                }
                WorkerList.Clear();  //After the loop exits clear the list of all background workers to release memory.
                //On the contrary you can also dispose your background workers.
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hasAllThreadsFinished;
        }

        public BackgroundServices(BaseRequestType objBaseRequestType, BaseResponseType objBaseResponseType, string ClassName, string MethodName)
        {
            try
            {
                if (objBaseRequestType != null && objBaseResponseType != null && ClassName != string.Empty && MethodName != string.Empty)
                {
                    myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);
                    myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
                    myWorker.ProgressChanged += new ProgressChangedEventHandler(myWorker_ProgressChanged);
                    myWorker.WorkerReportsProgress = true;
                    myWorker.WorkerSupportsCancellation = true;

                    WorkerList.Add(myWorker);

                    if (!myWorker.IsBusy)//Check if the worker is already in progress
                    {
                        object[] arrObjects = new object[] { objBaseRequestType, objBaseResponseType, ClassName, MethodName };//Declare the array of objects
                        myWorker.RunWorkerAsync(arrObjects);//Call the background worker
                    }
                    else
                    {
                        ErrorHandler objErrorHandler = new ErrorHandler();
                        objErrorHandler.SetLogger(objErrorHandler.GetLogger());
                        objErrorHandler.WriteToLog("BackgroundServices", ClassName + " and" + MethodName + " is busy with other services." + "Worklist count is:" + WorkerList.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler objErrorHandler = new ErrorHandler();
                objErrorHandler.SetLogger(objErrorHandler.GetLogger());
                objErrorHandler.WriteToLog("BackgroundServices", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        protected void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
            StringBuilder sb = new StringBuilder();//Declare a new string builder to store the result.

            var ConnectionList = new List<DBConnection>();

            BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event

            arrObjects = (object[])e.Argument;//Collect the array of objects the we recived from the main thread            

            ObjBaseRequestType = (BaseRequestType)arrObjects[0];
            objBaseResponseType = (BaseResponseType)arrObjects[1];
            ClassString = (string)arrObjects[2];
            MethodName = (string)arrObjects[3];

            object[] parametersArray = new object[] { ObjBaseRequestType };
            Type thisType = Type.GetType(ClassString, true, true);
            Object o = (Activator.CreateInstance(thisType));

            ConnectionList = _SyncSettingsDAL.DBConnectionList(ObjBaseRequestType.BaseIntegrateStoreID, ObjBaseRequestType.StoreIDs, ObjBaseRequestType.SyncMode);

            foreach (DBConnection objDBConnection in ConnectionList)
            {
                try
                {
                    if (!sendingWorker.CancellationPending)
                    {
                        ObjBaseRequestType.ConnectionString = objDBConnection.ConnectionString;
                        ObjBaseRequestType.DataSync = true;
                        ObjBaseRequestType.BaseID = BaseID;

                        if (objDBConnection.ConnectionType == "Main Server")
                        {
                            ObjBaseRequestType.RequestFrom = Enums.RequestFrom.MainServer;
                        }
                        else
                        {
                            ObjBaseRequestType.RequestFrom = Enums.RequestFrom.StoreServer;
                        }

                        BaseResponseType ResponseData = null;

                        ResponseData = (BaseResponseType)o.GetType().GetMethod(MethodName).Invoke(o, new[] { ObjBaseRequestType });
                        bool IsSynced = false;

                        if (ResponseData != null && ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                        {
                            IsSynced = true;
                        }
                        else if (ResponseData != null && ResponseData.StatusCode != EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                        {
                            SaveFailedData(objDBConnection.StoreID, objDBConnection.ConnectionType, objDBConnection.ConnectionString, ObjBaseRequestType, ClassString, MethodName, Convert.ToString(ResponseData.ExceptionMessage));
                        }
                        else
                        {
                            SaveFailedData(objDBConnection.StoreID, objDBConnection.ConnectionType, objDBConnection.ConnectionString, ObjBaseRequestType, ClassString, MethodName, "Response data is null or unknown error");
                        }

                        if (ObjBaseRequestType.DocumentNos != null && ObjBaseRequestType.DocumentDate != DateTime.MinValue && ObjBaseRequestType.DocumentDate != DateTime.MaxValue)
                        {
                            SaveDataSyncLog(ObjBaseRequestType.DocumentNos, ObjBaseRequestType.DocumentDate, (int)ObjBaseRequestType.DocumentType,(int)ObjBaseRequestType.ProcessMode, ObjBaseRequestType.BaseIntegrateStoreID, ObjBaseRequestType.BaseIntegrateStoreCode, objDBConnection.StoreID, IsSynced);
                        }
                    }
                    else
                    {
                        e.Cancel = true;//If a cancellation request is pending,assgine this flag a value of true
                        break;// If a cancellation request is pending, break to exit the loop
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            e.Result = sb.ToString();// Send our result to the main thread!
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
                objFailedStoreSyncData.DocumentIDs = ObjBaseRequestType.DocumentIDs != null ? ObjBaseRequestType.DocumentIDs : string.Empty;
                objFailedStoreSyncData.DocumentNos = ObjBaseRequestType.DocumentNos != null ? ObjBaseRequestType.DocumentNos : string.Empty;
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
                ErrorHandler objErrorHandler = new ErrorHandler();
                objErrorHandler.SetLogger(objErrorHandler.GetLogger());
                objErrorHandler.WriteToLog("SaveFailedData", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        private void SaveDataSyncLog(string DocumentNo,DateTime DocumentDate,int DocumentTypeID,int ProcessModeID, int BaseStoreID,string BaseStoreCode,int ToStoreID,bool IsSynced)
        {
            try
            {
                SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
                var objFailedStoreSyncData = new DataSyncLog();

                objFailedStoreSyncData.DocumentNo = DocumentNo;
                objFailedStoreSyncData.DocumentDate = DocumentDate;
                objFailedStoreSyncData.DocumentType = Enums.GetDocumentName(DocumentTypeID);
                objFailedStoreSyncData.ProcessMode = Enums.GetProcessModeName(ProcessModeID);
                objFailedStoreSyncData.BaseStoreID = BaseStoreID;
                objFailedStoreSyncData.BaseStoreCode = BaseStoreCode;
                objFailedStoreSyncData.ToStoreID = ToStoreID;
                objFailedStoreSyncData.IsSynced = IsSynced;

                _SyncSettingsDAL.SaveDataSyncLog(objFailedStoreSyncData);
            }
            catch(Exception ex)
            {
                ErrorHandler objErrorHandler = new ErrorHandler();
                objErrorHandler.SetLogger(objErrorHandler.GetLogger());
                objErrorHandler.WriteToLog("SaveDataSyncLog", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        protected void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been cancelled or if an error occured
            {
                //string result = (string)e.Result;//Get the result from the background thread

                if (RequestFrom != Enums.RequestFrom.MainServer && (ObjBaseRequestType.ProcessMode == Enums.ProcessMode.New || ObjBaseRequestType.ProcessMode == Enums.ProcessMode.BulkNew))
                {
                    SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
                    string TableName = Tables.TableName(ClassString, MethodName);   // Need to return the table name. otherwise base ID not updating
                    CommonUpdateRequest RequestData = new CommonUpdateRequest();
                    RequestData.TableName = TableName;
                    RequestData.DocumentID = DocumentID;
                    RequestData.BaseID = BaseID;

                    if (TableName != string.Empty && BaseID != 0)
                    {
                        var ResponseData = new CommonUpdateResponse();
                        //ResponseData = _SyncSettingsDAL.UpdateBaseID(RequestData);
                    }
                }

            }
            else if (e.Cancelled)
            {
                //lblStatus.Text = "User Cancelled";
            }
            else
            {
                //lblStatus.Text = "An error has occured";
            }
            //btnStart.Enabled = true;//Reneable the start button
        }

        protected void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //lblStatus.Text = string.Format("Counting number: {0}...", e.ProgressPercentage);
        }

        private int PerformHeavyOperation(int i)
        {
            return 0;
        }

        public static object InvokeMethod(Delegate method, params object[] args)
        {
            return method.DynamicInvoke(args);
        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    myWorker.CancelAsync();//Issue a cancellation request to stop the background worker
        //}
    }
}
