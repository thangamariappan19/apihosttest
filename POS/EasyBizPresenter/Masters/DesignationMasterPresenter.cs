using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDesignation;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.DesignationMasterResponse;
using EasyBizResponse.Masters.RoleResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class DesignationMasterPresenter
    { 
        IDesignationMasterView _IDesignationMasterView;
        DesignationMasterBLL _DesignationMasterBLL = new DesignationMasterBLL();

        RoleBLL _RoleMasterBLL = new RoleBLL();

        public DesignationMasterPresenter(IDesignationMasterView ViewObj)
        {
            _IDesignationMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDesignationMasterView.DesignationCode.Trim() == string.Empty)
            {
                _IDesignationMasterView.Message = "DesignationCode is missing Please Enter it.";
            }
            else if (_IDesignationMasterView.DesignationName.Trim() == string.Empty)
            {
                _IDesignationMasterView.Message = "Please Enter DesignationName";
            }
            else if (_IDesignationMasterView.RoleId == 0 )
            {
                _IDesignationMasterView.Message = "Please Select Role";
            }
            //else if (_IDesignationMasterView.Description.Trim() == string.Empty)
            //{
            //    _IDesignationMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveDesignationMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveDesignationMasterRequest();
                RequestData.DesignationMasterData = new DesignationMaster();

                RequestData.DesignationMasterData.ID = _IDesignationMasterView.ID;
                RequestData.DesignationMasterData.DesignationCode = _IDesignationMasterView.DesignationCode;
                RequestData.DesignationMasterData.DesignationName = _IDesignationMasterView.DesignationName;
                RequestData.DesignationMasterData.Description = _IDesignationMasterView.Description;
                RequestData.DesignationMasterData.RoleId = _IDesignationMasterView.RoleId;
                RequestData.DesignationMasterData.RoleCode = _IDesignationMasterView.RoleCode;
                RequestData.DesignationMasterData.CreateBy = _IDesignationMasterView.UserID;
                RequestData.DesignationMasterData.Active = _IDesignationMasterView.Active; 
                //RequestData.DesignationMasterData.CreateBy = _IDesignationMasterView.CreateBy;                               
                // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
                RequestData.DesignationMasterData.Remarks = _IDesignationMasterView.Remarks;
                RequestData.DesignationMasterData.SCN = _IDesignationMasterView.SCN;
                SaveDesignationMasterResponse ResponseData = _DesignationMasterBLL.SaveDesignationMaster(RequestData);
                _IDesignationMasterView.Message = ResponseData.DisplayMessage;
                _IDesignationMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IDesignationMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateDesignationMaster()
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateDesignationMasterRequest();
                RequestData.DesignationMasterData = new DesignationMaster();
                RequestData.DesignationMasterData.ID = _IDesignationMasterView.ID;
                RequestData.DesignationMasterData.DesignationCode = _IDesignationMasterView.DesignationCode;
                RequestData.DesignationMasterData.DesignationName = _IDesignationMasterView.DesignationName;
                RequestData.DesignationMasterData.RoleId = _IDesignationMasterView.RoleId;
                RequestData.DesignationMasterData.RoleCode = _IDesignationMasterView.RoleCode;
                RequestData.DesignationMasterData.Description = _IDesignationMasterView.Description;
                RequestData.DesignationMasterData.UpdateBy = _IDesignationMasterView.UserID;
                //RequestData.DesignationMasterData.UpdateOn = DateTime.Now;
                RequestData.DesignationMasterData.Active = _IDesignationMasterView.Active; 
                RequestData.DesignationMasterData.Remarks = _IDesignationMasterView.Remarks;
                RequestData.DesignationMasterData.SCN = _IDesignationMasterView.SCN;
                var ResponseData = _DesignationMasterBLL.UpdateDesignationMaster(RequestData);
                _IDesignationMasterView.Message = ResponseData.DisplayMessage;
                _IDesignationMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IDesignationMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteDesignationMaster()
        {
            try
        {

            var RequestData = new DeleteDesignationMasterRequest();
            RequestData.ID = _IDesignationMasterView.ID;
            var ResponseData = _DesignationMasterBLL.DeleteDesignationMaster(RequestData);
            _IDesignationMasterView.Message = ResponseData.DisplayMessage;
            _IDesignationMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDesignationMasterRecord()
        {


            var RequestData = new SelectByIDDesignationMasterRequest();
            RequestData.ID = _IDesignationMasterView.ID;

            var ResponseData = _DesignationMasterBLL.SelectDesignationMasterRecord(RequestData);
            _IDesignationMasterView.DesignationCode = ResponseData.DesignationMasterRecord.DesignationCode;
            _IDesignationMasterView.DesignationName = ResponseData.DesignationMasterRecord.DesignationName;
            _IDesignationMasterView.RoleId = ResponseData.DesignationMasterRecord.RoleId;
            _IDesignationMasterView.Description = ResponseData.DesignationMasterRecord.Description;
            _IDesignationMasterView.SCN = ResponseData.DesignationMasterRecord.SCN;
            _IDesignationMasterView.Remarks = ResponseData.DesignationMasterRecord.Remarks;
            _IDesignationMasterView.Active = ResponseData.DesignationMasterRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IDesignationMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IDesignationMasterView.Message = ResponseData.DisplayMessage;
            }

            _IDesignationMasterView.ProcessStatus = ResponseData.StatusCode;
        }
        public void GetRoleMasterLookUP()
        {
            SelectRoleMasterLookUpRequest RequestData = new SelectRoleMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectRoleMasterLookUpResponse ResponseData = _RoleMasterBLL.SelectRoleLookUP(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDesignationMasterView.RoleMasterLookup = ResponseData.RoleMasterList;
            }
        }
    }
    public class DesignationMasterListPresenter
    {

        DesignationMasterBLL _DesignationMasterBLL = new DesignationMasterBLL();

        IDesignationMasterList _IDesignationMasterList;

        public DesignationMasterListPresenter(IDesignationMasterList ViewObj)
        {
            _IDesignationMasterList = ViewObj;
        }

        public void GetDesignationMasterList()
        {

            var RequestData = new SelectAllDesignationMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllDesignationMasterResponse();
            ResponseData = _DesignationMasterBLL.SelectAllDesignationMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDesignationMasterList.DesignationMasterList = ResponseData.DesignationMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }

}

