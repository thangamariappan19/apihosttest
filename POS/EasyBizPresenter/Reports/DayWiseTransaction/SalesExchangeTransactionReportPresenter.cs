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
    public class SalesExchangeTransactionReportPresenter
    {
        ISalesExchangeTransactionReport _ISalesExchangeTransactionReport;
        SalesExchangeTransactionReportBLL _SalesExchangeTransactionReportBLL = new SalesExchangeTransactionReportBLL();

        public SalesExchangeTransactionReportPresenter(ISalesExchangeTransactionReport ViewObj)
        {
            _ISalesExchangeTransactionReport = ViewObj;
        }
        public void GetExchangeList()
        {
            try
            {
                var RequestData = new SalesExchangeTransactionRequest();
                RequestData.RequestFrom = _ISalesExchangeTransactionReport.RequestFrom;
                RequestData.FromDate = _ISalesExchangeTransactionReport.FromDate;
                RequestData.ToDate = _ISalesExchangeTransactionReport.ToDate;
                RequestData.StoreID = _ISalesExchangeTransactionReport.StoreID;
                var ResponseData = new SalesExchangeTransactionResponse();
                ResponseData = _SalesExchangeTransactionReportBLL.SalesExchangeTransactionList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                  _ISalesExchangeTransactionReport.SalesExchangeHeaderTransactionList = ResponseData.SalesExchangeHeaderList;
                  _ISalesExchangeTransactionReport.SEStoreName = ResponseData.StoreName;
                  _ISalesExchangeTransactionReport.FromDate = RequestData.FromDate;
                  _ISalesExchangeTransactionReport.ToDate = RequestData.ToDate;
                }
                else
                {
                    _ISalesExchangeTransactionReport.SalesExchangeHeaderTransactionList = ResponseData.SalesExchangeHeaderList;
                    _ISalesExchangeTransactionReport.SEStoreName = _ISalesExchangeTransactionReport.SEStoreName.ToString();
                    _ISalesExchangeTransactionReport.FromDate = DateTime.Parse(_ISalesExchangeTransactionReport.FromDate.ToString("dd/MMM/yyyy"));
                    _ISalesExchangeTransactionReport.ToDate = DateTime.Parse(_ISalesExchangeTransactionReport.ToDate.ToString("dd/MMM/yyyy"));
                    _ISalesExchangeTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSalesExchangeHeaderTransactionReportList()
        {
            try
            {
                var RequestData = new SalesExchangeTransactionRequest();
                var ResponseData = new SalesExchangeTransactionResponse();
                RequestData.RequestFrom = _ISalesExchangeTransactionReport.RequestFrom;
                RequestData.FromDate = _ISalesExchangeTransactionReport.FromDate;
                RequestData.ToDate = _ISalesExchangeTransactionReport.ToDate;
                RequestData.StoreID = _ISalesExchangeTransactionReport.StoreID;
                ResponseData = _SalesExchangeTransactionReportBLL.SalesEchangeTransactionReportList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeTransactionReport.SalesExchangeHeaderTransactionReportList = ResponseData.ReportDataTable;

                }
                else
                {
                    _ISalesExchangeTransactionReport.SalesExchangeHeaderTransactionReportList = new System.Data.DataTable();
                    _ISalesExchangeTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class SalesExchangeDetailTransactionViewPresenter
    {
        ISalesExchangeTransactionDetailsView _ISalesExchangeDetailsTransactionView;
        public SalesExchangeDetailTransactionViewPresenter(ISalesExchangeTransactionDetailsView ViewObj)
        {
            _ISalesExchangeDetailsTransactionView = ViewObj;
        }
        public void SelectExchangedetailtransactionlist()
        {
            try
            {
                var _SalesExchangeTransactionReportBLL = new SalesExchangeTransactionReportBLL();
                var RequestData = new SalesExchangeTransactionRequest();
                RequestData.ID = _ISalesExchangeDetailsTransactionView.ID;
                var ResponseData = _SalesExchangeTransactionReportBLL.SelectRecordByID(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesExchangeDetailsTransactionView.SalesExchangeDetailTransactionList = ResponseData.SalesExchangeDetailsTransactionList;
                    _ISalesExchangeDetailsTransactionView.SalesExchangeWithTransactionList = ResponseData.SalesExchangeWithTransactionList;
                    _ISalesExchangeDetailsTransactionView.StoreName = ResponseData.StoreName;
                    _ISalesExchangeDetailsTransactionView.ExchangeNumber = ResponseData.ExchangeNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }  
}
