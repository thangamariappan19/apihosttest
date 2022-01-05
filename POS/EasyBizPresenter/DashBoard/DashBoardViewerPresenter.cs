using EasyBizBLL.Dashboard;
using EasyBizDBTypes.Common;
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
      public class DashBoardViewerPresenter
        {
          DashboardViewerView _IDashBoardReportView;
          RegisterDashBoardBLL _DashBoardReportsBLL = new RegisterDashBoardBLL();
          public DashBoardViewerPresenter(DashboardViewerView ViewObj)
           {
            _IDashBoardReportView = ViewObj;
           }
     public void GetDashBoardReport()
        {
                try
            {
                SelectDashBoardRequest RequestData = new SelectDashBoardRequest();
                RequestData.ID = _IDashBoardReportView.ID;
                SelectRegisterDashBoardResponse ResponseData = _DashBoardReportsBLL.SelectDashBoardReportRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                   _IDashBoardReportView.ReportData = ResponseData.DashBoardReportsRecord.ReportFile;
                }
            }
            catch
            {

            }
        }
}
}
