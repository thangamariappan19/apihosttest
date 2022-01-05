using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView;
using EasyBizIView.Masters.IShift;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Common;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.ShiftResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
   public class CategoryTilesPresenter
    {
       
        IShiftTiles _IShiftTiles;
        
        ShiftBLL _ShiftBLL = new ShiftBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        DayShiftLOGBLL _DayShiftLOGBLL = new DayShiftLOGBLL();
        DayClosingBLL _DayClosingBLL = new DayClosingBLL();
        UsersBLL _UsersBLL = new UsersBLL();
        public CategoryTilesPresenter(IShiftTiles ViewObj)
        {
            _IShiftTiles = ViewObj;
        }
        public void SelectRetailSettings()
        {
            try
            {
                var _RetailSettingsBLL = new RetailSettingsBLL();
                var RequestData = new SelectByRetailIDRequest();
                RequestData.ID = _IShiftTiles.UserInformation.RetailID;
                var ResponseData = _RetailSettingsBLL.SelectRetailRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.RetailSetting = ResponseData.RetailRecord;
                }
                else
                {
                    _IShiftTiles.RetailSetting = new RetailSettingsType();
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

                SelectLogInRequest RequestData = new SelectLogInRequest();
                RequestData.UserName = _IShiftTiles.UserInformation.UserName;
                RequestData.Password = _IShiftTiles.UserInformation.Password;
                RequestData.FromOrToStoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.FromOrToStoreCode = _IShiftTiles.UserInformation.StoreCode;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                RequestData.SourceFrom = "POS";
                RequestData.CheckLoggedIn = "LogOut";
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = _IShiftTiles.RequestFrom;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;

                SelectLogInResponse ResponseData = _UsersBLL.SelectLogIn(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void SelectShiftMaster()
        //{
        //    try
        //    {
        //        var RequestData = new SelectByCountryIDRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
        //        SelectByCountryIDResponse ResponseData = _ShiftBLL.SelectCountryRecord(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
                  
        //            _IShiftTiles.shiftID = ResponseData.ShiftRecord.ID;

        //        }
        //        else
        //        {
        //            _IShiftTiles.Message = ResponseData.DisplayMessage;
        //            _IShiftTiles.ProcessStatus = ResponseData.StatusCode;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public void SaveShift()
        //{
        //    try
        //    {

        //        DateTime BusinessDate = DateTime.Now;

        //        SaveShiftLOGRequest RequestData = new SaveShiftLOGRequest();
        //        RequestData.ShiftRecord = new ShiftLOGTypes();
        //        RequestData.ShiftRecord.ID = 0;
        //        RequestData.ShiftRecord.CountryID = _IShiftTiles.UserInformation.CountryID;
        //        RequestData.ShiftRecord.StoreID = _IShiftTiles.UserInformation.StoreID;
        //        RequestData.ShiftRecord.POSID = _IShiftTiles.UserInformation.POSID;
        //        RequestData.ShiftRecord.ShiftID = _IShiftTiles.shiftID;
        //        RequestData.ShiftRecord.ShiftInUserID = _IShiftTiles.UserInformation.EmployeeID;
        //        RequestData.ShiftRecord.BusinessDate = _IShiftTiles.BusinessDate;
        //        RequestData.ShiftRecord.Status = "Open";

        //        SaveShiftLOGResponse ResponseData = _DayShiftLOGBLL.SaveDayClosing(RequestData);
        //        //_IInvoiceView.Message = ResponseData.DisplayMessage;
        //        _IShiftTiles.ProcessStatus = ResponseData.StatusCode;
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IShiftTiles.BusinessDate = BusinessDate;
        //        }
        //        else if (ResponseData.StatusCode == Enums.OpStatusCode.DuplicateRecordFound)
        //        {
        //            _IShiftTiles.Message = ResponseData.DisplayMessage;
        //            _IShiftTiles.BusinessDate = DateTime.Now.AddDays(1);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void SelectShiftListForDayClosing()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                RequestData.Type = "DayClosing";
                var ResponseData = new SelectShiftLogResponse();
                ResponseData = _ShiftBLL.SelectShiftLogRecordbyID(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.shiftlog = ResponseData.ShiftTypesData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectPOSUser()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                RequestData.ID = _IShiftTiles.UserInformation.ID;
                RequestData.Type = "POSLog";
                var ResponseData = new SelectShiftLogResponse();
                ResponseData = _ShiftBLL.SelectShiftLogRecordbyID(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.shiftlog = ResponseData.ShiftTypesData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void SelectAllShiftLog()
        //{
        //    try
        //    {
        //        var RequestData = new SelectShiftLogRequest();
        //        RequestData.ShowInActiveRecords = false;                              
        //        var ResponseData = new SelectShiftLogResponse();
        //        ResponseData = _ShiftBLL.SelectAllShiftLog(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IShiftTiles.AllShiftLOGTypesList = ResponseData.AllShiftLOGTypesList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void SelectJoinShiftMasterandLog()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectShiftLogResponse();
                RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                ResponseData = _ShiftBLL.SelectJoinShiftMasterandLog(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.AllShiftMasterandLogList = ResponseData.AllShiftLOGandTypesList;
                }
                else
                {
                    _IShiftTiles.AllShiftMasterandLogList = new List<ShiftMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectJoinShiftLogShift()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectShiftLogResponse();
                RequestData.Type = "Shift";
                RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                RequestData.RequestedByUserID = _IShiftTiles.UserInformation.ID;
                ResponseData = _ShiftBLL.SelectJoinShiftMasterandLog(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.AllShiftMasterandLogList = ResponseData.AllShiftLOGandTypesList;                   
                }
                else
                {
                    _IShiftTiles.AllShiftMasterandLogList = new List<ShiftMaster>();                    
                }
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }       
        public void SelectShiftInEnabled()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectShiftLogResponse();
                RequestData.Type = "Shift";
                RequestData.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.POSID = _IShiftTiles.UserInformation.POSID;
                ResponseData = _ShiftBLL.SelectShiftInEnabled(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.MaxShiftMasterID1 = ResponseData.MaxShiftTypesData1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectMaxShiftInEnabled()
        {
            try
            {
                var RequestData = new SelectShiftLogRequest();
                RequestData.ShowInActiveRecords = false;                
                var ResponseData = new SelectShiftLogResponse();               
                ResponseData = _ShiftBLL.SelectMaxShiftInEnabled(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.MaxShiftMasterID = ResponseData.MaxShiftTypesData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveDayClosing()
        {
            try
            {
                DateTime BusinessDate = DateTime.Now;

                SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                RequestData.DayClosingRecord = new DayClosing();

                RequestData.DayClosingRecord = _IShiftTiles.DayClosingRecord;
                SaveDayClosingResponse ResponseData = _DayClosingBLL.SaveDayClosing(RequestData);

                _IShiftTiles.ProcessStatus = ResponseData.StatusCode;

                //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                //{
                //    _IShiftTiles.BusinessDate = BusinessDate;
                //}
                //else if (ResponseData.StatusCode == Enums.OpStatusCode.DuplicateRecordFound)
                //{
                //    _IShiftTiles.Message = ResponseData.DisplayMessage;
                //    _IShiftTiles.BusinessDate = DateTime.Now.AddDays(1);
                //}
                //else if (ResponseData.StatusCode == 0)
                //{
                //    _IShiftTiles.BusinessDate = BusinessDate;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectManagerOverride(string Source)
        {
            try
            {
                var _ManagerOverrideBLL = new ManagerOverrideBLL();
                var RequestData = new SelectByIDManagerOverrideRequest();
                RequestData.ID = _IShiftTiles.ManagerOverrideID;
                var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                if (Source == "PAGELOAD")
                {
                    _IShiftTiles.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
                else
                {
                    _IShiftTiles.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public void UpdateDayClosing()
        {
            try
            {
                SaveDayClosingRequest RequestData = new SaveDayClosingRequest();
                RequestData.DayClosingRecord = new DayClosing();
                RequestData.DayClosingRecord.BuisnessDate = _IShiftTiles.BusinessDate;
                RequestData.DayClosingRecord.StartingTime = _IShiftTiles.BusinessDate;
                RequestData.DayClosingRecord.StoreID = _IShiftTiles.UserInformation.StoreID;
                RequestData.DayClosingRecord.CountryID = _IShiftTiles.UserInformation.CountryID;
                RequestData.DayClosingRecord.POSID = _IShiftTiles.UserInformation.POSID;
                RequestData.DayClosingRecord.ShiftID = 0;
                RequestData.DayClosingRecord.Amount = 0;
                RequestData.DayClosingRecord.ShiftOutUserID = _IShiftTiles.UserInformation.ID;
                RequestData.DayClosingRecord.Status = "Close";
                RequestData.DayClosingRecord.ClosingTime = DateTime.Now;
                SaveDayClosingResponse ResponseData = _DayClosingBLL.UpdateDayClosing(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectZReport()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.ZReportList = ResponseData.ZReportList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectZReport1()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.ZReportList1 = ResponseData.ZReportList1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectZReport2()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt2(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftTiles.ZReportList2 = ResponseData.Zreport2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteHoldSaleRecords()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new DeleteHoldSaleRecordsRequest();
                var ResponseData = new DeleteHoldSaleRecordsResponse();
                RequestData.BusinessDate = _IShiftTiles.BusinessDate;
                RequestData.StoreID = _IShiftTiles.UserInformation.StoreID;
                ResponseData = _InvoiceBLL.DeleteHoldSaleRecords(RequestData);
                //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                //{
                //    _IShiftTiles.Message = "The parked transaction are deleted from the server !.";
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
