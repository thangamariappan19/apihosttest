using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockRequest
{
    [Serializable]
    [DataContract]
    public class SelectByStockRequestDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<StockRequestDetails> StockRequestDetailsRecord { get; set; }
        public StockRequestHeader StockRequestHeaderRecord { get; set; }

        [DataMember]
        public List<int_stockreceipt> int_stockreceiptRecord { get; set; }
    }
}
