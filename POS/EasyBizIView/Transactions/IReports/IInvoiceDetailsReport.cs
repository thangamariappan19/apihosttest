using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
    public interface IInvoiceDetailsReport : IBaseView
    {
        int ID { get; set; }
        List<InvoiceDetails> InvoiceDetailsList { get; set; }       
    }
}
