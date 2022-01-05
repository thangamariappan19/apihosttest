using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IAllocationType;
using EasyBizRequest.Masters.AllocationTypeMasterRequest;
using EasyBizResponse.Masters.AllocationTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class AllocationTypeMasterPresenter
    {
        IAllocationTypeMasterView _IAllocationTypeMasterView;
        AllocationTypeMasterBLL _AllocationTypeMasterBLL = new AllocationTypeMasterBLL();

        public AllocationTypeMasterPresenter(IAllocationTypeMasterView ViewObj)
        {
            _IAllocationTypeMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IAllocationTypeMasterView.AllocationTypeCode.Trim() == string.Empty)
            {
                _IAllocationTypeMasterView.Message = "AllocationTypeCode is missing Please Enter it.";
            }
            else if (_IAllocationTypeMasterView.AllocationTypeName.Trim() == string.Empty)
            {
                _IAllocationTypeMasterView.Message = "Please Enter AllocationTypeName";
            }
            //else if (_IAllocationTypeMasterView.Description.Trim() == string.Empty)
            //{
            //    _IAllocationTypeMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveAllocationTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveAllocationTypeMasterRequest();
                RequestData.AllocationTypeMasterData = new AllocationTypeMaster();
                RequestData.AllocationTypeMasterData.AllocationTypeCode = _IAllocationTypeMasterView.AllocationTypeCode;
                RequestData.AllocationTypeMasterData.AllocationTypeName = _IAllocationTypeMasterView.AllocationTypeName;
                RequestData.AllocationTypeMasterData.Description = _IAllocationTypeMasterView.Description;
                RequestData.AllocationTypeMasterData.Remarks = _IAllocationTypeMasterView.Remarks;
                RequestData.AllocationTypeMasterData.CreateBy = _IAllocationTypeMasterView.UserID;
                RequestData.AllocationTypeMasterData.Active = _IAllocationTypeMasterView.Active;
                //RequestData.AllocationTypeMasterData.CreateBy = _IAllocationTypeMasterView.CreateBy;                               
                // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
                //  RequestData.ProductLineMasterData.Active = true;
                RequestData.AllocationTypeMasterData.SCN = _IAllocationTypeMasterView.SCN;
                SaveAllocationTypeMasterResponse ResponseData = _AllocationTypeMasterBLL.SaveAllocationTypeMaster(RequestData);
                _IAllocationTypeMasterView.Message = ResponseData.DisplayMessage;
                _IAllocationTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IAllocationTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAllocationTypeMaster()
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateAllocationTypeMasterRequest();
                RequestData.AllocationTypeMasterData = new AllocationTypeMaster();
                RequestData.AllocationTypeMasterData.ID = _IAllocationTypeMasterView.ID;
                RequestData.AllocationTypeMasterData.AllocationTypeCode = _IAllocationTypeMasterView.AllocationTypeCode;
                RequestData.AllocationTypeMasterData.AllocationTypeName = _IAllocationTypeMasterView.AllocationTypeName;
                RequestData.AllocationTypeMasterData.Description = _IAllocationTypeMasterView.Description;
                RequestData.AllocationTypeMasterData.Remarks = _IAllocationTypeMasterView.Remarks;
                RequestData.AllocationTypeMasterData.UpdateBy = _IAllocationTypeMasterView.UserID;
                //RequestData.AllocationTypeMasterData.UpdateOn = DateTime.Now;
                RequestData.AllocationTypeMasterData.Active = _IAllocationTypeMasterView.Active;
                RequestData.AllocationTypeMasterData.SCN = _IAllocationTypeMasterView.SCN;
                var ResponseData = _AllocationTypeMasterBLL.UpdateAllocationTypeMaster(RequestData);
                _IAllocationTypeMasterView.Message = ResponseData.DisplayMessage;
                _IAllocationTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IAllocationTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteAllocationTypeMaster()
        {
            try
        {

            var RequestData = new DeleteAllocationTypeMasterRequest();
            RequestData.ID = _IAllocationTypeMasterView.ID;
            var ResponseData = _AllocationTypeMasterBLL.DeleteAllocationTypeMaster(RequestData);
            _IAllocationTypeMasterView.Message = ResponseData.DisplayMessage;
            _IAllocationTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectAllocationTypeMasterRecord()
        {


            var RequestData = new SelectByIDAllocationTypeMasterRequest();
            RequestData.ID = _IAllocationTypeMasterView.ID;

            var ResponseData = _AllocationTypeMasterBLL.SelectAllocationTypeMasterRecord(RequestData);
            _IAllocationTypeMasterView.AllocationTypeCode = ResponseData.AllocationTypeMasterRecord.AllocationTypeCode;
            _IAllocationTypeMasterView.AllocationTypeName = ResponseData.AllocationTypeMasterRecord.AllocationTypeName;
            _IAllocationTypeMasterView.Description = ResponseData.AllocationTypeMasterRecord.Description;
            _IAllocationTypeMasterView.Remarks = ResponseData.AllocationTypeMasterRecord.Remarks;
            _IAllocationTypeMasterView.Active = ResponseData.AllocationTypeMasterRecord.Active;
            _IAllocationTypeMasterView.SCN = ResponseData.AllocationTypeMasterRecord.SCN;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IAllocationTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IAllocationTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            _IAllocationTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class AllocationTypeMasterListPresenter
    {

        AllocationTypeMasterBLL _AllocationTypeMasterBLL = new AllocationTypeMasterBLL();

        IAllocationTypeMasterList _IAllocationTypeMasterList;

        public AllocationTypeMasterListPresenter(IAllocationTypeMasterList ViewObj)
        {
            _IAllocationTypeMasterList = ViewObj;
        }

        public void GetAllocationTypeMasterList()
        {

            var RequestData = new SelectAllAllocationTypeMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllAllocationTypeMasterResponse();
            ResponseData = _AllocationTypeMasterBLL.SelectAllAllocationTypeMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IAllocationTypeMasterList.AllocationTypeMasterList = ResponseData.AllocationTypeMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }

}

