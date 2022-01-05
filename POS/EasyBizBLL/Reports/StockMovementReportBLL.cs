using EasyBizFactory;
using EasyBizRequest.Reports.CurrentStockReport;
using EasyBizRequest.Reports.StockMovementReport;
using EasyBizResponse.Reports.CurrentStockReportResponse;
using EasyBizResponse.Reports.StockMovementReportResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports
{
   public class StockMovementReportBLL
    {
       public StockMovementReportResponse StockMovementReport(StockMovementReportRequest objRequest)
        {
            StockMovementReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objStockMovementReportDAL= objFactory.GetDALRepository().GetStockMovementReportDAL();
                objResponse = (StockMovementReportResponse)objStockMovementReportDAL.StockMovementReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StockMovementReportResponse();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
