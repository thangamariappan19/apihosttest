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
   public class StockReturnTransactionReportPresenter
    {
        IStockReturnTransactionReport _IStockReturnTransactionReport;
        StockReturnTransactionReportBLL _StockReturnTransactionReportBLL = new StockReturnTransactionReportBLL();

        public StockReturnTransactionReportPresenter(IStockReturnTransactionReport ViewObj)
        {
            _IStockReturnTransactionReport = ViewObj;
        }
        public void GetStockReturnTransactionlist()
        {
            try
            {
                var RequestData = new StockReturnTransactionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.FromDate = _IStockReturnTransactionReport.FromDate;
                RequestData.ToDate = _IStockReturnTransactionReport.ToDate;
                RequestData.StoreID = _IStockReturnTransactionReport.StoreID;
                
                var ResponseData = new StockReturnTransactionResponse();
                ResponseData = _StockReturnTransactionReportBLL.SelectAllStockReturn (RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnTransactionReport.StockReturnHeaderTransactionList = ResponseData.StockReturnHeaderTransactionList;
                    _IStockReturnTransactionReport.StoreName = ResponseData.storename;
                    _IStockReturnTransactionReport.FromDate = ResponseData.FromDate;
                    _IStockReturnTransactionReport.ToDate = ResponseData.ToDate;
                }
                else
                {
                    _IStockReturnTransactionReport.StockReturnHeaderTransactionList = ResponseData.StockReturnHeaderTransactionList;
                    _IStockReturnTransactionReport.StoreName = _IStockReturnTransactionReport.StoreName.ToString();
                     _IStockReturnTransactionReport.FromDate =  DateTime.Parse(_IStockReturnTransactionReport.FromDate.ToString("dd/MMM/yyyy"));
                     _IStockReturnTransactionReport.ToDate = DateTime.Parse(_IStockReturnTransactionReport.ToDate.ToString("dd/MMM/yyyy"));
                    _IStockReturnTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockReturnHeaderTransactionReportList()
        {
            try
            {

                var RequestData = new StockReturnTransactionRequest();
                var ResponseData = new StockReturnTransactionResponse();
                RequestData.RequestFrom = _IStockReturnTransactionReport.RequestFrom;
                RequestData.FromDate = _IStockReturnTransactionReport.FromDate;
                RequestData.ToDate = _IStockReturnTransactionReport.ToDate;
                RequestData.StoreID = _IStockReturnTransactionReport.StoreID;
                ResponseData = _StockReturnTransactionReportBLL.SelectAllStockReturnReport(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnTransactionReport.StockReturnHeaderTransactionReportList = ResponseData.ReportDataTable;
                }
                else
                {
                    _IStockReturnTransactionReport.StockReturnHeaderTransactionReportList = new System.Data.DataTable();
                    _IStockReturnTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class StockReturnDetailTransactionViewPresenter
    {
        IStockReturnTransactionDetailsReport _IStockReturnTransactionDetailsReport;
        public StockReturnDetailTransactionViewPresenter(IStockReturnTransactionDetailsReport ViewObj)
        {
            _IStockReturnTransactionDetailsReport = ViewObj;
        }
        public void SelectStockReturndetailtransactionlist()
        {
            try
            {
                var _StockReturnTransactionReportBLL = new StockReturnTransactionReportBLL();
                var RequestData = new StockReturnTransactionRequest();
                RequestData.ID = _IStockReturnTransactionDetailsReport.ID;
                var ResponseData = _StockReturnTransactionReportBLL.SelectRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockReturnTransactionDetailsReport.StockReturnTransactionDetailsList = ResponseData.StockReturnDetailsTransactionList;
                    _IStockReturnTransactionDetailsReport.StoreName = ResponseData.storename;
                    _IStockReturnTransactionDetailsReport.ReturnNumber = ResponseData.ReturnNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }  
}
