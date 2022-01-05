using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class SaveTransactionLogRequest : BaseRequestType
    {
        public List<TransactionLog> TransactionLogList { get; set; }       
        public int StoreID { get; set; }
        public string StoreCode { get; set; }
    }
}
