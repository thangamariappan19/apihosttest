using EasyBizDBTypes.Transactions.StockRequest;
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
    public class UpdateStockRequestRequest : BaseRequestType
    {
        [DataMember]
        public StockRequestHeader StockRequestHeaderRecord { get; set; }
        [DataMember]
        public StockRequestDetails StockRequestDetailsList { get; set; }
    }
}
