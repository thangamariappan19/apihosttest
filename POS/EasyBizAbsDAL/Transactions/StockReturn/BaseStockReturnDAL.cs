using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.StockReturn
{
    public abstract class BaseStockReturnDAL : BaseDAL
    {
        public abstract SelectByStockReturnDetailsResponse SelectByStockReturnDetails(SelectByStockReturnDetailsRequest ObjRequest);

        public abstract SelectAllStockReturnResponse GetStockReturnHeaderReport(SelectAllStockReturnRequest objRequest);
        public abstract SelectAllStockReturnResponse GetStockReturnDetailsReport(SelectAllStockReturnRequest objRequest);
    }
}
