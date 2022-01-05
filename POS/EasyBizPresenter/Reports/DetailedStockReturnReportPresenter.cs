using EasyBizBLL.Reports;
using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizIView.Reports;
using EasyBizRequest.Reports;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Reports;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
  public  class DetailedStockReturnReportPresenter
    {
      IDetailedStockReturnReportView _IDetailedStockReturnReportView;

      public DetailedStockReturnReportPresenter(IDetailedStockReturnReportView ViewObj)
        {
            _IDetailedStockReturnReportView = ViewObj;
        }

      //public void GetDetailedStockReturnReport()
      //{
      //    try
      //    {
      //        var _StockReturnBLL = new StockReturnBLL();
      //        var RequestData = new SelectByStockReturnIDRequest();
      //        RequestData.ShowInActiveRecords = true;
      //        var ResponseData = new SelectByStockReturnIDResponse();
      //        RequestData.ID = _IDetailedStockReturnReportView.ID;
      //        ResponseData = _StockReturnBLL.GetDetailedStockReturnID(RequestData);
      //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
      //        {
      //            _IDetailedStockReturnReportView.StockReturnRecord = ResponseData.StockReturnHeaderRecord;

      //        }
      //        else
      //        {
      //            _IDetailedStockReturnReportView.StockReturnList = new List<StockReturnHeader>();
      //        }
      //    }
      //    catch (Exception ex)
      //    {
      //        throw ex;
      //    }
      //}

      public void GetDetailedStockReturnReportData()
      {
          var _ReportsBLL = new ReportsBLL();
          var RequestData = new CommonReportRequest();
          var ResponseData = new CommonReportRespose();

          RequestData.StockReturnID = _IDetailedStockReturnReportView.ID;         

          ResponseData = _ReportsBLL.GetDetailedStockReturnReportData(RequestData);
          if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
          {
              _IDetailedStockReturnReportView.DetailStockreturnReportTable = ResponseData.ReportDataTable;
          }
          else
          {
              _IDetailedStockReturnReportView.Message = ResponseData.DisplayMessage;
            //  _IDetailedStockReturnReportView.DetailStockreturnReportTable = new System.Data.DataTable();
          }
      }
    }
}
