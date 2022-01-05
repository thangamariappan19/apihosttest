using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReturn
{
    [Serializable]
    [DataContract]
    public class SelectByStockReturnDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReturnDetails> StockReturnDetailsRecord { get; set; }
        public List<TransactionLog> TransactionLogList { get; set; }
    }
}
