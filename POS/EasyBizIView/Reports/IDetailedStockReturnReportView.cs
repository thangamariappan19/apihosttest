using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
   public interface IDetailedStockReturnReportView : IBaseView
    {
       int ID { get; set; }

       StockReturnHeader StockReturnRecord { get; set; }

       List<StockReturnHeader> StockReturnList { set; }

       DataTable DetailStockreturnReportTable { set; }
    }
}
