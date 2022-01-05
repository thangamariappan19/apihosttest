using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
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
    public class SelectAllStockReceiptResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReceiptHeader> StockReceiptHeaderList { get; set; }

        [DataMember]
        public List<StockReceiptDetails> StockReceiptDetailsList { get; set; }

        [DataMember]
        public List<EasyBizDBTypes.Transactions.Stocks.StockReceipt.int_stockreceipt> int_stockreceiptRecord { get; set; }

        [DataMember]
        public List<StockReceiptHeader> StockReceiptHeaderListwmsFlag { get; set; }

        [DataMember]
        public List<TagIdItemDetails> RFIDTagList { get; set; }
    }
}
