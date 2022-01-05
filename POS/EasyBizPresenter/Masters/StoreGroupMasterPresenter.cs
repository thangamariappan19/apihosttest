using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IStoreGroup;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse.Masters.StoreGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class StoreGroupMasterPresenter
    {
           IStoreGroupMasterView _IStoreGroupMasterView;
        StoreGroupBLL _StoreGroupMasterBLL = new StoreGroupBLL();

        public StoreGroupMasterPresenter(IStoreGroupMasterView ViewObj)
        {
            _IStoreGroupMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStoreGroupMasterView.StoreGroupCode.Trim() == string.Empty)
            {
                _IStoreGroupMasterView.Message = "StoreGroupCode is missing Please Enter it.";
            }
            else if (_IStoreGroupMasterView.StoreGroupName.Trim() == string.Empty)
            {
                _IStoreGroupMasterView.Message = "Please Enter StoreGroupName";
            }
            //else if (_IStoreGroupMasterView.Description.Trim() == string.Empty)
            //{
            //    _IStoreGroupMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveStoreGroupMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveStoreGroupRequest();
                RequestData.StoreGroupDetailsList = _IStoreGroupMasterView.StoreGroupList;

                RequestData.StoreGroupMasterData = new StoreGroupMaster();
                RequestData.StoreGroupMasterData.ID = _IStoreGroupMasterView.ID;
                RequestData.StoreGroupMasterData.StoreGroupCode = _IStoreGroupMasterView.StoreGroupCode;
                RequestData.StoreGroupMasterData.StoreGroupName = _IStoreGroupMasterView.StoreGroupName;
                RequestData.StoreGroupMasterData.Description = _IStoreGroupMasterView.Description;
                RequestData.StoreGroupMasterData.Active = _IStoreGroupMasterView.Active;
                RequestData.StoreGroupMasterData.CreateBy = _IStoreGroupMasterView.UserID;    
              
                RequestData.StoreGroupMasterData.SCN = _IStoreGroupMasterView.SCN;
                SaveStoreGroupResponse ResponseData = _StoreGroupMasterBLL.SaveStoreGroupMaster(RequestData);
                _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
                _IStoreGroupMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IStoreGroupMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreDetailsList()
        {

            var RequestData = new SelectAllStoreGroupDetailsRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllStoreGroupDetailsResponse();
            ResponseData = _StoreGroupMasterBLL.SelectAllStoreGroupDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreGroupMasterView.StoreGroupList = ResponseData.StoreGroupDetailsList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
        public void UpdateStoreGroupMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateStoreGroupRequest();
                RequestData.StoreGroupMasterData = new StoreGroupMaster();
                RequestData.StoreGroupMasterData.ID = _IStoreGroupMasterView.ID;
                RequestData.StoreGroupMasterData.StoreGroupCode = _IStoreGroupMasterView.StoreGroupCode;
                RequestData.StoreGroupMasterData.StoreGroupName = _IStoreGroupMasterView.StoreGroupName;                
                RequestData.StoreGroupMasterData.Description = _IStoreGroupMasterView.Description;
                RequestData.StoreGroupMasterData.Active = _IStoreGroupMasterView.Active;
                RequestData.StoreGroupMasterData.UpdateBy = _IStoreGroupMasterView.UserID;
                //RequestData.StoreGroupMasterData.UpdateOn = DateTime.Now;
                //RequestData.StoreGroupMasterData.Active = true;
                RequestData.StoreGroupMasterData.SCN = _IStoreGroupMasterView.SCN;
                var ResponseData = _StoreGroupMasterBLL.UpdateStoreGroupMaster(RequestData);
                _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
                _IStoreGroupMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IStoreGroupMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteStoreGroupMaster()
        {

            var RequestData = new DeleteStoreGroupRequest ();
            RequestData.ID = _IStoreGroupMasterView.ID;
            var ResponseData = _StoreGroupMasterBLL.DeleteStoreGroupMaster(RequestData);
            _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
            _IStoreGroupMasterView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectStoreGroupDetails()
        {
            SelectStoreGroupDetailsRequest RequestData = new SelectStoreGroupDetailsRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IStoreGroupMasterView.ID;
            SelectStoreGroupDetailsResponse ResponseData = _StoreGroupMasterBLL.SelectStoreGroupDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreGroupMasterView.StoreGroupList = ResponseData.StoreGroupDetailsList;
            }
            else
            {
                _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
                _IStoreGroupMasterView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStoreGroupMasterRecord()
        {


            var RequestData = new SelectByIDStoreGroupRequest();
            RequestData.ID = _IStoreGroupMasterView.ID;

            var ResponseData = _StoreGroupMasterBLL.SelectStoreGroupMasterRecord(RequestData);
            _IStoreGroupMasterView.ID = ResponseData.StoreGroupMasterRecord.ID;
            _IStoreGroupMasterView.StoreGroupCode = ResponseData.StoreGroupMasterRecord.StoreGroupCode;
            _IStoreGroupMasterView.StoreGroupName = ResponseData.StoreGroupMasterRecord.StoreGroupName;
            _IStoreGroupMasterView.Description = ResponseData.StoreGroupMasterRecord.Description;
            _IStoreGroupMasterView.Active = ResponseData.StoreGroupMasterRecord.Active;
            _IStoreGroupMasterView.SCN = ResponseData.StoreGroupMasterRecord.SCN;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IStoreGroupMasterView.Message = ResponseData.DisplayMessage;
            }

            _IStoreGroupMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class StoreGroupMasterListPresenter
    {

        StoreGroupBLL _StoreGroupMasterBLL = new StoreGroupBLL();
        
        IStoreGroupMasterList _IStoreGroupMasterList;

        public StoreGroupMasterListPresenter(IStoreGroupMasterList ViewObj)
        {
            _IStoreGroupMasterList = ViewObj;
        }

        public void GetStoreGroupMasterList()
        {

            var RequestData = new SelectAllStoreGroupRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllStoreGroupResponse();
            ResponseData = _StoreGroupMasterBLL.SelectAllStoreGroupMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreGroupMasterList.StoreGroupMasterList = ResponseData.StoreGroupMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }

       
    }
    
}
