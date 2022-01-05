using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports.DayWiseTransaction
{
    public abstract class BaseStockAdjustmentTransactionDAL : BaseDAL
    {
        public abstract StockAdjustmentTransactionResponse SelectAll(StockAdjustmentTransactionRequest ObjRequest);
        public abstract StockAdjustmentTransactionResponse SelectAllReport(StockAdjustmentTransactionRequest ObjRequest);
        public abstract StockAdjustmentTransactionResponse SelectDetailAdjustment(StockAdjustmentTransactionRequest ObjRequest);
    }
}
