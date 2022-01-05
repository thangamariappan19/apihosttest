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
   public class DetailSalesReturnReportPresenter
    {
       IDetailSalesReturnReportView _IDetailSalesReturnReportView;

       public DetailSalesReturnReportPresenter(IDetailSalesReturnReportView ViewObj)
        {
            _IDetailSalesReturnReportView = ViewObj;
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
                   _IDetailSalesReturnReportView.StoreList = ResponseData.StoreMasterList;
               }
               else
               {
                   _IDetailSalesReturnReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void GetSalesReturnReportData()
       {
           var _ReportsBLL = new ReportsBLL();
           var RequestData = new CommonReportRequest();
           var ResponseData = new CommonReportRespose();

           RequestData.InvoiceNo = _IDetailSalesReturnReportView.InvoiceNo;
           RequestData.StoreID = _IDetailSalesReturnReportView.SelectedStoreId;
           RequestData.MODE = 2;

           ResponseData = _ReportsBLL.GetSalesReturnReportData(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IDetailSalesReturnReportView.DetailSalesReturnDataTable = ResponseData.ReportDataTable;
           }
           else
           {
               _IDetailSalesReturnReportView.Message = ResponseData.DisplayMessage;
               _IDetailSalesReturnReportView.DetailSalesReturnDataTable = new System.Data.DataTable();
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
               RequestData.ID = _IDetailSalesReturnReportView.SelectedStoreId;
               ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IDetailSalesReturnReportView.StoreMasterRecord = ResponseData.StoreMasterData;

               }
               else
               {
                   _IDetailSalesReturnReportView.StoreList = new List<StoreMaster>();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
