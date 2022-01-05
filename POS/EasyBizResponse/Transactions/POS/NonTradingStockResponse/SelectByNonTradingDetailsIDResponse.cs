using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.NonTradingStockResponse
{
    [DataContract]
    [Serializable]
    public class SelectByNonTradingDetailsIDResponse : BaseResponseType
    {
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsRecord { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
    }
}
