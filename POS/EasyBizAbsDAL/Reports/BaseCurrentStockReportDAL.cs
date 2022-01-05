using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.CurrentStockReport;
using EasyBizResponse.Reports.CurrentStockReportResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports
{
    public abstract class BaseCurrentStockReportDAL : BaseDAL
    {
        public abstract CurrentStockReportResponse CurrentStockReport(CurrentStockReportRequest ObjRequest);
    }
}
