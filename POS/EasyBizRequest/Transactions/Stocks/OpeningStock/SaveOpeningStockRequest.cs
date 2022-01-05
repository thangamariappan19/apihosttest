using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.OpeningStock
{
    [DataContract]
    [Serializable]
    public class SaveOpeningStockRequest : BaseRequestType
    {
        [DataMember]
        public OpeningStockHeader OpeningStockHeaderRecord { get; set; }
        [DataMember]
        public List<OpeningStockDetails> OpeningStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public OpeningStockDetails OpeningStockDetails { get; set; }
    }
}
