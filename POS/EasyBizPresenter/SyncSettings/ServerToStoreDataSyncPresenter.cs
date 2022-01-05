using EasyBizBLL.SyncSettings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizIView.SyncSettings;
using EasyBizRequest.SyncSettings;
using EasyBizResponse.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.SyncSettings
{
    public class ServerToStoreDataSyncPresenter
    {
        IServerToStoreDataSyncView _IServerToStoreDataSyncView;
        public ServerToStoreDataSyncPresenter(IServerToStoreDataSyncView ViewObj)
        {
            _IServerToStoreDataSyncView = ViewObj; 
        }
        public void GetSyncFailedList()
        {
            var _SyncSettingsBLL = new SyncSettingsBLL();
            var RequestData = new GetClientSyncFailedRecordsRequest();
            RequestData.SyncStatus = false;
            var ResponseData = new GetClientSyncFailedRecordsResponse();
            try
            {
                ResponseData = _SyncSettingsBLL.GetServerToStoreSyncRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IServerToStoreDataSyncView.SyncFailedList = ResponseData.SyncList;
                }
                else
                {
                    _IServerToStoreDataSyncView.Message = ResponseData.DisplayMessage;
                    _IServerToStoreDataSyncView.SyncFailedList = new List<ClientSyncFailed>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SyncData()
        {
            var _SyncSettingsBLL = new SyncSettingsBLL();          
            try
            {
                List<ClientSyncFailed> SyncFailedList = new List<ClientSyncFailed>();
                SyncFailedList = _IServerToStoreDataSyncView.SyncFailedList;
               
                int ProcessCount = 1;

                if(SyncFailedList.Count > 0)
                {
                    int Count = 0;
                    if(SyncFailedList.Where(x=> x.SyncStatus == false && x.Selected == true).ToList() != null)
                    {
                        Count = SyncFailedList.Where(x => x.SyncStatus == false && x.Selected == true).ToList().Count;
                    }

                    foreach(ClientSyncFailed objClientSyncFailed in SyncFailedList)
                    {
                        _IServerToStoreDataSyncView.Message = ProcessCount + "record are processing out of " + Count;
                        if (objClientSyncFailed.Selected == true && objClientSyncFailed.SyncStatus == false)
                        {
                            ProcessCount = ProcessCount +1;
                           _IServerToStoreDataSyncView.Message = _SyncSettingsBLL.SyncData(objClientSyncFailed, _IServerToStoreDataSyncView.RequestFrom);
                        }
                    }
                }
                GetSyncFailedList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ServerToStoreSync()
        {
            try
            {
                var _SyncSettingsBLL = new SyncSettingsBLL();
                var objClientSyncFailed = new ClientSyncFailed();
                objClientSyncFailed = _IServerToStoreDataSyncView.ClientSyncData;
                //_IServerToStoreDataSyncView.Message = _SyncSettingsBLL.SyncData(objClientSyncFailed,Enums.RequestFrom.MainServer);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
