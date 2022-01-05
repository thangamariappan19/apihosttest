using EasyBizAbsDAL.Common;


using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;

using EasyBizResponse.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.StockReceipt
{
    public abstract class BaseStockReceiptDAL : BaseDAL
    {
        public abstract SelectByStockReceiptDetailsResponse SelectByStockReceiptDetails(SelectByStockReceiptDetailsRequest ObjRequest);
        public abstract SaveStockReceiptResponse Saveint_stockreceipt(SaveStockReceiptRequest ObjRequest);
        public abstract SelectAllStockReceiptResponse SelectAllStockReceiptWms(SelectAllStockReceiptRequest ObjRequest);
        public abstract SelectAllStockReceiptResponse SelectAllStockReceiptWmsFlagCheck(SelectAllStockReceiptRequest ObjRequest);
       // public abstract SaveStockReceiptResponse SaveStockReceiptlistWmsFlagCheck(SelectAllStockReceiptRequest ObjRequest);
        public abstract SaveStockReceiptResponse saveintStockReceipt(SaveStockReceiptRequest ObjRequest);
        public abstract SaveStockReceiptResponse SaveStockReceiptlistWms(SaveStockReceiptRequest objRequest);
        public abstract SelectByStockReceiptDetailsResponse SelectByStockReceiptTransactionDetails(SelectByStockReceiptDetailsRequest ObjRequest);

        public abstract SaveStockReceiptResponse Update_ConfirmTransfer(SaveStockReceiptRequest ObjRequest);
        public abstract SaveStockReceiptResponse SaveStockReceiptlistWmsFlagCheck(SaveStockReceiptRequest RequestData);

        public abstract SelectAllStockReceiptDetailsResponse SelectAllStockReceiptDetailsForFlaglist(SelectAllStockReceiptDetailsRequest ObjRequest);
        public abstract SaveStockReceiptListWmsConfirmTransferResponse SaveStockReceiptListWmsConfirmTransfer(SaveStockReceiptListWmsConfirmTransferRequest objRequest);

        public abstract SelectTagIDListResponse SelectTagIDList(SelectTagIDListRequest ObjRequest);

        public abstract SelectAllStockReceiptResponse GetStockReceiptHeaderReport(SelectAllStockReceiptRequest objRequest);
        public abstract SelectAllStockReceiptResponse GetStockReceiptDetailsReport(SelectAllStockReceiptRequest objRequest);
        public abstract SelectAllStockReceiptResponse API_SelectALL(SelectAllStockReceiptRequest objRequest);
    }
}
