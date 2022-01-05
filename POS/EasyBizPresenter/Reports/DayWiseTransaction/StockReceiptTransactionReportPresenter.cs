using EasyBizBLL.Masters;
using EasyBizBLL.Reports.DayWiseTransaction;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizIView.Transactions.IReports.IDayWiseTransaction;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;

namespace EasyBizPresenter.Reports.DayWiseTransaction
{
    public class StockReceiptTransactionReportPresenter
    {
        IStockReceiptTransactionReport _IStockReceiptTransactionReport;
        StockReceiptTransactionReportBLL _StockReceiptTransactionReportBLL = new StockReceiptTransactionReportBLL();

        public StockReceiptTransactionReportPresenter(IStockReceiptTransactionReport ViewObj)
        {
            _IStockReceiptTransactionReport = ViewObj;
        }
        public void GetStockReceiptTransactionlist()
        {
            try
            {
                var RequestData = new StockReceiptTransactionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.FromDate = _IStockReceiptTransactionReport.FromDate;
                RequestData.ToDate = _IStockReceiptTransactionReport.ToDate;
                RequestData.StoreID = _IStockReceiptTransactionReport.StoreID;
                
                var ResponseData = new StockReceiptTransactionResponse();
                ResponseData = _StockReceiptTransactionReportBLL.SelectAllStockReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptTransactionReport.StockReceiptHeaderTransactionList = ResponseData.StockReceiptHeaderTransactionList;
                    _IStockReceiptTransactionReport.StoreName = ResponseData.storename;
                    _IStockReceiptTransactionReport.FromDate = ResponseData.FromDate;
                    _IStockReceiptTransactionReport.ToDate = ResponseData.ToDate;
                }
                else
                {
                    _IStockReceiptTransactionReport.StockReceiptHeaderTransactionList = ResponseData.StockReceiptHeaderTransactionList;
                    _IStockReceiptTransactionReport.StoreName = _IStockReceiptTransactionReport.StoreName.ToString();
                    _IStockReceiptTransactionReport.FromDate =  DateTime.Parse(_IStockReceiptTransactionReport.FromDate.ToString("dd/MMM/yyyy"));
                    _IStockReceiptTransactionReport.ToDate =  DateTime.Parse(_IStockReceiptTransactionReport.ToDate.ToString("dd/MMM/yyyy"));
                    _IStockReceiptTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockReceiptHeaderTransactionReportList()
        {
            try
            {

                var RequestData = new StockReceiptTransactionRequest();
                var ResponseData = new StockReceiptTransactionResponse();
                RequestData.RequestFrom = _IStockReceiptTransactionReport.RequestFrom;
                RequestData.FromDate = _IStockReceiptTransactionReport.FromDate;
                RequestData.ToDate = _IStockReceiptTransactionReport.ToDate;
                RequestData.StoreID = _IStockReceiptTransactionReport.StoreID;
                ResponseData = _StockReceiptTransactionReportBLL.SelectAllStockReceiptReport(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptTransactionReport.StockReceiptHeaderTransactionReportList = ResponseData.ReportDataTable;
                }
                else
                {
                    _IStockReceiptTransactionReport.StockReceiptHeaderTransactionReportList = new System.Data.DataTable();
                    _IStockReceiptTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class StockReceiptDetailTransactionViewPresenter
    {
        IStockReceiptTransactionDetailsReport _IStockReceiptTransactionDetailsReport;
        public StockReceiptDetailTransactionViewPresenter(IStockReceiptTransactionDetailsReport ViewObj)
        {
            _IStockReceiptTransactionDetailsReport = ViewObj;
        }
        public void SelectStockReceiptdetailtransactionlist()
        {
            try
            {
                var _StockReceiptTransactionReportBLL = new StockReceiptTransactionReportBLL();
                var RequestData = new StockReceiptTransactionRequest();
                RequestData.ID = _IStockReceiptTransactionDetailsReport.ID;
                var ResponseData = _StockReceiptTransactionReportBLL.SelectRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReceiptTransactionDetailsReport.StockReceiptTransactionDetailsList = ResponseData.StockReceiptDetailsTransactionList;
                    _IStockReceiptTransactionDetailsReport.StoreName = ResponseData.storename;
                    _IStockReceiptTransactionDetailsReport.ReceiptNumber = ResponseData.ReceiptNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }  
}
