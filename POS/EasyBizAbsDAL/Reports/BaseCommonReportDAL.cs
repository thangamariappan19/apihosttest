using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports
{
    public abstract class BaseCommonReportDAL : BaseDAL
    {
        public abstract CommonReportRespose GetSalesInvoiceReportData(CommonReportRequest ObjRequest);

        public abstract CommonReportRespose GetSalesReturnReportData(CommonReportRequest ObjRequest);
        public abstract CommonReportRespose GetSalesManWiseReport(CommonReportRequest ObjRequest);       
        public abstract CommonReportRespose GetCashierWiseReport(CommonReportRequest ObjRequest);
        public abstract CommonReportRespose GetStockReceiptReport(CommonReportRequest ObjRequest);
        public abstract CommonReportRespose GetDetailedShowroomSalesReport(CommonReportRequest ObjRequest);
        public abstract CommonReportRespose GetDetailedStockReturnReport(CommonReportRequest RequestData);
        public abstract CommonReportRespose GetDetailedStockReceiptReport(CommonReportRequest RequestData);
        public abstract CommonReportRespose GetDayWiseActivitiesReport(CommonReportRequest RequestData);
      
    }
}
