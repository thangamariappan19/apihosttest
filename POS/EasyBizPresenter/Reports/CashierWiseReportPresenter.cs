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
   public class CashierWiseReportPresenter
    {
       ICashierWiseReportView _ICashierWiseReportView;
       public CashierWiseReportPresenter(ICashierWiseReportView ViewObj)
        {
            _ICashierWiseReportView = ViewObj;
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
                   _ICashierWiseReportView.StoreList = ResponseData.StoreMasterList;
               }
               else
               {
                   _ICashierWiseReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void GetCashierWiseReportData()
       {
           var _ReportsBLL = new ReportsBLL();
           var RequestData = new CommonReportRequest();
           var ResponseData = new CommonReportRespose();

           RequestData.FromDate = _ICashierWiseReportView.BusinessDate;
           RequestData.StoreID = _ICashierWiseReportView.SelectedStoreId;

           ResponseData = _ReportsBLL.GetCashierWiseReport(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _ICashierWiseReportView.CashierWiseReportTable = ResponseData.ReportDataTable;
           }
           else
           {
               _ICashierWiseReportView.Message = ResponseData.DisplayMessage;
               _ICashierWiseReportView.CashierWiseReportTable = new System.Data.DataTable();
           }
       }

       public void GetStoreListByID()
       {
           try
           {
               var _StoreMasterBLL = new StoreMasterBLL();
               var RequestData = new SelectByIDStoreMasterRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectByIDStoreMasterResponse();
               RequestData.ID = _ICashierWiseReportView.SelectedStoreId;
               ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICashierWiseReportView.StoreMasterRecord = ResponseData.StoreMasterData;

               }
               else
               {
                   _ICashierWiseReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
