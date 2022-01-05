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
    public class HoldSalesPresenter
    {
        IHoldSalesView _IHoldSalesView;
        public HoldSalesPresenter(IHoldSalesView ViewObj)
        {
            _IHoldSalesView = ViewObj;
        }
        public void GetAllInvoiceList()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectAllInvoiceRequest();

                int Status = (int)Enums.InvoiceStatus.ParkSale;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                
                RequestData.SalesStatus = TypeName;
                RequestData.BusinessDate = _IHoldSalesView.BusinessDate;

                var ResponseData = _InvoiceBLL.SelectAllInvoice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IHoldSalesView.HoldInvoiceList = ResponseData.InvoiceHeaderList;
                }
                else
                {
                    _IHoldSalesView.HoldInvoiceList = new List<InvoiceHeader>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectInvoiceDetailsList()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SelectInvoiceDetailsListRequest();
                RequestData.InvoiceHeaderID = _IHoldSalesView.HoldInvoiceHeaderID;
                RequestData.SalesStatus = "ParkSale";
                var ResponseData = _InvoiceBLL.SelectInvoiceDetailsList(RequestData);
                _IHoldSalesView.InvoiceDetailsList = ResponseData.InvoiceDetailsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStatus()
        {
            try
            {
                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new UpdateInvoiceStatusRequest();
                var ResponseData = new UpdateInvoiceStatusResponse();

                int Status = (int)Enums.InvoiceStatus.Resale;
                string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                RequestData.StoreID = _IHoldSalesView.StoreID;
                RequestData.InvoiceID = _IHoldSalesView.HoldInvoiceHeaderID;
                RequestData.Status = TypeName;
                ResponseData = _InvoiceBLL.UpdateInvoiceStatus(RequestData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }        
    }
}
