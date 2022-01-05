using EasyBizBLL.Dashboard;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Dashboard;
using EasyBizIView.Dashboard;
using EasyBizRequest.DashBoardRequest;
using EasyBizResponse.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.DashBoard
{
    public class RegisterDashboardPresenter
    {
        IDashBoardReportView _IDashBoardReportView;
        RegisterDashBoardBLL _DashBoardReportsBLL = new RegisterDashBoardBLL();
        public RegisterDashboardPresenter(IDashBoardReportView Viewobj)
        {
            _IDashBoardReportView = Viewobj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDashBoardReportView.ReportName.Trim() == string.Empty)
            {
                _IDashBoardReportView.Message = "  Please Enter Report Name.";
            }
            else if (_IDashBoardReportView.ReportFile == null)
            {
                _IDashBoardReportView.Message = " Please Upload Report File";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveDashBoardReports()
        {
            try
            {
                if (IsValidForm())
                {
                    SaveDashBoardRequest RequestData = new SaveDashBoardRequest();
                    RequestData.DashBoardReportsRecord = new RegisterDashboard();
                    RequestData.DashBoardReportsRecord.ReportName = _IDashBoardReportView.ReportName;
                    // RequestData.DashBoardReportsRecord.AccessRoles = _IDashBoardReportView.AccessRoles;            
                    RequestData.DashBoardReportsRecord.Remarks = _IDashBoardReportView.Remarks;
                    RequestData.DashBoardReportsRecord.ReportFile = _IDashBoardReportView.ReportFile;
                    RequestData.DashBoardReportsRecord.CreateBy = _IDashBoardReportView.UserID;
                    RequestData.DashBoardReportsRecord.CreateOn = DateTime.Now;
                    RequestData.DashBoardReportsRecord.IsActive = _IDashBoardReportView.IsActive;
                    RequestData.DashBoardReportsRecord.SCN = _IDashBoardReportView.SCN;
                    SaveDashBoardResponse ResponseData = _DashBoardReportsBLL.SaveDashBoard(RequestData);

                    _IDashBoardReportView.Message = ResponseData.DisplayMessage;
                    _IDashBoardReportView.ProcessStatus = ResponseData.StatusCode;
                }

            }
            catch
            {

            }
        }
        public void UpdateDashBoardReports()
        {
            try
            {
                if (IsValidForm())
                {
                    UpdateDashBoardRequest RequestData = new UpdateDashBoardRequest();
                    RequestData.DashBoardReportsRecord = new RegisterDashboard();
                    //  RequestData.ShowIsActiveRecords = true;
                    RequestData.DashBoardReportsRecord.ID = _IDashBoardReportView.ID;
                    RequestData.DashBoardReportsRecord.ReportName = _IDashBoardReportView.ReportName;
                    //RequestData.DashBoardReportsRecord.AccessRoles = _IDashBoardReportView.AccessRoles;
                    //RequestData.DashBoardReportsRecord.Purpose = _IDashBoardReportView.Purpose;
                    RequestData.DashBoardReportsRecord.Remarks = _IDashBoardReportView.Remarks;
                    RequestData.DashBoardReportsRecord.ReportFile = _IDashBoardReportView.ReportFile;
                    RequestData.DashBoardReportsRecord.CreateBy = _IDashBoardReportView.UserID;
                    RequestData.DashBoardReportsRecord.CreateOn = DateTime.Now;
                    RequestData.DashBoardReportsRecord.IsActive = _IDashBoardReportView.IsActive;
                    RequestData.DashBoardReportsRecord.SCN = _IDashBoardReportView.SCN;

                    UpdateRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.UpdateDashBoard(RequestData);

                    _IDashBoardReportView.Message = ResponseData.DisplayMessage;
                    _IDashBoardReportView.ProcessStatus = ResponseData.StatusCode;
                    //}
                    //else
                    //{
                    //    _IDashBoardReportView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch
            {

            }
        }
        public void DeleteDashBoardReports()
        {
            try
            {
                DeleteRegisterDashboardRequest RequestData = new DeleteRegisterDashboardRequest();
                RequestData.ID = _IDashBoardReportView.ID;
                DeleteRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.DeleteDashBoard(RequestData);
                _IDashBoardReportView.Message = ResponseData.DisplayMessage;
                _IDashBoardReportView.ProcessStatus = ResponseData.StatusCode;
            }
            catch
            {

            }
        }

        public void GetDashBoardReportRecord()
        {
            try
            {
                SelectDashBoardRequest RequestData = new SelectDashBoardRequest();
                RequestData.ID = _IDashBoardReportView.ID;
                SelectRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.SelectDashBoardReportRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDashBoardReportView.ReportName = ResponseData.DashBoardReportsRecord.ReportName;
                    //_IDashBoardReportView.AccessRoles = ResponseData.DashBoardReportsRecord.AccessRoles;
                    //_IDashBoardReportView.Purpose = ResponseData.DashBoardReportsRecord.Purpose;
                    _IDashBoardReportView.Remarks = ResponseData.DashBoardReportsRecord.Remarks;
                    _IDashBoardReportView.ReportFile = ResponseData.DashBoardReportsRecord.ReportFile;
                    _IDashBoardReportView.IsActive = ResponseData.DashBoardReportsRecord.IsActive;
                    _IDashBoardReportView.SCN = ResponseData.DashBoardReportsRecord.SCN;
                }
            }
            catch
            {

            }
        }
        //public void GetAllDashBoardReportList()
        //{
        //    try
        //    {
        //        SelectAllRegisterDashboardRequest RequestData = new SelectAllRegisterDashboardRequest();
        //        RequestData.ShowIsActiveRecords = true;
        //        SelectAllRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.GetAllDashBoardReportList(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IDashBoardReportListView.DashBoardReportsList = ResponseData.DashBoardReportsList;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
    }

    public class DashBoardReportListPresenter
    {
        IDashBoardReportListView _IDashBoardReportListView;
        RegisterDashBoardBLL _DashBoardReportsBLL = new RegisterDashBoardBLL();
        public DashBoardReportListPresenter(IDashBoardReportListView ViewObj)
        {
            _IDashBoardReportListView = ViewObj;
        }
        public void GetAllDashBoardReportList()
        {
            try
            {
                SelectAllRegisterDashboardRequest RequestData = new SelectAllRegisterDashboardRequest();
                // RequestData.ShowIsActiveRecords = true;
                SelectAllRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.GetAllDashBoardReportList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDashBoardReportListView.DashBoardReportsList = ResponseData.DashBoardReportsList;
                }
            }
            catch
            {

            }
        }
    }
}
