using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
    public  class InvoiceSearchPresenter
    {
        IInvoiceSearchView _IInvoiceSearchView;
        public InvoiceSearchPresenter(IInvoiceSearchView ViewObj)
        {
            _IInvoiceSearchView = ViewObj;
        }
        public void GetAllInvoiceList()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectAllInvoiceRequest();

                int Status = (int)Enums.InvoiceStatus.Completed;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);

                RequestData.SalesStatus = TypeName;
                RequestData.RequestFrom = _IInvoiceSearchView.RequestFrom;
                RequestData.SearchString = _IInvoiceSearchView.SearchString;               

                var ResponseData = _InvoiceBLL.SelectAllInvoice(RequestData);

                if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                {
                    _IInvoiceSearchView.DefaultInvoiceList = ResponseData.InvoiceHeaderList;
                }
                else
                {
                    _IInvoiceSearchView.SearchInvoiceList = ResponseData.InvoiceHeaderList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInvoiceReceipt()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceSearchView.InvoiceNo;
                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceSearchView.InvoiceReceiptList = ResponseData.InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSubInvoiceReceipt()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceReceiptByInvoiceNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceSearchView.InvoiceNo;
                var ResponseData = new SelectInvoiceReceiptByInvoiceNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceSearchView.InvoiceSubReceiptList = ResponseData.InvoiceSubReceiptTList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetInvoiceApprovalNum()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceApprovalNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.InvoiceNum = _IInvoiceSearchView.InvoiceNo;
                var ResponseData = new SelectInvoiceReceiptApprovalNumResponse();
                ResponseData = _InvoiceBLL.GetInvoiceReceipt2(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IInvoiceSearchView.ApprovalNumReceiptList = ResponseData.ApprovalNumReceiptList;
                }
                else
                {
                    _IInvoiceSearchView.ApprovalNumReceiptList = new List<ApprovalNumReceipt>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
