using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizIView.Transactions.IReports.IDayWiseTransaction
{
    public interface IStockReceiptTransactionReport : IBaseView
     {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string StoreName { get;  set; }
        int StoreID { get; }
        DataTable StockReceiptHeaderTransactionReportList { set; }
        List<StockReceiptHeaderTransaction> StockReceiptHeaderTransactionList { set; }
    }
    public interface IStockReceiptTransactionDetailsReport
    {
        String ID { get; }
        string StoreName { set; }
        String ReceiptNumber { set; }
        List<StockReceiptDetailTransaction> StockReceiptTransactionDetailsList { set; }
    }
}
