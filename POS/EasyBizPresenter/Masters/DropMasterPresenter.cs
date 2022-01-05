using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDropMaster;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.DropMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class DropMasterPresenter
    {
        IDropMasterView _IDropMasterView;
        DropMasterBLL _DropMasterBLL = new DropMasterBLL();
        public DropMasterPresenter(IDropMasterView ViewObj)
        {
            _IDropMasterView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDropMasterView.DropCode.Trim() == string.Empty)
            {
                _IDropMasterView.Message = "Drop Code is missing Please Enter it.";
            }
            else if (_IDropMasterView.DropCode.Length > 8)
            {
                _IDropMasterView.Message = "Please Enter Valid Code";
            }
            else if (_IDropMasterView.DropCode.Length < 2)
            {
                _IDropMasterView.Message = "Drop Code must have 2 characters";
            }
            else if (_IDropMasterView.DropName.Trim() == string.Empty)
            {
                _IDropMasterView.Message = "Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveDropMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDropMasterRequest();
                    RequestData.DropMasterTypesRecord = new DropMasterTypes();

                    RequestData.DropMasterTypesRecord.ID = _IDropMasterView.ID;
                    RequestData.DropMasterTypesRecord.DropCode = _IDropMasterView.DropCode;
                    RequestData.DropMasterTypesRecord.DropName = _IDropMasterView.DropName;
                    RequestData.DropMasterTypesRecord.CreateBy = 1;
                    RequestData.DropMasterTypesRecord.Remarks = _IDropMasterView.Remarks;
                    RequestData.DropMasterTypesRecord.Active = _IDropMasterView.Active;
                    var ResponseData = _DropMasterBLL.SaveDropMaster(RequestData);
                    _IDropMasterView.Message = ResponseData.DisplayMessage;
                    _IDropMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDropMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }

        public void UpdateDropMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateDropMasterRequest();
                    RequestData.DropMasterTypesRequestData = new DropMasterTypes();
                    RequestData.DropMasterTypesRequestData.ID = _IDropMasterView.ID;
                    RequestData.DropMasterTypesRequestData.DropCode = _IDropMasterView.DropCode;
                    RequestData.DropMasterTypesRequestData.DropName = _IDropMasterView.DropName;
                    RequestData.DropMasterTypesRequestData.SCN = _IDropMasterView.SCN;
                    RequestData.DropMasterTypesRequestData.UpdateBy = 1;
                    RequestData.DropMasterTypesRequestData.Remarks = _IDropMasterView.Remarks;
                    RequestData.DropMasterTypesRequestData.Active = _IDropMasterView.Active;

                    var ResponseData = _DropMasterBLL.UpdateDropMaster(RequestData);
                    _IDropMasterView.Message = ResponseData.DisplayMessage;
                    _IDropMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDropMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

        }

        public void DeleteDropMasterView()
        {
            try
            {
                var RequestData = new DeleteDropMasterRequest();

                RequestData.ID = _IDropMasterView.ID;
                var ResponseData = _DropMasterBLL.DeleteDropMaster(RequestData);
                _IDropMasterView.Message = ResponseData.DisplayMessage;
                _IDropMasterView.ProcessStatus = ResponseData.StatusCode;
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
         

        }


        public void SelectByIDDropMaster()
        {
            try
            {
                var RequestData = new SelectByIDDropMasterRequest();
                RequestData.ID = _IDropMasterView.ID;
                var ResponseData = _DropMasterBLL.SelectByIDDropMaster(RequestData);
                _IDropMasterView.DropCode = ResponseData.DropMasterTypesData.DropCode;
                _IDropMasterView.DropName = ResponseData.DropMasterTypesData.DropName;
                _IDropMasterView.SCN = ResponseData.DropMasterTypesData.SCN;
                _IDropMasterView.Remarks = ResponseData.DropMasterTypesData.Remarks;
                _IDropMasterView.Active = ResponseData.DropMasterTypesData.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IDropMasterView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IDropMasterView.Message = ResponseData.DisplayMessage;
                }

                _IDropMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class DropMasterPresenterList
    {
        IDropMasterViewList _IDropMasterViewList;
        DropMasterBLL _DropMasterBLL = new DropMasterBLL();
        public DropMasterPresenterList(IDropMasterViewList ViewObj)
        {
            _IDropMasterViewList = ViewObj;
        }

        public void SelectAllDropMaster()
        {

            try
            {
                var RequestData = new SelectAllDropMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllDropMasterResponse();

                ResponseData = _DropMasterBLL.SelectAllDropMaster(RequestData);


                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDropMasterViewList.DropMasterTypesList = ResponseData.DropMasterTypesList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    _IDropMasterViewList.DropMasterTypesList = ResponseData.DropMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
