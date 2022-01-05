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
    public class InvoiceTransactionReportPresenter
    {
        IInvoiceTransactionReport _IInvoiceTransactionReport;
        InvoiceTransactionReportBLL _InvoiceTransactionReportBLL = new InvoiceTransactionReportBLL();
        public InvoiceTransactionReportPresenter(IInvoiceTransactionReport ViewObj)
        {
            _IInvoiceTransactionReport = ViewObj;
        }
        public void GetInvoiceHeaderTransactionList()
        {
            try
            {
                var RequestData = new InvoiceHeaderTransactionRequest();
                RequestData.RequestFrom = _IInvoiceTransactionReport.RequestFrom;
                RequestData.FromDate = _IInvoiceTransactionReport.FromDate;
                RequestData.ToDate = _IInvoiceTransactionReport.ToDate;
                RequestData.StoreID = _IInvoiceTransactionReport.StoreID;
                var ResponseData = _InvoiceTransactionReportBLL.SelectAllInvoice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceTransactionReport.InvoiceHeaderTransactionList = ResponseData.InvoiceHeaderTransactionList;
                    _IInvoiceTransactionReport.InvStoreName = ResponseData.InvStoreName; 
                    _IInvoiceTransactionReport.InvFromDate = ResponseData.InvFromDate;
                    _IInvoiceTransactionReport.InvToDate = ResponseData.InvToDate;
                    
                }
                else
                {
                    _IInvoiceTransactionReport.InvoiceHeaderTransactionList = ResponseData.InvoiceHeaderTransactionList;
                    _IInvoiceTransactionReport.InvStoreName = _IInvoiceTransactionReport.InvStoreName.ToString();
                    _IInvoiceTransactionReport.InvFromDate = _IInvoiceTransactionReport.FromDate.ToString("dd/MMM/yyyy");
                    _IInvoiceTransactionReport.InvToDate = _IInvoiceTransactionReport.ToDate.ToString("dd/MMM/yyyy");
                    _IInvoiceTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetInvoiceHeaderTransactionReportList()
        {
            try
            {
                var RequestData = new InvoiceHeaderTransactionRequest();
                var ResponseData = new InvoiceHeaderTransactionReponse();
                RequestData.RequestFrom = _IInvoiceTransactionReport.RequestFrom;
                RequestData.FromDate = _IInvoiceTransactionReport.FromDate;
                RequestData.ToDate = _IInvoiceTransactionReport.ToDate;
                RequestData.StoreID = _IInvoiceTransactionReport.StoreID;
                 ResponseData = _InvoiceTransactionReportBLL.SelectAllInvoiceReport(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceTransactionReport.InvoiceHeaderTransactionReportList = ResponseData.ReportDataTable;
                    _IInvoiceTransactionReport.InvStoreName = ResponseData.InvStoreName;
                    _IInvoiceTransactionReport.InvFromDate = ResponseData.InvFromDate;
                    _IInvoiceTransactionReport.InvToDate = ResponseData.InvToDate;

                }
                else
                {
                    _IInvoiceTransactionReport.InvoiceHeaderTransactionReportList = new System.Data.DataTable(); 
                    _IInvoiceTransactionReport.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class InvoiceDetailTransactionViewPresenter
    {
        IInvoiceDetailsTransactionView _IInvoiceDetailsTransactionView;
        public InvoiceDetailTransactionViewPresenter(IInvoiceDetailsTransactionView ViewObj)
        {
            _IInvoiceDetailsTransactionView = ViewObj;
        }
        public void Selectinvoicedetailtransactionlist()
        {
            try
            {
                var _InvoiceTransactionReportBLL = new InvoiceTransactionReportBLL();
                var RequestData = new InvoiceHeaderTransactionRequest();
                RequestData.ID = _IInvoiceDetailsTransactionView.ID;
                var ResponseData = _InvoiceTransactionReportBLL.SelectRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceDetailsTransactionView.InvoiceDetailsTransactionList = ResponseData.InvoiceDetailsTransactionList;
                    _IInvoiceDetailsTransactionView.InvoicePaymentTransactionList = ResponseData.InvoicePaymentTransactionList;
                    _IInvoiceDetailsTransactionView.InvDStoreName = ResponseData.InvDStoreName;
                    _IInvoiceDetailsTransactionView.InvDInvNumber = ResponseData.InvDInvNumber;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    
}
