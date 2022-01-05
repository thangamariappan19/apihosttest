using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IRequestType;
using EasyBizRequest.Masters.RequestTypeMasterRequest;
using EasyBizResponse.Masters.RequestTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class RequestTypeMasterPresenter
    {
         IRequestTypeMasterView _IRequestTypeMasterView;
         RequestTypeMasterBLL _RequestTypeMasterBLL = new RequestTypeMasterBLL();

        public RequestTypeMasterPresenter(IRequestTypeMasterView ViewObj)
        {
            _IRequestTypeMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IRequestTypeMasterView.RequestTypeCode.Trim() == string.Empty)
            {
                _IRequestTypeMasterView.Message = "RequestTypeCode is missing Please Enter it.";
            }
            else if (_IRequestTypeMasterView.RequestTypeName.Trim() == string.Empty)
            {
                _IRequestTypeMasterView.Message = "Please Enter RequestTypeName";
            }
            //else if (_IRequestTypeMasterView.Description.Trim() == string.Empty)
            //{
            //    _IRequestTypeMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveRequestTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveRequestTypeMasterRequest();
                RequestData.RequestTypeMasterData = new RequestTypeMaster();
                RequestData.RequestTypeMasterData.RequestTypeCode = _IRequestTypeMasterView.RequestTypeCode;
                RequestData.RequestTypeMasterData.RequestTypeName = _IRequestTypeMasterView.RequestTypeName;                
                RequestData.RequestTypeMasterData.Description = _IRequestTypeMasterView.Description;
                RequestData.RequestTypeMasterData.CreateBy = _IRequestTypeMasterView.UserID;
                RequestData.RequestTypeMasterData.Active = _IRequestTypeMasterView.Active;
                //RequestData.RequestTypeMasterData.CreateBy = _IRequestTypeMasterView.CreateBy;                               
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
              //  RequestData.ProductLineMasterData.Active = true;
                RequestData.RequestTypeMasterData.SCN = _IRequestTypeMasterView.SCN;
                SaveRequestTypeMasterResponse ResponseData = _RequestTypeMasterBLL.SaveRequestTypeMaster(RequestData);
                _IRequestTypeMasterView.Message = ResponseData.DisplayMessage;
                _IRequestTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IRequestTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRequestTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateRequestTypeMasterRequest();
                RequestData.RequestTypeMasterData = new RequestTypeMaster();
                RequestData.RequestTypeMasterData.ID = _IRequestTypeMasterView.ID;
                RequestData.RequestTypeMasterData.RequestTypeCode = _IRequestTypeMasterView.RequestTypeCode;
                RequestData.RequestTypeMasterData.RequestTypeName = _IRequestTypeMasterView.RequestTypeName;                
                RequestData.RequestTypeMasterData.Description = _IRequestTypeMasterView.Description;
                RequestData.RequestTypeMasterData.Active = _IRequestTypeMasterView.Active;
                RequestData.RequestTypeMasterData.UpdateBy = _IRequestTypeMasterView.UserID;
                //RequestData.RequestTypeMasterData.UpdateOn = DateTime.Now;
                //RequestData.RequestTypeMasterData.Active = true;
                RequestData.RequestTypeMasterData.SCN = _IRequestTypeMasterView.SCN;
                var ResponseData = _RequestTypeMasterBLL.UpdateRequestTypeMaster(RequestData);
                _IRequestTypeMasterView.Message = ResponseData.DisplayMessage;
                _IRequestTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IRequestTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteRequestTypeMaster()
        {
            try
        {

            var RequestData = new DeleteRequestTypeMasterRequest ();
            RequestData.ID = _IRequestTypeMasterView.ID;
            var ResponseData = _RequestTypeMasterBLL.DeleteRequestTypeMaster(RequestData);
            _IRequestTypeMasterView.Message = ResponseData.DisplayMessage;
            _IRequestTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectRequestTypeMasterRecord()
        {


            var RequestData = new SelectByIDRequestTypeMasterRequest();
            RequestData.ID = _IRequestTypeMasterView.ID;

            var ResponseData = _RequestTypeMasterBLL.SelectRequestTypeMasterRecord(RequestData);
            _IRequestTypeMasterView.RequestTypeCode = ResponseData.RequestTypeMasterRecord.RequestTypeCode;
            _IRequestTypeMasterView.RequestTypeName = ResponseData.RequestTypeMasterRecord.RequestTypeName;
            _IRequestTypeMasterView.Description = ResponseData.RequestTypeMasterRecord.Description;
            _IRequestTypeMasterView.Active = ResponseData.RequestTypeMasterRecord.Active;
            _IRequestTypeMasterView.SCN = ResponseData.RequestTypeMasterRecord.SCN;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IRequestTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IRequestTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            _IRequestTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class RequestTypeMasterListPresenter
    {

        RequestTypeMasterBLL _RequestTypeMasterBLL = new RequestTypeMasterBLL();
        
        IRequestTypeMasterList _IRequestTypeMasterList;

        public RequestTypeMasterListPresenter(IRequestTypeMasterList ViewObj)
        {
            _IRequestTypeMasterList = ViewObj;
        }

        public void GetRequestTypeMasterList()
        {

            var RequestData = new SelectAllRequestTypeMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllRequestTypeMasterResponse();
            ResponseData = _RequestTypeMasterBLL.SelectAllRequestTypeMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IRequestTypeMasterList.RequestTypeMasterList = ResponseData.RequestTypeMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    } 
}
