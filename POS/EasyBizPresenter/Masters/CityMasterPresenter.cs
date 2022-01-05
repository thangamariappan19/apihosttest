using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICityMaster;
using EasyBizRequest.Masters.CityMasterRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.CityMasterResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CityMasterPresenter
    {
        StateMasterBLL _StateBLL = new StateMasterBLL();
        CityMasterBLL _CityMasterBLL = new CityMasterBLL();
        ICityMasterView _ICityMasterView;


        public CityMasterPresenter(ICityMasterView ViewObj)
        {
            _ICityMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICityMasterView.CityCode.Trim() == string.Empty)
            {
                _ICityMasterView.Message = "City Code is missing Please Enter it.";
            }          
            else if (_ICityMasterView.CityName.Trim() == string.Empty)
            {
                _ICityMasterView.Message = "City Name is missing Please Enter it.";
            }
            else if (_ICityMasterView.State.Trim() == string.Empty)
            {
                _ICityMasterView.Message = "State Name is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void GetStateAloneLookUP()
        {
            SelectStateAloneLookUPRequest RequestData = new SelectStateAloneLookUPRequest();
            RequestData.ShowInActiveRecords = false;
            SelectStateAloneLookUPResponse ResponseData = _StateBLL.SelectStateAloneLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICityMasterView.StateMasterLookup = ResponseData.StateMasterList;

            }
        }

        public void SelectCityRecord()
        {
            try
            {
                var RequestData = new SelectByCityIDRequest();
                RequestData.ID = _ICityMasterView.ID;
                var ResponseData = _CityMasterBLL.SelectCityRecord(RequestData);
                _ICityMasterView.CityCode = ResponseData.CityMasterRecord.CityCode;
                _ICityMasterView.CityName = ResponseData.CityMasterRecord.CityName;
                _ICityMasterView.StateID = ResponseData.CityMasterRecord.StateID;
                _ICityMasterView.State = ResponseData.CityMasterRecord.StateName;
                _ICityMasterView.SCN = ResponseData.CityMasterRecord.SCN;
                _ICityMasterView.Remarks = ResponseData.CityMasterRecord.Remarks;
                _ICityMasterView.Active = ResponseData.CityMasterRecord.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICityMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICityMasterView.Message = ResponseData.DisplayMessage;
                }

                _ICityMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveCityMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCityRequest();
                    RequestData.CityRecord = new CityMaster();

                    RequestData.CityRecord.ID = _ICityMasterView.ID;
                    RequestData.CityRecord.CityCode = _ICityMasterView.CityCode;
                    RequestData.CityRecord.CityName = _ICityMasterView.CityName;
                    RequestData.CityRecord.StateName = _ICityMasterView.State;
                    RequestData.CityRecord.StateID = _ICityMasterView.StateID;
                    RequestData.CityRecord.CreateBy = _ICityMasterView.UserID;
                    RequestData.CityRecord.CreateOn = DateTime.Now;
                    RequestData.CityRecord.Active = _ICityMasterView.Active;
                    RequestData.CityRecord.Remarks = _ICityMasterView.Remarks;
                    RequestData.CityRecord.SCN = _ICityMasterView.SCN;

                    var ResponseData = _CityMasterBLL.SaveCityMaster(RequestData);

                    _ICityMasterView.Message = ResponseData.DisplayMessage;
                    _ICityMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ICityMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCityMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateCityRequest();
                    RequestData.CityRecord = new CityMaster();
                    RequestData.CityRecord.ID = _ICityMasterView.ID;
                    RequestData.CityRecord.CityCode = _ICityMasterView.CityCode;
                    RequestData.CityRecord.CityName = _ICityMasterView.CityName;
                    RequestData.CityRecord.StateID = _ICityMasterView.StateID;
                    RequestData.CityRecord.StateName = _ICityMasterView.State;
                    RequestData.CityRecord.UpdateBy = _ICityMasterView.UserID;
                    RequestData.CityRecord.UpdateOn = DateTime.Now;
                    RequestData.CityRecord.Active = _ICityMasterView.Active;
                    RequestData.CityRecord.Remarks = _ICityMasterView.Remarks;
                    RequestData.CityRecord.SCN = _ICityMasterView.SCN;
                    var ResponseData = _CityMasterBLL.UpdateCity(RequestData);
                    _ICityMasterView.ProcessStatus = ResponseData.StatusCode;
                    _ICityMasterView.Message = ResponseData.DisplayMessage;

                }
                else
                {
                    _ICityMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        }
    public class CityMasterListPresenter
    {
        CityMasterBLL _CityMasterBLL = new CityMasterBLL();
        ICityMasterCollectionView _ICityMasterCollectionView;
        
        public CityMasterListPresenter(ICityMasterCollectionView ViewObj)
        {
            _ICityMasterCollectionView = ViewObj;
        }

        public void GetCityList()
        {
            try
            {
                var RequestData = new SelectAllCityRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCityResponse();
                ResponseData = _CityMasterBLL.SelectAllRecordCityMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICityMasterCollectionView.CityList = ResponseData.CityList;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }
