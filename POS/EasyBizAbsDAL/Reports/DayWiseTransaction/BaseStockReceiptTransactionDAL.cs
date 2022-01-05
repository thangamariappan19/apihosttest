using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizAbsDAL.Reports.DayWiseTransaction
{
    public abstract class BaseStockReceiptTransactionDAL : BaseDAL
    {
        public abstract StockReceiptTransactionResponse SelectAll(StockReceiptTransactionRequest ObjRequest);
        public abstract StockReceiptTransactionResponse SelectDetailReceipt(StockReceiptTransactionRequest ObjRequest);
        public abstract StockReceiptTransactionResponse SelectAllReport(StockReceiptTransactionRequest ObjRequest);
    }
}
