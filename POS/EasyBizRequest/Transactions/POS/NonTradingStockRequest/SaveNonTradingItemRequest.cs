using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizDBTypes.Transactions.TransactionLogs;

namespace EasyBizRequest.Transactions.POS.NonTradingStockRequest
{
    [DataContract]
    [Serializable]
    public class SaveNonTradingItemRequest : BaseRequestType
    {
        [DataMember]
        public NonTradingStockHeaderTypes NonTradingItemRecord { get; set; }
        [DataMember]
        public long RunningNo { get; set; }

        [DataMember]
        public long DocumentNumberingID { get; set; }
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public string RefDocumentNo { get; set; }

    }
}
