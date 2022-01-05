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
    public class SelectAllStockRequestResponse : BaseResponseType
    {
        public List<StockRequestHeader> StockRequestHeaderList { get; set; }
    }
}
