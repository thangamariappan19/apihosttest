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
    public class SelectByNonTradingHeaderIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo{ get; set; }
        [DataMember]
        public string RefDocumentNo { get; set; }
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
    }
}
