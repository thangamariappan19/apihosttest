using EasyBizBLL.Masters;
using EasyBizBLL.SyncSettings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.SyncSettings;
using EasyBizIView.SyncSettings;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.SyncSettings;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.SyncSettings
{
    public  class ManualSyncPresenter
    {
        IManualSyncView _IManualSyncView;
        public ManualSyncPresenter(IManualSyncView ViewObj)
        {
            _IManualSyncView = ViewObj;
        }
        public void GetCuntryMasterList()
        {
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IManualSyncView.CountryList = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStoreList()
        {
            try
            {                
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.type = string.Empty;
                RequestData.CountryID = _IManualSyncView.CountryID;
                RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                var ResponseData = new SelectStoreMasterLookUpResponse();
                ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IManualSyncView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _IManualSyncView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SyncData(ClientSyncFailed objClientSyncFailed)
        {
            var _SyncSettingsBLL = new SyncSettingsBLL();
            String Msg = string.Empty;
            try
            {
                Msg = _SyncSettingsBLL.ManualSyncData(objClientSyncFailed);
                if (Msg == "Success")
                {                   
                    _IManualSyncView.Status = "Success";
                    _IManualSyncView.ExceptionMsg = string.Empty;
                }
                else
                {
                    _IManualSyncView.Status = "Failed";
                    _IManualSyncView.ExceptionMsg = Msg;
                }
            }
            catch (Exception ex)
            {
                _IManualSyncView.Status = "Failed";
                _IManualSyncView.ExceptionMsg = ex.Message;
            }
        }
        public dynamic GetResponseData(ClientSyncFailed objClientSyncFailed)
        {
            var _SyncSettingsBLL = new SyncSettingsBLL();
            dynamic DynamicData = null;
            try
            {
                DynamicData = _SyncSettingsBLL.GetResponseData(objClientSyncFailed);               
            }
            catch
            {
                DynamicData = null;
            }
            return DynamicData;
        }
        public void SyncRecords(ClientSyncFailed objClientSyncFailed,dynamic TransactionData)
        {
            var _SyncSettingsBLL = new SyncSettingsBLL();
            String Msg = string.Empty;
            try
            {
                Msg = _SyncSettingsBLL.SyncRecords(objClientSyncFailed, TransactionData);
                if (Msg == "Success")
                {
                    _IManualSyncView.Status = "Success";
                    _IManualSyncView.ExceptionMsg = string.Empty;
                }
                else
                {
                    _IManualSyncView.Status = "Failed";
                    _IManualSyncView.ExceptionMsg = Msg;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    _IManualSyncView.SyncFailedList = ResponseData.SyncList;
                }
                else
                {
                    //_IManualSyncView.Message = ResponseData.DisplayMessage;
                    _IManualSyncView.SyncFailedList = new List<ClientSyncFailed>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
