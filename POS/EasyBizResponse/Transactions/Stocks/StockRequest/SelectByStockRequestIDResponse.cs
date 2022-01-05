using EasyBizDBTypes.Transactions.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
    public class SelectByStockRequestIDResponse : BaseResponseType
    {
        public StockRequestHeader StockRequestHeaderRecord { get; set; }
    }
}
