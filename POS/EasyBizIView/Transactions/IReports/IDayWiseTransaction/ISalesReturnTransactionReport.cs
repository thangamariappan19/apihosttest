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
    public interface ISalesReturnTransactionReport : IBaseView
    {
        List<SalesReturnHeaderTransaction> SalesReturnHeaderTransactionList { set; }
        DataTable SalesReturnHeaderTransactionReportList { set; }
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        String InvStoreName { get; set; }
        String InvFromDate { set; }
        String InvToDate { set; }
        int StoreID { get; }
    }
    public interface ISalesReturnDeatilTransactionView
    {

        String ID { get; set; }
        String InvDStoreName { set; }
        String InvDInvNumber { set; }
        List<SalesReturnDetailTransaction> SalesReturnDetailsTransactionList { set; }
    }
}
