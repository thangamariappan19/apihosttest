using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Reports.UserReports;
using EasyBizResponse.Reports.UserReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
    public class UserReportRegisterPresenter
    {
        IUserReportRegister _IUserReportRegister;
        UserReportBLL _UserReportBLL = new UserReportBLL();
        public UserReportRegisterPresenter(IUserReportRegister ViewObj)
        {
            _IUserReportRegister = ViewObj;
        }
        public void GetRollLookUp()
        {
            try
            {
                var _RoleBLL = new RoleBLL();
                var RequestData = new SelectRoleMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _RoleBLL.SelectRoleLookUP(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IUserReportRegister.RoleMasterList = ResponseData.RoleMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveUserReportRegister()
        {
            try
            {
                var RequestData = new InsertUserReportRequest();
                var ResponseData = new InsertUserReportResponse();

                RequestData.UserReportRecord = _IUserReportRegister.UserReportRecord;
                ResponseData = _UserReportBLL.SaveUserReport(RequestData);
                _IUserReportRegister.Message = ResponseData.DisplayMessage;
                _IUserReportRegister.ProcessStatus = ResponseData.StatusCode;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    public class UserReportListViewPresenter
    {
        UserReportBLL _UserReportBLL = new UserReportBLL();
        IUserReportRegisterView _IUserReportRegisterView;
        public UserReportListViewPresenter(IUserReportRegisterView ViewObj)
        {
            _IUserReportRegisterView = ViewObj;
        }
        public void GetUserReportRegisterList()
        {
            try
            {
                var RequestData = new SelectAllUserReportRequest();
                var ResponseData = new SelectAllUserReportResponse();
                RequestData.ShowInActiveRecords = true;
                ResponseData = _UserReportBLL.SelectAllUserReportList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IUserReportRegisterView.UserReportList = ResponseData.UserReportList;
                }
                else
                {
                    _IUserReportRegisterView.UserReportList = new List<UserReport>();
                }
            }
            catch (Exception ex)
            {
                _IUserReportRegisterView.UserReportList = new List<UserReport>();
                throw ex;
            }
        }
    }
}
