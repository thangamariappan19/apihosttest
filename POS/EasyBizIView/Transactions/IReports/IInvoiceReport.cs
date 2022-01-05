using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
    public interface IInvoiceReport : IBaseView
    {
        List<InvoiceHeader> InvoiceHeaderList { set; } 
        DateTime BusinessDate { get; }        
        int StoreID { get; }        
    }
    public interface IInvoiceDetailsView
    {
        int ID { get; set; } 
        List<PaymentDetail> PaymentList { set; }
        List<InvoiceDetails> InvoiceDetailsList { set; }
    }
}
