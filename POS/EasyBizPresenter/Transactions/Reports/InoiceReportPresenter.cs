using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizBLL.Transactions.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IReports;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.AgentMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Reports
{
    public class InoiceReportPresenter
    {
        IInvoiceReport _IInvoiceReport;
       
        InvoiceBLL _InvoiceReportBLL = new InvoiceBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        InvoiceCardDetailsBLL _InvoiceCardDetailsBLL = new InvoiceCardDetailsBLL();
        public InoiceReportPresenter(IInvoiceReport ViewObj)
        {
            _IInvoiceReport = ViewObj;
        }
        public void GetInvoiceHeaderList()
        {
            try
            {
                var RequestData = new SelectAllInvoiceRequest();
                RequestData.RequestFrom = _IInvoiceReport.RequestFrom;
                RequestData.BusinessDate = _IInvoiceReport.BusinessDate;
                RequestData.StoreID = _IInvoiceReport.StoreID;              
                var ResponseData = _InvoiceReportBLL.SelectAllInvoice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceReport.InvoiceHeaderList = ResponseData.InvoiceHeaderList;
                }
                else
                {
                    var InvoiceList = new List<InvoiceHeader>();
                    _IInvoiceReport.InvoiceHeaderList = InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
       
    }
    public class InvoiceDetailViewPresenter
    {
        IInvoiceDetailsView _IInvoiceDetailsView;
        public InvoiceDetailViewPresenter(IInvoiceDetailsView ViewObj)
        {
            _IInvoiceDetailsView = ViewObj;
        }
        public void Selectinvoicedetaillist()
        {
            try
            {
                var _InvoiceReportBLL = new InvoiceBLL();
                var RequestData = new SelectByIDInvoiceRequest();               
                RequestData.ID = _IInvoiceDetailsView.ID;                
                var ResponseData = _InvoiceReportBLL.SelectRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceDetailsView.InvoiceDetailsList = ResponseData.InvoiceHeaderData.InvoiceDetailList;
                    _IInvoiceDetailsView.PaymentList = ResponseData.InvoiceHeaderData.PaymentList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }    
}
