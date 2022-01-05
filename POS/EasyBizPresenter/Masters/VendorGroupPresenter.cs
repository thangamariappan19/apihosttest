using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.VendorGroup;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class VendorGroupPresenter
    {
        IVendorGroupView _IVendorGroupView;
        VendorGroupMasterBLL _VendorGroupMasterBLL = new VendorGroupMasterBLL();
        public VendorGroupPresenter(IVendorGroupView ViewObj)
        {
            _IVendorGroupView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IVendorGroupView.VendorGroupCode.Trim() == string.Empty)
            {
                _IVendorGroupView.Message = "Vendor Code is missing Please Enter it.";
            }
            else if (_IVendorGroupView.VendorGroupCode.Length > 8)
            {
                _IVendorGroupView.Message = " Vendor Code not allow more than eight Character.";
            }
            else if (_IVendorGroupView.VendorGroupName.Trim() == string.Empty)
            {
                _IVendorGroupView.Message = "Vendor Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveVendorGroup()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveVendorGroupRequest();
                    RequestData.VendorGroupRecord = new VendorGroupMaster();

                    RequestData.VendorGroupRecord.ID = _IVendorGroupView.ID;
                    RequestData.VendorGroupRecord.VendorGroupCode = _IVendorGroupView.VendorGroupCode;
                    RequestData.VendorGroupRecord.VendorGroupName = _IVendorGroupView.VendorGroupName;
                    RequestData.VendorGroupRecord.CreateBy = _IVendorGroupView.UserID;
                    RequestData.VendorGroupRecord.CreateOn = DateTime.Now;
                    RequestData.VendorGroupRecord.Active = _IVendorGroupView.Active;
                    RequestData.VendorGroupRecord.SCN = _IVendorGroupView.SCN;
                    RequestData.VendorGroupRecord.Remarks = _IVendorGroupView.Remarks;
                    var ResponseData = _VendorGroupMasterBLL.SaveVendorGroup(RequestData);
                    _IVendorGroupView.Message = ResponseData.DisplayMessage;
                    _IVendorGroupView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IVendorGroupView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateVendorGroup()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateVendorGroupRequest();
                    RequestData.VendorGroupRecord = new VendorGroupMaster();
                    RequestData.VendorGroupRecord.ID = _IVendorGroupView.ID;
                    RequestData.VendorGroupRecord.VendorGroupCode = _IVendorGroupView.VendorGroupCode;
                    RequestData.VendorGroupRecord.VendorGroupName = _IVendorGroupView.VendorGroupName;
                    RequestData.VendorGroupRecord.UpdateBy = _IVendorGroupView.UserID;
                    RequestData.VendorGroupRecord.UpdateOn = DateTime.Now;
                    RequestData.VendorGroupRecord.Active = true;
                    RequestData.VendorGroupRecord.SCN = _IVendorGroupView.SCN;
                    RequestData.VendorGroupRecord.Remarks = _IVendorGroupView.Remarks;
                    RequestData.VendorGroupRecord.Active = _IVendorGroupView.Active;
                    var ResponseData = _VendorGroupMasterBLL.UpdateVendorGroup(RequestData);
                    _IVendorGroupView.Message = ResponseData.DisplayMessage;
                    _IVendorGroupView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IVendorGroupView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectVendorGroupRecord()
        {
            try
            {
                var RequestData = new SelectByVendorGroupIDRequest();
                RequestData.ID = _IVendorGroupView.ID;
                var ResponseData = _VendorGroupMasterBLL.SelectVendorGroupRecord(RequestData);
                _IVendorGroupView.VendorGroupCode = ResponseData.VendorGroupRecord.VendorGroupCode;
                _IVendorGroupView.VendorGroupName = ResponseData.VendorGroupRecord.VendorGroupName;
                _IVendorGroupView.SCN = ResponseData.VendorGroupRecord.SCN;
                _IVendorGroupView.Remarks = ResponseData.VendorGroupRecord.Remarks;
                _IVendorGroupView.Active = ResponseData.VendorGroupRecord.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IVendorGroupView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IVendorGroupView.Message = ResponseData.DisplayMessage;
                }
                _IVendorGroupView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteVendorGroup()
        {
            try
            {
                var RequestData = new DeleteVendorGroupRequest();
                RequestData.ID = _IVendorGroupView.ID;
                var ResponseData = _VendorGroupMasterBLL.DeleteVendorGroup(RequestData);
                _IVendorGroupView.Message = ResponseData.DisplayMessage;
                _IVendorGroupView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
    

    }
    public class VendorGroupMasterListPresenter
    {
        
        VendorGroupMasterBLL _VendorGroupMasterBLL = new VendorGroupMasterBLL();
        IVendorGroupCollectionView _IVendorGroupCollectionView;
        public VendorGroupMasterListPresenter(IVendorGroupCollectionView ViewObj)
        {
            _IVendorGroupCollectionView = ViewObj;
        }
        public void GetVendorGroupList()
        {
            try
            {
                var RequestData = new SelectAllVendorGroupRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllVendorGroupResponse();
                ResponseData = _VendorGroupMasterBLL.SelectAllRecordVendorGroup(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IVendorGroupCollectionView.VendorGroupList = ResponseData.VendorGroupList;
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
