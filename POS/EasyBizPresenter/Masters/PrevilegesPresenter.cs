using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPrevileges;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class PrevilegesPresenter
    {
       RoleBLL _RoleBLL = new RoleBLL();
       PrevilegesBLL _PrevilegesBLL = new PrevilegesBLL();
       IPrevilegesView _IPrevilegesView;

       public PrevilegesPresenter(IPrevilegesView ViewObj)
        {
            _IPrevilegesView = ViewObj;
        }

       public void GetRoleNameLookUp()
       {
           try
           {

               var RequestData = new SelectRoleMasterLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _RoleBLL.SelectRoleLookUP(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IPrevilegesView.RoleListLookup = ResponseData.RoleMasterList;
               }              
           }
           catch
           {

           }
       }
       public void GetScreenNames()
       {
           try
           {

               var RequestData = new GetScreenNamesRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _PrevilegesBLL.POSScreenNames(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IPrevilegesView.POSScreenList = ResponseData.POSScreenTypesList;
               }
           }
           catch
           {

           }
       }
      
       public void SaveMASUserPrivilages()
       {
           try
           {
               if (IsvalidForm())
               {
                   SavePrevilegesRequestt RequestData = new SavePrevilegesRequestt();
                   RequestData.UserPrivilagesData = new UserPrivilagesTypes();

                   RequestData.UserPrivilagesData.RoleID = _IPrevilegesView.RoleId;
                   RequestData.UserPrivilagesData.ScreenName = _IPrevilegesView.ScreenName;
                   RequestData.UserPrivilagesData.IsActive = true;
                   RequestData.UserPrivilagesData.CreateBy = _IPrevilegesView.UserID;
                   RequestData.UserPrivilagesData.CreateOn = DateTime.Now;
                   RequestData.UserPrivilagesData.UpdateBy = _IPrevilegesView.UserID;
                   //RequestData.MASStoreData.CountryName = _IMASStoreView.CountryName;
                   RequestData.UserPrivilagesData.UpdateOn = DateTime.Now;

                   SavePrevilegesResponse ResponseData = _PrevilegesBLL.SaveMASUserprivilagesResponse(RequestData);
                   _IPrevilegesView.Message = ResponseData.DisplayMessage;
                   _IPrevilegesView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IPrevilegesView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch
           {

           }

       }
       public void SelectStoreRecord()
       {
           try
           {
               SelectByUserIDPrivilagesRequest RequestData = new SelectByUserIDPrivilagesRequest();
               RequestData.ID = _IPrevilegesView.RoleId;
               SelectByUserIDPrivilagesResponse ResponseData = _PrevilegesBLL.SelectUserIDPrivilagesResponse(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IPrevilegesView.ScreenName = ResponseData.MASUserPrivilagesRecord.ScreenName;
                   _IPrevilegesView.RoleId = ResponseData.MASUserPrivilagesRecord.RoleID;
                   _IPrevilegesView.ProcessStatus = ResponseData.StatusCode;
                   if (ResponseData.StatusCode == Enums.OpStatusCode.FileNotFound)
                   {
                       _IPrevilegesView.Message = ResponseData.DisplayMessage;
                   }
               }
           }
           catch
           {

           }
       }
       public bool IsvalidForm()
       {
           bool ObjBool = false;
           if (_IPrevilegesView.RoleId == 0)
           {
               _IPrevilegesView.Message = "Please Select Atleast One Role!";
           }
           else
           {
               ObjBool = true;
           }
           return ObjBool;
       }
    }
}
