using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class SelectByStockReturnIDResponse : BaseResponseType
    {
        public StockReturnHeader StockReturnHeaderRecord { get; set; }
    }
}
