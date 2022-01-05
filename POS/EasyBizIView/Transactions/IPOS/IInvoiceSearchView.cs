using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IInvoiceSearchView 
    {
        List<InvoiceHeader> DefaultInvoiceList { get; set; }

        List<InvoiceHeader> SearchInvoiceList { get; set; }

        Enums.RequestFrom RequestFrom { get; set; }

        string SearchString { get; set; }

        List<InvoiceReceiptTypes> InvoiceReceiptList { get; set; }

        List<InvoiceSubReceiptTypes> InvoiceSubReceiptList { get; set; }
        List<ApprovalNumReceipt> ApprovalNumReceiptList { get; set; }
        string InvoiceNo { get; set; }       

    }       

}
