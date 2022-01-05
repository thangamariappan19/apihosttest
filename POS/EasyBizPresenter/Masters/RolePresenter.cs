using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.RoleResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class RolePresenter
    {
        IRoleView _IRoleMasterView;
        RoleBLL _RoleMasterBLL = new RoleBLL();

        public RolePresenter(IRoleView ViewObj)
        {
            _IRoleMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IRoleMasterView.RoleCode.Trim() == string.Empty)
            {
                _IRoleMasterView.Message = "Role Code is missing Please Enter it.";
            }
            else if (_IRoleMasterView.RoleCode.Length > 8)
            {
                _IRoleMasterView.Message = "Please Enter Valid Code";
            }
            else if (_IRoleMasterView.RoleName.Trim() == string.Empty)
            {
                _IRoleMasterView.Message = "Role Name is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveRoleMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveRoleRequest();
                RequestData.RoleMasterData = new RoleMaster();

                RequestData.RoleMasterData.ID = _IRoleMasterView.ID;
                RequestData.RoleMasterData.RoleCode = _IRoleMasterView.RoleCode;
                RequestData.RoleMasterData.RoleName = _IRoleMasterView.RoleName;
                RequestData.RoleMasterData.CreateBy = _IRoleMasterView.UserID;
                RequestData.RoleMasterData.CreateOn = DateTime.Now;
                RequestData.RoleMasterData.Active = _IRoleMasterView.Active;
                RequestData.RoleMasterData.SCN = _IRoleMasterView.SCN;
                RequestData.RoleMasterData.Remarks = _IRoleMasterView.Remarks;

                var ResponseData = _RoleMasterBLL.SaveRoleMaster(RequestData);

                _IRoleMasterView.Message = ResponseData.DisplayMessage;
                _IRoleMasterView.ProcessStatus = ResponseData.StatusCode;

            }
            else
            {
                _IRoleMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRoleMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateRoleRequest();
                RequestData.RoleMasterData = new RoleMaster();
                RequestData.RoleMasterData.ID = _IRoleMasterView.ID;
                RequestData.RoleMasterData.RoleCode = _IRoleMasterView.RoleCode;
                RequestData.RoleMasterData.RoleName = _IRoleMasterView.RoleName;
                RequestData.RoleMasterData.UpdateBy = _IRoleMasterView.UserID;
                RequestData.RoleMasterData.UpdateOn = DateTime.Now;
                RequestData.RoleMasterData.Active = _IRoleMasterView.Active;
                RequestData.RoleMasterData.SCN = _IRoleMasterView.SCN;
                RequestData.RoleMasterData.Remarks = _IRoleMasterView.Remarks;
             
                var ResponseData = _RoleMasterBLL.UpdateRoleMaster(RequestData);

                _IRoleMasterView.Message = ResponseData.DisplayMessage;
                _IRoleMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IRoleMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteRoleMaster()
        {
            try
        {
            var RequestData = new DeleteRoleRequest();
            RequestData.ID = -_IRoleMasterView.ID;
            var ResponseData = _RoleMasterBLL.DeleteRoleMaster(RequestData);
            _IRoleMasterView.Message = ResponseData.DisplayMessage;
            _IRoleMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectRoleMaster()
        {
            try
        {
            var RequestData = new SelectByIDRoleRequest();
            RequestData.ID = _IRoleMasterView.ID;
            var ResponseData = _RoleMasterBLL.SelectRoleMaster(RequestData);
            _IRoleMasterView.RoleCode = ResponseData.RoleMasterRecord.RoleCode;
            _IRoleMasterView.RoleName = ResponseData.RoleMasterRecord.RoleName;
            _IRoleMasterView.Remarks = ResponseData.RoleMasterRecord.Remarks;
            _IRoleMasterView.Active = ResponseData.RoleMasterRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IRoleMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IRoleMasterView.Message = ResponseData.DisplayMessage;
            }

            _IRoleMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectAllRoleMaster()
         {
            var RequestData = new SelectAllRoleRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _RoleMasterBLL.SelectAllRoleMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IRoleMasterView.RoleMasterList = ResponseData.RoleMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _IRoleMasterView.Message = ResponseData.DisplayMessage;
            }
        }
    }

    public class RoleListPresenter
    {
        RoleBLL _RoleMasterBLL = new RoleBLL();
        IRoleList _IRoleMasterList;
        public RoleListPresenter(IRoleList ViewObj)
        {
            _IRoleMasterList = ViewObj;
        }
        public void GetRoleMasterList()
        {
            var RequestData = new SelectAllRoleRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllRoleResponse();
            ResponseData = _RoleMasterBLL.SelectAllRoleMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IRoleMasterList.IRoleMasterList = ResponseData.RoleMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }
    }
}


    


