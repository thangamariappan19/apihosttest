using EasyBizFactory;
using EasyBizRequest.Reports.DailySalesReport;
using EasyBizResponse.Reports.DailySalesReportResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports
{
    public class DailySalesReportBLL
    {
        public DailySalesReportResponse DailySalesReport(DailySalesReportRequest objRequest)
        {
            DailySalesReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseDailySalesReportDAL = objFactory.GetDALRepository().GetBaseDailySalesReportDAL();
                objResponse = (DailySalesReportResponse)objBaseDailySalesReportDAL.DailySalesReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new DailySalesReportResponse();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
