using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseOpeningStockDAL : BaseDAL
    {       
        public abstract SelectByOpeningStockDetailsResponse SelectByStockRequestDetails(SelectByOpeningStockDetailsRequest ObjRequest);
        public abstract SelectAllOpeningStockResponse API_SelectALL(SelectAllOpeningStockRequest objRequest);
    }
}
