using EasyBizDBTypes.Transactions.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReceipt
{
    [DataContract]
    [Serializable]
    public class SelectByStockReceiptIDResponse : BaseResponseType
    {
        public StockReceiptHeader StockReceiptHeaderRecord { get; set; }
    }
}
