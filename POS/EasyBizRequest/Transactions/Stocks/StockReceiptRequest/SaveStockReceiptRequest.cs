using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockReceiptRequest
{
    [DataContract]
    [Serializable]
    public class SaveStockReceiptRequest : BaseRequestType
    {
        [DataMember]
        public List<int_stockreceipt> int_stockreceiptList { get; set; }
        [DataMember]
        public StockReceiptHeader StockReceiptHeaderRecord { get; set; }
        [DataMember]
        public List<StockReceiptDetails> StockReceiptDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList {get;set;}
        [DataMember]
        public List<BinLogTypes> BinLogList { get; set; }
        [DataMember]
        public List<StockReceiptHeader> StockReceiptHeaderListWms { get; set; }
        [DataMember]
        public List<StockReceiptDetails> StockReceiptDetailsListWms { get; set; }
        [DataMember]
        public List<StockReceiptHeader> StockReceiptHeaderListWmsFlagCheck { get; set; }
        [DataMember]
        public List<TagIdItemDetails> RFIDTagList { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
    }
}
