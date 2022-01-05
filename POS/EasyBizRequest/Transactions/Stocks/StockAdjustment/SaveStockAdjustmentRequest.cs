using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockAdjustment
{
    [DataContract]
    [Serializable]
    public class SaveStockAdjustmentRequest :BaseRequestType
    {
        [DataMember]
        public StockAdjustmentHeader StockAdjustmentRecord { get; set; }
        public List<TransactionLog> TransactionLogList { get; set; } 
    }
}
