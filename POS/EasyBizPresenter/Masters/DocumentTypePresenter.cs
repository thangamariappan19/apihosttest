using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDocumentType;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizResponse.Masters.DocumentTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class DocumentTypePresenter
    {
        IDocumentTypeView _IDocumentTypeView;
        DocumentTypeBLL _DocumentTypeBLL = new DocumentTypeBLL();
        public DocumentTypePresenter(IDocumentTypeView ViewObj)
        {
            _IDocumentTypeView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDocumentTypeView.DocumentCode.Trim() == string.Empty)
            {
                _IDocumentTypeView.Message = " Code is missing Please Enter it.";
            }
            else if (_IDocumentTypeView.DocumentCode.Length > 8)
            {
                _IDocumentTypeView.Message = " Please Enter Vail Code.";
            }
            else if (_IDocumentTypeView.DocumentName.Trim() == string.Empty)
            {
                _IDocumentTypeView.Message = "DocumentType Name is missing Please Enter it. ";
            }
            else if (_IDocumentTypeView.Description.Trim() == string.Empty)
            {
                _IDocumentTypeView.Message = "Description is missing Please Enter it. ";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveDocumentType()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveDocumentTypeRequest();
                RequestData.DocumentTypeRecord = new DocumentTypes();

                RequestData.DocumentTypeRecord.DocumentCode = _IDocumentTypeView.DocumentCode;
                RequestData.DocumentTypeRecord.DocumentName = _IDocumentTypeView.DocumentName;
                RequestData.DocumentTypeRecord.Description = _IDocumentTypeView.Description;
                RequestData.DocumentTypeRecord.CreateBy = _IDocumentTypeView.UserID;
                RequestData.DocumentTypeRecord.CreateOn = DateTime.Now;
                RequestData.DocumentTypeRecord.Active = _IDocumentTypeView.Active;
                RequestData.DocumentTypeRecord.SCN = _IDocumentTypeView.SCN;

                var ResponseData = _DocumentTypeBLL.SaveDocumentType(RequestData);

                _IDocumentTypeView.Message = ResponseData.DisplayMessage;
                _IDocumentTypeView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IDocumentTypeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateDocumentType()
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateDocumentTypeRequest();
                RequestData.DocumentTypeRecord = new DocumentTypes();
                RequestData.DocumentTypeRecord.ID = _IDocumentTypeView.ID;
                RequestData.DocumentTypeRecord.DocumentCode = _IDocumentTypeView.DocumentCode;
                RequestData.DocumentTypeRecord.DocumentName = _IDocumentTypeView.DocumentName;
                RequestData.DocumentTypeRecord.Description = _IDocumentTypeView.Description;
                RequestData.DocumentTypeRecord.UpdateBy = _IDocumentTypeView.UserID;
                RequestData.DocumentTypeRecord.UpdateOn = DateTime.Now;
                RequestData.DocumentTypeRecord.Active = _IDocumentTypeView.Active;
                RequestData.DocumentTypeRecord.SCN = _IDocumentTypeView.SCN;
              
                var ResponseData = _DocumentTypeBLL.UpdateDocumentType(RequestData);

                _IDocumentTypeView.Message = ResponseData.DisplayMessage;
                _IDocumentTypeView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IDocumentTypeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteDocumentType()
        {
            try
        {
            var RequestData = new DeleteDocumentTypeRequest();
            RequestData.ID = -_IDocumentTypeView.ID;
            var ResponseData = _DocumentTypeBLL.DeleteDocumentType(RequestData);
            _IDocumentTypeView.Message = ResponseData.DisplayMessage;
            _IDocumentTypeView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDocumentType()
        {
            var RequestData = new SelectByIDDocumentTypeRequest();
            RequestData.ID = _IDocumentTypeView.ID;
            var ResponseData = _DocumentTypeBLL.SelectDocumentType(RequestData);
            _IDocumentTypeView.DocumentCode = ResponseData.DocumentTypeRecord.DocumentCode;
            _IDocumentTypeView.DocumentName = ResponseData.DocumentTypeRecord.DocumentName;
            _IDocumentTypeView.Description = ResponseData.DocumentTypeRecord.Description;
            _IDocumentTypeView.Active = ResponseData.DocumentTypeRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IDocumentTypeView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IDocumentTypeView.Message = ResponseData.DisplayMessage;
            }

            _IDocumentTypeView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectAllDocumentType()
        {
            var RequestData = new SelectAllDocumentTypeRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _DocumentTypeBLL.SelectAllDocumentType(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentTypeView.DocumentTypeList = ResponseData.DocumentTypeList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _IDocumentTypeView.Message = ResponseData.DisplayMessage;
            }
        }
    }

    public class DocumentTypeListPresenter
    {
       DocumentTypeBLL _DocumentTypeBLL = new DocumentTypeBLL();
      IDocumentList _IDocumentList;
      public DocumentTypeListPresenter(IDocumentList ViewObj)
        {
            _IDocumentList = ViewObj;
        }
      public void GetDocumentTypeList()
        {
            var RequestData = new SelectAllDocumentTypeRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllDocumentTypeResponse();
            ResponseData = _DocumentTypeBLL.SelectAllDocumentType(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentList.DocumentTypeList = ResponseData.DocumentTypeList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }
    }
}

