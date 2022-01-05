using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IHoldSalesView : IBaseView
    {
        List<InvoiceHeader> HoldInvoiceList { get; set; }
        List<InvoiceDetails> InvoiceDetailsList { get; set; }
        long HoldInvoiceHeaderID { get; set; }
        DateTime BusinessDate { get; }
        int StoreID { get; }
    }

}
