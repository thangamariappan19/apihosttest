using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters;
using EasyBizIView.Masters.IWarehouseMaster;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class WarehouseMasterPresenter
    {


        IWarehouseMasterView _IWarehouseMasterView;
        WarehouseMasterBLL _WarehouseMasterBLL = new WarehouseMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        CompanySettingBLL _CompanySettingBLLBLL = new CompanySettingBLL();
        WarehouseTypeMasterBLL _WarehouseTypeMasterBLL = new WarehouseTypeMasterBLL();

        public WarehouseMasterPresenter(IWarehouseMasterView ViewObj)
        {
            _IWarehouseMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IWarehouseMasterView.WarehouseCode.Trim() == string.Empty)
            {
                _IWarehouseMasterView.Message = " Code is missing Please Enter it.";
            }
            else if (_IWarehouseMasterView.WarehouseName.Trim() == string.Empty)
            {
                _IWarehouseMasterView.Message = "Please Enter Warehouse Name";
            }
            //else if (_IWarehouseMasterView.Description.Trim() == string.Empty)
            //{
            //    _IWarehouseMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveWarehouseMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveWarehouseMasterRequest();
                RequestData.WarehouseMasterData = new WarehouseMaster();

                RequestData.WarehouseMasterData.ID = _IWarehouseMasterView.ID;
                RequestData.WarehouseMasterData.WarehouseCode = _IWarehouseMasterView.WarehouseCode;
                RequestData.WarehouseMasterData.WarehouseName = _IWarehouseMasterView.WarehouseName;
                RequestData.WarehouseMasterData.CountryID = _IWarehouseMasterView.CountryID;
                RequestData.WarehouseMasterData.CompanyID = _IWarehouseMasterView.CompanyID;
                RequestData.WarehouseMasterData.WarehouseTypeID = _IWarehouseMasterView.WarehouseTypeID;
                RequestData.WarehouseMasterData.CreateBy = _IWarehouseMasterView.UserID;
                RequestData.WarehouseMasterData.Remarks = _IWarehouseMasterView.Remarks;
                RequestData.WarehouseMasterData.Active = _IWarehouseMasterView.Active;

                //RequestData.WarehouseMasterData.CreateBy = _IWarehouseMasterView.CreateBy;                               
                // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
                //  RequestData.ProductLineMasterData.Active = true;
                RequestData.WarehouseMasterData.SCN = _IWarehouseMasterView.SCN;
                SaveWarehouseMasterResponse ResponseData = _WarehouseMasterBLL.SaveWarehouseMaster(RequestData);
                _IWarehouseMasterView.Message = ResponseData.DisplayMessage;
                _IWarehouseMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IWarehouseMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateWarehouseMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateWarehouseMasterRequest();
                RequestData.WarehouseMasterData = new WarehouseMaster();
                RequestData.WarehouseMasterData.ID = _IWarehouseMasterView.ID;
                RequestData.WarehouseMasterData.WarehouseCode = _IWarehouseMasterView.WarehouseCode;
                RequestData.WarehouseMasterData.WarehouseName = _IWarehouseMasterView.WarehouseName;
                RequestData.WarehouseMasterData.CountryID = _IWarehouseMasterView.CountryID;
                RequestData.WarehouseMasterData.CompanyID = _IWarehouseMasterView.CompanyID;
                RequestData.WarehouseMasterData.WarehouseTypeID = _IWarehouseMasterView.WarehouseTypeID;
                RequestData.WarehouseMasterData.UpdateBy = _IWarehouseMasterView.UserID;
                RequestData.WarehouseMasterData.Remarks = _IWarehouseMasterView.Remarks;
                RequestData.WarehouseMasterData.Active = _IWarehouseMasterView.Active;
                //RequestData.WarehouseMasterData.UpdateOn = DateTime.Now;
                //RequestData.WarehouseMasterData.Active = true;
                RequestData.WarehouseMasterData.SCN = _IWarehouseMasterView.SCN;
                var ResponseData = _WarehouseMasterBLL.UpdateWarehouseMaster(RequestData);
                _IWarehouseMasterView.Message = ResponseData.DisplayMessage;
                _IWarehouseMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IWarehouseMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteWarehouseMaster()
        {
            try
        {

            var RequestData = new DeleteWarehouseMasterRequest();
            RequestData.ID = _IWarehouseMasterView.ID;
            var ResponseData = _WarehouseMasterBLL.DeleteWarehouseMaster(RequestData);
            _IWarehouseMasterView.Message = ResponseData.DisplayMessage;
            _IWarehouseMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectWarehouseMasterRecord()
        {


            var RequestData = new SelectByIDWarehouseMasterRequest();
            RequestData.ID = _IWarehouseMasterView.ID;

            var ResponseData = _WarehouseMasterBLL.SelectWarehouseMasterRecord(RequestData);
            _IWarehouseMasterView.WarehouseCode = ResponseData.WarehouseMasterRecord.WarehouseCode;
            _IWarehouseMasterView.WarehouseName = ResponseData.WarehouseMasterRecord.WarehouseName;
            _IWarehouseMasterView.CountryID = ResponseData.WarehouseMasterRecord.CountryID;
            _IWarehouseMasterView.CompanyID = ResponseData.WarehouseMasterRecord.CompanyID;
            _IWarehouseMasterView.WarehouseTypeID = ResponseData.WarehouseMasterRecord.WarehouseTypeID;
            _IWarehouseMasterView.SCN = ResponseData.WarehouseMasterRecord.SCN;
            _IWarehouseMasterView.Remarks = ResponseData.WarehouseMasterRecord.Remarks;
            _IWarehouseMasterView.Active = ResponseData.WarehouseMasterRecord.Active;


            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IWarehouseMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IWarehouseMasterView.Message = ResponseData.DisplayMessage;
            }

            _IWarehouseMasterView.ProcessStatus = ResponseData.StatusCode;
        }

        public void GetCountryLookUP()
        {
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IWarehouseMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
            }
        }
        public void GetCompanyLookUP()
        {
            SelectCompanySettingsLookUpRequest RequestData = new SelectCompanySettingsLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IWarehouseMasterView.CountryID;
            SelectCompanySettingsLookUpResponse ResponseData = _CompanySettingBLLBLL.SelectCompanySettingsLookUp(RequestData);                
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IWarehouseMasterView.CompanySettingsLookUp = ResponseData.CompanySettingsList;
            }
        }
        public void GetWarehouseTypeMasterLookUP()
        {
            SelectWarehouseTypeMasterLookUpRequest RequestData = new SelectWarehouseTypeMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectWarehouseTypeMasterLookUpResponse ResponseData = _WarehouseTypeMasterBLL.SelectWarehouseTypeMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IWarehouseMasterView.WarehouseTypeMasterLookUp = ResponseData.WarehouseTypeMasterList;
            }
        }

    }
}
public class WarehouseMasterListPresenter
{

    WarehouseMasterBLL _WarehouseMasterBLL = new WarehouseMasterBLL();

    IWarehouseMasterList _IWarehouseMasterList;

    public WarehouseMasterListPresenter(IWarehouseMasterList ViewObj)
    {
        _IWarehouseMasterList = ViewObj;
    }

    public void GetWarehouseMasterList()
    {

        var RequestData = new SelectAllWarehouseMasterRequest();
        RequestData.ShowInActiveRecords = true;

        var ResponseData = new SelectAllWarehouseMasterResponse();
        ResponseData = _WarehouseMasterBLL.SelectAllWarehouseMaster(RequestData);
        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        {
            _IWarehouseMasterList.WarehouseMasterList = ResponseData.WarehouseMasterList;
        }
        else
        {
            //_IMASCompanyList.Message = ResponseData.DisplayMessage;
        }

    }
}

