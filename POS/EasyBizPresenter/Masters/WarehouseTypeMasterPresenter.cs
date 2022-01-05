using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IWarehouseTypeMaster;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class WarehouseTypeMasterPresenter
    {
        
        IWarehouseTypeMasterView _IWarehouseTypeMasterView;
        WarehouseTypeMasterBLL _WarehouseTypeMasterBLL = new WarehouseTypeMasterBLL();

        public WarehouseTypeMasterPresenter(IWarehouseTypeMasterView ViewObj)
        {
            _IWarehouseTypeMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IWarehouseTypeMasterView.WarehouseTypeCode.Trim() == string.Empty)
            {
                _IWarehouseTypeMasterView.Message = "WarehouseTypeCode is missing Please Enter it.";
            }
            else if (_IWarehouseTypeMasterView.WarehouseTypeName.Trim() == string.Empty)
            {
                _IWarehouseTypeMasterView.Message = "Please Enter WarehouseTypeName";
            }
            //else if (_IWarehouseTypeMasterView.Description.Trim() == string.Empty)
            //{
            //    _IWarehouseTypeMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveWarehouseTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveWarehouseTypeMasterRequest();
                RequestData.WarehouseTypMasterData = new WarehouseTypeMaster();

                RequestData.WarehouseTypMasterData.ID = _IWarehouseTypeMasterView.ID;
                RequestData.WarehouseTypMasterData.WarehouseTypeCode = _IWarehouseTypeMasterView.WarehouseTypeCode;
                RequestData.WarehouseTypMasterData.WarehouseTypeName = _IWarehouseTypeMasterView.WarehouseTypeName;                
                RequestData.WarehouseTypMasterData.Description = _IWarehouseTypeMasterView.Description;
                RequestData.WarehouseTypMasterData.CreateBy = _IWarehouseTypeMasterView.UserID;
                RequestData.WarehouseTypMasterData.Remarks = _IWarehouseTypeMasterView.Remarks;
                RequestData.WarehouseTypMasterData.Active = _IWarehouseTypeMasterView.Active;
                //RequestData.WarehouseTypeMasterData.CreateBy = _IWarehouseTypeMasterView.CreateBy;                               
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
              //  RequestData.ProductLineMasterData.Active = true;
                RequestData.WarehouseTypMasterData.SCN = _IWarehouseTypeMasterView.SCN;
                SaveWarehouseTypeMasterResponse ResponseData = _WarehouseTypeMasterBLL.SaveWarehouseTypeMaster(RequestData);
                _IWarehouseTypeMasterView.Message = ResponseData.DisplayMessage;
                _IWarehouseTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IWarehouseTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateWarehouseTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateWarehouseTypeMasterRequest();
                RequestData.WarehouseTypeMasterData = new WarehouseTypeMaster();
                RequestData.WarehouseTypeMasterData.ID = _IWarehouseTypeMasterView.ID;
                RequestData.WarehouseTypeMasterData.WarehouseTypeCode = _IWarehouseTypeMasterView.WarehouseTypeCode;
                RequestData.WarehouseTypeMasterData.WarehouseTypeName = _IWarehouseTypeMasterView.WarehouseTypeName;                
                RequestData.WarehouseTypeMasterData.Description = _IWarehouseTypeMasterView.Description;
                RequestData.WarehouseTypeMasterData.UpdateBy = _IWarehouseTypeMasterView.UserID;
                //RequestData.WarehouseTypeMasterData.UpdateOn = DateTime.Now;
                RequestData.WarehouseTypeMasterData.Active = _IWarehouseTypeMasterView.Active;
                RequestData.WarehouseTypeMasterData.Remarks = _IWarehouseTypeMasterView.Remarks;
                RequestData.WarehouseTypeMasterData.SCN = _IWarehouseTypeMasterView.SCN;
                var ResponseData = _WarehouseTypeMasterBLL.UpdateWarehouseTypeMaster(RequestData);
                _IWarehouseTypeMasterView.Message = ResponseData.DisplayMessage;
                _IWarehouseTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IWarehouseTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteWarehouseTypeMaster()
        {
            try
        
        {

            var RequestData = new DeleteWarehouseTypeMasterRequest ();
            RequestData.ID = _IWarehouseTypeMasterView.ID;
            var ResponseData = _WarehouseTypeMasterBLL.DeleteWarehouseTypeMaster(RequestData);
            _IWarehouseTypeMasterView.Message = ResponseData.DisplayMessage;
            _IWarehouseTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectWarehouseTypeMasterRecord()
        {


            var RequestData = new SelectByIDWarehouseTypeMasterRequest();
            RequestData.ID = _IWarehouseTypeMasterView.ID;

            var ResponseData = _WarehouseTypeMasterBLL.SelectWarehouseTypeMasterRecord(RequestData);
            _IWarehouseTypeMasterView.WarehouseTypeCode = ResponseData.WarehouseTypeMasterRecord.WarehouseTypeCode;
            _IWarehouseTypeMasterView.WarehouseTypeName = ResponseData.WarehouseTypeMasterRecord.WarehouseTypeName;
            _IWarehouseTypeMasterView.Description = ResponseData.WarehouseTypeMasterRecord.Description;
            _IWarehouseTypeMasterView.SCN = ResponseData.WarehouseTypeMasterRecord.SCN;
            _IWarehouseTypeMasterView.Remarks = ResponseData.WarehouseTypeMasterRecord.Remarks;
            _IWarehouseTypeMasterView.Active = ResponseData.WarehouseTypeMasterRecord.Active;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IWarehouseTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IWarehouseTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            _IWarehouseTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class WarehouseTypeMasterListPresenter
    {

        WarehouseTypeMasterBLL _WarehouseTypeMasterBLL = new WarehouseTypeMasterBLL();
        
        IWarehouseTypeMasterList _IWarehouseTypeMasterList;

        public WarehouseTypeMasterListPresenter(IWarehouseTypeMasterList ViewObj)
        {
            _IWarehouseTypeMasterList = ViewObj;
        }

        public void GetWarehouseTypeMasterList()
        {

            var RequestData = new SelectAllWarehouseTypeMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllWarehouseTypeMasterResponse();
            ResponseData = _WarehouseTypeMasterBLL.SelectAllWarehouseTypeMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IWarehouseTypeMasterList.WarehouseTypeMasterList = ResponseData.WarehouseTypeMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }
 
}
