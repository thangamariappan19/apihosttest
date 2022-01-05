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
    public class SelectByNonTradingStockIDResponse : BaseResponseType
    {
        [DataMember]
        public NonTradingStockHeaderTypes  NonTradingStockHeaderRecord { get; set; }
        [DataMember]
        public List<NonTradingStockHeaderTypes> NonTradingStockHeaderList { get; set; }
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> NonTradingStockList { get; set; }
    }
}
