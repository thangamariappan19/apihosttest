using EasyBizAbsDAL.Common;

using EasyBizRequest.Transactions.Stocks.StockRequest;

using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.StockRequest
{
    public abstract class BaseStockRequestDAL : BaseDAL
    {
        public abstract SelectByStockRequestDetailsResponse SelectByStockRequestDetails(SelectByStockRequestDetailsRequest ObjRequest);
    }
}
