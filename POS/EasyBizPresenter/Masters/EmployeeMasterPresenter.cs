using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IEmployeeMaster;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizRequest.Masters.EmployeeDiscountInfoRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class EmployeeMasterPresenter
    {
        IEmployeeView _IEmployeeView;
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
        RoleBLL _RoleBLL = new RoleBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        DesignationMasterBLL _DesignationMasterBLL = new DesignationMasterBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public EmployeeMasterPresenter(IEmployeeView ViewObj)
        {
            _IEmployeeView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IEmployeeView.EmployeeCode.Trim() == string.Empty)
            {
                _IEmployeeView.Message = "Employee Code is missing Please Enter it.";
            }
            else if (_IEmployeeView.EmployeeCode.Length > 8)
            {
                _IEmployeeView.Message = " Please Enter Vail Code.";
            }
            else if (_IEmployeeView.EmployeeName.Trim() == string.Empty)
            {
                _IEmployeeView.Message = "Employee Name is missing Please Enter it. ";
            }
            else if (_IEmployeeView.RoleName.Trim() == string.Empty)
            {
                _IEmployeeView.Message = "Role Name is missing Please Enter it. ";
            }
            else if (_IEmployeeView.Designation.Trim() == string.Empty)
            {
                _IEmployeeView.Message = "Designation is missing Please Enter it. ";
            }
            else if (_IEmployeeView.CountryID == 0)
            {
                _IEmployeeView.Message = "Country is Missing ";
            }
            else if (_IEmployeeView.StoreID == 0)
            {
                _IEmployeeView.Message = "Store is missing Please Enter it. ";
            }      
            else if (_IEmployeeView.DateofJoining == null)
            {
                _IEmployeeView.Message = "DateOfJoining is missing Please Enter it.";
            }
            else if (_IEmployeeView.Salary == null)
            {
                _IEmployeeView.Message = "Salary is missing Please Enter it.";
            }
           
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveEmployeeMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveEmployeeMasterRequest();
                RequestData.EmployeeMasterRecord = new EmployeeMaster();

                RequestData.EmployeeMasterRecord.ID = _IEmployeeView.ID;               
                RequestData.EmployeeMasterRecord.EmployeeCode = _IEmployeeView.EmployeeCode;
                RequestData.EmployeeMasterRecord.EmployeeName = _IEmployeeView.EmployeeName;
                RequestData.EmployeeMasterRecord.RoleName = _IEmployeeView.RoleName;
                RequestData.EmployeeMasterRecord.Designation = _IEmployeeView.Designation;
                RequestData.EmployeeMasterRecord.DateofJoining = _IEmployeeView.DateofJoining;
                RequestData.EmployeeMasterRecord.Salary = _IEmployeeView.Salary;
                RequestData.EmployeeMasterRecord.CountryID = _IEmployeeView.CountryID;
                RequestData.EmployeeMasterRecord.StoreID = _IEmployeeView.StoreID;
                RequestData.EmployeeMasterRecord.Address = _IEmployeeView.Address;
                RequestData.EmployeeMasterRecord.PhoneNo = _IEmployeeView.PhoneNo;
                RequestData.EmployeeMasterRecord.CountryCode = _IEmployeeView.CountryCode;
                RequestData.EmployeeMasterRecord.StoreCode = _IEmployeeView.StoreCode;
                RequestData.EmployeeMasterRecord.IsSelection = _IEmployeeView.IsSelection;
                RequestData.EmployeeMasterRecord.CreateBy = _IEmployeeView.UserID;
                RequestData.EmployeeMasterRecord.CreateOn = DateTime.Now;
                RequestData.EmployeeMasterRecord.Active = _IEmployeeView.IsSelection;
                RequestData.EmployeeMasterRecord.SCN = _IEmployeeView.SCN;
                RequestData.EmployeeMasterRecord.Remarks = _IEmployeeView.Remarks;
                RequestData.EmployeeMasterRecord.EmployeeImage = _IEmployeeView.EmployeeImage;
                RequestData.RequestFrom = _IEmployeeView.RequestFrom;
                var ResponseData = _EmployeeMasterBLL.SaveEmployeeMaster(RequestData);

                _IEmployeeView.Message = ResponseData.DisplayMessage;
                _IEmployeeView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IEmployeeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateEmployeeMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateEmployeeMasterRequest();
                RequestData.EmployeeMasterRecord = new EmployeeMaster();
                RequestData.EmployeeMasterRecord.ID = _IEmployeeView.ID;
                RequestData.EmployeeMasterRecord.BaseID = _IEmployeeView.BaseID;
                RequestData.EmployeeMasterRecord.EmployeeCode = _IEmployeeView.EmployeeCode;
                RequestData.EmployeeMasterRecord.EmployeeName = _IEmployeeView.EmployeeName;
                RequestData.EmployeeMasterRecord.Designation = _IEmployeeView.Designation;
                RequestData.EmployeeMasterRecord.RoleName = _IEmployeeView.RoleName;
                RequestData.EmployeeMasterRecord.DateofJoining = _IEmployeeView.DateofJoining;
                RequestData.EmployeeMasterRecord.Salary = _IEmployeeView.Salary;
                RequestData.EmployeeMasterRecord.CountryID = _IEmployeeView.CountryID;
                RequestData.EmployeeMasterRecord.StoreID = _IEmployeeView.StoreID;
                RequestData.EmployeeMasterRecord.Address = _IEmployeeView.Address;
                RequestData.EmployeeMasterRecord.PhoneNo = _IEmployeeView.PhoneNo;
                RequestData.EmployeeMasterRecord.CountryCode = _IEmployeeView.CountryCode;
                RequestData.EmployeeMasterRecord.StoreCode = _IEmployeeView.StoreCode;
                RequestData.EmployeeMasterRecord.IsSelection = _IEmployeeView.IsSelection;
                RequestData.EmployeeMasterRecord.UpdateBy = _IEmployeeView.UserID;
                RequestData.EmployeeMasterRecord.UpdateOn = DateTime.Now;
                RequestData.EmployeeMasterRecord.Active = _IEmployeeView.IsSelection;
                RequestData.EmployeeMasterRecord.SCN = _IEmployeeView.SCN;
                RequestData.EmployeeMasterRecord.Remarks = _IEmployeeView.Remarks;
                RequestData.EmployeeMasterRecord.EmployeeImage = _IEmployeeView.EmployeeImage;
                RequestData.RequestFrom = _IEmployeeView.RequestFrom;

                var ResponseData = _EmployeeMasterBLL.UpdateEmployeeMaster(RequestData);

                _IEmployeeView.Message = ResponseData.DisplayMessage;
                _IEmployeeView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IEmployeeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteEmployeeMaster()
        {
            try
        {
            var RequestData = new DeleteEmployeeMasterRequest();
            RequestData.ID = -_IEmployeeView.ID;
            var ResponseData = _EmployeeMasterBLL.DeleteEmployeeMaster(RequestData);
            _IEmployeeView.Message = ResponseData.DisplayMessage;
            _IEmployeeView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectEmployeeMaster()
        {
            var RequestData = new SelectByIDEmployeeMasterRequest();
            RequestData.ID = _IEmployeeView.ID;
            var ResponseData = _EmployeeMasterBLL.SelectEmployeeMaster(RequestData);
            _IEmployeeView.BaseID = ResponseData.EmployeeMasterRecord.BaseID;
            _IEmployeeView.EmployeeCode = ResponseData.EmployeeMasterRecord.EmployeeCode;
            _IEmployeeView.EmployeeName = ResponseData.EmployeeMasterRecord.EmployeeName;
            _IEmployeeView.Designation = ResponseData.EmployeeMasterRecord.Designation;
            _IEmployeeView.RoleName = ResponseData.EmployeeMasterRecord.RoleName;
            _IEmployeeView.DateofJoining = ResponseData.EmployeeMasterRecord.DateofJoining;
            _IEmployeeView.Salary = ResponseData.EmployeeMasterRecord.Salary;
            _IEmployeeView.CountryID = ResponseData.EmployeeMasterRecord.CountryID;
            _IEmployeeView.StoreID = ResponseData.EmployeeMasterRecord.StoreID;           
            _IEmployeeView.Address = ResponseData.EmployeeMasterRecord.Address;
            _IEmployeeView.PhoneNo = ResponseData.EmployeeMasterRecord.PhoneNo;
            _IEmployeeView.IsSelection = ResponseData.EmployeeMasterRecord.IsSelection;
            _IEmployeeView.Remarks = ResponseData.EmployeeMasterRecord.Remarks;
            _IEmployeeView.EmployeeImage = ResponseData.EmployeeMasterRecord.EmployeeImage;
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IEmployeeView.Message = ResponseData.DisplayMessage;
            }
            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IEmployeeView.Message = ResponseData.DisplayMessage;
            }

            _IEmployeeView.ProcessStatus = ResponseData.StatusCode;
        }
        public void GetRoleLookUp()
        {
            var RequestData = new SelectRoleMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _RoleBLL.SelectRoleLookUP(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IEmployeeView.RoleLookUp = ResponseData.RoleMasterList;
            }
        }
     

        public void GetDesignationLookUp()
        {
            var RequestData = new SelectDesignationMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _DesignationMasterBLL.SelectDesignationMasterLookUp(RequestData);
            if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IEmployeeView.DesignationLookUp = ResponseData.DesignationMasterList;
            }
        }
        public void SelectAllEmployeeMaster()
        {
            var RequestData = new SelectAllEmployeeMasterRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _EmployeeMasterBLL.SelectAllEmployeeMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IEmployeeView.EmployeeMasterList = ResponseData.EmployeeMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _IEmployeeView.Message = ResponseData.DisplayMessage;
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
                    _IEmployeeView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStoreLookUp()
        {
            var RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IEmployeeView.CountryID;
            var ResponseData = _EmployeeMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IEmployeeView.StoreMasterLookUp = ResponseData.StoreMasterList;
            }
        }
        public void SelectEmployeediscountInfoByCustCode()
        {
            var RequestData = new SelectEmployeeDiscountInfoByCustCode();
            // RequestData.ShowInActiveRecords = false;
            RequestData.CustomerCode = "";
            var ResponseData = _EmployeeMasterBLL.SelectEmployeediscountInfoByCustCode(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                //_IEmployeeView.EmployeeMasterList = ResponseData.EmployeeMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
               //_IEmployeeView.Message = ResponseData.DisplayMessage;
            }
        }
    }
}

public class EmployeeMasterListPresenter
{
    IEmployeeList _IEmployeeList;
    EmployeeMasterBLL _EmployeeMAsterBLL = new EmployeeMasterBLL();
    public EmployeeMasterListPresenter(IEmployeeList ViewObj)
    {
        _IEmployeeList = ViewObj;
    }
    public void GetEmployeeMasterList()
    {
        var RequestData = new SelectAllEmployeeMasterRequest();
        RequestData.ShowInActiveRecords = true;
        var ResponseData = new SelectAllEmployeeMasterResponse();
        ResponseData = _EmployeeMAsterBLL.SelectAllEmployeeMaster(RequestData);
        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        {
            _IEmployeeList.EmployeeMasterList = ResponseData.EmployeeMasterList;
        }
        else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
        {

        }
    }
}

