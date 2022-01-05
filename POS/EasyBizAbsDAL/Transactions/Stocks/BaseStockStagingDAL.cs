using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.StockStaging;
using EasyBizResponse.Transactions.Stocks.StockStaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseStockStagingDAL : BaseDAL
    {
        public abstract GetStockStagingRecordsByStyleCodeResponse GetStockByStyleCode(GetStockStagingRecordsByStyleCodeRequest RequestObj);
    }
}
