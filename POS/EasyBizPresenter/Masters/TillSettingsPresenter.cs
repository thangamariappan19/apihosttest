using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ITill;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CountTypeMasterRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.TillSettingRequest;
using EasyBizResponse.Masters.CountTypeMasterResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.RoleResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.TillSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class TillSettingsPresenter
    {
        ITillSettingsView _ITillSettingsView;
        TillSettingsBLL _TillSettingsBLL = new TillSettingsBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        PosMasterBLL _PosMasterBLL = new PosMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        RoleBLL _RoleMasterBLL = new RoleBLL();
        CountTypeMasterBLL _CountTypeMasterBLL = new CountTypeMasterBLL();


        public TillSettingsPresenter(ITillSettingsView ViewObj)
        {
            _ITillSettingsView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ITillSettingsView.CountryID <= 0 || Convert.ToString(_ITillSettingsView.CountryID) == String.Empty)
            {
                _ITillSettingsView.Message = "Country is missing Please Enter it";
            }
            else if (_ITillSettingsView.StoreID <= 0 || Convert.ToString(_ITillSettingsView.StoreID) == String.Empty)
            {
                _ITillSettingsView.Message = "Store Name is missing Please Enter it.";
            }
            else if (_ITillSettingsView.PosID <= 0 || Convert.ToString(_ITillSettingsView.PosID) == String.Empty)
             {
                 _ITillSettingsView.Message = "Pos Name is missing Please Enter it";
             }

            else if (_ITillSettingsView.UserTeamID <= 0 || Convert.ToString(_ITillSettingsView.UserTeamID) == String.Empty)
             {
                 _ITillSettingsView.Message = "User Name is missing Please Enter it";
             }

            else if (_ITillSettingsView.CountType <= 0 || Convert.ToString(_ITillSettingsView.CountType) == String.Empty)
            {
                _ITillSettingsView.Message = "count Type is missing Please Enter it";
            } 
            //else if (_ITillSettingsView.Description.Trim() == string.Empty)
            //{
            //    _ITillSettingsView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveTillSettings()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveTillSettingsRequest();
                RequestData.TillSettingsData = new TillSettings();
                RequestData.TillSettingsData.CountryID = _ITillSettingsView.CountryID;
                RequestData.TillSettingsData.StoreID = _ITillSettingsView.StoreID;
                RequestData.TillSettingsData.PosID = _ITillSettingsView.PosID;
                RequestData.TillSettingsData.UserTeamID = _ITillSettingsView.UserTeamID;
                RequestData.TillSettingsData.CountRequired = _ITillSettingsView.CountRequired;
                RequestData.TillSettingsData.CountType = _ITillSettingsView.CountType;
                RequestData.TillSettingsData.FloatingAmount = _ITillSettingsView.FloatingAmount;
                RequestData.TillSettingsData.TillCountOnAssign = _ITillSettingsView.TillCountOnAssign;
                RequestData.TillSettingsData.TillCountOnClose = _ITillSettingsView.TillCountOnClose;
                RequestData.TillSettingsData.TillCountOnFinalize = _ITillSettingsView.TillCountOnFinalize;
                RequestData.TillSettingsData.CreateBy = _ITillSettingsView.UserID;
                RequestData.TillSettingsData.Remarks = _ITillSettingsView.Remarks;
                RequestData.TillSettingsData.Active = _ITillSettingsView.Active;
                //RequestData.TillSettingsData.CreateBy = _ITillSettingsView.CreateBy;                               
                // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
                //  RequestData.ProductLineMasterData.Active = true;
                RequestData.TillSettingsData.SCN = _ITillSettingsView.SCN;
                SaveTillSettingsResponse ResponseData = _TillSettingsBLL.SaveTillSettings(RequestData);
                _ITillSettingsView.Message = ResponseData.DisplayMessage;
                _ITillSettingsView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ITillSettingsView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateTillSettings()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateTillSettingsRequest();
                RequestData.TillSettingsData = new TillSettings();
                RequestData.TillSettingsData.ID = _ITillSettingsView.ID;
                RequestData.TillSettingsData.CountryID = _ITillSettingsView.CountryID;
                RequestData.TillSettingsData.StoreID = _ITillSettingsView.StoreID;
                RequestData.TillSettingsData.PosID = _ITillSettingsView.PosID;
                RequestData.TillSettingsData.UserTeamID = _ITillSettingsView.UserTeamID;
                RequestData.TillSettingsData.CountRequired = _ITillSettingsView.CountRequired;
                RequestData.TillSettingsData.CountType = _ITillSettingsView.CountType;
                RequestData.TillSettingsData.FloatingAmount = _ITillSettingsView.FloatingAmount;
                RequestData.TillSettingsData.TillCountOnAssign = _ITillSettingsView.TillCountOnAssign;
                RequestData.TillSettingsData.TillCountOnClose = _ITillSettingsView.TillCountOnClose;
                RequestData.TillSettingsData.TillCountOnFinalize = _ITillSettingsView.TillCountOnFinalize;
                RequestData.TillSettingsData.UpdateBy = _ITillSettingsView.UserID;
                RequestData.TillSettingsData.Remarks = _ITillSettingsView.Remarks;
                RequestData.TillSettingsData.Active = _ITillSettingsView.Active;
                //RequestData.TillSettingsData.UpdateOn = DateTime.Now;
                //RequestData.TillSettingsData.Active = true;
                RequestData.TillSettingsData.SCN = _ITillSettingsView.SCN;
                var ResponseData = _TillSettingsBLL.UpdateTillSettings(RequestData);
                _ITillSettingsView.Message = ResponseData.DisplayMessage;
                _ITillSettingsView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ITillSettingsView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTillSettings()
        {
            try
        {

            var RequestData = new DeleteTillSettingsRequest();
            RequestData.ID = _ITillSettingsView.ID;
            var ResponseData = _TillSettingsBLL.DeleteTillSettings(RequestData);
            _ITillSettingsView.Message = ResponseData.DisplayMessage;
            _ITillSettingsView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTillSettingsRecord()
        {


            var RequestData = new SelectByIDTillSettingsRequest();
            RequestData.ID = _ITillSettingsView.ID;

            var ResponseData = _TillSettingsBLL.SelectTillSettingsRecord(RequestData);
            _ITillSettingsView.CountryID = ResponseData.TillSettingsRecord.CountryID;
            _ITillSettingsView.StoreID = ResponseData.TillSettingsRecord.StoreID;
            _ITillSettingsView.PosID = ResponseData.TillSettingsRecord.PosID;
            _ITillSettingsView.UserTeamID = ResponseData.TillSettingsRecord.UserTeamID;
            _ITillSettingsView.CountRequired = ResponseData.TillSettingsRecord.CountRequired;
            _ITillSettingsView.CountType = ResponseData.TillSettingsRecord.CountType;
            _ITillSettingsView.FloatingAmount = ResponseData.TillSettingsRecord.FloatingAmount;
            _ITillSettingsView.TillCountOnAssign = ResponseData.TillSettingsRecord.TillCountOnAssign;
            _ITillSettingsView.TillCountOnClose = ResponseData.TillSettingsRecord.TillCountOnClose;
            _ITillSettingsView.TillCountOnFinalize = ResponseData.TillSettingsRecord.TillCountOnFinalize;
            _ITillSettingsView.SCN = ResponseData.TillSettingsRecord.SCN;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ITillSettingsView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ITillSettingsView.Message = ResponseData.DisplayMessage;
            }

            _ITillSettingsView.ProcessStatus = ResponseData.StatusCode;
        }
        public void GetStoreMasterLookUP()
        {
            EasyBizRequest.Masters.StoreMasterRequest.SelectStoreMasterLookUpRequest RequestData = new EasyBizRequest.Masters.StoreMasterRequest.SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _ITillSettingsView.CountryID;
            EasyBizResponse.Masters.StoreMasterResponse.SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITillSettingsView.StoreMasterLookUp = ResponseData.StoreMasterList;
            }
        }
        public void GetPosMasterLookUP()
        {
            SelectPosMasterLookUpRequest RequestData = new SelectPosMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _ITillSettingsView.CountryID;
            SelectPosMasterLookUpResponse ResponseData = _PosMasterBLL.SelectPosMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITillSettingsView.PosMasterLookUp = ResponseData.PosMasterList;
            }
        }
        public void GetRoleMasterLookUP()
        {
            SelectRoleMasterLookUpRequest RequestData = new SelectRoleMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectRoleMasterLookUpResponse ResponseData = _RoleMasterBLL.SelectRoleLookUP(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITillSettingsView.RoleMasterLookUp = ResponseData.RoleMasterList;
            }
        }

        public void GetCountTypeMasterLookUP()
        {
            SelectCountTypeMasterLookUpRequest RequestData = new SelectCountTypeMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountTypeMasterLookUpResponse ResponseData = _CountTypeMasterBLL.SelectCountTypeMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITillSettingsView.CountTypeMasterLookUp= ResponseData.CountTypeMasterList;
            }
        }
        public void GetCountryMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ITillSettingsView.CountryMasterLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    public class TillSettingsListPresenter
    {

        TillSettingsBLL _TillSettingsBLL = new TillSettingsBLL();

        ITillSettingsList _ITillSettingsList;

        public TillSettingsListPresenter(ITillSettingsList ViewObj)
        {
            _ITillSettingsList = ViewObj;
        }

        public void GetTillSettingsList()
        {

            var RequestData = new SelectAllTillSettingsRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllTillSettingsResponse();
            ResponseData = _TillSettingsBLL.SelectAllTillSettings(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITillSettingsList.TillSettingsList = ResponseData.TillSettingsList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }

}
