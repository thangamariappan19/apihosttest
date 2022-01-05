using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizBLL.Transactions.PriceChange;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using EasyBizIView.Masters.ILogIn;
using EasyBizIView.Masters.IPosMaster;
using EasyBizIView.Masters.IPrevileges;
using EasyBizRequest;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Reports.UserReports;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse;
using EasyBizResponse.Common;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Reports.UserReports;
using EasyBizResponse.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    
   public class LogInPresenter
    {
       PosMasterBLL _PosMasterBLL = new PosMasterBLL();
       PrevilegesBLL _PrevilegesBLL = new PrevilegesBLL();
       StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
       DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();     
        ILogInView _ILogInView;
        //IPosMasterView _IPosMasterView;
        DayClosingBLL _DayClosingBLL = new DayClosingBLL();
        //IPrevilegesView _IPrevilegesView;
        UsersBLL _UsersBLL = new UsersBLL();
        public LogInPresenter(ILogInView ViewObj)
        {
            _ILogInView = ViewObj;
        }
        public bool BackgroundProcessIsCompleted()
        {
            bool IsCompleted = false;
            try
            {
                BaseRequestType objBaseRequestType = null;
                BaseResponseType objBaseResponseType = null;
                BackgroundServices _BackgroundServices = new BackgroundServices(objBaseRequestType, objBaseResponseType, string.Empty, string.Empty);
                IsCompleted = _BackgroundServices.CheckAllThreadsHaveFinishedWorking();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return IsCompleted;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ILogInView.UserName.Trim() == string.Empty)
            {
                _ILogInView.Message = "Please Enter UserName";
            }

            else if (_ILogInView.Password.Trim() == string.Empty)
            {
                _ILogInView.Message = "Please Enter Password";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public bool IsValidFingerPrint()
        {
            bool objBool = false;

            if (_ILogInView.captureResult.Quality != DPUruNet.Constants.CaptureQuality.DP_QUALITY_GOOD)
            {
                _ILogInView.Message = "Quality Not good Try again";
            }
            else if (_ILogInView.captureResult.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
            {
                _ILogInView.Message = "Scan Finger Print Again";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SelectLogIn()
        {
            try
            {
                if (IsValidForm())
                {
                    SelectLogInRequest RequestData = new SelectLogInRequest();
                    RequestData.UserName = _ILogInView.UserName;
                    RequestData.Password = _ILogInView.Password;                    
                    RequestData.SourceFrom = "ENTERPRISE";
                    RequestData.RequestFrom = _ILogInView.RequestFrom;

                    RequestData.ShowInActiveRecords = true;
                    SelectLogInResponse ResponseData = _UsersBLL.SelectLogIn(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true && ResponseData.UsersRecord.PasswordReset == true)
                    {
                        //_ILogInView.Message = "At first Login,User have to Change the Password";
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.Success;
                        _ILogInView.UserInfo = ResponseData.UsersRecord;
                        GetUserReportRegisterList();
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.VersionNotUpdate)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.VersionNotUpdate;
                        _ILogInView.Message = "Please update the latest Application and Database !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true )
                    //else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true && ResponseData.UsersRecord.IsLoggedIn ==false)
                    {
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        _ILogInView.UserInfo = ResponseData.UsersRecord;
                        //SelectUserDetails();
                        _ILogInView.ProcessStatus = ResponseData.StatusCode;
                        GetUserReportRegisterList();
                    }
                    else if(ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == false)
                    {
                        _ILogInView.Message = "User is locked!";
                       // _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if(ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true && ResponseData.UsersRecord.IsLoggedIn == true)
                    {
                        _ILogInView.Message = "User Is Already Logged In";
                        //_ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                    {
                        _ILogInView.Message = "Invalid username or password !";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.RecordNotFound;
                    }                 
                    else
                    {
                        _ILogInView.Message = ResponseData.DisplayMessage;
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SelectStoreLogIn()
        {
            try
            {
                if (IsValidForm())
                {
                    SelectLogInRequest RequestData = new SelectLogInRequest();
                    RequestData.UserName = _ILogInView.UserName;
                    RequestData.Password = _ILogInView.Password;
                    RequestData.FromOrToStoreID = _ILogInView.StoreID;
                    RequestData.FromOrToStoreCode = _ILogInView.StoreCode;
                    RequestData.SourceFrom = "STORE";
                    RequestData.ShowInActiveRecords = true;
                    RequestData.RequestFrom = _ILogInView.RequestFrom;
                    RequestData.StoreID = _ILogInView.StoreID;

                    SelectLogInResponse ResponseData = _UsersBLL.SelectLogIn(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true)
                    {
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        _ILogInView.ManagerOverrideID = ResponseData.UsersRecord.ManagerOverrideID;
                        _ILogInView.UserInfo = ResponseData.UsersRecord;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        //SelectUserDetails();
                        _ILogInView.ProcessStatus = ResponseData.StatusCode;
                        GetUserReportRegisterList();
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.VersionNotUpdate)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.VersionNotUpdate;
                        _ILogInView.Message = "Please update the latest Application and Database !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == false)
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = "User is locked!";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = "Invalid username or password !";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.RecordNotFound;
                    }
                    else
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = ResponseData.DisplayMessage;
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectStoreLogInfromFingerPrint()
        {
            try
            {
                if (IsValidFingerPrint())
                {
                    SelectLogInByFingerPrintRequest RequestData = new SelectLogInByFingerPrintRequest();
                    //RequestData.UserName = _ILogInView.UserName;
                    //RequestData.Password = _ILogInView.Password;
                    RequestData.captureResult = _ILogInView.captureResult;
                    //RequestData.FromOrToStoreID = _ILogInView.StoreID;
                    //RequestData.FromOrToStoreCode = _ILogInView.StoreCode;
                    //RequestData.SourceFrom = "STORE";
                    //RequestData.ShowInActiveRecords = true;
                    //RequestData.RequestFrom = _ILogInView.RequestFrom;
                    //RequestData.StoreID = _ILogInView.StoreID;

                    SelectLogInResponse ResponseData = _UsersBLL.SelectUserDeatilsfromFingerPrint(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true)
                    {
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        _ILogInView.ManagerOverrideID = ResponseData.UsersRecord.ManagerOverrideID;
                        _ILogInView.UserInfo = ResponseData.UsersRecord;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        //SelectUserDetails();
                        _ILogInView.ProcessStatus = ResponseData.StatusCode;
                        GetUserReportRegisterList();
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.VersionNotUpdate)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.VersionNotUpdate;
                        _ILogInView.Message = "Please update the latest Application and Database !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == false)
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = "User is locked!";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = "Invalid username or password !";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.RecordNotFound;
                    }
                    else
                    {
                        _ILogInView.ManagerOverrideID = 0;
                        _ILogInView.Message = ResponseData.DisplayMessage;
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectPOSLogIn()
        {
            try
            {
                if (IsValidForm())
                {
                    SelectLogInRequest RequestData = new SelectLogInRequest();
                    RequestData.UserName = _ILogInView.UserName;
                    RequestData.Password = _ILogInView.Password;
                    RequestData.FromOrToStoreID = _ILogInView.StoreID;
                    RequestData.FromOrToStoreCode = _ILogInView.StoreCode;
                    RequestData.POSID = _ILogInView.POSID;
                    RequestData.SourceFrom = "POS";
                    RequestData.ShowInActiveRecords = true;
                    RequestData.RequestFrom = _ILogInView.RequestFrom;
                    RequestData.StoreID = _ILogInView.StoreID;

                    SelectLogInResponse ResponseData = _UsersBLL.SelectLogIn(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true)
                    {
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        GetPosRecord(); 
                        SelectUserDetails();                                               
                        _ILogInView.ProcessStatus = ResponseData.StatusCode;
                        GetUserReportRegisterList();
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.LogInExist)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.LogInExist;
                        _ILogInView.Message = "User already loged in !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.VersionNotUpdate)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.VersionNotUpdate;
                        _ILogInView.Message = "Please update the latest Application and Database !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == false)
                    {
                        _ILogInView.Message = "User is locked!";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                    {
                        _ILogInView.Message = "Invalid username or password !";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.RecordNotFound;
                    }
                    else
                    {
                        _ILogInView.Message = ResponseData.DisplayMessage;
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectPOSLogInfromFingerPrint()
        {
            try
            {
                if (IsValidFingerPrint())
                {
                    SelectLogInByFingerPrintRequest RequestData = new SelectLogInByFingerPrintRequest();
                    RequestData.captureResult = _ILogInView.captureResult;
                    

                    SelectLogInResponse ResponseData = _UsersBLL.SelectUserDeatilsfromFingerPrint(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == true)
                    {
                        int RoleID = ResponseData.UsersRecord.RoleID;
                        GetPOSScreenNames();
                        GetUserprivilegeNames(RoleID);
                        GetPosRecord();
                        //SelectUserDetails();
                        _ILogInView.UserInfo = ResponseData.UsersRecord;
                        _ILogInView.ProcessStatus = ResponseData.StatusCode;
                        GetUserReportRegisterList();
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.LogInExist)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.LogInExist;
                        _ILogInView.Message = "User already loged in !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.VersionNotUpdate)
                    {
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.VersionNotUpdate;
                        _ILogInView.Message = "Please update the latest Application and Database !.";
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && ResponseData.UsersRecord.Active == false)
                    {
                        _ILogInView.Message = "User is locked!";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                    {
                        _ILogInView.Message = "User Finger Print Not Valid!";
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.RecordNotFound;
                    }
                    else
                    {
                        _ILogInView.Message = ResponseData.DisplayMessage;
                        _ILogInView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetUserprivilegeNames(int RoleID)
        {
            try
            {
                var RequestData = new SelectPrevilegesLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.RoleID = RoleID;
                var ResponseData = _PrevilegesBLL.SelectPrevilegesLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ILogInView.AppliedForms = ResponseData.UserPrivilagesTypesList.FirstOrDefault().ScreenName;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateUniqueID(string DiskID,string CpuID)
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateUniqueIDRequest();
                    
                    if (_ILogInView.RequestFrom == Enums.RequestFrom.StoreServer)
                    {
                        _StoreMasterBLL.InsertDBConnections(_ILogInView.MainServerConnection);

                        RequestData.ID = _ILogInView.StoreID;
                        RequestData.Type = "Store";
                    }
                    else if (_ILogInView.RequestFrom == Enums.RequestFrom.StoreSales)
                    {
                        RequestData.ID = _ILogInView.POSID;
                        RequestData.Type = "POS";
                    }

                    RequestData.DiskID = DiskID;
                    RequestData.CPUID = CpuID;                   
                    var ResponseData = _StoreMasterBLL.UpdateUniqueID(RequestData);  
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreCodeLookUP()
        {
            var RequestData = new SelectByIDStoreMasterRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.RequestFrom = _ILogInView.RequestFrom;
            RequestData.FromOrToStoreID = _ILogInView.StoreID;
            var ResponseData = new SelectByIDStoreMasterResponse();
            ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);

            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ILogInView.StoreMasterData = ResponseData.StoreMasterData;               
            }          

        }



        public void GetStoreMasterLookUP()
        {
            var RequestData = new SelectAllStoreMasterRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.RequestFrom = _ILogInView.RequestFrom;
            RequestData.FromOrToStoreID = _ILogInView.StoreID;
            var ResponseData = new SelectAllStoreMasterResponse();
            ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ILogInView.StoreMasterList = ResponseData.StoreMasterList;

                //if (ResponseData.StoreMasterList.First().DiskID.Trim() != string.Empty && ResponseData.StoreMasterList.First().CPUID.Trim() != string.Empty)
                //{
                //    _ILogInView.InitializeCompleted = true;
                //}
                //else
                //{
                //    _ILogInView.InitializeCompleted = false;
                //}
            }
            //else
            //{
            //    GetStoreFromEnterprise();
            //}

        }
        //public void GetStoreFromEnterprise()
        //{
        //    var RequestData = new SelectAllStoreMasterRequest();
        //    RequestData.ShowInActiveRecords = true;
        //    RequestData.RequestFrom = Enums.RequestFrom.StoreServer;

        //    RequestData.ConnectionString = _ILogInView.MainServerConnection;
        //    var ResponseData = new SelectAllStoreMasterResponse();
        //    ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
        //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //    {
        //        _ILogInView.StoreMasterList = ResponseData.StoreMasterList;

        //        if (ResponseData.StoreMasterList.First().DiskID.Trim() != string.Empty && ResponseData.StoreMasterList.First().CPUID.Trim() != string.Empty)
        //        {
        //            _ILogInView.InitializeCompleted = true;
        //        }
        //        else
        //        {
        //            _ILogInView.InitializeCompleted = false;
        //        }
        //    }            
        //}
       public void GetPosRecord()
        {
            var RequestData =  new SelectByIDPosMasterRequest();
            var ResponseData = new SelectByIDPosMasterResponse();
            RequestData.ID = _ILogInView.POSID;
            ResponseData = _PosMasterBLL.SelectPosMasterRecord(RequestData);

           if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _ILogInView.PosMasterRecord = ResponseData.PosMasterRecord;
           }

        }
        public void GetPosLookUP()
        {
            SelectAllPosMasterRequest RequestData = new SelectAllPosMasterRequest();
            RequestData.RequestFrom = _ILogInView.RequestFrom;
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _ILogInView.StoreID;
            SelectAllPosMasterResponse ResponseData = _PosMasterBLL.SelectAllPosMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ILogInView.PosMasterLookUp = ResponseData.PosMasterList;
            }
            else
            {
                var PosList = new List<PosMaster>();
                _ILogInView.PosMasterLookUp = PosList;
            }
        }
        public void GetPOSScreenNames()
        {
            try
            {

                var RequestData = new GetScreenNamesRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _PrevilegesBLL.POSScreenNames(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ILogInView.ScreenName = ResponseData.POSScreenTypesList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SelectUserDetails()
        {
            try
            {
                if (IsValidForm())
                {
                    SelectLogInRequest RequestData = new SelectLogInRequest();

                    RequestData.UserName = _ILogInView.UserName;
                    RequestData.StoreID = _ILogInView.StoreID;
                    RequestData.POSID = _ILogInView.POSID;

                    RequestData.ShowInActiveRecords = true;
                    SelectLogInResponse ResponseData = _UsersBLL.SelectUserDeatils(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ILogInView.UserInfo = ResponseData.UsersRecord;                        
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                    {
                        _ILogInView.Message = ResponseData.DisplayMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
        public void InsertStoreInfo()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ID = _ILogInView.StoreID;
                var ResponseData = _StoreMasterBLL.SelectByIDStoreMaster(RequestData);
                if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    var InsertRequestData = new SaveStoreMasterRequest();
                    InsertRequestData.StoreMasterRecord = new StoreMaster();
                    InsertRequestData.StoreMasterRecord = ResponseData.StoreMasterData;
                    var InsertResponseData = _StoreMasterBLL.SaveStoreMaster(InsertRequestData);
                    if(InsertResponseData.StatusCode != Enums.OpStatusCode.Success)
                    {
                        _ILogInView.Message = InsertResponseData.DisplayMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectPriceChangeStaus(out int IsInProgressCount, out int PriceUpdateCount, out bool AllowLogin, out string PriceUpdateMsg)
        {
            IsInProgressCount = 0;
            PriceUpdateCount = 0;
            AllowLogin = true;
            PriceUpdateMsg = "";
           try
           {
               var _PriceChangeBLL = new PriceChangeBLL();
               var RequestData = new SelectPriceChangeStatusRequest();
               var ResponseData = new SelectPriceChangeStatusResponse();
               ResponseData = _PriceChangeBLL.SelectPriceChangeStatus(RequestData);
               if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   IsInProgressCount = ResponseData.IsInProgressCount;
                   PriceUpdateCount = ResponseData.PriceUpdateCount;
                   if(IsInProgressCount > 0 || PriceUpdateCount > 0)
                   {
                       AllowLogin = false;
                       if (IsInProgressCount > 0)
                           PriceUpdateMsg = "Style Price Update in Progress, Kindly wait or try again later";
                       else if (IsInProgressCount == 0 && PriceUpdateCount > 0)
                           PriceUpdateMsg = "New Style Price Update is available.";
                   }
               }
           }
           catch(Exception ex)
           {
               throw ex;
           }
        }

        public void UpdateStylePrice()
        {
            try
            {
                var _PriceChangeBLL = new PriceChangeBLL();
                var RequestData = new PriceUpdateRequest();
                RequestData.StoreID = _ILogInView.StoreID;
                RequestData.PriceChangeDate = DateTime.Now.Date;
                var ResponseData = new PriceUpdateResponse();
                ResponseData = _PriceChangeBLL.UpdateStylePrice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetUserReportRegisterList()
        {
            try
            {
                var _UserReportBLL = new UserReportBLL();
                var RequestData = new SelectAllUserReportRequest();
                var ResponseData = new SelectAllUserReportResponse();
                RequestData.ShowInActiveRecords = true;
                ResponseData = _UserReportBLL.SelectAllUserReportList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ILogInView.UserReportList = ResponseData.UserReportList;
                }
                else
                {
                    _ILogInView.UserReportList = new List<UserReport>();
                }
            }
            catch (Exception ex)
            {
                _ILogInView.UserReportList = new List<UserReport>();
                throw ex;
            }
        }
    }
}
