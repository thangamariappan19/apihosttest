using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IBarcodeSettings;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class BarcodeSettingsPresenter
    {
        IBarcodeSettingsView _IBarcodeSettingsView;
        BarcodeSettingsBLL _BarcodeSettingsBLL = new BarcodeSettingsBLL();

        public BarcodeSettingsPresenter(IBarcodeSettingsView ViewObj)
        {
            _IBarcodeSettingsView = ViewObj;
        }


        public void SaveBarcodeSettings()
        {
            try
            {
                //if (IsValidForm())
                //{
                var RequestData = new SaveBarcodeSettingsRequest();
                RequestData.BarcodeSettingsList = _IBarcodeSettingsView.BarcodeSettingsList;
                SaveBarcodeSettingsResponse ResponseData = _BarcodeSettingsBLL.SaveBarcodeSettings(RequestData);
                _IBarcodeSettingsView.Message = ResponseData.DisplayMessage;
                _IBarcodeSettingsView.ProcessStatus = ResponseData.StatusCode;
                }
                //else
                //{
                //    _IBarcodeSettingsView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                //}
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectAllBarcodeSettings()
        {
            SelectAllBarcodeSettingsRequest RequestData = new SelectAllBarcodeSettingsRequest();

            SelectAllBarcodeSettingsResponse ResponseData = _BarcodeSettingsBLL.SelectAllBarcodeSettings(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IBarcodeSettingsView.BarcodeSettingsList = ResponseData.BarcodeSettingsList;
            }
            else
            {
                _IBarcodeSettingsView.Message = ResponseData.DisplayMessage;
                _IBarcodeSettingsView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void UpdateBarcodeSettings()
        {

        }


        public class BarcodeSettingsListPresenter
        {

            BarcodeSettingsBLL _BarcodeSettingsBLL = new BarcodeSettingsBLL();

            IBarcodeSettingsList _IBarcodeSettingsList;

            public BarcodeSettingsListPresenter(IBarcodeSettingsList ViewObj)
            {
                _IBarcodeSettingsList = ViewObj;
            }

            public void GetBarcodeSettingsList()
            {

                var RequestData = new SelectAllBarcodeSettingsRequest();
                RequestData.ShowInActiveRecords = false;

                var ResponseData = new SelectAllBarcodeSettingsResponse();
                ResponseData = _BarcodeSettingsBLL.SelectAllBarcodeSettings(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IBarcodeSettingsList.BarcodeSettingsList = ResponseData.BarcodeSettingsList;
                }
                else
                {
                    //_IMASCompanyList.Message = ResponseData.DisplayMessage;
                }

            }
        }
    }
}
 
   

