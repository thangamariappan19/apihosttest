using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizFactory;
using EasyBizRequest.Reports.UserReports;
using EasyBizResponse.Reports.UserReports;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports
{
    public class UserReportBLL
    {
        public InsertUserReportResponse SaveUserReport(InsertUserReportRequest objRequest)
        {
            InsertUserReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.UserReportRecord = (UserReport)objRequest.RequestDynamicData;
                }
                var objUserReportDAL = objFactory.GetDALRepository().GetUserReportDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objUserReport = new UserReport();
                    objUserReport = (UserReport)objRequest.RequestDynamicData;

                    objRequest.UserReportRecord = objUserReport;
                }
                objResponse = (InsertUserReportResponse)objUserReportDAL.InsertRecord(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.UserReportRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentNos = objRequest.UserReportRecord.ReportName;
                    objRequest.DocumentType = Enums.DocumentType.USERREPORTS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Reports.UserReportBLL", "SaveUserReport");
                }
            }
            catch (Exception ex)
            {
                objResponse = new InsertUserReportResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "User Report");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllUserReportResponse SelectAllUserReportList(SelectAllUserReportRequest objRequest)
        {
            SelectAllUserReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objUserReportDAL = objFactory.GetDALRepository().GetUserReportDAL();
                objResponse = (SelectAllUserReportResponse)objUserReportDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllUserReportResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "User Report");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
