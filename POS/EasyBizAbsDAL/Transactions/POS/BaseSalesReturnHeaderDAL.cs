using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesReturnRequest;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
    public abstract class BaseSalesReturnHeaderDAL : BaseDAL
    {
        public abstract SelectSalesReturnDetailsByIDResponse SelectByIDSalesReturnDetails(SelectSalesReturnDetailsByIDRequest ObjRequest);
        public abstract SelectInvoiceReturnReceiptByInvoiceNumResponse GetInvoiceReturnReceipt(SelectInvoiceReturnReceiptByInvoiceNumRequest RequestObj);
    }
}
