using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizIView.Transactions.IReports.IDayWiseTransaction
{
    public interface IStockAdjustmentTransactionReport : IBaseView
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string StoreName { get;  set; }
        int StoreID { get; }
        DataTable StockAdjustmentHeaderTransactionReportList { set; }
        List<StockAdjustmentHeaderTransaction> StockAdjustmentHeaderTransactionList { set; }
    }
    public interface IStockAdjustmentTransactionDetailsReport
    {
        String ID { get; }
        string StoreName { set; }
        String AdjustmentNumber { set; }
        List<StockAdjustmentDetailTransaction> StockAdjustmentTransactionDetailsList { set; }
    }
}
