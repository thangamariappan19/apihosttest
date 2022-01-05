using EasyBizFactory;
using EasyBizRequest.Reports;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Reports
{
    public class ReportsBLL
    {
        public CommonReportRespose GetSalesInvoiceReportData(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetSalesInvoiceReportData(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetSalesReturnReportData(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetSalesReturnReportData(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public CommonReportRespose GetSalesManWiseReport(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetSalesManWiseReport(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetCashierWiseReport(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetCashierWiseReport(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public CommonReportRespose GetStockReceiptReport(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetStockReceiptReport(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetDetailedShowroomSalesReport(CommonReportRequest ObjRequest)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetDetailedShowroomSalesReport(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetDetailedStockReturnReportData(CommonReportRequest RequestData)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetDetailedStockReturnReport(RequestData);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetDetailedStockReceiptReportData(CommonReportRequest RequestData)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetDetailedStockReceiptReport(RequestData);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public CommonReportRespose GetDayWiseActivitiesReport(CommonReportRequest RequestData)
        {
            CommonReportRespose objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objCommonReportDAL = objFactory.GetDALRepository().GetCommonReportDAL();
                objResponse = (CommonReportRespose)objCommonReportDAL.GetDayWiseActivitiesReport(RequestData);
            }
            catch (Exception ex)
            {
                objResponse = new CommonReportRespose();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }

}
