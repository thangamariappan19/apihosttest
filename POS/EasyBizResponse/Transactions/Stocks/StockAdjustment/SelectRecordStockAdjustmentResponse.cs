using EasyBizDBTypes.Transactions.StockStaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockAdjustment
{
    [DataContract]
    [Serializable]
    public class SelectRecordStockAdjustmentResponse : BaseResponseType
    {
        public StockAdjustmentHeader StockAdjustmentHeaderRecord { get; set; }
    }
}
