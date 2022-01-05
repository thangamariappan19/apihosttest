using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IStyleStockView : IBaseView
    {
        List<TransactionLog> StockList { set; }       
        string SearchValue { get;  }
    }
}
