using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizIView.Transactions.IStockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
    public interface IStockReceipt : IBaseView
    {          
        DateTime BusinessDate { get; }
        int StoreID { get; }
        int ID { get; set; }
        List<StockReceiptHeader> StockReceiptHeaderList { set; }
    }
    public interface IStockReceiptDetailsView
    {
        int ID { get; set; }
        List<StockReceiptDetails> StockReceiptDetailsList { set; }
    }
}
