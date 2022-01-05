using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizIView.Transactions.IReports.IDayWiseTransaction
{
    public interface  IStockReturnTransactionReport : IBaseView
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string StoreName { get; set; }
        int StoreID { get; }
        DataTable StockReturnHeaderTransactionReportList { set; }
        List<StockReturnHeaderTransaction> StockReturnHeaderTransactionList { set; }
    }
      public interface IStockReturnTransactionDetailsReport
    {
        String ID { get; }
        string StoreName { set; }
        String ReturnNumber { set; }
        List<StockReturnDetailTransaction> StockReturnTransactionDetailsList { set; }
    }
}
