using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockReceipt
{
    public interface IStockReceiptCollectionView : IBaseView
    {
        List<StockReceiptHeader> StockReceiptHeaderList { get; set; }
        List<EasyBizDBTypes.Transactions.Stocks.StockReceipt.int_stockreceipt> int_stockreceiptListWms { get; set; }

        List<EasyBizDBTypes.Transactions.Stocks.StockReceipt.int_stockreceipt> int_stockreceiptListWmsFlagCheck { get; set; }

        List<StockReceiptHeader> StockReceiptHeaderListWms { get; set; }

        List<StockReceiptDetails> StockReceiptDetailsListWms { get; set; }


        List<StockReceiptHeader> StockReceiptHeaderListWmsFlagCheck { get; set; }
        List<EasyBizDBTypes.Transactions.StockReceipt.StockReceiptDetails> StockReceiptDetailsListWmsFlag { get; set; }

        List<int_stockreceipt> int_stockreceiptConfirmTransfer { get; set; }

        string StoreCode { get; }

        List<TagIdItemDetails> RFIDTagList { get; set; }
    }
}
