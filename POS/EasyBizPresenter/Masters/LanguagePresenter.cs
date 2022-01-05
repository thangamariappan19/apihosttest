using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.Language;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizResponse.Masters.LanguageResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class LanguagePresenter
    {
        ILanguageView _ILanguageView;
        LanguageBLL _LanguageBLL = new LanguageBLL();
        public LanguagePresenter(ILanguageView ViewObj)
        {
            _ILanguageView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ILanguageView.LanguageCode.Trim() == string.Empty)
            {
                _ILanguageView.Message = "Code is missing Please Enter it.";
            }
            else if (_ILanguageView.LanguageCode.Length > 8)
            {
                _ILanguageView.Message = "Please Enter Valid Code";
            }
            else if (_ILanguageView.LanguageName.Trim() == string.Empty)
            {
                _ILanguageView.Message = "Name is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveLanguage()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveLanguageRequest();
                RequestData.LanguageMasterRecord = new LanguageMaster();

                RequestData.LanguageMasterRecord.ID = _ILanguageView.ID;
                RequestData.LanguageMasterRecord.LanguageCode = _ILanguageView.LanguageCode;
                RequestData.LanguageMasterRecord.LanguageName = _ILanguageView.LanguageName;
                RequestData.LanguageMasterRecord.CreateBy = _ILanguageView.UserID;
                RequestData.LanguageMasterRecord.CreateOn = DateTime.Now;
                RequestData.LanguageMasterRecord.Active = _ILanguageView.Active;
                RequestData.LanguageMasterRecord.Remarks = _ILanguageView.Remarks; 
                RequestData.LanguageMasterRecord.SCN = _ILanguageView.SCN;
                var ResponseData = _LanguageBLL.SaveLanguage(RequestData);

                _ILanguageView.Message = ResponseData.DisplayMessage;
                _ILanguageView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ILanguageView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateLanguage()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateLanguageRequest();
                RequestData.LanguageMasterRecord = new LanguageMaster();
                RequestData.LanguageMasterRecord.ID = _ILanguageView.ID;
                RequestData.LanguageMasterRecord.LanguageCode = _ILanguageView.LanguageCode;
                RequestData.LanguageMasterRecord.LanguageName = _ILanguageView.LanguageName;
                RequestData.LanguageMasterRecord.UpdateBy = _ILanguageView.UserID;
                RequestData.LanguageMasterRecord.UpdateOn = DateTime.Now;
                RequestData.LanguageMasterRecord.Active = _ILanguageView.Active;
                RequestData.LanguageMasterRecord.Remarks = _ILanguageView.Remarks; 
                RequestData.LanguageMasterRecord.SCN = _ILanguageView.SCN;
                var ResponseData = _LanguageBLL.UpdateLanguage(RequestData);

                _ILanguageView.Message = ResponseData.DisplayMessage;
                _ILanguageView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ILanguageView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteLanguage()
        {
            try
        {
            var RequestData = new DeleteLanguageRequest();
            RequestData.ID = _ILanguageView.ID;
            var ResponseData = _LanguageBLL.DeleteLanguage(RequestData);
            _ILanguageView.Message = ResponseData.DisplayMessage;
            _ILanguageView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectLanguage()
        {
            var RequestData = new SelectByLanguageIDRequest();
            RequestData.ID = _ILanguageView.ID;
            var ResponseData = _LanguageBLL.SelectLanguage(RequestData);
            _ILanguageView.LanguageCode = ResponseData.LanguageMasterRecord.LanguageCode;
            _ILanguageView.LanguageName = ResponseData.LanguageMasterRecord.LanguageName;
            _ILanguageView.SCN = ResponseData.LanguageMasterRecord.SCN;
            _ILanguageView.Remarks = ResponseData.LanguageMasterRecord.Remarks;
            _ILanguageView.Active = ResponseData.LanguageMasterRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ILanguageView.Message = ResponseData.DisplayMessage;
            }
            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ILanguageView.Message = ResponseData.DisplayMessage;
            }
            _ILanguageView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectAllLanguage()
        {
            var RequestData = new SelectAllLanguageRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _LanguageBLL.SelectAllLanguage(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ILanguageView.LanguageMasterList = ResponseData.LanguageMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _ILanguageView.Message = ResponseData.DisplayMessage;
            }
        }
    }
}
public class LanguageListPresenter
{
    LanguageBLL _LanguageBLL = new LanguageBLL();
    ILanguageList _ILanguageList;
    public LanguageListPresenter(ILanguageList ViewObj)
    {
        _ILanguageList = ViewObj;
    }
    public void GetLanguageList()
    {
        var RequestData = new SelectAllLanguageRequest();
        RequestData.ShowInActiveRecords = true;
        var ResponseData = new SelectAllLanguageResponse();
        ResponseData = _LanguageBLL.SelectAllLanguage(RequestData);
        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        {
            _ILanguageList.LanguageMasterList = ResponseData.LanguageMasterList;
        }
        else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
        {

        }
    }
}

