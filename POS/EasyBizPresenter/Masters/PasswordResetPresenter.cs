using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IUsers;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.UsersResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class PasswordResetPresenter
    {
        IUserPasswordReset _IUserPasswordReset;        
        UsersBLL _UsersBLL=new UsersBLL();
        public PasswordResetPresenter(IUserPasswordReset ViewObj)
        {
            _IUserPasswordReset = ViewObj;
        }

      
        public bool IsValidForm()
        {

            bool objBool = false;
            if(_IUserPasswordReset.CurrentPassword.Trim() == string.Empty)
            {
                _IUserPasswordReset.Message = "Please Enter Current Password"; 
            }
            else if(_IUserPasswordReset.NewPassword.Trim() == string.Empty)
            {
                _IUserPasswordReset.Message = "Please Enter New Password"; 
            }           
            else if (_IUserPasswordReset.ConfirmPassword.Trim() == string.Empty)
            {
                _IUserPasswordReset.Message = "Please Enter Confirm Password";
            }
            else if (_IUserPasswordReset.NewPassword != _IUserPasswordReset.ConfirmPassword)
            {
                _IUserPasswordReset.Message = "Password Mismatched";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void PasswordReset()
        {
            try
            {
                if (IsValidForm())
                {
                    UpdateUsersRequest RequestData = new UpdateUsersRequest();
                    UpdateUsersResponse ResponseData = new UpdateUsersResponse();
                    RequestData.UsersRecord = new UsersSettings();
                   // RequestData.UsersRecord.UserCode = _IUserPasswordReset.UserCode;
                    RequestData.UsersRecord.UserName = _IUserPasswordReset.UserName;
                    RequestData.UsersRecord.CurrentPassword = _IUserPasswordReset.CurrentPassword;
                    //RequestData.UsersRecord.NewPassword = _IUserPasswordReset.NewPassword;
                    RequestData.UsersRecord.ConfirmPassword = _IUserPasswordReset.ConfirmPassword;
                     ResponseData = _UsersBLL.PasswordReset(RequestData);
                     _IUserPasswordReset.Message = ResponseData.DisplayMessage;
                     _IUserPasswordReset.ProcessStatus = ResponseData.StatusCode;
             
                }
                else
                {
                    _IUserPasswordReset.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void SelectUserPasswordRecord()
        //{
        //    try
        //    {
        //        var RequestData = new SelectByUsersIDRequest();
        //        RequestData.ID = _IUserPasswordReset.ID;
        //        var ResponseData = _UsersBLL.SelectUserPassword(RequestData);
        //        _IUserPasswordReset.CurrentPassword = ResponseData.UsersRecord.CurrentPassword;
        //        _IUserPasswordReset.NewPassword = ResponseData.UsersRecord.NewPassword;
        //        _IUserPasswordReset.ConfirmPassword = ResponseData.UsersRecord.ConfirmPassword;
               

        //        if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
        //        {
        //            _IUserPasswordReset.Message = ResponseData.DisplayMessage;
        //        }
        //        else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
        //        {
        //            _IUserPasswordReset.Message = ResponseData.DisplayMessage;
        //        }
        //        _IUserPasswordReset.ProcessStatus = ResponseData.StatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
