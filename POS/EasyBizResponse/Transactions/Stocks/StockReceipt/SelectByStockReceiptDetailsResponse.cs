using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReceipt
{
    [Serializable]
    [DataContract]
    public class SelectByStockReceiptDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReceiptDetails> StockReceiptDetailsRecord { get; set; }        
        public List<StockReceiptHeader> StockReceiptHeaderList { get; set; }
        public List<TransactionLog> TransactionLogList { get; set; }
    }
}
