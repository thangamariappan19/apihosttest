using EasyBizFactory;
using EasyBizRequest.Reports.CurrentStockReport;
using EasyBizResponse.Reports.CurrentStockReportResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports
{
    public class CurrentStockReportBLL
    {
        public CurrentStockReportResponse CurrentStockReport(CurrentStockReportRequest objRequest)
        {
            CurrentStockReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCurrentStockReportDAL = objFactory.GetDALRepository().GetBaseCurrentStockReportDAL();
                objResponse = (CurrentStockReportResponse)objBaseCurrentStockReportDAL.CurrentStockReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CurrentStockReportResponse();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
         
    }
}
