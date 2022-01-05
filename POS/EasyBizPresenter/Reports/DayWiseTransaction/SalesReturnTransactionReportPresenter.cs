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
    public class SalesReturnTransactionReportPresenter
    {
        ISalesReturnTransactionReport _ISalesReturnTransactionReport;
        SalesReturnTransactionReportBLL _SalesReturnTransactionReportBLL = new SalesReturnTransactionReportBLL();
        public SalesReturnTransactionReportPresenter(ISalesReturnTransactionReport ViewObj)
        {
            _ISalesReturnTransactionReport = ViewObj;
        }
        public void GetSalesReturnHeaderTransactionList()
        {
            try
            {
                var RequestData = new SalesReturnTransactionRequest();
                RequestData.RequestFrom = _ISalesReturnTransactionReport.RequestFrom;
                RequestData.FromDate = _ISalesReturnTransactionReport.FromDate;
                RequestData.ToDate = _ISalesReturnTransactionReport.ToDate;
                RequestData.StoreID = _ISalesReturnTransactionReport.StoreID;
                var ResponseData = _SalesReturnTransactionReportBLL.SelectAllInvoice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnTransactionReport.SalesReturnHeaderTransactionList = ResponseData.SalesReturnHeaderTransactionList;
                    _ISalesReturnTransactionReport.InvStoreName = ResponseData.InvStoreName;
                    _ISalesReturnTransactionReport.InvFromDate = ResponseData.InvFromDate;
                    _ISalesReturnTransactionReport.InvToDate = ResponseData.InvToDate;
                    
                }
                else
                {
                    _ISalesReturnTransactionReport.SalesReturnHeaderTransactionList = ResponseData.SalesReturnHeaderTransactionList;
                    _ISalesReturnTransactionReport.InvStoreName = _ISalesReturnTransactionReport.InvStoreName.ToString();
                    _ISalesReturnTransactionReport.InvFromDate = _ISalesReturnTransactionReport.FromDate.ToString("dd/MMM/yyyy");
                    _ISalesReturnTransactionReport.InvToDate = _ISalesReturnTransactionReport.ToDate.ToString("dd/MMM/yyyy");
                    _ISalesReturnTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSalesReturnHeaderTransactionReportList()
        {
            try
            {
              
                var RequestData = new SalesReturnTransactionRequest();
                var ResponseData = new SalesReturnTransactionResponse();
                RequestData.RequestFrom = _ISalesReturnTransactionReport.RequestFrom;
                RequestData.FromDate = _ISalesReturnTransactionReport.FromDate;
                RequestData.ToDate = _ISalesReturnTransactionReport.ToDate;
                RequestData.StoreID = _ISalesReturnTransactionReport.StoreID;
                ResponseData = _SalesReturnTransactionReportBLL.SelectAllSalesReturnReport(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnTransactionReport.SalesReturnHeaderTransactionReportList = ResponseData.ReportDataTable;
                }
                else
                {
                    _ISalesReturnTransactionReport.SalesReturnHeaderTransactionReportList = ResponseData.ReportDataTable;
                    _ISalesReturnTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class SalesReturnDetailTransactionViewPresenter
    {
        ISalesReturnDeatilTransactionView _ISalesReturnDetailsTransactionView;
        public SalesReturnDetailTransactionViewPresenter(ISalesReturnDeatilTransactionView ViewObj)
        {
            _ISalesReturnDetailsTransactionView = ViewObj;
        }
        public void Selectinvoicedetailtransactionlist()
        {
            try
            {
                var _SalesReturnTransactionReportBLL = new SalesReturnTransactionReportBLL();
                var RequestData = new SalesReturnTransactionRequest();
                RequestData.ID = _ISalesReturnDetailsTransactionView.ID;
                var ResponseData = _SalesReturnTransactionReportBLL.SelectRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturnDetailsTransactionView.SalesReturnDetailsTransactionList = ResponseData.SalesReturnDetailsTransactionList;
                    _ISalesReturnDetailsTransactionView.InvDStoreName = ResponseData.InvDStoreName;
                    _ISalesReturnDetailsTransactionView.InvDInvNumber = ResponseData.InvDInvNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }  
}
