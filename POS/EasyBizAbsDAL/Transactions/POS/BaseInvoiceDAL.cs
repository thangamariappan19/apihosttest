using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions
{
    public abstract class BaseInvoiceDAL : BaseDAL
    {
        //public abstract SelectInvoiceDetailsResponse SelectByIDInvoiceDetails(SelectInvoiceDetailsRequest RequestObj);
        public abstract SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt(SelectInvoiceReceiptByInvoiceNumRequest RequestObj);
        public abstract SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt1(SelectInvoiceReceiptByInvoiceNumRequest RequestObj);
        public abstract SelectInvoiceReceiptApprovalNumResponse GetInvoiceReceipt2(SelectInvoiceApprovalNumRequest RequestObj);
        public abstract SelectHoldReceiptByInvoiceNumResponse GetHoldReceipt(SelectHoldReceiptByInvoiceNumRequest RequestObj);
        public abstract SelectInvoiceDetailsListResponse SelectInvoiceDetailsList(SelectInvoiceDetailsListRequest RequestObj);
        public abstract SelectInvoiceDetailsByIDResponse SelectInvoiceDetailsByID(SelectInvoiceDetailsByIDRequest RequestObj);
        public abstract UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest RequestObj);
        public abstract DeleteHoldSaleRecordsResponse DeleteHoldSaleRecords(DeleteHoldSaleRecordsRequest RequestObj);
        public abstract SaveInvoiceResponse SavePaymentProcesor(SaveInvoiceRequest objRequest);
        public abstract GetSearchInvoiceHeaderDetailsResponse GetSearchInvoiceHeaderDetails(SelectInvoiceDetailsListRequest objRequest);
        public abstract SelectAllInvoiceResponse SelectBillCompletedSalesInvoice(SelectAllInvoiceRequest objRequest);
        public abstract SelectAllInvoiceResponse SelectHoldSalesInvoice(SelectAllInvoiceRequest objRequest);
        public abstract SelectAllInvoiceResponse SelectedPOSSearchInvoice(SelectAllInvoiceRequest objRequest);
        public abstract SelectAllInvoiceResponse SelectPOSSearchAllInvoice(SelectAllInvoiceRequest objRequest);
        public abstract GetSearchInvoiceDetailsListResponse GetInvocieDetailsListBasedonInvoiceno(GetSearchInvoiceDetailsListRequest objRequest);
        public abstract GetSearchInvoiceHeaderDetailsResponse GetExchangeItemDetails(SelectInvoiceDetailsListRequest objRequest);
        public abstract SelectInvoiceDetailsListResponse GetSelectedRecallInvoice(SelectInvoiceDetailsListRequest objRequest);
        public abstract SaveInvoiceResponse SaveSalesRecord(SaveInvoiceRequest objRequest);
        public abstract SaveInvoiceResponse SaveCashandCardRecord(SaveInvoiceRequest objRequest);

        public abstract SearchCommonInvoiceResponse GetSearchInvoice(SearchCommonInvoiceRequest objRequest);


    }
}
