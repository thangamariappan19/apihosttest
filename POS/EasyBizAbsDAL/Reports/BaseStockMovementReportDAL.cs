using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.StockMovementReport;
using EasyBizResponse.Reports.StockMovementReportResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports
{
    public abstract class BaseStockMovementReportDAL : BaseDAL
    {
        public abstract StockMovementReportResponse StockMovementReport(StockMovementReportRequest ObjRequest);
    }
}
