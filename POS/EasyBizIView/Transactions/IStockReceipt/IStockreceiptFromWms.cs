using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockReceipt
{
    public interface IStockreceiptFromWms : IBaseView
    {
        List<int_stockreceipt> int_stockreceiptList { get; set; }
    }
}
