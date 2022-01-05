using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView;
using EasyBizIView.Masters.IShift;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
   public class SalesTilesPresenter
    {
        ISalesTiles _ISalesTiles;
        IShiftMasterView _IShiftMasterView;
        UsersBLL _UsersBLL = new UsersBLL();
        ShiftBLL _ShiftBLL = new ShiftBLL();
        CountryBLL _CountryBLL = new CountryBLL();

        public SalesTilesPresenter(ISalesTiles ViewObj)
        {
            _ISalesTiles = ViewObj;
        }
       public void SelectShiftListForCategory()
       {
           try
           {
               var RequestData = new SelectByCountryIDRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.CountryID = _ISalesTiles.CountryID;
               SelectByCountryIDResponse ResponseData = _ShiftBLL.SelectCountryRecord(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ISalesTiles.ShiftRecodrd = ResponseData.ShiftRecord;
                   _ISalesTiles.shiftID = ResponseData.ShiftRecord.ID;
                 
               }
               else
               {
                   _ISalesTiles.Message = ResponseData.DisplayMessage;
                   _ISalesTiles.ProcessStatus = ResponseData.StatusCode;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void OneUserLogin()
       {
           try
           {

               SelectLogInRequest RequestData = new SelectLogInRequest();
               SelectLogInResponse ResponseData = new SelectLogInResponse();
                   //RequestData.UsersRecord = new UsersSettings();
                   // RequestData.UsersRecord.UserCode = _IUserPasswordReset.UserCode;
               RequestData.UserName = _ISalesTiles.UserName;
               RequestData.Password = _ISalesTiles.Password;                   
                   ResponseData = _UsersBLL.OneUserLogin(RequestData);
                   //_IUserPasswordReset.Message = ResponseData.DisplayMessage;
                   //_IUserPasswordReset.ProcessStatus = ResponseData.StatusCode;

              
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
