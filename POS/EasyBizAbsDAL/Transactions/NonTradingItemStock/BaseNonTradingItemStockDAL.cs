using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;

namespace EasyBizAbsDAL.Transactions.NonTradingItemStock
{
    public abstract class BaseNonTradingItemStockDAL : BaseDAL
    {
        public abstract SelectByNonTradingDetailsIDResponse SelectByNonTradingStockDetails(SelectByNonTraddingDetailsIDRequest ObjRequest);
        public abstract SelectALLNonTradingStockResponse API_SelectALL(SelectALLNonTradingStockRequest objRequest);
    }
}
