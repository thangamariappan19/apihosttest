using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
    public class SaveStockRequestRequest : BaseRequestType
    {
        [DataMember]
        public StockRequestHeader StockRequestHeaderRecord { get; set; }
        public List<StockRequestDetails> StockRequestDetailsList { get; set; }

        public List<int_stockrequestTypes> int_stockrequestTypesList { get; set; }
    }
}
