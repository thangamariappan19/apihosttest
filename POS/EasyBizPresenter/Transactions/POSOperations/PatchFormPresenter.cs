using EasyBizBLL.PatchFormBLL;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizIView.Transactions.IPOSOperations.IPatchForm;
using EasyBizRequest.PatchFormRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POSOperations
{
   public class PatchFormPresenter
    {
       IPatchFormView _IPatchFormView;
       PatchFormBLL _PatchFormBLL = new PatchFormBLL();

       public PatchFormPresenter(IPatchFormView ViewObj)
        {
            _IPatchFormView = ViewObj;
        }
       public void SavePatchForm()
       {
           try
           {
                if (IsValidForm())
                {
                    var RequestData = new SavePatchFormRequest();

                   RequestData.PatchFormTypesRecord = new PatchFormTypes();
                   RequestData.PatchFormTypesRecord.ID = _IPatchFormView.ID;
                   RequestData.PatchFormTypesRecord.ApplicationType = _IPatchFormView.ApplicationType;
                   RequestData.PatchFormTypesRecord.ApplicationVersion = _IPatchFormView.ApplicationVersion;
                   RequestData.PatchFormTypesRecord.DBVersion = _IPatchFormView.DBVersion;
                   RequestData.PatchFormTypesRecord.AppPatchFile = _IPatchFormView.AppPatchFile;
                   RequestData.PatchFormTypesRecord.Extension = _IPatchFormView.Extension;

                    var ResponseData = _PatchFormBLL.SavePatchForm(RequestData);

                   _IPatchFormView.Message = ResponseData.DisplayMessage;
                   _IPatchFormView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPatchFormView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        public bool IsValidForm()
        {
            bool objBool = false;

            if (_IPatchFormView.ApplicationType  == String.Empty)
            {
                _IPatchFormView.Message = "Please Select ApplicationType";
            }

            else if (_IPatchFormView.ApplicationVersion.Trim() == string.Empty)
            {
                _IPatchFormView.Message = "Please Upload a file Properly";

            }
            else if (_IPatchFormView.DBVersion.Trim() == string.Empty)
            {
                _IPatchFormView.Message = "Please Upload a file Properly";

            }
            else if (_IPatchFormView.AppPatchFile == null)
            {
                _IPatchFormView.Message = "Please Upload a file Properly";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

    }
}
