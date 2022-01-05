using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.DailySalesReport;
using EasyBizResponse.Reports.DailySalesReportResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports
{

    public abstract class BaseDailySalesReportDAL : BaseDAL
    {
        public abstract DailySalesReportResponse DailySalesReport(DailySalesReportRequest ObjRequest);
        
    }
}
