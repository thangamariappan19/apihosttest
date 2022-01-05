using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizIView.Reports;
using EasyBizRequest.Reports;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
   public class DetailStockReceiptReportPresenter
    {
       IDetailedStockReceiptReportView _IDetailedStockReceiptReportView;

       public DetailStockReceiptReportPresenter(IDetailedStockReceiptReportView ViewObj)
        {
            _IDetailedStockReceiptReportView = ViewObj;
        }
        public void GetDetailedStockReceiptReportData()
        {
            var _ReportsBLL = new ReportsBLL();
            var RequestData = new CommonReportRequest();
            var ResponseData = new CommonReportRespose();

            RequestData.StockReceiptID = _IDetailedStockReceiptReportView.ID;

            ResponseData = _ReportsBLL.GetDetailedStockReceiptReportData(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDetailedStockReceiptReportView.DetailStockReceiptReportTable = ResponseData.ReportDataTable;
            }
            else
            {
                _IDetailedStockReceiptReportView.Message = ResponseData.DisplayMessage;
                //_IDetailedStockReceiptReportView.DetailStockReceiptReportTable = new System.Data.DataTable();
            }
        }
    }
}
