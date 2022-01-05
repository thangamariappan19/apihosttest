using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class SaveStockReturnRequest : BaseRequestType
    {
        [DataMember]
        public StockReturnHeader StockReturnHeaderRecord { get; set; }
        public List<StockReturnDetails> StockReturnDetailsList { get; set; }
        public List<TransactionLog> TransactionLogList { get; set; }    
        public List<StockReturnDetails> StockReturnDetailsList1 { get; set; }
        public List<int_stockreturn> int_stockreturnList { get; set; }
        public int RunningNo { get; set; }
        public int DetailID { get; set; }
       
        public string Username { get; set; }
       
        public string Password { get; set; }

    }
}
