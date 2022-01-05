using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ISeason;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizResponse.Masters.SeasonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class SeasonPresenter
    {
        ISeasonview _ISeasonview;
        SeasonBLL _SeasonBLL = new SeasonBLL();

        public SeasonPresenter(ISeasonview ViewObj)
        {
            _ISeasonview = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objbool = false;
            if (_ISeasonview.SeasonCode.ToString()== "")
            {
                _ISeasonview.Message = "Code is missing Please Enter it.";
            }
            else if (_ISeasonview.SeasonCode.Length > 8)
            {
                _ISeasonview.Message = "Code is not Valid";
            }
            else if (_ISeasonview.SeasonCode.Length < 4)
            {
                _ISeasonview.Message = "Season Code must have 4 characters";
            }
            else if (_ISeasonview.SeasonName == string.Empty)
            {
                _ISeasonview.Message = "Name is missing please Enter it.";
            }
            //else if (_ISeasonview.SeasonDrop == null)
            //{
            //    _ISeasonview.Message = "Season Drop is missing please Enter it.";
            //}
            else if (_ISeasonview.IsSelected == null)
            {
                _ISeasonview.Message = "Season is Not Selected.";
            }
            else
            {
                objbool = true;
            }
            return objbool;
        }
        public void SaveSeason()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveSeasonRequest();
                RequestData.SeasonRecord = new SeasonMaster();

                RequestData.SeasonRecord.ID = _ISeasonview.ID;
                RequestData.SeasonRecord.SeasonCode = _ISeasonview.SeasonCode;
                RequestData.SeasonRecord.SeasonName = _ISeasonview.SeasonName;
                RequestData.SeasonRecord.SeasonDrop = _ISeasonview.SeasonDrop;
                RequestData.SeasonRecord.SeasonStartDate = _ISeasonview.SeasonStartDate;
                RequestData.SeasonRecord.SeasonEndDate = _ISeasonview.SeasonendDate;
                RequestData.SeasonRecord.NoOfWeeks = _ISeasonview.NoOfWeeks;
                RequestData.SeasonRecord.NoOfDays = _ISeasonview.NoOfDays;
                RequestData.SeasonRecord.IsSelected = _ISeasonview.IsSelected;
                RequestData.SeasonRecord.CreateBy = _ISeasonview.UserID;
                RequestData.SeasonRecord.CreateOn = DateTime.Now;
                RequestData.SeasonRecord.Active = _ISeasonview.IsSelected;
                RequestData.SeasonRecord.SCN = _ISeasonview.SCN;

                var ResponseData = _SeasonBLL.SaveSeasonMaster(RequestData);

                _ISeasonview.Message = ResponseData.DisplayMessage;
                _ISeasonview.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ISeasonview.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateSeason()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateSeasonRequest();
                RequestData.SeasonMasterData = new SeasonMaster();

                RequestData.SeasonMasterData.ID = _ISeasonview.ID;
                RequestData.SeasonMasterData.SeasonCode = _ISeasonview.SeasonCode;
                RequestData.SeasonMasterData.SeasonName = _ISeasonview.SeasonName;
                RequestData.SeasonMasterData.SeasonDrop = _ISeasonview.SeasonDrop;
                RequestData.SeasonMasterData.SeasonStartDate = _ISeasonview.SeasonStartDate;
                RequestData.SeasonMasterData.SeasonEndDate = _ISeasonview.SeasonendDate;
                RequestData.SeasonMasterData.NoOfWeeks = _ISeasonview.NoOfWeeks;
                RequestData.SeasonMasterData.NoOfDays = _ISeasonview.NoOfDays;
                RequestData.SeasonMasterData.IsSelected = _ISeasonview.IsSelected;
                RequestData.SeasonMasterData.UpdateBy = _ISeasonview.UserID;
                RequestData.SeasonMasterData.UpdateOn = DateTime.Now;
                RequestData.SeasonMasterData.Active = true;
                RequestData.SeasonMasterData.SCN = _ISeasonview.SCN;
                var ResponseData = _SeasonBLL.UpdateSeasonMaster(RequestData);

                _ISeasonview.Message = ResponseData.DisplayMessage;
                _ISeasonview.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ISeasonview.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteSeason()
        {
            try
        {
            var RequestData = new DeleteSeasonRequest();
            RequestData.ID = -_ISeasonview.ID;
            var ResponseData = _SeasonBLL.DeleteSeasonMaster(RequestData);
            _ISeasonview.Message = ResponseData.DisplayMessage;
            _ISeasonview.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectSeason()
        {
            var RequestData = new SelectBySeasonIDRequest();
            RequestData.ID = _ISeasonview.ID;
            var ResponseData = _SeasonBLL.SelectSeasonMaster(RequestData);
            _ISeasonview.SeasonCode = ResponseData.SeasonMasterRecord.SeasonCode;
            _ISeasonview.SeasonName = ResponseData.SeasonMasterRecord.SeasonName;
            _ISeasonview.SeasonDrop = ResponseData.SeasonMasterRecord.SeasonDrop;
            _ISeasonview.SeasonStartDate = ResponseData.SeasonMasterRecord.SeasonStartDate;
            _ISeasonview.SeasonendDate = ResponseData.SeasonMasterRecord.SeasonEndDate;
            _ISeasonview.NoOfWeeks = ResponseData.SeasonMasterRecord.NoOfWeeks;
            _ISeasonview.NoOfDays = ResponseData.SeasonMasterRecord.NoOfDays;
            //_ISeasonview.IsSelected = ResponseData.SeasonMasterRecord.IsSelected;
            _ISeasonview.IsSelected = ResponseData.SeasonMasterRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ISeasonview.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ISeasonview.Message = ResponseData.DisplayMessage;
            }

            _ISeasonview.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectAllSeason()
        {
            var RequestData = new SelectAllSeasonRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _SeasonBLL.SelectAllSeasonMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ISeasonview.SeasonMasterList = ResponseData.SeasonMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _ISeasonview.Message = ResponseData.DisplayMessage;
            }
        }
    }

    public class SeasonMasterListPresenter
    {
        ISeasonList _ISeasonList;
        SeasonBLL _SeasonBLL = new SeasonBLL();

        public SeasonMasterListPresenter(ISeasonList ViewObj)
        {
            _ISeasonList = ViewObj;
        }
        public void GetSeasonMasterList()
        {
            var RequestData = new SelectAllSeasonRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllSeasonResponse();
            ResponseData = _SeasonBLL.SelectAllSeasonMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ISeasonList.SeasonMasterList = ResponseData.SeasonMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }
    }
}
