using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IUsers;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.UsersResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class UsersMasterPresenter
    {
       IUsersMasterView _IUsersMasterView;
       UsersBLL _UsersBLL = new UsersBLL();
       CountryBLL _CountryBLL = new CountryBLL();
       ManagerOverrideBLL _ManagerOverrideBLL = new ManagerOverrideBLL();
       RetailSettingsBLL _RetailSettingsBLL = new RetailSettingsBLL();
       CompanySettingBLL _CompanySettingBLL = new CompanySettingBLL();
       RoleBLL _RoleBLL = new RoleBLL();
       EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
       StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
       StateMasterBLL _StateBLL = new StateMasterBLL();
       public UsersMasterPresenter(IUsersMasterView ViewObj)
        {
            _IUsersMasterView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_IUsersMasterView.UserCode.Trim() == string.Empty)
           {
               _IUsersMasterView.Message = "Please Enter User Code";
           }
           else if (_IUsersMasterView.UserName.Trim() == string.Empty)
           {
               _IUsersMasterView.Message = "Please Enter User Name";
           }
           else if (_IUsersMasterView.EmployeeID == 0)
           {
               _IUsersMasterView.Message = "Employee Name is Missing ";
           }
           //else if (_IUsersMasterView.CountryID==0)
           //{
           //    _IUsersMasterView.Message = "Country is Missing ";
           //}
           //else if (_IUsersMasterView.StoreID==0)
           //{
           //    _IUsersMasterView.Message = "Store is Missing ";
           //}
           else if(_IUsersMasterView.ManagerOverrideID==0)
           {
               _IUsersMasterView.Message = "Manager Override is Missing";
           }
           else if(_IUsersMasterView.RetailID == 0)
           {
               _IUsersMasterView.Message = "Retail Is Missing";
           }
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveUsersMaster()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveUsersRequest();
                   RequestData.UsersRecord = new UsersSettings();

                   RequestData.UsersRecord.ID = _IUsersMasterView.ID;
                   RequestData.UsersRecord.UserCode = _IUsersMasterView.UserCode;
                   RequestData.UsersRecord.UserName = _IUsersMasterView.UserName;
                   RequestData.UsersRecord.Password = _IUsersMasterView.Password;
                   RequestData.UsersRecord.EmployeeID = _IUsersMasterView.EmployeeID;
                   RequestData.UsersRecord.EmployeeCode = _IUsersMasterView.EmployeeCode;
                   //RequestData.UsersRecord.CompanyID = _IUsersMasterView.CompanyID;
                 //  RequestData.UsersRecord.CountryID = _IUsersMasterView.CountryID;
                   RequestData.UsersRecord.ManagerOverrideID = _IUsersMasterView.ManagerOverrideID;
                   RequestData.UsersRecord.ManagerOverrideCode = _IUsersMasterView.ManagerOverrideCode;                   
                   RequestData.UsersRecord.RetailID = _IUsersMasterView.RetailID;
                   RequestData.UsersRecord.RetailSettingCode = _IUsersMasterView.RetailCode;                   
                   //RequestData.UsersRecord.StateID = _IUsersMasterView.StateID;
                  // RequestData.UsersRecord.StoreID = _IUsersMasterView.StoreID;
                   RequestData.UsersRecord.RoleID = _IUsersMasterView.RoleID;
                   RequestData.UsersRecord.RoleCode = _IUsersMasterView.RoleCode;
                   RequestData.UsersRecord.CreateBy = _IUsersMasterView.UserID;
                   RequestData.UsersRecord.CreateOn = DateTime.Now;
                   RequestData.UsersRecord.Active = _IUsersMasterView.Active;
                   RequestData.UsersRecord.PasswordReset = _IUsersMasterView.PasswordReset;
                   RequestData.UsersRecord.SCN = _IUsersMasterView.SCN;
                    RequestData.UsersRecord.AllowStockEdit = _IUsersMasterView.AllowStockEdit;
                    RequestData.UsersRecord.MobileUser = _IUsersMasterView.MobileUser;
                    var ResponseData = _UsersBLL.SaveUsers(RequestData);

                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
                   _IUsersMasterView.ProcessStatus = ResponseData.StatusCode;

               }
               else
               {
                   _IUsersMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateUsersMaster()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new UpdateUsersRequest();
                   RequestData.UsersRecord = new UsersSettings();
                   RequestData.UsersRecord.ID = _IUsersMasterView.ID;
                   RequestData.UsersRecord.UserCode = _IUsersMasterView.UserCode;
                   RequestData.UsersRecord.UserName = _IUsersMasterView.UserName;
                   RequestData.UsersRecord.Password = _IUsersMasterView.Password;
                   RequestData.UsersRecord.EmployeeID = _IUsersMasterView.EmployeeID;
                   RequestData.UsersRecord.EmployeeCode = _IUsersMasterView.EmployeeCode;
                  // RequestData.UsersRecord.CompanyID = _IUsersMasterView.CompanyID;
                //   RequestData.UsersRecord.CountryID = _IUsersMasterView.CountryID;
                   RequestData.UsersRecord.ManagerOverrideID = _IUsersMasterView.ManagerOverrideID;
                   RequestData.UsersRecord.ManagerOverrideCode = _IUsersMasterView.ManagerOverrideCode;
                   RequestData.UsersRecord.RetailID = _IUsersMasterView.RetailID;
                   RequestData.UsersRecord.RetailSettingCode = _IUsersMasterView.RetailCode;
                   //RequestData.UsersRecord.StateID = _IUsersMasterView.StateID;
                   //RequestData.UsersRecord.StoreID = _IUsersMasterView.StoreID;
                   RequestData.UsersRecord.RoleID = _IUsersMasterView.RoleID;
                   RequestData.UsersRecord.RoleCode = _IUsersMasterView.RoleCode;
                   RequestData.UsersRecord.UpdateBy = _IUsersMasterView.UserID;
                   RequestData.UsersRecord.UpdateOn = DateTime.Now;
                   RequestData.UsersRecord.Active = _IUsersMasterView.Active;
                   RequestData.UsersRecord.PasswordReset = _IUsersMasterView.PasswordReset;
                   RequestData.UsersRecord.SCN = _IUsersMasterView.SCN;
                    RequestData.UsersRecord.AllowStockEdit = _IUsersMasterView.AllowStockEdit;
                    RequestData.UsersRecord.MobileUser = _IUsersMasterView.MobileUser;
                    var ResponseData = _UsersBLL.UpdateUsers(RequestData);

                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
                   _IUsersMasterView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IUsersMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteUsersMaster()
       {
           try
           {
               var RequestData = new DeleteUsersRequest();
               RequestData.ID = _IUsersMasterView.ID;
               var ResponseData = _UsersBLL.DeleteUsers(RequestData);
               _IUsersMasterView.Message = ResponseData.DisplayMessage;
               _IUsersMasterView.ProcessStatus = ResponseData.StatusCode;

               //var RequestData = new DeleteUsersRequest();
               //RequestData.ID = -_IUsersMasterView.ID;
               //var ResponseData = _UsersBLL.DeleteUsers(RequestData);
               //_IUsersMasterView.Message = ResponseData.DisplayMessage;
               //_IUsersMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectUsersMaster()
       {
           try
           {
               var RequestData = new SelectByUsersIDRequest();
               RequestData.ID = _IUsersMasterView.ID;
               var ResponseData = _UsersBLL.SelectUserMaster(RequestData);
               _IUsersMasterView.UserCode = ResponseData.UsersRecord.UserCode;
               _IUsersMasterView.UserName = ResponseData.UsersRecord.UserName;
               _IUsersMasterView.Password = ResponseData.UsersRecord.Password;
              // _IUsersMasterView.CountryID = ResponseData.UsersRecord.CountryID;
               _IUsersMasterView.ManagerOverrideID = ResponseData.UsersRecord.ManagerOverrideID;               
               _IUsersMasterView.RetailID = ResponseData.UsersRecord.RetailID;
               //_IUsersMasterView.StateID = ResponseData.UsersRecord.StateID;
               //_IUsersMasterView.StoreID = ResponseData.UsersRecord.StoreID;
               //_IUsersMasterView.CompanyID = ResponseData.UsersRecord.CompanyID;
               _IUsersMasterView.RoleID = ResponseData.UsersRecord.RoleID;
               _IUsersMasterView.EmployeeID = ResponseData.UsersRecord.EmployeeID;
               _IUsersMasterView.Active = ResponseData.UsersRecord.Active;
               _IUsersMasterView.PasswordReset = ResponseData.UsersRecord.PasswordReset;
               _IUsersMasterView.SCN = ResponseData.UsersRecord.SCN;
                _IUsersMasterView.AllowStockEdit = ResponseData.UsersRecord.AllowStockEdit;
                _IUsersMasterView.MobileUser = ResponseData.UsersRecord.MobileUser;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
               }

               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
               }

               _IUsersMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectRoleName()
       {
           try
           {
               var RequestData = new SelectByIDEmployeeMasterRequest();
               RequestData.ID = _IUsersMasterView.EmployeeID;
               var ResponseData = _EmployeeMasterBLL.SelectEmployeeMaster(RequestData);
               _IUsersMasterView.RoleID = ResponseData.EmployeeMasterRecord.RoleID;
              

               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
               }

               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
               }

               _IUsersMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectAllUsersMaster()
       {
           try
           {
               var RequestData = new SelectAllUsersRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = _UsersBLL.SelectAllUsers(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterView.UsersList = ResponseData.UsersList;
               }
               else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
               {
                   _IUsersMasterView.Message = ResponseData.DisplayMessage;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //public void GetCountryLookUp()
       //{
       //    try
       //    {
       //        var RequestData = new SelectCountryLookUpRequest();
       //        RequestData.ShowInActiveRecords = false;
       //        var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
       //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
       //        {
       //            _IUsersMasterView.CountryLookUp = ResponseData.CountryMasterList;
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
       public void GetStateLookUP()
       {
           SelectStateLookUpRequest RequestData = new SelectStateLookUpRequest();
           RequestData.ShowInActiveRecords = false;
          // RequestData.CountryID = _IUsersMasterView.CountryID;
           SelectStateLookUpResponse ResponseData = _StateBLL.SelectStateLookUp(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IUsersMasterView.StateMasterLookUp = ResponseData.StateMasterList;
           }
       }
       public void GetCompanyLookUp()
       {
           try
           {
               var RequestData = new SelectCompanySettingsLookUpRequest();
               RequestData.ShowInActiveRecords = false;
              // RequestData.CountryID = _IUsersMasterView.CountryID;
               SelectCompanySettingsLookUpResponse ResponseData = _CompanySettingBLL.SelectCompanySettingsLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   //_IUsersMasterView.CompanyLookUp = ResponseData.CompanySettingsList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetRollLookUp()
       {
           try
           {
               var RequestData = new SelectRoleMasterLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _RoleBLL.SelectRoleLookUP(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterView.RoleLookUp = ResponseData.RoleMasterList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetEmployeeLookUp()
       {
           try
           {
               var RequestData = new SelectEmployeeLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _EmployeeMasterBLL.SelectEmployeeLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterView.EmployeeLookUp = ResponseData.EmployeeList;
                  
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //public void GetStoreLookUp()
       //{
       //    try
       //    {
       //        var RequestData = new SelectStoreMasterLookUpRequest();
       //        RequestData.ShowInActiveRecords = false;
       //        //RequestData.CountryID = _IUsersMasterView.CountryID;
       //        var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
       //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
       //        {
       //            _IUsersMasterView.StoreMasterLookUp = ResponseData.StoreMasterList;
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}

       public void GetManagerOverrideLookUp()
       {
           try
           {
               var RequestData = new SelectManagerOverrideLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _ManagerOverrideBLL.SelectManagerOverrideLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterView.ManagerLookUp = ResponseData.ManagerOverrideList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void GetRetailLookUp()
       {
           try
           {
               var RequestData = new SelectRetailSettingsLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _RetailSettingsBLL.SelectRetailSettingsLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterView.RetailLookUp = ResponseData.RetailSettingsList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
   public class UsersListPresenter
   {
       UsersBLL _UsersBLL = new UsersBLL();
       IUsersMasterCollectionView _IUsersMasterCollectionView;
       public UsersListPresenter(IUsersMasterCollectionView ViewObj)
       {
           _IUsersMasterCollectionView = ViewObj;
       }
       public void GetUsersList()
       {
           try
           {
               var RequestData = new SelectAllUsersRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllUsersResponse();
               ResponseData = _UsersBLL.SelectAllUsers(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IUsersMasterCollectionView.UsersList = ResponseData.UsersList;
               }
               else 
               {
                    _IUsersMasterCollectionView.UsersList = new List<UsersSettings>();
                }
           }
           catch (Exception ex)
           {
                _IUsersMasterCollectionView.UsersList = new List<UsersSettings>();
                throw ex;
            }
       }
   }
}
