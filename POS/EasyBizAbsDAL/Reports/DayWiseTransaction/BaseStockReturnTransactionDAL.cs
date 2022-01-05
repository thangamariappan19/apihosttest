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
    public abstract class BaseStockReturnTransactionDAL : BaseDAL
    {
        public abstract StockReturnTransactionResponse SelectAll(StockReturnTransactionRequest ObjRequest);
        public abstract StockReturnTransactionResponse SelectAllReport(StockReturnTransactionRequest ObjRequest);
        public abstract StockReturnTransactionResponse SelectDetailReturn(StockReturnTransactionRequest ObjRequest);
    }
}
