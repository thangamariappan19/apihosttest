using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseStockReturnDAL : BaseDAL
    {
        public abstract SelectByStockReturnDetailsResponse SelectByStockReturnDetails(SelectByStockReturnDetailsRequest ObjRequest);
        public abstract SaveStockReturnResponse Saveint_stock(SaveStockReturnRequest ObjRequest);
        public abstract UpdateStockReturnResponse CloseOpenDocuments(UpdateStockReturnRequest objRequest);
        public abstract SelectAllStockReturnResponse GetStockReturnHeaderReport(SelectAllStockReturnRequest objRequest);
        public abstract SelectAllStockReturnResponse GetStockReturnDetailsReport(SelectAllStockReturnRequest objRequest);
        public abstract SelectAllStockReturnResponse API_SelectALL(SelectAllStockReturnRequest objRequest);
    }
}
