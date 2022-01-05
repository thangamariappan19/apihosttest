using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters;
using EasyBizIView.Masters.State;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class StateMasterPresenter
    {
        IStateMasterView _IStateMasterView;
        StateMasterBLL _StateMasterBLL = new StateMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        public StateMasterPresenter(IStateMasterView ViewObj)
        {
            _IStateMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStateMasterView.StateCode.Trim() == string.Empty)
            {
                _IStateMasterView.Message = "State Code is missing Please Enter it.";
            }
            else if (_IStateMasterView.StateCode.Length > 8)
            {
                _IStateMasterView.Message = "State Code not allow more than eight Character.";
            }
            else if (_IStateMasterView.StateName.Trim() == string.Empty)
            {
                _IStateMasterView.Message = "State Name is missing Please Enter it.";
            }
            else if (_IStateMasterView.Country.Trim() == string.Empty)
            {
                _IStateMasterView.Message = "Country Name is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveStateMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStateRequest();
                    RequestData.StateRecord = new StateMaster();

                    RequestData.StateRecord.ID = _IStateMasterView.ID;
                    RequestData.StateRecord.StateCode = _IStateMasterView.StateCode;
                    RequestData.StateRecord.StateName = _IStateMasterView.StateName;
                    RequestData.StateRecord.CountryName = _IStateMasterView.Country;
                    RequestData.StateRecord.CountryID = _IStateMasterView.CountryID;
                    RequestData.StateRecord.CreateBy = _IStateMasterView.UserID;
                    RequestData.StateRecord.CreateOn = DateTime.Now;
                    RequestData.StateRecord.Active = _IStateMasterView.Active;
                    RequestData.StateRecord.Remarks = _IStateMasterView.Remarks;
                    RequestData.StateRecord.SCN = _IStateMasterView.SCN;

                    var ResponseData = _StateMasterBLL.SaveStateMaster(RequestData);

                    _IStateMasterView.Message = ResponseData.DisplayMessage;
                    _IStateMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IStateMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateState()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateStateRequest();
                    RequestData.StateRecord = new StateMaster();
                    RequestData.StateRecord.ID = _IStateMasterView.ID;
                    RequestData.StateRecord.StateCode = _IStateMasterView.StateCode;
                    RequestData.StateRecord.StateName = _IStateMasterView.StateName;
                    RequestData.StateRecord.CountryID = _IStateMasterView.CountryID;
                    RequestData.StateRecord.CountryName = _IStateMasterView.Country;
                    RequestData.StateRecord.UpdateBy = _IStateMasterView.UserID;
                    RequestData.StateRecord.UpdateOn = DateTime.Now;
                    RequestData.StateRecord.Active = _IStateMasterView.Active;
                    RequestData.StateRecord.Remarks = _IStateMasterView.Remarks;
                    RequestData.StateRecord.SCN = _IStateMasterView.SCN;
                    var ResponseData = _StateMasterBLL.UpdateState(RequestData);
                    _IStateMasterView.ProcessStatus = ResponseData.StatusCode;
                    _IStateMasterView.Message = ResponseData.DisplayMessage;
                
                }
                else
                {
                    _IStateMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectStateRecord()
        {
            try
            {
                var RequestData = new SelectByStateIDRequest();
                RequestData.ID = _IStateMasterView.ID;
                var ResponseData = _StateMasterBLL.SelectStateRecord(RequestData);
                _IStateMasterView.StateCode = ResponseData.StateMasterRecord.StateCode;
                _IStateMasterView.StateName = ResponseData.StateMasterRecord.StateName;
                _IStateMasterView.CountryID = ResponseData.StateMasterRecord.CountryID;
                _IStateMasterView.Country = ResponseData.StateMasterRecord.CountryName;
                _IStateMasterView.SCN = ResponseData.StateMasterRecord.SCN;
                _IStateMasterView.Remarks = ResponseData.StateMasterRecord.Remarks;
                _IStateMasterView.Active = ResponseData.StateMasterRecord.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStateMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IStateMasterView.Message = ResponseData.DisplayMessage;
                }

                _IStateMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteStateMaster()
        {
            try
            {
                var RequestData = new DeleteStateRequest();
                RequestData.ID = _IStateMasterView.ID;
                var ResponseData = _StateMasterBLL.DeleteStateMaster(RequestData);
                _IStateMasterView.Message = ResponseData.DisplayMessage;
                _IStateMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCountryLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStateMasterView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
public class StateMasterListPresenter
    {
         StateMasterBLL _StateMasterBLL = new StateMasterBLL();
        IStateMasterCollectionView _IStateMasterCollectionView;
        public StateMasterListPresenter(IStateMasterCollectionView ViewObj)
        {
            _IStateMasterCollectionView = ViewObj;
        }


        public void GetStateList()
        {
            try
            {
                var RequestData = new SelectAllStateRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStateResponse();
                ResponseData = _StateMasterBLL.SelectAllRecordStateMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStateMasterCollectionView.StateList = ResponseData.StateList;
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

