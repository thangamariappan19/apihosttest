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
    public class ManualMasterSyncPresenter
    {
        IManualMasterSyncView _IManualMasterSyncView;
        ManualMasterSyncBLL _ManualMasterSyncBLL;
        public ManualMasterSyncPresenter(IManualMasterSyncView ViewObj)
        {
            _IManualMasterSyncView = ViewObj;
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
                    _IManualMasterSyncView.CountryList = ResponseData.CountryMasterList;
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
                RequestData.CountryID = _IManualMasterSyncView.CountryID;
                //RequestData.RequestFrom = Enums.RequestFrom.SyncService;
                var ResponseData = new SelectStoreMasterLookUpResponse();
                ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IManualMasterSyncView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _IManualMasterSyncView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSKUCodeInsert()
        {
            try
            {
                
                var SyncSKU = new ManualMasterSyncDBType();
                SyncSKU.StoreID = _IManualMasterSyncView.StoreID;
                SyncSKU.Module = _IManualMasterSyncView.Module;

                var RequestData = new ManualMasterSyncRequest();
                RequestData.SyncSKU = SyncSKU;

                _ManualMasterSyncBLL = new ManualMasterSyncBLL();

                var ResponseData = _ManualMasterSyncBLL.SyncSKUCode(RequestData);
                _IManualMasterSyncView.Message = ResponseData.DisplayMessage;
                _IManualMasterSyncView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
