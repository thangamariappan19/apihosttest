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
  public class DetailedShowroomSalesPresenter
     
    {
      IDetailedShowroomSales _IDetailedShowroomSales;

      public DetailedShowroomSalesPresenter(IDetailedShowroomSales ViewObj)
        {
            _IDetailedShowroomSales = ViewObj;
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
                  _IDetailedShowroomSales.StoreList = ResponseData.StoreMasterList;
              }
              else
              {
                  _IDetailedShowroomSales.StoreList = new List<StoreMaster>();
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      public void GetDetailedShowroomSalesReportData()
      {
          var _ReportsBLL = new ReportsBLL();
          var RequestData = new CommonReportRequest();
          var ResponseData = new CommonReportRespose();

          RequestData.FromDate = _IDetailedShowroomSales.BusinessDate;
          RequestData.StoreID = _IDetailedShowroomSales.SelectedStoreId;

          ResponseData = _ReportsBLL.GetDetailedShowroomSalesReport(RequestData);
          if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
          {
              _IDetailedShowroomSales.DetailedShowroomSalesReportTable = ResponseData.ReportDataTable;
          }
          else
          {
              _IDetailedShowroomSales.Message = ResponseData.DisplayMessage;
              _IDetailedShowroomSales.DetailedShowroomSalesReportTable = new System.Data.DataTable();
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
              RequestData.ID = _IDetailedShowroomSales.SelectedStoreId;
              ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _IDetailedShowroomSales.StoreMasterRecord = ResponseData.StoreMasterData;

              }
              else
              {
                  _IDetailedShowroomSales.StoreList = new List<StoreMaster>();
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    }
}
