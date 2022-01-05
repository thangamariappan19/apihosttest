using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.WebOrderSalesResponse
{
    [DataContract]
    [Serializable]
    public class WebSalesOrderResponse : BaseResponseType
    {
        public List<WebSalesOrderHeader> WebSalesOrderHeader { get; set; }
        public List<WebSalesOrderDetails> WebSalesOrderDetails { get; set; }
        public List<WebSalesOrderHeader> DocumentNoLookup { get; set; }
        [DataMember]
        public List<TransactionLog> NonTradingStockList { get; set; }
    }
}
