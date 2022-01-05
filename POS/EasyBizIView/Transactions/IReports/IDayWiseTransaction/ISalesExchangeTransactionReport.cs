using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizIView.Transactions.IReports.IDayWiseTransaction
{
    public interface ISalesExchangeTransactionReport : IBaseView
    {
        List<SalesExchangeHeaderTransaction> SalesExchangeHeaderTransactionList { set; }
        DataTable SalesExchangeHeaderTransactionReportList { set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        String SEStoreName { get; set; }
        int StoreID { get; } 
    }
    public interface ISalesExchangeTransactionDetailsView
    {
        String ID { get; set; }
        List<SalesExchangeDetailTransaction> SalesExchangeDetailTransactionList { set; }
        List<SalesExchangeWithTransaction> SalesExchangeWithTransactionList { set; }
        String StoreName { set; }
        String ExchangeNumber { set; }
    }
}
