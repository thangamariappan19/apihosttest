using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.UsersResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class XReportViewerPresenter
    {
        IXReportView _IXReportView;
        DayShiftLOGBLL _DayShiftLOGBLL = new DayShiftLOGBLL();
        ShiftBLL _ShiftBLL = new ShiftBLL();
        UsersBLL _UsersBLL = new UsersBLL();
        PosMasterBLL _PosMasterBLL = new PosMasterBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public XReportViewerPresenter(IXReportView ViewObj)
        {
            _IXReportView = ViewObj;
        }       

        public void GetXReceipt()
        {
            try
            {
                var RequestData = new SelectXReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CashierID = _IXReportView.UserID;
                RequestData.ShiftID = _IXReportView.ShiftID;
                RequestData.BusinessDate = _IXReportView.BusinessDate;
                RequestData.StoreID = _IXReportView.UserInformation.StoreID;
                RequestData.POSID = _IXReportView.POSID;
                var ResponseData = new SelectXReportByDetailsResponse();
                ResponseData = _DayShiftLOGBLL.GetXReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IXReportView.XreportTypesList = ResponseData.XReportList;
                }
                else
                {
                    _IXReportView.XreportTypesList = new List<XreportTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetShift()
        {
            try
            {
                var RequestData = new SelectShiftLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = new SelectShiftLookUpResponse();
                RequestData.BusinessDate = _IXReportView.BusinessDate;
                RequestData.StoreID = _IXReportView.UserInformation.StoreID;
                RequestData.POSID = _IXReportView.POSID;
                RequestData.CashierID = _IXReportView.UserID;
                RequestData.type= "XREPORT";
                ResponseData = _ShiftBLL.SelectAllShiftRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IXReportView.ShiftMasterLookUp = ResponseData.ShiftList;
                }
                else
                {
                    _IXReportView.ShiftMasterLookUp = new List<ShiftMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetPOS()
        {
            try
            {
                var RequestData = new SelectAllPosMasterRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = new SelectAllPosMasterResponse();
                RequestData.StoreID = _IXReportView.StoreID;
                ResponseData = _PosMasterBLL.SelectAllPosMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IXReportView.PosMasterLookUp = ResponseData.PosMasterList;
                }
                else
                {
                    _IXReportView.PosMasterLookUp = new List<PosMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IXReportView.StoreID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IXReportView.StoreMasterRecord = ResponseData.StoreMasterData;
            }
        }
        public void GetUser()
        {
            try
            {
                var RequestData = new SelectAllUsersRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = new SelectAllUsersResponse();
                RequestData.StoreID = _IXReportView.StoreID;
                ResponseData = _UsersBLL.SelectAllUsers(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IXReportView.UsersLookUp = ResponseData.UsersList;
                }
                else
                {
                    _IXReportView.UsersLookUp = new List<UsersSettings>();
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
                RequestData.CashierID = _IXReportView.UserID;
                RequestData.ShiftID = _IXReportView.ShiftID;
                RequestData.BusinessDate = _IXReportView.BusinessDate;
                RequestData.StoreID = _IXReportView.UserInformation.StoreID;
                RequestData.POSID = _IXReportView.POSID;
                var ResponseData = new SelectXReportByDetailsResponse();
                ResponseData = _DayShiftLOGBLL.GetXReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IXReportView.XSubreportList = ResponseData.XSubReportList;
                }
                else
                {
                    _IXReportView.XSubreportList = new List<XSubreportTypes>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
