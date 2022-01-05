using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Reports;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
   public class DayWiseActivitiesReportPresenter
    { 
       IDayWiseActivitiesReportView _IDayWiseActivitiesReportView;
       public DayWiseActivitiesReportPresenter(IDayWiseActivitiesReportView ViewObj)
        {
            _IDayWiseActivitiesReportView = ViewObj;
        }

       public void GetStoreListByID()
       {
           try
           {
               var _StoreMasterBLL = new StoreMasterBLL();
               var RequestData = new SelectByIDStoreMasterRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectByIDStoreMasterResponse();
               RequestData.ID = _IDayWiseActivitiesReportView.SelectedStoreId;
               ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IDayWiseActivitiesReportView.StoreMasterRecord = ResponseData.StoreMasterData;

               }
               else
               {
                   _IDayWiseActivitiesReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void GetStoreList()
       {
           try
           {
               var _StoreMasterBLL = new StoreMasterBLL();
               var RequestData = new SelectAllStoreMasterRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllStoreMasterResponse();
               ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IDayWiseActivitiesReportView.StoreList = ResponseData.StoreMasterList;
               }
               else
               {
                   _IDayWiseActivitiesReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void GetDayWiseActivityReportData()
       {
           var _ReportsBLL = new ReportsBLL();
           var RequestData = new CommonReportRequest();
           var ResponseData = new CommonReportRespose();

           RequestData.FromDate = _IDayWiseActivitiesReportView.BusinessDate;
           RequestData.StoreID = _IDayWiseActivitiesReportView.SelectedStoreId;

           ResponseData = _ReportsBLL.GetDayWiseActivitiesReport(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IDayWiseActivitiesReportView.DayWiseActivitiesReportTable = ResponseData.ReportDataTable;
           }
           else
           {
               _IDayWiseActivitiesReportView.Message = ResponseData.DisplayMessage;
              // _IDayWiseActivitiesReportView.DayWiseActivitiesReportTable = new System.Data.DataTable();
           }
       }
    }
}
