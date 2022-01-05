using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Common;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
   public class ClosesalesPresenter
    {
       ICloseSales _ICloseSales;
       DayShiftLOGBLL _DayShiftLOGBLL = new DayShiftLOGBLL();
       DayClosingBLL _DayClosingBLL = new DayClosingBLL();
       ShiftBLL _ShiftBLL = new ShiftBLL();
       UsersBLL _UsersBLL = new UsersBLL();
       public ClosesalesPresenter(ICloseSales ViewObj)
        {
            _ICloseSales = ViewObj;
        }
       
       public void SelectJoinShiftMasterandLog()
       {
           try
           {
               var RequestData = new SelectShiftLogRequest();
               RequestData.ShowInActiveRecords = false;
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               var ResponseData = new SelectShiftLogResponse();
               RequestData.CountryID = _ICloseSales.UserInformation.CountryID;
               RequestData.StoreID = _ICloseSales.UserInformation.StoreID;
               RequestData.POSID = _ICloseSales.UserInformation.POSID;
               ResponseData = _ShiftBLL.SelectJoinShiftMasterandLog(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICloseSales.AllShiftMasterandLogList = ResponseData.AllShiftLOGandTypesList;
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
                   RequestData.UserName = _ICloseSales.UserInformation.UserName;
                   RequestData.Password = _ICloseSales.UserInformation.Password;
                   RequestData.FromOrToStoreID = _ICloseSales.UserInformation.StoreID;
                   RequestData.FromOrToStoreCode = _ICloseSales.UserInformation.StoreCode;
                   RequestData.POSID = _ICloseSales.UserInformation.POSID;
                   RequestData.SourceFrom = "POS";
                   RequestData.CheckLoggedIn = "LogOut";
                   RequestData.ShowInActiveRecords = true;
                   RequestData.RequestFrom = _ICloseSales.RequestFrom;
                   RequestData.StoreID = _ICloseSales.UserInformation.StoreID;

                   SelectLogInResponse ResponseData = _UsersBLL.SelectLogIn(RequestData);                                   
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
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               var ResponseData = new SelectZReportByDetailsResponse();              
               RequestData.StoreID = _ICloseSales.UserInformation.StoreID;
               ResponseData = _DayShiftLOGBLL.GetZReceipt(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICloseSales.ZReportList = ResponseData.ZReportList;
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
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               var ResponseData = new SelectZReportByDetailsResponse();
               RequestData.StoreID = _ICloseSales.UserInformation.StoreID;
               ResponseData = _DayShiftLOGBLL.GetZReceipt1(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICloseSales.ZReportList1 = ResponseData.ZReportList1;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetXReceipt()
       {
           try
           {
               var RequestData = new SelectXReportByDetailsRequest();
               RequestData.ShowInActiveRecords = false;
               RequestData.CashierID = _ICloseSales.UserInformation.ID;
               RequestData.ShiftID = _ICloseSales.ShiftID;
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               RequestData.StoreID = _ICloseSales.UserInformation.StoreID;
               RequestData.POSID = _ICloseSales.UserInformation.POSID;
               var ResponseData = new SelectXReportByDetailsResponse();
               ResponseData = _DayShiftLOGBLL.GetXReceipt(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICloseSales.XreportTypesList = ResponseData.XReportList;
               }
               else
               {
                   _ICloseSales.XreportTypesList = new List<XreportTypes>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetXReceipt1()
       {
           try
           {
               var RequestData = new SelectXReportByDetailsRequest();
               RequestData.ShowInActiveRecords = false;
               RequestData.CashierID = _ICloseSales.UserInformation.ID;
               RequestData.ShiftID = _ICloseSales.ShiftID;
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               RequestData.StoreID = _ICloseSales.UserInformation.StoreID;
               RequestData.POSID = _ICloseSales.UserInformation.POSID;
               var ResponseData = new SelectXReportByDetailsResponse();
               ResponseData = _DayShiftLOGBLL.GetXReceipt1(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICloseSales.XSubreportList = ResponseData.XSubReportList;
               }
               else
               {
                   _ICloseSales.XSubreportList = new List<XSubreportTypes>();
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
               RequestData.DayClosingRecord.BuisnessDate = _ICloseSales.BusinessDate;
               RequestData.DayClosingRecord.StartingTime = _ICloseSales.BusinessDate;
               RequestData.DayClosingRecord.StoreID = _ICloseSales.UserInformation.StoreID;
               RequestData.DayClosingRecord.CountryID = _ICloseSales.UserInformation.CountryID;
               RequestData.DayClosingRecord.POSID = _ICloseSales.UserInformation.POSID;
               RequestData.DayClosingRecord.ShiftID = _ICloseSales.ShiftID;
               RequestData.DayClosingRecord.Amount = _ICloseSales.amount;
               RequestData.DayClosingRecord.ShiftOutUserID = _ICloseSales.UserInformation.ID;
               RequestData.DayClosingRecord.Status = "Close";
               RequestData.DayClosingRecord.ClosingTime = DateTime.Now;
               SaveDayClosingResponse ResponseData = _DayClosingBLL.UpdateDayClosing(RequestData);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateShift()
       {
           try
           {
               DateTime BusinessDate = DateTime.Now;
               SaveShiftLOGRequest RequestData = new SaveShiftLOGRequest();
               RequestData.ShiftRecord = new ShiftLOGTypes();
               RequestData.ShiftRecord.CountryID = _ICloseSales.UserInformation.CountryID;
               RequestData.ShiftRecord.StoreID = _ICloseSales.UserInformation.StoreID;
               RequestData.ShiftRecord.POSID = _ICloseSales.UserInformation.POSID;
               RequestData.ShiftRecord.ShiftID = _ICloseSales.ShiftID;
               RequestData.ShiftRecord.Amount = _ICloseSales.amount;
               RequestData.ShiftRecord.BusinessDate = _ICloseSales.BusinessDate;
               RequestData.ShiftRecord.Status = "Open";
               RequestData.ShiftRecord.ShiftOutUserID = _ICloseSales.UserInformation.ID;
               RequestData.ShiftRecord.ShiftInUserID = _ICloseSales.UserInformation.ID;
               RequestData.ShiftRecord.ShiftInOutUserCode = _ICloseSales.UserInformation.UserCode;
               SaveShiftLOGResponse ResponseData = _DayShiftLOGBLL.UpdateDayClosing(RequestData);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void SelectJoinShiftAmount()
       {
           try
           {
               var RequestData = new SelectShiftLogRequest();
               RequestData.ShowInActiveRecords = false;
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               RequestData.ShiftID = _ICloseSales.ShiftID;
               RequestData.POSID = _ICloseSales.POSID;
               var ResponseData = new SelectShiftLogResponse();
               ResponseData = _DayClosingBLL.SelectJoinShiftAmount(RequestData);
               _ICloseSales.ShiftAmount = ResponseData.ShiftAmount.Amount;
               _ICloseSales.Cardamount = ResponseData.ShiftAmount.CardAmount;
               _ICloseSales.Shiftlogamount = ResponseData.ShiftAmount.ShiftAmount;
               _ICloseSales.SalesRetunAmount = ResponseData.ShiftAmount.SalesRetunAmount;
               _ICloseSales.CashIn = ResponseData.ShiftAmount.CashInAmount;
               _ICloseSales.CashOut = ResponseData.ShiftAmount.CashOutAmount;
               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _ICloseSales.Message = ResponseData.DisplayMessage;
               }
               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _ICloseSales.Message = ResponseData.DisplayMessage;
               }
               _ICloseSales.ProcessStatus = ResponseData.StatusCode;
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
               RequestData.BusinessDate = _ICloseSales.BusinessDate;
               RequestData.StoreID = _ICloseSales.StoreID;
               ResponseData = _InvoiceBLL.DeleteHoldSaleRecords(RequestData);
               //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               //{
               //    _ICloseSales.Message = "The parked transaction are deleted from the server !.";
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
               RequestData.ID = _ICloseSales.ManagerOverrideID;
               var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
               if (Source == "PAGELOAD")
               {
                   _ICloseSales.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
               }
               else
               {
                   _ICloseSales.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
