using EasyBizDBTypes.Transactions.StockReceipt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
   public interface IDetailedStockReceiptReportView : IBaseView
    {
       int ID { get; set; }
       StockReceiptHeader StockReceiptRecord { get; set; }
       DataTable DetailStockReceiptReportTable { set; }
    }
}
