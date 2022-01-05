using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;


namespace EasyBizIView.Transactions.IReports.IDayWiseTransaction
{
    public interface IInvoiceTransactionReport : IBaseView
    {
       List<InvoiceHeaderTransaction> InvoiceHeaderTransactionList { set; }
       DataTable InvoiceHeaderTransactionReportList { set; }
       DateTime FromDate { get; }
       DateTime ToDate { get; }
       String InvStoreName { get; set; }
       String InvFromDate { set; }
       String InvToDate { set; }
       int StoreID { get; }
    }

  
    public interface IInvoiceDetailsTransactionView
    {

        String ID { get; set; }
        String InvDStoreName { set; }
        String InvDInvNumber { set; }
        List<InvoiceDetailTransaction> InvoiceDetailsTransactionList { set; }
        List<InvoicePaymentTransaction> InvoicePaymentTransactionList { set; }
    }
}
