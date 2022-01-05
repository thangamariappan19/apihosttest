using EasyBizBLL.Masters;
using EasyBizBLL.Reports.DayWiseTransaction;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizIView.Transactions.IReports.IDayWiseTransaction;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports.DayWiseTransaction
{
    public class StockAdjustmentTransactionReportPresenter
    {
        IStockAdjustmentTransactionReport _IStockAdjustmentTransactionReport;
        StockAdjustmentTransactionReportBLL _StockAdjustmentTransactionReportBLL = new StockAdjustmentTransactionReportBLL();
        public StockAdjustmentTransactionReportPresenter(IStockAdjustmentTransactionReport ViewObj)
        {
            _IStockAdjustmentTransactionReport = ViewObj;
        }
        public void GetStockAdjustmentHeaderTransactionList()
        {
            try
            {
                var RequestData = new StockAdjustmentTransactionRequest();
                RequestData.RequestFrom = _IStockAdjustmentTransactionReport.RequestFrom;
                RequestData.FromDate = _IStockAdjustmentTransactionReport.FromDate;
                RequestData.ToDate = _IStockAdjustmentTransactionReport.ToDate;
                RequestData.StoreID = _IStockAdjustmentTransactionReport.StoreID;
                var ResponseData = _StockAdjustmentTransactionReportBLL.SelectAll(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockAdjustmentTransactionReport.StockAdjustmentHeaderTransactionList = ResponseData.StockAdjustmentHeaderTransactionList;
                    _IStockAdjustmentTransactionReport.StoreName = ResponseData.storename;
                    _IStockAdjustmentTransactionReport.FromDate = ResponseData.FromDate;
                    _IStockAdjustmentTransactionReport.ToDate = ResponseData.ToDate;
                    
                }
                else
                {                   
                    _IStockAdjustmentTransactionReport.StockAdjustmentHeaderTransactionList = ResponseData.StockAdjustmentHeaderTransactionList;
                    _IStockAdjustmentTransactionReport.StoreName = _IStockAdjustmentTransactionReport.StoreName.ToString();
                    _IStockAdjustmentTransactionReport.FromDate =  DateTime.Parse(_IStockAdjustmentTransactionReport.FromDate.ToString("dd/MMM/yyyy"));
                    _IStockAdjustmentTransactionReport.ToDate = DateTime.Parse(_IStockAdjustmentTransactionReport.ToDate.ToString("dd/MMM/yyyy"));
                    _IStockAdjustmentTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockAdjustmentHeaderTransactionReportList()
        {
            try
            {
              
                var RequestData = new StockAdjustmentTransactionRequest();
                var ResponseData = new StockAdjustmentTransactionResponse();
                RequestData.RequestFrom = _IStockAdjustmentTransactionReport.RequestFrom;
                RequestData.FromDate = _IStockAdjustmentTransactionReport.FromDate;
                RequestData.ToDate = _IStockAdjustmentTransactionReport.ToDate;
                RequestData.StoreID = _IStockAdjustmentTransactionReport.StoreID;
                ResponseData = _StockAdjustmentTransactionReportBLL.SelectAllReport(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockAdjustmentTransactionReport.StockAdjustmentHeaderTransactionReportList = ResponseData.ReportDataTable;
                }
                else
                {
                    _IStockAdjustmentTransactionReport.StockAdjustmentHeaderTransactionReportList = new System.Data.DataTable();
                    _IStockAdjustmentTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class StockAdjustmentDetailTransactionViewPresenter
    {
        IStockAdjustmentTransactionDetailsReport _IStockAdjustmentTransactionDetailsReport;
        public StockAdjustmentDetailTransactionViewPresenter(IStockAdjustmentTransactionDetailsReport ViewObj)
        {
            _IStockAdjustmentTransactionDetailsReport = ViewObj;
        }
        public void SelectStockAdjustmentdetailtransactionlist()
        {
            try
            {
                var _StockAdjustmentTransactionReportBLL = new StockAdjustmentTransactionReportBLL();
                var RequestData = new StockAdjustmentTransactionRequest();
                RequestData.ID = _IStockAdjustmentTransactionDetailsReport.ID;
                var ResponseData = _StockAdjustmentTransactionReportBLL.SelectDetailAdjustment(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockAdjustmentTransactionDetailsReport.StockAdjustmentTransactionDetailsList = ResponseData.StockAdjustmentDetailsTransactionList;
                    _IStockAdjustmentTransactionDetailsReport.StoreName = ResponseData.storename;
                    _IStockAdjustmentTransactionDetailsReport.AdjustmentNumber = ResponseData.AdjustmentNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }  
}
